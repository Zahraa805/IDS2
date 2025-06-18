using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDS.Models
{
    public class ReferredTo
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Ticket"), Required]
        //public string TicketID { get; set; }

        [Display(Name = "Oral med. & Perio")]
        public bool Oral { get; set; } = false;

        [Display(Name = "Removable prosth")]
        public bool RemovableProsth { get; set; } = false;

        [Display(Name = "Operative")]
        public bool Operative { get; set; } = false;

        [Display(Name = "Endodontic")]
        public bool Endodontic { get; set; } = false;

        [Display(Name = "Ortho")]
        public bool Ortho { get; set; } = false;

        [Display(Name = " Crown & Bridge")]
        public bool CrownAndBridge { get; set; } = false;

        [Display(Name = "Surgery")]
        public bool Surgery { get; set; } = false;

        [Display(Name = "Pedo")]
        public bool Pedo { get; set; } = false;

        [Display(Name = "X-ray")]
        public bool XRay { get; set; } = false;

        //Navigation Properties
        public Ticket Ticket { get; set; }
    }
}
