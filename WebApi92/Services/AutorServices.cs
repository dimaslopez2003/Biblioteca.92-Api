using Dapper;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebApi92.Context;

namespace WebApi92.Services
{
    public class AutorServices : IAutorServices
    {
        private readonly ApplicationDBContext _context;
        private string _connectionString;

        public AutorServices(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Response<List<Autor>>> GetAutores()
        {
            try
            {
                List<Autor> Response = new List<Autor>();
                var result = await _context.Database.GetDbConnection().QueryAsync<Autor>("spGetAutores", new { }, commandType: CommandType.StoredProcedure);
                Response = result.ToList();

                return new Response<List<Autor>>(Response);
            }

            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }


        public async Task<Response<Autor>> Crear(Autor i)
        {
            try
            {
                Autor result = (await _context.Database.GetDbConnection().QueryAsync<Autor>("spCrearAutor",
                    new { i.Nombre, i.Nacionalidad }, commandType: CommandType.StoredProcedure)).FirstOrDefault();

                return new Response<Autor>(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }


        public async Task<Response<Autor>> Actualizar(Autor autor)
        {

            using var connection = _context.Database.GetDbConnection();
            var parameters = new DynamicParameters();
            parameters.Add("@PkAutor", autor.PkAutor, DbType.Int32);
            parameters.Add("@Nombre", autor.Nombre, DbType.String);
            parameters.Add("@Nacionalidad", autor.Nacionalidad, DbType.String);

            var result = await connection.QueryFirstOrDefaultAsync<Autor>(
                "spUpdateAutor", parameters, commandType: CommandType.StoredProcedure
            );

            if (result == null)
            {
                return new Response<Autor>(null, message: "Autor no encontrado");
            }
            else
            {
                return new Response<Autor>(result);
            }
        }

        public async Task<Response<Autor>> Eliminar(int id)
        {
            using var connection = _context.Database.GetDbConnection();
            await connection.ExecuteAsync("spDeleteAutor", new { PkAutor = id }, commandType: CommandType.StoredProcedure);
            return new Response<Autor>("Autor eliminado correctamente");
        }

        public async Task<Response<List<Autor>>> GetById(int id)
        {
            try
            {
                List<Autor> Response = new List<Autor>();
                var result = await _context.Database.GetDbConnection().QueryAsync<Autor>("spGetAutorById", new { PkAutor = id }, commandType: CommandType.StoredProcedure);
                var autores = result.ToList();

                if (!autores.Any())
                {
                    return new Response<List<Autor>>(null, message: "Autores no encontrados");
                }
                else
                {
                    return new Response<List<Autor>>(autores);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error: " + ex.Message);
            }
        }
    }
}

