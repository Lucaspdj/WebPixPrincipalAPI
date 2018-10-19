using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using Microsoft.AspNetCore.Cors;
using WebPixPrincipalAPI.Helper;
using System.Threading.Tasks;
using WebPixPrincipalAPI.Model;
using System;
using CustomExtensions;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class UsuarioController : Controller
    {
        [ActionName("SaveUsuario")]
        [HttpPost("{token}")]
        public async Task<JsonResult> SaveUsuario([FromBody]Usuario usuario, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                if (UsuarioDAO.Save(usuario))
                {
                    return Json("Usuario salva com sucesso");
                }
                else
                {
                    return Json("Encontramos algum problema ao salvar o usuario. Entre em contato com o suporte");
                }
            }
            return Json("Você nao tem acesso a esse plugin");
        }

        [ActionName("GetAllUsuario")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Usuario>> GetAllUsuario(int idcliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
                return UsuarioDAO.GetAll().Where(x => x.idCliente == idcliente).ToList();
            else
                return new List<Usuario>();

        }

        [ActionName("GetUsuarioById")]
        [HttpGet("{idCliente:int}/{idUsuario:int}/{token}")]
        public async Task<Usuario> GetUsuarioById(int idCliente, int idUsuario, string token)
        {
            if(await Seguranca.validaTokenAsync(token))
            {
                return UsuarioDAO.GetById(idCliente, idUsuario);
            }
            else
            {
                return new Usuario();
            }
        }

        [ActionName("LoginUsuario")]
        [HttpPost("{token}")]
        public async Task<Usuario> LoginUsuario([FromBody]object ObjLogin, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                dynamic obj = ObjLogin;
                string login = obj.Login;
                string senha = obj.Senha;
                int idCliente = obj.idCliente;
                Usuario user = UsuarioDAO.GetAll().Find(x => x.Login == login && x.Senha == senha && x.idCliente == idCliente);
                if (user != null)
                {
                    return user;
                }
                else
                    return new Usuario();


            }
            else
                return new Usuario();
        }

        [ActionName("DeletarUsuario")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarUsuario([FromBody]object Usuario, string token)
        {
            dynamic objEn = Usuario;
            string a = objEn.idUsuario.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Usuario obj = UsuarioDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = UsuarioDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }
    }
}
