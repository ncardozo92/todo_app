using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDo_api.Models;

namespace ToDo_api.Controllers
{
    public class UsuarioController : ApiController
    {
        public AppModel Context = new AppModel();

        [HttpPost]
        public HttpResponseMessage Login(Usuario usuario)
        {
            if (usuario.Username.Length == 0 || usuario.Clave.Length == 0)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Usuario no autorizado");

            Usuario usuarioAutenticado = Context.Usuario.FirstOrDefault(u => u.Username == usuario.Username && u.Clave == usuario.Clave);

            if( usuarioAutenticado != null)
                return Request.CreateResponse(HttpStatusCode.OK, usuarioAutenticado);
            else
                return Request.CreateResponse(HttpStatusCode.NotFound, "No se encontró el usuario");
        }
        
        [HttpPost]
        public HttpResponseMessage Registrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Debe ingresarse todos los datos");

            if ((Context.Usuario.Where(u => u.Username == usuario.Username || u.Mail == usuario.Mail).FirstOrDefault()) != null)
                return Request.CreateResponse(HttpStatusCode.Conflict,"Ya existe un usuario con el mismo Username o la misma dirección de e-mail.");

            try
                {
                    Context.Usuario.Add(usuario);
                    Context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.OK, "Usuario Registrado");
            }
                catch (Exception e)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public IHttpActionResult Logout()
        {

            return Ok("Uso futuro");
        }
    }

}
