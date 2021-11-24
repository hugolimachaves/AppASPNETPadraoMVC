using System;
using System.Collections.Generic;
using System.Text;

namespace Alura.ListaLeitura.App.HTML
{
    class HtmlUtils
    {
        public static string CarregaArquivoHTML(string nomeArquivo)
        {

            var nomeCompletoArquivo = $"D:/Cursos/C#_web/Alura.ListaLeitura/Alura.ListaLeitura.App/HTML/{nomeArquivo}.html";
            //Nota: importante usar o $ para interpolar string com variaveis
            using (var arquivo = System.IO.File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();
            }
        }
    }
}
