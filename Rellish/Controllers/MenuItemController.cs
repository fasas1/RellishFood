using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rellish.Data;
using Rellish.Models;
using System.Net;

namespace Rellish.Controllers
{
    [Route("api/MenuItem")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private ApiResponse _response;
        public MenuItemController(ApplicationDbContext  db)
        {
            _db = db;
            _response = new ApiResponse();
        }

        [HttpGet]

        public async Task<IActionResult> GetMenuItems()
        {
            _response.Result = _db.MenuItems;
            _response.StatusCode = HttpStatusCode.OK;
             return Ok(_response);
        }
    }
}
