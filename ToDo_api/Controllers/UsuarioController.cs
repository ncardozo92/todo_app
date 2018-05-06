using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using ToDo_api.Models;
using ToDo_api.Services;

namespace ToDo_api.Controllers
{
    public class UsuarioController : ApiController
    {
        private UsuarioService usuarioService = new UsuarioService();
        //private AppModel Context = new AppModel();
        
        [HttpGet]
        public HttpResponseMessage GetId(string jwt)
        {
            return Request.CreateResponse(HttpStatusCode.OK, usuarioService.GetIdFromJwt(jwt));
        }

        [HttpPost]
        public HttpResponseMessage Login(Usuario usuario)
        {
            if (usuario.Username.Length == 0 || usuario.Clave.Length == 0)
                return Request.CreateResponse(HttpStatusCode.Unauthorized, "Usuario no autorizado");

            Usuario usuarioEncontrado = usuarioService.GetUsuario(usuario);

                if (usuarioEncontrado != null && usuarioEncontrado.Clave == usuario.Clave)
                {
                    string token = usuarioService.GenerateToken(usuarioEncontrado);

                    if(token != null)
                        return Request.CreateResponse(HttpStatusCode.OK,token);
                    else
                        return Request.CreateResponse(HttpStatusCode.InternalServerError, "No se ha podido iniciar sesión");
                }

                return Request.CreateResponse(HttpStatusCode.NotFound, "Usuario no encontrado");
        }


        [HttpPost]
        public HttpResponseMessage Registrar(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Debe ingresarse todos los datos");

            if (usuarioService.GetUsuario(usuario) != null)
                return Request.CreateResponse(HttpStatusCode.Conflict,"Ya existe un usuario con el mismo Username o la misma dirección de e-mail.");

            if (usuarioService.CrearUsuario(usuario))
                return Request.CreateResponse(HttpStatusCode.OK, "Usuario creado");
            else
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Error al crear el usuario, intentelo más tarde");
        }

        public IHttpActionResult Logout()
        {

            return Ok("Uso futuro");
        }
    }

}
