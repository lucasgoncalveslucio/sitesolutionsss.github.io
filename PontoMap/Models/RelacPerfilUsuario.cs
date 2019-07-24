using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoMap.Models
{
    public class RelacPerfilUsuario
    {
        [Required(ErrorMessage = "Descrição do Perfil de usuário obrigatória")]
        [Display(Name = "Descrição do Perfil de usuário")]
        public string DsPerfil { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required(ErrorMessage = "Informe o Id do usuário")]
        public int IdUsuario { get; set; }
    }
}