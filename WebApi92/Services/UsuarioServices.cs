using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi92.Context;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi92.Services
{
    public class UsuarioServices : IUsuariosServices
    {
        private readonly ApplicationDBContext _context;

        public UsuarioServices(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Response<List<UsuariosResponse>>> GetUsuarios()
        {
            try
            {
                var usuarios = await _context.Usuarios
                    .Include(u => u.Roles)
                    .Select(u => new UsuariosResponse
                    {
                        Id = u.PkUsuario,
                        Nombre = u.Nombre,
                        User = u.User,
                        Password = u.Password,
                        RolNombre = u.Roles.Nombre
                    })
                    .ToListAsync();

                return new Response<List<UsuariosResponse>>(usuarios);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }
        }


        public async Task<Response<Usuario>> GetById(int id)
        {
            try
            {
                Usuario response = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                return new Response<Usuario>(response);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedió un error: " + ex.Message);
            }
        }

        public async Task<Response<UsuariosResponse>> CrearUsuario(UsuariosResponse request)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    User = request.User,
                    Password = request.Password,
                    FkRol = request.FkRol  // Aseguramos que se asigne el FkRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                // Obtener el nombre del rol
                var rolNombre = await _context.Roles
                                    .Where(r => r.PkRol == request.FkRol)
                                    .Select(r => r.Nombre)
                                    .FirstOrDefaultAsync();

                request.RolNombre = rolNombre;

                return new Response<UsuariosResponse>(request);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Ocurrió un error al guardar los cambios en la base de datos: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error general: " + ex.Message);
            }
        }


        public async Task<Response<UsuariosResponse>> UpdateUsuario(int id, UsuariosResponse request)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                usuarioExistente.Nombre = request.Nombre;
                usuarioExistente.User = request.User;
                usuarioExistente.Password = request.Password;
                usuarioExistente.FkRol = request.FkRol;

                _context.Usuarios.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                var response = new UsuariosResponse
                {
                    Id = usuarioExistente.PkUsuario,
                    Nombre = usuarioExistente.Nombre,
                    User = usuarioExistente.User,
                    Password = usuarioExistente.Password,
                    FkRol = usuarioExistente.FkRol ?? 0,
                    RolNombre = (await _context.Roles.FindAsync(usuarioExistente.FkRol))?.Nombre
                };

                return new Response<UsuariosResponse>(response);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Ocurrió un error al guardar los cambios en la base de datos: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error general: " + ex.Message);
            }
        }

        public async Task<Response<bool>> DeleteUsuario(int id)
        {
            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(id);
                if (usuarioExistente == null)
                {
                    throw new Exception("Usuario no encontrado");
                }

                _context.Usuarios.Remove(usuarioExistente);
                await _context.SaveChangesAsync();

                return new Response<bool>(true);
            }
            catch (DbUpdateException dbEx)
            {
                throw new Exception("Ocurrió un error al guardar los cambios en la base de datos: " + dbEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error general: " + ex.Message);
            }
        }
    }
}
