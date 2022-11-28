using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UpArquivos.Models
{
    public class Home
    {
        [Key]
        public int ArquivoID { get; set; }
        
        [Required(ErrorMessage = "Informe o Nome do Arquivo")]
        [Display(Name = "Nome do Arquivo")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe a Descrição")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        
        [Required(ErrorMessage = "Insira o Arquivo")]
        [Display(Name = "Arquivo")]
        public IFormFile Arquivo { get; set; }

        public string NomeArquivo { get; set; }
    }
}
