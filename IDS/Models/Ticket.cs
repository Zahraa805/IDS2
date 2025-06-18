using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IDS.Models
{
    public class Ticket
    {
        private readonly AppDbContext _context;

        public Ticket(AppDbContext context)
        {
            bool ValidId = true;
            _context = context;

            do
            {
                TicketId = new Random().Next(1, 10_00_001).ToString();
                if (_context.Tickets.Select(t => t.TicketId).Contains(TicketId))
                {
                    ValidId = !ValidId;
                }
            }
            while (!ValidId);
            
                   
        }


        [Key]
        [Required(ErrorMessage = "رقم التذكره مطلوب.")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "يجب أن يتكون رقم التذكره من 6 أرقام فقط.")]
        public string TicketId { get; set; }

        [ForeignKey("Patient"), Required]
        public string PatientId { get; set; }

        [ForeignKey("MedicalHistory")]
        public int MedicalHistoryId { get; set; }

        [ForeignKey("ReferredTo")]
        public int ReferredToId { get; set; }





        [Required(ErrorMessage = "تاريخ الكشف مطلوب.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "تاريخ الكشف يجب أن يكون اليوم أو في المستقبل")]
        [Display(Name = "تاريخ الكشف.")]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        [Display(Name = "شكوي المريض.")]
        [StringLength(1000, ErrorMessage = " .يجب ألا يتجاوز عدد الاحرف 1000 حرفًا")]
        [MinLength(3, ErrorMessage = " .يجب أن تكون شكوي المريض على الأقل 3 أحرف")]
        public string ChiefComlant { get; set; } = "N/A";

        [Display(Name = "التشخيص المبدئي.")]
        [StringLength(1000, ErrorMessage = " .يجب ألا يتجاوز عدد الاحرف 1000 حرفًا")]
        [MinLength(3, ErrorMessage = " .يجب أن يكون التشخيص المبدئي على الأقل 3 أحرف")]
        public string PrevisionalDiagnosis { get; set; } = "N/A";


        [Display(Name = "تاريخ الزيارة القادمه.")]
        [FutureDate(ErrorMessage = "تاريخ الزيارة القادمه يجب أن يكون اليوم أو في المستقبل")]

        //Second version in the model
        public DateTime? NextDate { get; set; }
        public string Status { get; set; } = "Reception"; // part of third changes
        public bool IsValid { get; set; } = true;

        public string? LevelOfCompletness { get; set; }  // part of third changes


        //Navigation properties
        public Patient Patient { get; set; }
        public MedicalHistory MedicalHistory { get; set; }
        public ReferredTo ReferredTo { get; set; }
        public TicketAccountancy Accountancy { get; set; }  // part of third changes




    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date < DateTime.Today)
                {
                    return new ValidationResult("تاريخ الكشف يجب أن يكون اليوم أو في المستقبل.");
                }
            }
            return ValidationResult.Success;
        }
    }


}

