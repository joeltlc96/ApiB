﻿using ApiB.Comunes;
using ApiB.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Actor actor)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                using (SqlCommand cmd = new SqlCommand("InsertActor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", actor.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", actor.Apellido);
                    cmd.Parameters.AddWithValue("@fecha_nacimiento", actor.Fecha_nacimiento);
                    cmd.Parameters.AddWithValue("@nacionalidad", actor.Nacionalidad);
                    cmd.Parameters.AddWithValue("@genero_biografia", actor.Genero_biografia);
                    cmd.Parameters.AddWithValue("@premios", actor.Premios);
                    cmd.Parameters.AddWithValue("@numero_peliculas", actor.Numero_peliculas);
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok("Actor insertado con éxito");
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Actor> actores = new List<Actor>();
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Actor", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Actor actor = new Actor
                    {
                        Id_actor = (int)reader["id_actor"],
                        Nombre = reader["nombre"].ToString(),
                        Apellido = reader["apellido"].ToString(),
                        Fecha_nacimiento = (DateTime)reader["fecha_nacimiento"],
                        Nacionalidad = reader["nacionalidad"].ToString(),
                        Genero_biografia = reader["genero_biografia"].ToString(),
                        Premios = reader["premios"].ToString(),
                        Numero_peliculas = (int)reader["numero_peliculas"],
                        Fecha_creacion = (DateTime)reader["fecha_creacion"]
                    };
                    actores.Add(actor);
                }
            }
            return Ok(actores);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Actor actor)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("UPDATE Actor SET nombre=@nombre, apellido=@apellido, fecha_nacimiento=@fecha_nacimiento, nacionalidad=@nacionalidad, genero_biografia=@genero_biografia, premios=@premios, numero_peliculas=@numero_peliculas WHERE id_actor=@id", connection);
                cmd.Parameters.AddWithValue("@nombre", actor.Nombre);
                cmd.Parameters.AddWithValue("@apellido", actor.Apellido);
                cmd.Parameters.AddWithValue("@fecha_nacimiento", actor.Fecha_nacimiento);
                cmd.Parameters.AddWithValue("@nacionalidad", actor.Nacionalidad);
                cmd.Parameters.AddWithValue("@genero_biografia", actor.Genero_biografia);
                cmd.Parameters.AddWithValue("@premios", actor.Premios);
                cmd.Parameters.AddWithValue("@numero_peliculas", actor.Numero_peliculas);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return Ok("Actor actualizado con éxito");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (SqlConnection connection = ConexionDB.abrirConexion())
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Actor WHERE id_actor=@id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            return Ok("Actor eliminado con éxito");
        }
    }
}
