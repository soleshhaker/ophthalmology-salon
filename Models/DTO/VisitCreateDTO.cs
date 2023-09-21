using Ophthalmology.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enums;

namespace Models.DTO
{
    public class VisitCreateDTO
    {
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public VisitType VisitType { get; set; }
    }
}
