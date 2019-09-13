using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class HomeListViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string Titel { get; set; }
        public byte[] Foto { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Beschrijving { get; set; }
        public string Gezien { get; set; }
        public List<SelectListItem> GezienStatussen { get; set; }
    }
}
