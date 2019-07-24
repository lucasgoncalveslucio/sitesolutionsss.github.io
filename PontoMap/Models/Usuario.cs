using PontoMap.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PontoMap.Models
{
    public class Usuario : BaseModel
    {
        [Range(1, Int32.MaxValue)]
        [Required(ErrorMessage = "Informe o Id do usuário")]
        public int IdUsuario { get; set; }

        [Range(1, Int32.MaxValue)]
        [Required(ErrorMessage = "Informe a empresa do Usuário")]
        public int IdEmpresa { get; set; }


        public List<Perfil> Perfis  { get; set; }

        [Required(ErrorMessage = "CPF obrigatório")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        [Display(Name = "CPF")]
        public string CdCpf { get; set; }

        [Required(ErrorMessage = "Email obrigatório")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        [Display(Name = "Email")]
        public string DsEmail { get; set; }

        [Required(ErrorMessage = "Celular obrigatório")]
        [Display(Name = "Celular")]
        public string DsCelular { get; set; }

        [Required(ErrorMessage = "Senha obrigatório")]
        [Display(Name = "Senha")]
        [StringLength(50, ErrorMessage = "Sua senha deve ter no mínimo 5 caracteres.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string CdPassword { get; set; }


        [Required(ErrorMessage = "Data de Nascimento obrigatória")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date,ErrorMessage = "Insira uma data válida no formato dd/mm/yyyy")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtNascimento { get; set; }

        [Required(ErrorMessage = "Nome do Usuário obrigatório")]
        [Display(Name = "Nome Usuário")]
        public string NmUsuario { get; set; }


        [Required(ErrorMessage = "Ativo obrigatório")]
        [Display(Name = "CdAtivo")]
        public bool CdAtivo { get; set; }

        [Required(ErrorMessage = "CdIsAdmin obrigatório")]
        [Display(Name = "CdIsAdmin")]
        public bool CdIsAdmin { get; set; }
    }
}