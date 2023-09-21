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
    public class VisitReadDTO
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }
        public VisitType VisitType { get; set; }
        public VisitStatus VisitStatus { get; set; }
        public float Cost { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
