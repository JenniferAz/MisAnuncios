using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MisAnuncios.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
    }

}