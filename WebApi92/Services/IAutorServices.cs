using Domain.Entities;

namespace WebApi92.Services
{
    public interface IAutorServices
    {
        public Task<Response<List<Autor>>> GetAutores();
        public Task<Response<Autor>> Crear(Autor i);
         public Task<Response<Autor>> Actualizar(Autor autor);
          public Task<Response<Autor>> Eliminar(int id);
       public Task<Response<List<Autor>>> GetById(int id);
    }
}
