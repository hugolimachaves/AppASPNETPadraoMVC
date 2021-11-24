using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alura.ListaLeitura.App.HTML;

namespace Alura.ListaLeitura.App.Logica
{
    class LivrosLogica
    {
        public static Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV();
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        


        public static Task LivrosParaLer(HttpContext context) //context é um objeto que tem todas as informações específicas sobre a requisição.
        {
            var _repo = new LivroRepositorioCSV();
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("ParaLer");

            foreach (var livro in _repo.ParaLer.Livros)
            {
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM", $"<li>{livro.Titulo} - {livro.Autor}</li>#NOVO-ITEM");
            }
            conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM", "");
            return context.Response.WriteAsync(conteudoArquivo); // aqui eu vou escreve nas resposta alguma coisa (nesse contexto é uma lista de livros)
        }

       

        public static Task LivrosLendo(HttpContext context)
        {
            LivroRepositorioCSV _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public static Task LivrosLidos(HttpContext context)
        {
            LivroRepositorioCSV _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }


      

   
    }
}
