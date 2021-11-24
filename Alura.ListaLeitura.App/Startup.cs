using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Alura.ListaLeitura.App.Negocio;
using System;
using System.Linq;
using System.IO;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(); //serviço de roteamento do aspnet core
        }
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes); // colocando a restrição no tipo de dado de id : {tipo}
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", ProcessaFormulario);
            var rotas = builder.Build();

            app.UseRouter(rotas);
            //app.Run(Roteamento);
        }

        public Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Autor = context.Request.Query["autor"].First(),
                Titulo = context.Request.Query["titulo"].First()
            };
            Console.WriteLine(livro.Titulo);
            Console.WriteLine(livro.Autor);
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso");


        }

        public Task ExibeFormulario(HttpContext context)
        {
            var html = CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        private string CarregaArquivoHTML(string nomeArquivo)
        {
        
            var nomeCompletoArquivo = $"D:/Cursos/C#_web/Alura.ListaLeitura/Alura.ListaLeitura.App/HTML/{nomeArquivo}.html";
            //Nota: importante usar o $ para interpolar string com variaveis
            using( var arquivo = File.OpenText(nomeCompletoArquivo)) 
            {
                return arquivo.ReadToEnd();
            }
        }

        public Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }
        public Task NovoLivroParaLer(HttpContext context)
        {

            var livro = new Livro()
            {
                Titulo = context.GetRouteValue("nome").ToString(), // ambas conversões de string são válidas!
                Autor = Convert.ToString(context.GetRouteValue("autor"))  // ambas conversões de string são válidas!
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
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

        LivroRepositorioCSV _repo = new LivroRepositorioCSV();
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
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }

       
    }
}