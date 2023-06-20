using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingWeb.Models;
using ShoppingWeb.Services;

namespace ShoppingWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public readonly ITodoService _carCosmosService;
        public TodoController(ITodoService carCosmosService)
        {
            _carCosmosService = carCosmosService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sqlCosmosQuery = "Select * from c";
            var result = await _carCosmosService.Get(sqlCosmosQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TodoItemsModel newCar)
        {
            //newCar.Id = Guid.NewGuid().ToString();
            var result = await _carCosmosService.AddAsync(newCar);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(TodoItemsModel carToUpdate)
        {
            var result = await _carCosmosService.Update(carToUpdate);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _carCosmosService.Delete(id);
            return Ok();
        }
    }
}
