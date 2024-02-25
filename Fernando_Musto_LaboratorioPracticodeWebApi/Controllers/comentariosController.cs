using Fernando_Musto_LaboratorioPracticodeWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fernando_Musto_LaboratorioPracticodeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogContext _comentarioContext;

        public comentariosController(blogContext comentarioContextt)
        {
            _comentarioContext = comentarioContextt;

        }

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("todas_los_comentarios")]

        public IActionResult allcomments()
        {
            List<comentarios> list_comment = (from e in _comentarioContext.comentarios
                                                select e).ToList();
            if (list_comment.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list_comment);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AgregarComentario")]
        public IActionResult save_coments([FromBody] comentarios newcomment)
        {
            try
            {
                _comentarioContext.comentarios.Add(newcomment);
                _comentarioContext.SaveChanges();
                return Ok("💚SE HA AGREGADO EXITOSAMENTE😎\n\n" + newcomment);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar_Comentario/{id}")]
        public IActionResult update_comments(int id, [FromBody] comentarios update_comment)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            comentarios? Coment_select = (from e in _comentarioContext.comentarios
                                            where e.cometarioId == id
                                            select e).FirstOrDefault();
            //Verificar que si existe el registro con el id correspondiente
            //Si se encuentra modificar

            if (Coment_select == null)
            {
                return NotFound();
            }
            else
            {
                Coment_select.comentario = update_comment.comentario;
                Coment_select.publicacionId = update_comment.publicacionId;
                Coment_select.usuarioId = update_comment.usuarioId;

                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _comentarioContext.Entry(Coment_select).State = EntityState.Modified;
                _comentarioContext.SaveChanges();
                return Ok("SE HA ACTUALIZADO EXITOSAMENTE😎\n\n" + "Id de la publicacion: " + update_comment.publicacionId +
                    "\nId del usuario: " + update_comment.usuarioId + "\n Comentario: " + update_comment.comentario);


            }

        }


        //Eliminar un registro
        [HttpDelete]
        [Route("DeleteComentarioID/{id}")]
        public IActionResult delete_comentario(int id)
        {
            //Obtener el registro que se desea eliminar
            comentarios? comment_select = (from e in _comentarioContext.comentarios
                                            where e.cometarioId == id
                                            select e).FirstOrDefault();

            //Verificamos si existe
            if (comment_select == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _comentarioContext.comentarios.Attach(comment_select);
                _comentarioContext.comentarios.Remove(comment_select);
                _comentarioContext.SaveChanges();
                return Ok("💔Se a eliminado el comentario exitosamente💔 \n");


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("buscarid_Comentario/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            comentarios? comments_select = (from e in _comentarioContext.comentarios
                                            where e.usuarioId == id
                                            select e).FirstOrDefault();

            //Verificar si existe
            if (comments_select == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("✔️Busqueda realizada con exito✔️\n " + "Usuario ID: " + comments_select.usuarioId +
                    "\nID del la calificación: " + comments_select.cometarioId + "\nPublicación ID: " + comments_select.usuarioId+ "\n Comentario: " + comments_select.comentario);
            }
        }
    }
}
