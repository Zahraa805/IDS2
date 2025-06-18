using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;


namespace IDS.Models
{


    public class TicketAccountancy
    {
        [Key]
        [ForeignKey(nameof(Ticket))] // This sets up the 1:1 relationship with Ticket
        public string TicketId { get; set; }

        [ForeignKey(nameof(ReceptionEmployee))]
        public string ReceptionEmpId { get; set; }

        [ForeignKey(nameof(DiagnosisDoc))]
        public string DiagnosisDocId { get; set; }

        //  [ForeignKey(nameof(ClinicDoc))]
        //  public string ClinicDocId { get; set; }

        // Navigation properties
        public ApplicationUser ReceptionEmployee { get; set; }
        public ApplicationUser DiagnosisDoc { get; set; }

        // public ApplicationUser ClinicDoc { get; set; }

        public Ticket Ticket { get; set; }
    }


}
