using Microsoft.AspNetCore.Mvc.Rendering;
using MovieWatcher.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Models
{
    public class HomeRatingViewModel
    {
        public string Titel { get; set; }
        public byte[] Foto { get; set; }
        public string Beschrijving { get; set; }
        public Rating Rating { get; set; }


        public GezienStatus Gezien { get; set; }
        public List<SelectListItem> Ratings { get; set; }
        public IEnumerable<GezienStatus> gezienStatuses { get; set; }
        
    }
}
