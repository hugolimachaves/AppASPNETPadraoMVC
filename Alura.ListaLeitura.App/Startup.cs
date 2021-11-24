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
using Alura.ListaLeitura.App.Logica;

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
            builder.MapRoute("Livros/ParaLer", LivrosLogica.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLogica.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLogica.LivrosLidos);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.ExibeDetalhes); // colocando a restrição no tipo de dado de id : {tipo}
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivroParaLer);
            builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);
            var rotas = builder.Build();

            app.UseRouter(rotas); // Esse código determina que o estágio terminal do
                                  // request pipeline irá tratar as rotas definidas
                                  // pela coleção armazenada na variável rotas.
                                  // Sem ele não seria possível tratar qualquer requisição na aplicação.
        }

        
        

   
       

      
       
    }
}