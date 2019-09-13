using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class HomeCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Titel { get; set; }
        public IFormFile Foto { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Beschrijving { get; set; }
    }
}
