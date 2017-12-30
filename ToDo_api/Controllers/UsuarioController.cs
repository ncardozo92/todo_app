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
        private Context Context = new Context();

        public IEnumerable<Usuario> GetUsuarios()
        {
            return Context.Usuario.ToList();
        }
    }
}
