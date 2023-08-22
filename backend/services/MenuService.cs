using backend.db;
using backend.dto;
using backend.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace backend.services
{
    public class MenuService: IMenuService
    {
        private readonly Connection _connection;
        public MenuService(Connection connection) {
            this._connection = connection;
        }

        public IResponse CreateMenu(CreateMenuDto createMenuDto)
        {
            try
            {
                //insert menu to the end of parent child list
                if (createMenuDto.parent_ID != null && createMenuDto.subRight == null)
                {
                    MenuModel parent = this._connection.MenuModels.Find(createMenuDto.parent_ID);
                    if (parent != null)
                    {
                        int rightValue = parent.rgt;
                        //update left & right 
                        var menuList = this._connection.MenuModels.Where(m => m.lft > rightValue).ToList();
                        foreach (var item in menuList)
                        {
                            item.lft += 2;
                            this._connection.MenuModels.Update(item);
                        }
                        menuList = this._connection.MenuModels.Where(m => m.rgt >= rightValue).ToList();
                        foreach (var item in menuList)
                        {
                            item.rgt += 2;
                            this._connection.MenuModels.Update(item);
                        }
                        //insert new menu
                        MenuModel menu = new MenuModel
                        {
                            content = createMenuDto.content,
                            lft = rightValue,
                            rgt = rightValue + 1,
                            parent = parent
                        };
                        this._connection.MenuModels.Add(menu);
                        //save
                        this._connection.SaveChanges();
                        IResponseData res = new IResponseData();
                        res.Menu = menu;
                        return new IResponse(200, "insert menu success!", res);
                    }
                    else
                    {
                        return new IResponse(404, "parent menu notfound!", null);
                    }
                }
                //insert menu after subling
                else if (createMenuDto.parent_ID == null && createMenuDto.subRight != null)
                {
                    MenuModel subling = this._connection.MenuModels.Single(menu => menu.rgt == createMenuDto.subRight);
                    if (subling != null)
                    {
                        var menuList = this._connection.MenuModels.Where(m => m.lft >= subling.rgt + 1).ToList();
                        foreach (var item in menuList)
                        {
                            item.lft += 2;
                            this._connection.MenuModels.Update(item);
                        }
                        menuList = this._connection.MenuModels.Where(m => m.rgt >= subling.rgt + 2 || m.rgt == subling.rgt + 2 - 1).ToList();
                        foreach (var item in menuList)
                        {
                            item.rgt += 2;
                            this._connection.MenuModels.Update(item);
                        }
                        MenuModel menu = new MenuModel();
                        menu.lft = subling.rgt + 1;
                        menu.rgt = subling.rgt + 2;
                        menu.content = createMenuDto.content;
                        if (subling.parentID != null)
                        {
                            menu.parent = this._connection.MenuModels.Find(subling.parentID);
                        }
                        this._connection.MenuModels.Add(menu);
                        //save
                        this._connection.SaveChanges();
                        IResponseData res = new IResponseData();
                        res.Menu = menu;
                        return new IResponse(200, "insert menu success!", res);
                    }else
                    {
                        return new IResponse(404, "subling menu notfound!", null);
                    }

                }
                //insert new menu to the end of root list
                else if (createMenuDto.parent_ID == null && createMenuDto.subRight == null)
                {
                    int maxRight = this._connection.MenuModels.Max(x => (int?)x.rgt) ?? 0;
                    MenuModel menu = new MenuModel();
                    menu.content = createMenuDto.content;
                    if (maxRight == 0)
                    {
                        menu.lft = 1;
                        menu.rgt = 2;
                    }
                    else
                    {
                        menu.lft = maxRight + 1;
                        menu.rgt = maxRight + 2;
                    }
                    this._connection.MenuModels.Add(menu);
                    this._connection.SaveChanges();
                    IResponseData res = new IResponseData();
                    res.Menu = menu;
                    return new IResponse(200, "insert menu success!", res);
                }
                else { return new IResponse(404, "someThing went wrong! Please try again!", null); }
            }
            catch (Exception ex) {
                return new IResponse(404, "someThing went wrong! Please try again!", null);
            }
        }

        public IResponse DeleteMenu(DeleteMenuDto deleteMenuDto)
        {
            try
            {
                MenuModel menu = this._connection.MenuModels.Find(deleteMenuDto.ID);
                if (menu != null)
                {
                    int width = menu.rgt - menu.lft + 1;
                    List<MenuModel> deleteList = this._connection.MenuModels.Where(m => m.lft >= menu.lft && m.rgt <= menu.rgt).ToList();
                    this._connection.RemoveRange(deleteList);
                    this._connection.SaveChanges(true);
                    List<MenuModel> updateList = this._connection.MenuModels.Where(m => m.lft >= menu.lft).ToList();
                    foreach (var item in updateList)
                    {
                        item.lft -= width;
                        this._connection.MenuModels.Update(item);
                    }
                    updateList = this._connection.MenuModels.Where(m => m.rgt >= menu.rgt).ToList();
                    foreach (var item in updateList)
                    {
                        item.rgt -= width;
                        this._connection.MenuModels.Update(item);
                    }
                    this._connection.SaveChanges();
                    IResponseData res = new IResponseData();
                    res.Menu = menu;
                    return new IResponse(200, "delete menu success!", res);
                }
                else { return new IResponse(404, "menu not found!", null); }
            }catch (Exception ex)
            {
                return new IResponse(404, "someThing went wrong! Please try again!", null);
            }
        }

        public IResponse UpdateMenu(UpdateMenuDto updateMenuDto)
        {
            try
            {
                MenuModel menu = this._connection.MenuModels.Find(updateMenuDto.ID);
                if (menu != null)
                {
                    menu.content = updateMenuDto.content;
                    this._connection.Update(menu);
                    this._connection.SaveChanges();
                    IResponseData res = new IResponseData();
                    res.Menu= menu;
                    return new IResponse(200, "update success!", res);
                }
                else { return new IResponse(404, "menu not found!", null); }
            }
            catch (Exception ex)
            {
                return new IResponse(404, "someThing went wrong! Please try again!", null);
            }
        }

        IResponse IMenuService.GetMenu()
        {
            try
            {
                List<MenuModel> list = this._connection.MenuModels.ToList().Where(menu => menu.parentID == null).ToList();
                IResponseData res = new IResponseData();
                res.MenuList = list;
                return new IResponse(200, "success!", res);
            }
            catch (Exception ex)
            {
                return new IResponse(404, "someThing went wrong! Please try again!", null);
            }
        }
    

    }
}
