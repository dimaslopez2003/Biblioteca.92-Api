using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi92.Services;
using System.Threading.Tasks;

namespace WebApi92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosServices;

        public UsuariosController(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        [HttpGet]
        public async Task<ActionResult> GetUsuarios()
        {
            var response = await _usuariosServices.GetUsuarios();
            return Ok(response);
        }




        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _usuariosServices.GetById(id));

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuariosResponse request)
        {
            var response = await _usuariosServices.CrearUsuario(request);
            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuariosResponse request)
        {
            try
            {
                var response = await _usuariosServices.UpdateUsuario(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            try
            {
                var response = await _usuariosServices.DeleteUsuario(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
