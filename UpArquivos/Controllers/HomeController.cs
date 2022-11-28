using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using UpArquivos.Data.Commum;
using UpArquivos.Models;
using static UpArquivos.Data.Commum.UploadArquivo;

namespace UpArquivos.Controllers
{
    public class HomeController : BasicController
    {

        private readonly IWebHostEnvironment webHostEnvironment;

        //Método construtur para passar o caminho do servidor 
        public HomeController(IHostEnvironment enviroment, IWebHostEnvironment hostEnvironment)
        {
            Enviroment = enviroment;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index(IFormCollection collection, UploadsArquivos arquivos)
        {

            string nomeArquivo = "";

            if (arquivos.Arquivo != null)
            {
                nomeArquivo = UploadedFile(arquivos, webHostEnvironment.WebRootPath, "Arquivos");
                // salvando arquivo em disco
                new BinaryReader(arquivos.Arquivo.OpenReadStream());
            }

            try
            {
                var dados = new Home()
                {
                    Nome = collection["Loja"],
                    Descricao = collection["Descricao"],
                    NomeArquivo = nomeArquivo
                };

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }


        }
    }
}
