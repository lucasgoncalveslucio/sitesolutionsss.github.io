using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PontoMap.Models;

namespace PontoMap.CustomValidations
{
    public class Util
    {
        // <summary>
        /// Remove caracteres não numéricos
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveNaoNumericos(string text)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"[^0-9]");
            string ret = reg.Replace(text, string.Empty);
            return ret;
        }

        /// <summary>
        /// Valida se um cpf é válido
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool ValidaCPF(string cpf)
        {
            //Remove formatação do número, ex: "123.456.789-01" vira: "12345678901"
            cpf = Util.RemoveNaoNumericos(cpf);

            if (cpf.Length > 11)
                return false;

            while (cpf.Length != 11)
                cpf = '0' + cpf;

            bool igual = true;
            for (int i = 1; i < 11 && igual; i++)
                if (cpf[i] != cpf[0])
                    igual = false;

            if (igual || cpf == "12345678909")
                return false;

            int[] numeros = new int[11];

            for (int i = 0; i < 11; i++)
                numeros[i] = int.Parse(cpf[i].ToString());

            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];

            int resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];

            resultado = soma % 11;

            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }
            else
                if (numeros[10] != 11 - resultado)
                return false;

            return true;
        }


        public static bool ValidaCNPJ(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return true;
        }

        public static bool ValidaObjeto(BaseModel objeto)
        {

            ValidationContext vc = new ValidationContext(objeto);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            if (Validator.TryValidateObject(objeto, vc, results, true)) return true;

            foreach (var result in results) objeto.Mensagem += result.MemberNames.FirstOrDefault() + " : " + result;

            return false;
        }

        public static bool ValidaAtributos(BaseModel objeto, List<string> atributos)
        {
            ValidationContext vc = new ValidationContext(objeto);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            if (Validator.TryValidateObject(objeto, vc, results, true)) return true;

            foreach (var result in results)
            {
                if (atributos.Contains(result.MemberNames.FirstOrDefault()))
                {
                    objeto.Mensagem += result.MemberNames.FirstOrDefault() + " : " + result;
                    return false;
                }

            }
            return true;
        }

        /// <summary>
        /// Verifica se é um inteiro
        /// O retorno é o próprio número caso inteiro se não retorna um zero
        /// </summary>
        /// <param name="input"></param>
        /// <param name="valueIfNotConverted"></param>
        /// <returns>Próprio número caso inteiro se não 0</returns>
        public static int ValidaInteiro(string input, int valueIfNotConverted)
        {
            int value;
            if (Int32.TryParse(input, out value))
            {
                return value;
            }
            return valueIfNotConverted;
        }


        public static DateTime HrBrasilia()
        {
            DateTime dateTime = DateTime.UtcNow;
            TimeZoneInfo hrBrasilia = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, hrBrasilia);
        }

        public static DateTime AbsoluteStart(DateTime dateTime)
        {
            return dateTime.Date;
        }

        /// <summary>
        /// Gets the 11:59:59 instance of a DateTime
        /// </summary>
        public static DateTime AbsoluteEnd(DateTime dateTime)
        {
            return AbsoluteStart(dateTime).AddDays(1).AddTicks(-1);
        }
    }
}