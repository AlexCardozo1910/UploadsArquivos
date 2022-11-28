using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpArquivos.Data.Commum
{
    public class UploadArquivo
    {
		public UploadArquivo(string caminhoBaseArquivo)
		{
			CaminhoBase = caminhoBaseArquivo;
		}

		public string CaminhoBase { get; set; }
		public UploadsArquivos UploadArquivos { get; set; }

		public string AbrirArquivo()
		{
			return System.IO.File.ReadAllText(System.IO.Path.Combine(CaminhoBase, UploadArquivos.Nome));
		}

		public class UploadsArquivos
		{
			public string Nome { get; set; }
			public IFormFile Arquivo { get; set; }
		}
	}
}
