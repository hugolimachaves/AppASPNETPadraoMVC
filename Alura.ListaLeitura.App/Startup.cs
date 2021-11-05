using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
        }

        public Task Roteamento(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var caminhosAtendidos = new  Dictionary<string, string>
            {
                {"/Livros/ParaLer", _repo.ParaLer.ToString() },
                {"/Livros/Lendo", _repo.Lendo.ToString() },
                {"/Livros/Lidos", _repo.Lidos.ToString()}

            };

            if( caminhosAtendidos.ContainsKey(context.Request.Path) )
            {
                return context.Response.WriteAsync(caminhosAtendidos[context.Request.Path.ToString()]);
            }

            return context.Response.WriteAsync("caminho inexistente");
        }

        public Task LivrosParaLer(HttpContext context) //context é um objeto que tem todas as informações específicas sobre a requisição.
        {

            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.ParaLer.ToString()); // aqui eu vou escreve nas resposta alguma coisa (nesse contexto é uma lista de livros)
        }

       
    }
}