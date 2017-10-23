using System;
using Microsoft.AspNetCore.Mvc;
using WebPixPrincipalRepository.Entity;
using Microsoft.AspNetCore.Cors;
using WebPixPrincipalBLL;
using WebPixPrincipalAPI.Helper;
using System.Threading.Tasks;

namespace WebPixPrincipalAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    public class EmailController : Controller
    {
        [ActionName("EnviaSimplesEmail")]
        [HttpPost("{token}")]
        public async Task<string> EnviaSimplesEmail([FromBody]Object envio, string token)
        {
            if (await Seguranca.validaTokenAsync(token))
            {
                dynamic obj = envio;

                Email email = new Email();

                email.Conteudo = obj.Conteudo;
                email.Titulo = obj.Titulo;
                string remetente = obj.remetente;
                string destinatario = obj.destinatario;
                int idCliente = obj.idCliente;


                if (EmailBO.EnviaSimplesEmail(email, remetente, destinatario, idCliente))
                {
                    return "E-mail enviado com sucesso";
                }
                else
                {
                    return "Houve uma falha ao enviar email";
                }
            }
            else
                return "Você nao tem acesso esse plugin";


        }

        [ActionName("GetSimples")]
        [HttpGet]
        public string GetSimples()
        {
            return "teste";
        }
        
    }
}