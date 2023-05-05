using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rellish.Data;
using Rellish.Models;
using Rellish.Models.DTO;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpGet("{id:int}", Name="GetMenuItem")]
        public async Task<IActionResult> GetMenuItem(int id)
        { 
            if(id == 0)
            {  
                _response.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }
            MenuItem menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == id);
            if(menuItem == null )
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_response);
            }
            _response.Result = menuItem;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromBody]MenuItemCreateDTO menuItemCreateDTO)
        {
            try
            {
                    if (menuItemCreateDTO == null )
                    {
                        return BadRequest(menuItemCreateDTO);
                    }
                    MenuItem menuItemToCreate = new()
                    {
                        Name= menuItemCreateDTO.Name,
                        Price= menuItemCreateDTO.Price,
                        Category= menuItemCreateDTO.Category,
                        SpecialTag= menuItemCreateDTO.SpecialTag,
                        Image = menuItemCreateDTO.Image,
                        Description = menuItemCreateDTO.Description     
                };
                    await _db.MenuItems.AddAsync(menuItemToCreate);
                   await _db.SaveChangesAsync();
                    _response.Result = menuItemToCreate;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new {id = menuItemToCreate.Id}, _response);


                //else
                //{
                //    _response.IsSuccess = false;
                //}
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "GetMenuItem")]
        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id,[FromBody] MenuItemUpdateDTO menuItemUpdateDTO)
        {
            try
            {
                if (menuItemUpdateDTO == null || id != menuItemUpdateDTO.Id)
                {
                    return BadRequest();
                }
                MenuItem menuItemFromDb = await _db.MenuItems.FindAsync(id);
                if (menuItemFromDb == null)
                {
                    return BadRequest();
                }

              
                {
                    menuItemFromDb.Name = menuItemUpdateDTO.Name;
                    menuItemFromDb.Price = menuItemUpdateDTO.Price;
                    menuItemFromDb.Category = menuItemUpdateDTO.Category;
                    menuItemFromDb.SpecialTag = menuItemUpdateDTO.SpecialTag;
                    menuItemFromDb.Image = menuItemUpdateDTO.Image;
                    menuItemFromDb.Description = menuItemUpdateDTO.Description;
                };
                 _db.MenuItems.Update(menuItemFromDb);
                 _db.SaveChanges();
                
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);


                //else
                //{
                //    _response.IsSuccess = false;
                //}
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }

        [HttpDelete("{id:int}", Name = "GetMenuItem")]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
        {
            try
            {
                if ( id == 0)
                {
                    return BadRequest();
                }
                MenuItem menuItemFromDb = await _db.MenuItems.FindAsync(id);
                if (menuItemFromDb == null)
                {
                    return BadRequest();
                }
                int milliseconds = 2000;
                Thread.Sleep(milliseconds);

                _db.MenuItems.Remove(menuItemFromDb);
                _db.SaveChanges();

                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);


                //else
                //{
                //    _response.IsSuccess = false;
                //}
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.Message };
            }
            return _response;
        }
    }
}
