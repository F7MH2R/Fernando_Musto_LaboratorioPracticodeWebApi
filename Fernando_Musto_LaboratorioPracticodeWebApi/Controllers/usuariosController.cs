using Fernando_Musto_LaboratorioPracticodeWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fernando_Musto_LaboratorioPracticodeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogContext _usuarioContex;

        public usuariosController(blogContext usuarioContex)
        {
            _usuarioContex = usuarioContex;

        }

        //Peticiones

        ///Mostrar todo GET
        [HttpGet]
        [Route("TODOSLOS_USUARIOS")]

        public IActionResult todousuario()
        {
            List<usuarios> listatipoEquip = (from e in _usuarioContex.usuarios
                                                select e).ToList();
            if (listatipoEquip.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(listatipoEquip);

            }

        }
        //Peticion para agregar un equipo
        [HttpPost]
        [Route("AGREGAR")]
        public IActionResult save_usuario([FromBody] usuarios newusuario)
        {
            try
            {
                _usuarioContex.usuarios.Add(newusuario);
                _usuarioContex.SaveChanges();
                return Ok("SE HA AGREGADO EXITOSAMENTE😎\n\n"+newusuario);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        //Peticion para actualizar un registro
        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult update_usuario(int id, [FromBody] usuarios usuarioUpdate)
        {

            //Buscar el registro que se desea modificar
            //Contener en el objeto equiposelection
            usuarios? usuarioselect = (from e in _usuarioContex.usuarios
                                       where e.usuarioId == id
                                       select e).FirstOrDefault();

            //Verificar que si existe el registro con el id correspondiente

            //Si se encuentra modificar

            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                usuarioselect.nombreUsuario = usuarioUpdate.nombreUsuario;
                usuarioselect.nombre = usuarioUpdate.nombre;
                usuarioselect.apellido = usuarioUpdate.apellido;



                //Marcamos el registro modificado
                //Enviar modificaciones a la base de datos

                _usuarioContex.Entry(usuarioselect).State = EntityState.Modified;
                _usuarioContex.SaveChanges();
                return Ok("SE HA ACTUALIZADO EXITOSAMENTE😎\n\n" + "Nombre de usuario: "+usuarioUpdate.nombreUsuario +
                    "\nNombre: " +usuarioUpdate.nombre +"\n Apellido: "+usuarioUpdate.apellido);


            }

        }


        //Eliminar un registro
        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult delete_usuario(int id)
        {
            //Obtener el registro que se desea eliminar
            usuarios? usuarioselect = (from e in _usuarioContex.usuarios
                                       where e.usuarioId == id
                                       select e).FirstOrDefault();

            //Verificamos si existe
            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                //si existe ejecutamos la accion de eliminar
                _usuarioContex.usuarios.Attach(usuarioselect);
                _usuarioContex.usuarios.Remove(usuarioselect);
                _usuarioContex.SaveChanges();
                return Ok("Se a eliminado el usuario \n" +"Nombre de usuario: " + usuarioselect.nombreUsuario +
                    "\nNombre: " + usuarioselect.nombre + "\n Apellido: " + usuarioselect.apellido);


            }

        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscarnameu/{nombreusuario}")]
        public IActionResult search_ref(string nombreusuario)
        {

            //Buscar el registro con la consulta
            usuarios? usuarioselect = (from e in _usuarioContex.usuarios
                                       where e.nombreUsuario == nombreusuario
                                       select e).FirstOrDefault();


            //Verificar si existe
            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Nombre de usuario: " + usuarioselect.nombreUsuario +
                    "\nNombre: " + usuarioselect.nombre + "\n Apellido: " + usuarioselect.apellido);
            }
        }
        //Filtrado de un registro
        [HttpGet]
        [Route("Buscarname/{nombre}")]
        public IActionResult search_name(string nombre)
        {

            //Buscar el registro con la consulta
            usuarios? usuarioselect = (from e in _usuarioContex.usuarios
                                       where e.nombre == nombre
                                       select e).FirstOrDefault();


            //Verificar si existe
            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Nombre de usuario: " + usuarioselect.nombreUsuario +
                    "\nNombre: " + usuarioselect.nombre + "\n Apellido: " + usuarioselect.apellido);
            }
        }

        //Filtrado de un registro
        [HttpGet]
        [Route("Buscarlastname/{apellido}")]
        public IActionResult search_apellido(string apellido)
        {

            //Buscar el registro con la consulta
            usuarios? usuarioselect = (from e in _usuarioContex.usuarios
                                       where e.apellido == apellido
                                       select e).FirstOrDefault();


            //Verificar si existe
            if (usuarioselect == null)
            {
                return NotFound();
            }
            else
            {
                return Ok("Busqueda realizada con exito\n " + "Nombre de usuario: " + usuarioselect.nombreUsuario +
                    "\nNombre: " + usuarioselect.nombre + "\n Apellido: " + usuarioselect.apellido);
            }
        }

    }
}
