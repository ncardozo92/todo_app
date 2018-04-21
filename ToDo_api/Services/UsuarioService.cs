using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using ToDo_api.Models;

namespace ToDo_api.Services
{
    public class UsuarioService
    {
        private AppModel Context = new AppModel();

       private const string Secret = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";
        //key es la clave con la que voy a encriptar todas las partes del token

        private JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();//se encarga de  generar y leer el token


        public Usuario GetUsuario(Usuario usuario)
        {
            return Context.Usuario.FirstOrDefault(u => u.Username == usuario.Username && u.Clave == usuario.Clave);
        }

        public string GenerateToken(Usuario usuario)
        {
            var symmetricKey = Convert.FromBase64String(Secret);//se crea la clave simetrica para el cifrado 

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]//los claims son los datos de una entidad, componen el payload 
                {
                    new Claim("Username", usuario.Username),
                    new Claim("UserId", usuario.Id.ToString())//el valor de los claims debe ser de tipo string
                }),

                Expires = DateTime.UtcNow.AddHours(5),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
                //las credenciales son las que encriptan el jwt
            };

            var stoken = handler.CreateToken(tokenDescriptor);
            
            return handler.WriteToken(stoken);
        }

        public long GetIdFromJwt(string token)
        {
            var jwtToken = handler.ReadJwtToken(token);
            
            return long.Parse(jwtToken.Claims.First(claim => claim.Type == "UserId").Value);
        }

        public bool ValidarToken(string token)
        {
            SecurityToken securityToken;//?
            var parameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Secret))
            };

            var principal = handler.ValidateToken(token,parameters, out securityToken);

            //flata todavia, usar manejo de errores
        }

        public bool CrearUsuario(Usuario usuario)
        {

            try
            {
                Context.Usuario.Add(usuario);
                Context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}