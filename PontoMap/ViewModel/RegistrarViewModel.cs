using PontoMap.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoMap.ViewModel
{
    public class RegistrarViewModel
    {
        public Empresa Empresa { get; set; }
        public Usuario Usuario { get; set; }



        [Required(ErrorMessage = "Confirmação sua senha")]
        [StringLength(50, ErrorMessage = "Sua senha deve ter no mínimo 5 caracteres.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Confirmação sua senha")]
        [StringLength(50, ErrorMessage = "Sua senha deve ter no mínimo 5 caracteres.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirmação de senha")]
        public string ConfirmPassword { get; set; }
    }
}