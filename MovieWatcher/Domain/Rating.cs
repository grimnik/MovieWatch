using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Domain
{
    public class Rating
    {
       
        public int FilmId { get; set; }
        public Film Film { get; set; }
        
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        [Display(Name = "Rating")]
        [Range(0,10,ErrorMessage ="Kies een waarde tussen 0 en 10")]
        public int RatingPoints { get; set; }
        [Required]
        [MaxLength(500)]
        [DataType(DataType.MultilineText)]
        public string Review { get; set; }
        
    }
}
