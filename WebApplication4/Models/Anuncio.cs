using MisAnuncios.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MisAnuncios.Models
{
    public class Anuncio
    {
        public int ID { get; set; }
       
        [Display(Name = "Email")]
        public ApplicationUser Usuario { get; set; }
        public string Ciudad { get; set; }

        [Required]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Required]
        
        public string Contenido { get; set; }


        [Display(Name = "Categoría")]
        public Categoria Cat { get; set; }
       
    }
}