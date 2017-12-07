using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCWebApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="El nombre de usuario es obligatorio")]
        [StringLength(4,ErrorMessage ="La longitud maxima es de 4 caracteres")]
        public string User { get; set; }
    }
}