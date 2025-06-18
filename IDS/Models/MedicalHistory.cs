
using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;

namespace IDS.Models
{
    public class MedicalHistory
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Ticket"), Required]
        //public string TicketID { get; set; }

        [Display(Name = "Heart Trouble")]
        public bool HeartTrouble { get; set; } = false;

        [Display(Name = "Hypertention")]
        public bool Hyperttention { get; set; } = false;

        [Display(Name = "Pregnancy")]
        public bool Pregnancy { get; set; } = false;

        [Display(Name = "Diabetes")]
        public bool Diabetes { get; set; } = false;

        [Display(Name = "Hepatitis")]
        public string Hepatitis { get; set; } = "A"; // back Again

        [Display(Name = "AIDs")]
        public bool AIDs { get; set; } = false;


        [Display(Name = "Tuberculosis")]
        public bool Tuberculosis { get; set; } = false;

        [Display(Name = "Allergies")]
        public bool Allergies { get; set; } = false;

        [Display(Name = "Anemia")]
        public bool Anemia { get; set; } = false;

        [Display(Name = "Rheu.Arthritis")] //rheumatism
        public bool Rheumatism { get; set; } = false;

        [Display(Name = "Rad.Therapy")]
        public bool RadTherapy { get; set; } = false;

        [Display(Name = "Haemophilia")]
        public bool Haemophilia { get; set; } = false;

        [Display(Name = "Aspirin Intake")]
        public bool AspirinIntake { get; set; } = false;

        [Display(Name = "Kidney Troubles")]
        public bool KidneyTroubles { get; set; } = false;

        [Display(Name = "Asthma")]
        public bool Asthma { get; set; } = false;

        [Display(Name = "Hay Fever")]
        public bool HayFever { get; set; } = false;

        [Display(Name = "Medical History & Present Medication")]
        [DataType(DataType.MultilineText)]
        public string MedicalHistoryText { get; set; } = "N/A";




        //Navigation Properties
        public Ticket Ticket { get; set; }

    }
}
