using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        LivroRepositorioCSV _repo = new LivroRepositorioCSV();
        public void Configure(IApplicationBuilder app)
        {
            app.Run(Roteamento);
        }

        public Task Roteamento(HttpContext context)
        {
            //var _repo = new LivroRepositorioCSV();
            var caminhosAtendidos = new  Dictionary<string, RequestDelegate>
            {
                {"/Livros/ParaLer", LivrosParaLer},
                {"/Livros/Lendo",  LivrosLendo },
                {"/Livros/Lidos", LivrosLidos}

            };

            if( caminhosAtendidos.ContainsKey(context.Request.Path) )
            {
                var metodo = caminhosAtendidos[context.Request.Path]; // a variável e do tipo request delegate
                return metodo.Invoke(context); // retornando a variavel request delegate sendo invocada
            }
            context.Response.StatusCode = 404; //retornando um erro de requisição 404
            return context.Response.WriteAsync("caminho inexistente");
        }

        public Task LivrosParaLer(HttpContext context) //context é um objeto que tem todas as informações específicas sobre a requisição.
        {

            //var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.ParaLer.ToString()); // aqui eu vou escreve nas resposta alguma coisa (nesse contexto é uma lista de livros)
        }

        public Task LivrosLendo(HttpContext context) 
        {
            //var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext context)
        {
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

       
    }
}