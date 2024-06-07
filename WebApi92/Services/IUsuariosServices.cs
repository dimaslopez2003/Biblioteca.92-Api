using Domain.Entities;

namespace WebApi92.Services
{
    public interface IUsuariosServices
    {

        public Task<Response<List<UsuariosResponse>>> GetUsuarios();
        public Task<Response<UsuariosResponse>> CrearUsuario(UsuariosResponse request);

        public Task<Response<UsuariosResponse>> UpdateUsuario(int id, UsuariosResponse request);
        public Task<Response<bool>> DeleteUsuario(int id);

        public Task<Response<Usuario>> GetById(int id);
        
    }
}
