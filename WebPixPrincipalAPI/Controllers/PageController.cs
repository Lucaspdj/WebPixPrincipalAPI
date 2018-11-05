using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using WebPixPrincipalRepository;
using WebPixPrincipalAPI.Helper;
using System;
using WebPixPrincipalAPI.Model;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class PageController : Controller
    {

        [HttpPost("{token}")]
        [ActionName("SavePagina")]
        public async Task<JsonResult> SavePagina([FromBody]PageViewModel page, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                Page pagina = new Page();
                if (page.idCliente != 0)
                {
                    if (PageDAO.Save(ConvertToEntity(page)))
                    {
                        return Json("Pagina salva com sucesso");
                    }
                    else
                    {
                        return Json("Encontramos algum problema ao salvar a pagina. Entre em contato com o suporte");
                    }
                }
                else
                    return Json("Encontramos algum problema ao salvar a pagina. Entre em contato com o suporte");
            }
            else
            {
                return Json("Você nao tem acesso neste plugin");
            }
        }

        private Page ConvertToEntity(PageViewModel page)
        {
            Page pagina = new Page
            {
                Ativo = page.Ativo,
                Conteudo = System.Text.Encoding.UTF8.GetString(page.Conteudo),
                DataCriacao = page.DataCriacao,
                DateAlteracao = page.DateAlteracao,
                Descricao = page.Descricao,
                ID = page.ID,
                idCliente = page.idCliente,
                idMenu = page.idMenu,
                Nome = page.Nome,
                Status = page.Status,
                Titulo = page.Titulo,
                Url = page.Url,
                UsuarioCriacao= page.UsuarioCriacao,
                UsuarioEdicao = page.UsuarioEdicao
            };
            return pagina;
        }

        [ActionName("GetAllPagina")]
        [HttpGet("{idCliente}/{token}")]
        public async Task<IEnumerable<Page>> GetAllPagina(int idcliente, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                var aa = PageDAO.GetAll().Where(x => x.idCliente == idcliente).ToList();
                return aa;
            }
            else
                return new List<Page>();
        }

        [ActionName("GetPagina")]
        [HttpGet("{idpage}/{idCliente}/{token}")]
        public async Task<IEnumerable<Page>> GetPagina(int idcliente,int idpage, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                return PageDAO.GetAll().Where(x => x.idCliente == idcliente && x.ID == idpage).ToList();
            }
            else
                return new List<Page>();
        }

        [ActionName("DeletarPagina")]
        [HttpPost("{token}")]
        public async Task<JsonResult> DeletarPagina([FromBody]object page,  string token)
        {
            dynamic objEn = page;
            string a = objEn.idPagina.ToString();
            if (await Seguranca.validaTokenAsync(token))
            {
                Page obj = PageDAO.GetAll().Where(x => x.ID == Convert.ToInt32(a)).FirstOrDefault();
                return Json(new { msg = PageDAO.Remove(obj) });
                //return Json(new { msg = false });
            }
            else
                return Json(new { msg = false });
        }
    }
}