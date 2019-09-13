using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieWatcher.Domain
{
    public class UserFilmGezienStatus
    {
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int GezienStatusId { get; set; }
        public GezienStatus GezienStatus { get; set; }

    }
}
