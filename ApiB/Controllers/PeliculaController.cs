using ApiB.Comunes;
using ApiB.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860


namespace ApiB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculaController : ControllerBase
    {
        private const string BaseUrlApiA = "https://localhost:7252/api/Peliculas";

        [HttpPost]
        public IActionResult Post([FromBody] Pelicula pelicula)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("InsertPelicula", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@titulo", pelicula.Titulo);
                    cmd.Parameters.AddWithValue("@genero", pelicula.Genero);
                    cmd.Parameters.AddWithValue("@director", pelicula.Director);
                    cmd.Parameters.AddWithValue("@anio_estreno", pelicula.Anio_estreno);
                    cmd.Parameters.AddWithValue("@duracion", pelicula.Duracion);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Pelicula insertada con éxito");
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Pelicula> peliculas = new List<Pelicula>();
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pelicula", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Pelicula pelicula = new Pelicula
                    {
                        Id_pelicula = (int)reader["id_pelicula"],
                        Titulo = reader["titulo"].ToString(),
                        Genero = reader["genero"].ToString(),
                        Director = reader["director"].ToString(),
                        Anio_estreno = (int)reader["anio_estreno"],
                        Duracion = (int)reader["duracion"],
                        Fecha_creacion = (DateTime)reader["fecha_creacion"]
                    };
                    peliculas.Add(pelicula);
                }
            }
            return Ok(peliculas);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Pelicula pelicula)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Pelicula SET titulo=@titulo, genero=@genero, director=@director, anio_estreno=@anio_estreno, duracion=@duracion WHERE id_pelicula=@id", connection);
                cmd.Parameters.AddWithValue("@titulo", pelicula.Titulo);
                cmd.Parameters.AddWithValue("@genero", pelicula.Genero);
                cmd.Parameters.AddWithValue("@director", pelicula.Director);
                cmd.Parameters.AddWithValue("@anio_estreno", pelicula.Anio_estreno);
                cmd.Parameters.AddWithValue("@duracion", pelicula.Duracion);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return Ok("Pelicula actualizada con éxito");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Pelicula WHERE id_pelicula=@id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return Ok("Pelicula eliminada con éxito");
        }
    }
}
