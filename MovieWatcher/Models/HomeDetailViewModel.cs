using MovieWatcher.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class HomeDetailViewModel
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
        public string UserName { get; set; }
        public ICollection<UserFilmGezienStatus> UserFilmGezienStatuses { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public int AantalGezien { get; set; }
        public int AantalWil { get; set; }
    }
}
