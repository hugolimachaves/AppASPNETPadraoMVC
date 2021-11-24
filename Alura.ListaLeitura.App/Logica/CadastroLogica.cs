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
    class CadastroLogica
    {
        public static Task ExibeFormulario(HttpContext context)
        {
            var html = HtmlUtils.CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

      

        public static Task NovoLivroParaLer(HttpContext context)
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

        public static Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Autor = context.Request.Form["autor"].First(),
                Titulo = context.Request.Form["titulo"].First()
            };
            Console.WriteLine(livro.Titulo);
            Console.WriteLine(livro.Autor);
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi adicionado com sucesso");
        }
    }
}
