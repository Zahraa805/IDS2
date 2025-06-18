using System.ComponentModel.DataAnnotations;

namespace IDS.Models.ViewModels
{
    public class PatientProfileVm
    {

    
        // PatientData
        public string PatientId { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; } = "-";
        public string phoneNumber { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }


        //Tickets


        public List<Ticket> Tickets { get; set; }

        //public string TicketId { get; set; }
        //public string RefferedTo { get; set; }

        //public DateTime AppointmentDate { get; set; }


    }
}
