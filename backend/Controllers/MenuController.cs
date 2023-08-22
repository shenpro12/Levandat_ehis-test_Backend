using backend.dto;
using backend.Interface;
using backend.Models;
using backend.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            this._menuService=menuService;
        }

        [HttpPost]
        public IActionResult CreateMenu(CreateMenuDto createMenuDto)
        {
            return Ok(this._menuService.CreateMenu(createMenuDto));
        }

        [HttpGet]
        public IActionResult GetMenu()
        {
            return Ok(this._menuService.GetMenu());
        }

        [HttpDelete]
        public IActionResult DeleteMenu(DeleteMenuDto deleteMenuDto)
        {

            return Ok(this._menuService.DeleteMenu(deleteMenuDto));
        }

        [HttpPatch]
        public IActionResult UpdateMenu(UpdateMenuDto updateMenuDto)
        {

            return Ok(this._menuService.UpdateMenu(updateMenuDto));
        }
    }
}
