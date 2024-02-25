using Fernando_Musto_LaboratorioPracticodeWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fernando_Musto_LaboratorioPracticodeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class calificacionesController : ControllerBase
    {
        private readonly blogContext _calificacionesContext;

        public calificacionesController(blogContext calificacionesContex)
        {
            _calificacionesContext = calificacionesContex;

        }

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("TODAS_LAS_CALIFICACIONES")]

        public IActionResult allgrade()
        {
            List<calificaciones> list_califi = (from e in _calificacionesContext.calificaciones
                                             select e).ToList();
            if (list_califi.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(list_califi);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGAR_CALIFICACION")]
        public IActionResult save_grade([FromBody] calificaciones newgrade)
        {
            try
            {
                _calificacionesContext.calificaciones.Add(newgrade);
                _calificacionesContext.SaveChanges();
                return Ok("SE HA AGREGADO EXITOSAMENTE😎\n\n" + newgrade);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar_por_el_idpublicacion/{id}")]
        public IActionResult update_grade(int id, [FromBody] calificaciones grade_update)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            calificaciones? grade_select = (from e in _calificacionesContext.calificaciones
                                       where e.publicacionId == id
                                       select e).FirstOrDefault();
            //Verificar que si existe el registro con el id correspondiente
            //Si se encuentra modificar

            if (grade_select == null)
            {
                return NotFound();
            }
            else
            {
                grade_select.calificacion = grade_update.calificacion;
                grade_select.publicacionId = grade_update.publicacionId;
                grade_select.usuarioId = grade_update.usuarioId;

                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _calificacionesContext.Entry(grade_select).State = EntityState.Modified;
                _calificacionesContext.SaveChanges();
                return Ok("SE HA ACTUALIZADO EXITOSAMENTE😎\n\n" + "Id de la publicacion: " + grade_update.publicacionId +
                    "\nId del usuario: " + grade_update.usuarioId + "\n Calificacion: " + grade_update.calificacion);


            }

        }


        //Eliminar un registro
        [HttpDelete]
        [Route("Deletecal_idcalificacion/{id}")]
        public IActionResult delete_calificacion(int id)
        {
            //Obtener el registro que se desea eliminar
            calificaciones? grade_select = (from e in _calificacionesContext.calificaciones
                                       where e.calificacionId == id
                                       select e).FirstOrDefault();

            //Verificamos si existe
            if (grade_select == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _calificacionesContext.calificaciones.Attach(grade_select);
                _calificacionesContext.calificaciones.Remove(grade_select);
                _calificacionesContext.SaveChanges();
                return Ok("Se a eliminado el calificación exitosamente \n" );


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("buscar_idpubli/{id}")]
        public IActionResult search_ref(int id)
        {

            //Buscar el registro con la consulta
            calificaciones? grade_select = (from e in _calificacionesContext.calificaciones
                                       where e.publicacionId == id
                                       select e).FirstOrDefault();

            //Verificar si existe
            if (grade_select == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Publicacion ID: " + grade_select.publicacionId +
                    "\nID del la calificacio: " + grade_select.calificacionId + "\n Calificación: " + grade_select.calificacion);
            }
        }
       
    }
}
