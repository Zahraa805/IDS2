using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IDS.Models
{

    public class Patient
    {

        [Key]
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
        [MinLength(5, ErrorMessage = "يجب أن يكون العنوان على الأقل 5 أحرف")]
      //  [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = " .العنوان يحتوي على رموز غير مسموحة")]
        public string? Address { get; set; } = "N/A";


        [Required(ErrorMessage = "المهنة مطلوبة.")]
        [StringLength(100, ErrorMessage = " .يجب ألا يتجاوز عدد الاحرف 100 حرفًا")]
        [MinLength(3, ErrorMessage = " .يجب أن تكون المهنة على الأقل 3 أحرف")]
      //  [RegularExpression(@"^[a-zA-Z0-9\s,.-]+$", ErrorMessage = "المهنة تحتوي على رموز غير مسموحة")]
        public string profession { get; set; } = "N/A";

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [StringLength(11, ErrorMessage = ".رقم الهاتف يجب أن يكون 11 رقم")]
        [RegularExpression("^0\\d{10}$", ErrorMessage = "رقم الهاتف يجب أن يكون 11 رقمًا ويبدأ بصفر")]
        [Display(Name = "رقم الهاتف")]
        public string phoneNumber { get; set; }

        [Required(ErrorMessage = "الرجاء إختيار جنس المريض.")]
        //[RegularExpression("^(ذكر|انثي)$", ErrorMessage = "يجب أن يكون الجنس ذكر أو انثي فقط")]
        [Display(Name = "النوع")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "الرجاء إختيار سن المريض.")]
        [Display(Name = "السن")]
        public int Age { get; set; }


        public IEnumerable<Ticket>? Tickets { get; set; }
    }


}