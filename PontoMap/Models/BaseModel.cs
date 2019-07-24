using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PontoMap.Models
{
    public class BaseModel
    {
        /// <summary>
        /// Retorno das mensagens Ex: Sucesso, Erro...
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Retorno lógico mensagens Ex: Sucesso = 1, Erro = 0...
        /// </summary>
        public int Status { get; set; } = 0;
    }
}