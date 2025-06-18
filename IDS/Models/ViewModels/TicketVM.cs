using IDS.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace IDS.Models
{
    public class TicketVM
    {

        /// <summary>
        /// ////////////////////////// Patient Properties
        /// </summary>
        /// 


        [Required(ErrorMessage = "يجب ادخال الرقم القومي")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومي يجب ان يكون 14 رقم")]
        [RegularExpression("^\\d+$", ErrorMessage = "الرقم اقومي يجب ان يحتوي علي ارقام فقط")]
        public string PatientId { get; set; }


        [Required(ErrorMessage = "يجب ادخال الاسم")]
        [StringLength(50, ErrorMessage = "الاسم يجب ان يكون اقل من 50 حرف")]
        [Display(Name = " الاسم رباعي")]
        public string Name { get; set; }

        [Required(ErrorMessage = "العنوان مطلوب.")]
        [StringLength(255, ErrorMessage = "يجب ألا يتجاوز العنوان 255 حرفًا")]
      //  [MinLength(5, ErrorMessage = "يجب أن يكون العنوان على الأقل 5 أحرف")]
       // [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = " .العنوان يحتوي على رموز غير مسموحة")]
        public string? Address { get; set; } = "N/A";


        [StringLength(100, ErrorMessage = " .يجب ألا يتجاوز عدد الاحرف 100 حرفًا")]
       // [MinLength(3, ErrorMessage = " .يجب أن تكون المهنة على الأقل 3 أحرف")]
        [Required(AllowEmptyStrings = true, ErrorMessage = "يرجى إدخال  المهنه")]
        //  [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "المهنة تحتوي على رموز غير مسموحة")]
        public string profession { get; set; } = "N/A";

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(11, ErrorMessage = ".رقم الهاتف يجب أن يكون 11 رقم")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقمًا ويبدأ بصفر")]
        [Display(Name = "رقم الهاتف")]
      public string phoneNumber { get; set; }



        [Required(ErrorMessage = "الرجاء إختيار جنس المريض.")]
        [RegularExpression("^(Male|Female)$", ErrorMessage = "يجب أن يكون الجنس ذكر أو انثي فقط")]
        [Display(Name = "النوع")]
        public string Gender { get; set; }



        [Required(ErrorMessage = "الرجاء إختيار سن المريض.")]
        [Display(Name = "السن")]
        public int Age { get; set; }



        /// <summary>
        /// ////////////////////////// Ticket Properties
        /// </summary>



        //   public IEnumerable<Ticket> Tickets { get; set; }

        //[Required(ErrorMessage = "رقم التذكره مطلوب.")]
        //[RegularExpression(@"^\d{6}$", ErrorMessage = "يجب أن يتكون رقم التذكره من 6 أرقام فقط.")]
        //public string TicketId { get; set; }
        public string? TicketID { get; set; }


        [Required(ErrorMessage = "تاريخ الكشف مطلوب.")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "تاريخ الكشف يجب أن يكون اليوم أو في المستقبل")]
        [Display(Name = "تاريخ الكشف.")]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;

        [Display(Name = "شكوي المريض.")]
        [StringLength(1000, ErrorMessage = " .يجب ألا يتجاوز عدد الاحرف 1000 حرفًا")]
        //[MinLength(3, ErrorMessage = " .يجب أن تكون شكوي المريض على الأقل 3 أحرف")]
        [Required(ErrorMessage = "يرجى إدخال  شكوي المريض")]
        //[BindProperty]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        public string ChiefComlant { get; set; } = "N/A";

        [Display(Name = "التشخيص المبدئي.")]
        [StringLength(1000, ErrorMessage = " .يجب ألا يتجاوز عدد الاحرف 1000 حرفًا")]
      //  [MinLength(3, ErrorMessage = " .يجب أن يكون التشخيص المبدئي على الأقل 3 أحرف")]
        [Required(ErrorMessage = "يرجى إدخال التشخيص المبدئي")]
        //[BindProperty]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string PrevisionalDiagnosis { get; set; } = "N/A";


        [Display(Name = "تاريخ الكشف القادم")]
        [DataType(DataType.DateTime)]

        public DateTime? NextDate { get; set; }

        [Display(Name = "يتوجه إالي.")]
        public string Status { get; set; } = "Reception";

        [Display(Name = " تذكره صالحه ؟.")]

        public bool IsValid { get; set; } = true;


        /// <summary>
        /// ////////////////////////// Medical History Properties
        /// </summary>

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
        [Required(ErrorMessage = "يرجى إدخال  التاريخ المرضي اذا اردت")]
        //[BindProperty]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [DataType(DataType.MultilineText)]
        public string MedicalHistoryText { get; set; } = "N/A";




        /// <summary>
        /// ////////////////////////// ReferredTo  Properties
        /// </summary>
        /// 
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



    }
}