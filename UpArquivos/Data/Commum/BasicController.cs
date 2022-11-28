using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static UpArquivos.Data.Commum.UploadArquivo;

namespace UpArquivos.Data.Commum
{
    public class BasicController : Controller
    {
		public const string SessionNomeUsuario = "_NomeUsuario";
		public const string SessionUsuarioID = "_UsuarioID";
		public const string SessionPerfilID = "_PerfilID";
		public const string Token = "_Token";

		public const string SystemMessage = "MY_DIALOG";

		public IHostEnvironment Enviroment { get; set; }

		/*Converte em criptografia*/
		public static string HashValue(string value)
		{
			UnicodeEncoding encoding = new UnicodeEncoding();
			byte[] hashBytes;
			using (HashAlgorithm hash = SHA1.Create())
				hashBytes = hash.ComputeHash(encoding.GetBytes(value));

			StringBuilder hashValue = new StringBuilder(hashBytes.Length * 2);
			foreach (byte b in hashBytes)
			{
				hashValue.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
			}

			return hashValue.ToString();
		}

		/*Limpa o R$*/
		public string LimpaReais(string valor)
		{
			if (String.IsNullOrEmpty(valor))
				return "0.00";

			var limpa_real = valor.Replace("R$", "");
			var final = limpa_real.Replace(".", "");

			return final;
		}

		public string LimpaReaisDecimal(string valor)
		{

			if (String.IsNullOrEmpty(valor))
				return "0.00";

			var limpa_real = valor.Replace("R$", "");
			var limpa_ponto = limpa_real.Replace(".", "");
			var final = limpa_ponto.Replace(",", ".");

			return final;
		}

		public string ConverteReais(decimal valor)
		{
			var convert = valor.ToString("N2");
			var final = "R$ " + convert;

			return final;
		}

		public string TodosMeses(int mes)
		{
			return mes switch
			{
				1 => "Janeiro",
				2 => "Fevereiro",
				3 => "Março",
				4 => "Abril",
				5 => "Maio",
				6 => "Junho",
				7 => "Julho",
				8 => "Agosto",
				9 => "Setembro",
				10 => "Outubro",
				11 => "Novembro",
				12 => "Dezembro",
				_ => throw new System.NotImplementedException(),
			};
		}

		/**
		* Função para formatar a data de várias formas tanto para usuário quanto para o banco de dados
		* @Author Alex Cardozo
		* @param String tipo user['dd/mm/aaaa'], user2['dd/mm/aaaa hh:mm:ss'], bd['aaaa-mm-dd'], bd2['aaaa-mm-dd hh:mm:ss']
		* @param data
		* @return String
		*/
		public string FormatarData(string tipo, string data)
		{

			if (data == "" || data == null)
				return data;

			string data_formatada = "";

			if (tipo == "user")
			{
				string[] data_hora = data.Split(' ');
				string[] array_data = data_hora[0].Split('-');
				data_formatada = array_data[2] + "/" + array_data[1] + "/" + array_data[0];
			}
			else if (tipo == "user2")
			{
				string[] data_hora = data.Split(' ');
				string[] array_data = data_hora[0].Split('-');
				data_formatada = array_data[2] + "/" + array_data[1] + "/" + array_data[0] + " " + data_hora[1];
			}
			else if (tipo == "bd")
			{
				string[] data_hora = data.Split(' ');
				string[] array_data = data_hora[0].Split('/');
				data_formatada = array_data[2] + "-" + array_data[1] + "-" + array_data[0];
			}
			else if (tipo == "bd2")
			{
				string[] data_hora = data.Split(' ');
				string[] array_data = data_hora[0].Split('/');
				data_formatada = array_data[2] + "-" + array_data[1] + "-" + array_data[0] + " " + data_hora[1];
			}

			return data_formatada;
		}

		public string UploadedFile(UploadsArquivos arquivos, string CaminhoArquivo, string LocalArquivo)
		{
			string nomeUnicoArquivo = null;

			if (arquivos.Arquivo != null)
			{
				string pastaArquivo = Path.Combine(CaminhoArquivo, LocalArquivo);
				nomeUnicoArquivo = DateTime.Now.ToString("yyyyMMddssfff") + "_" + arquivos.Arquivo.FileName.Replace(" ", "");
				string caminhoArquivo = Path.Combine(pastaArquivo, nomeUnicoArquivo);
				using var fileStream = new FileStream(caminhoArquivo, FileMode.Create);
				arquivos.Arquivo.CopyTo(fileStream);
			}
			return nomeUnicoArquivo;
		}
	}
}
