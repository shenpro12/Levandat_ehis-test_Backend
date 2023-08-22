using backend.dto;
using backend.Models;

namespace backend.Interface
{
    public interface IMenuService
    {
        public IResponse CreateMenu(CreateMenuDto createMenuDto);
        public IResponse GetMenu();
        public IResponse DeleteMenu(DeleteMenuDto deleteMenuDto);
        public IResponse UpdateMenu(UpdateMenuDto updateMenuDto);
    }
}
