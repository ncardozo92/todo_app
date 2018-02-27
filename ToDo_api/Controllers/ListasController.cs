using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDo_api.Models;

namespace ToDo_api.Controllers
{
    public class ListasController : ApiController
    {
        public AppModel Context = new AppModel(); 

        [HttpPost]
        public IHttpActionResult GetListas(Usuario Usuario)
        {
            List<ToDoList> Listas = Context.ToDoList.Where(td => td.IdUsuario == Usuario.Id).ToList();

            return Ok(Listas);
        }

        [HttpGet]
        public IHttpActionResult GetLista(int Id)
        {
            ToDoList Lista = Context.ToDoList.Find(Id);

            return Ok(Lista);
        }

        [HttpPost]
        public HttpResponseMessage Crear( ToDoList ToDoList)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "hay campos incompletos");


            try
            {
                Context.ToDoList.Add(ToDoList);
                Context.SaveChanges();

                //return Ok("Lista creada");
                return Request.CreateResponse(HttpStatusCode.OK, "Lista creada correctamente");
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage ModificarLista(ToDoList ListaModificada)
        {

            ToDoList Lista = Context.ToDoList.Find(ListaModificada.Id);

            if (Lista == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró la lista solicitada");
            try
            {


                Lista.Nombre = ListaModificada.Nombre;
                Lista.Descripcion = ListaModificada.Descripcion;

                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Modificación realizada");
            }
            catch (Exception e)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage Eliminar(long Id)
        {
            ToDoList ToDoList = Context.ToDoList.Find( Id );

            if (ToDoList == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Lista no encontrada");
            /*
            foreach (Tarea Tarea in ToDoList.Tarea)
            {
                Context.Tarea.Remove(Tarea);
            }
            
            Context.ToDoList.Remove(ToDoList);

            Context.SaveChanges();

            return Ok("");
            */

            List<Tarea> Tareas = Context.Tarea.Where(t => t.IdTodo == Id).ToList();

            foreach (Tarea Tarea in Tareas)
            {

                Context.Tarea.Remove(Tarea);
            }

            Context.ToDoList.Remove(ToDoList);

            Context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Lista eliminada");
        }

        [HttpGet]
        public IHttpActionResult GetTareas(long Id)
        {
            return Ok(Context.Tarea.Where( t => t.IdTodo == Id));
        }

        [HttpPost]
        public HttpResponseMessage AgregarTarea(Tarea Tarea)
        {
            Tarea.Hecho = false; //pasarlo como propiedad de atributo de tabla
            
            try
            {
                Context.Tarea.Add(Tarea);
                Context.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Tarea agregada");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage EliminarTarea(long Id)
        {
            Tarea Tarea = Context.Tarea.Find(Id);

            if (Tarea == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Lista no encontrada");

            Context.Tarea.Remove(Tarea);
            Context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "Modificación realizada");
        }

        [HttpGet]
        public HttpResponseMessage ActualizarTarea(long Id)
        {

            Tarea Tarea = Context.Tarea.Find(Id);

            if(Tarea == null)
                return Request.CreateResponse(HttpStatusCode.NotFound, "Tarea no encontrada");

            bool Hecho = Tarea.Hecho;

            Tarea.Hecho = !Hecho;

            Context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "tarea actualizada");
        }
    }
}
