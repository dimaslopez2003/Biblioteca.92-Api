using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi92.Services;

namespace WebApi92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorServices _autorServices;

        public AutoresController(IAutorServices autorServices)
        {
            _autorServices = autorServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAutores()
        {
            return Ok(await _autorServices.GetAutores());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _autorServices.GetById(id));
        }


        [HttpPost]
        public async Task<IActionResult> Crear(Autor autor)
        {
            return Ok(await _autorServices.Crear(autor));
        }

        [HttpPut]
        public async Task<IActionResult> Actualizar([FromBody] Autor autor)
        {
            return Ok(await _autorServices.Actualizar(autor));
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var response = await _autorServices.Eliminar(id);
            return Ok(response);
        }
    }
}
