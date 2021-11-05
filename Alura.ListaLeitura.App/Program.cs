using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Alura.ListaLeitura.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _repo = new LivroRepositorioCSV();

            IWebHost host = new WebHostBuilder()  // IWebhost: Hospedeiro Web. Para criar um objeto que implementa essa insterface, utilizamos um builder (WebHostBuilder)
                .UseKestrel() // Vamos usar para esse host o servidor Kestrel
                .UseStartup<Startup>() // indica qual é a classe de inicialização desse servidor
                .Build();

            host.Run();//bloqueia a execução das linhas abaixo até que o servido seja encerrado

            ImprimeLista(_repo.ParaLer);
            ImprimeLista(_repo.Lendo);
            ImprimeLista(_repo.Lidos);
        }

        static void ImprimeLista(ListaDeLeitura lista)
        {
            Console.WriteLine(lista);
        }
    }

}
