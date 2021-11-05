using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
        }

        public Task LivrosParaLer(HttpContext context) //context é um objeto que tem todas as informações específicas sobre a requisição.
        {

            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.ParaLer.ToString()); // aqui eu vou escreve nas resposta alguma coisa (nesse contexto é uma lista de livros)
        }

        public Task Roteamento(HttpContext context)
        {
            return context.Response.WriteAsync(context.Request.Path);
        }
    }
}