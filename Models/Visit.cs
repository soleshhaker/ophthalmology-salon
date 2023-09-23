using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Utility.Enums;

namespace Ophthalmology.Models
{
    public class Visit
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public DateTime End { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [Required]
        public VisitType VisitType { get; set; }
        [Required]
        public VisitStatus VisitStatus { get; set; }
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Cost must be greater than or equal to 0")]
        public float Cost { get; set; }
        public string? AdditionalInfo { get; set; }
    }
}
