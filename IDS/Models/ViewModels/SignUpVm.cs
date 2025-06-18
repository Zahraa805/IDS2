using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;


namespace IDS.Models.ViewModels
{
    public class SignUpVm
    {

        [Required (ErrorMessage = "اسم المستخدم مطلوب") ]
         [StringLength(50, ErrorMessage = "اسم المستخدم طويل للغايه")]
        [Display(Name = "اسم المستخدم")]


        public string UserName { get; set; }
        [Required(ErrorMessage = "كلمة المرور مطلوبة")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "يجب أن تحتوي كلمة المرور على 6 أحرف على الأقل")]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "تأكيد كلمة المرور مطلوب")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "كلمه السر غير متطابقه")]
        [Display(Name = "تأكيد كلمة المرور")]
        public string ConfirmPassword { get; set; }




        [Required(ErrorMessage = "الرجاء إدخال الإسم ")]
        [MinLength(9 , ErrorMessage = "الرجاء إدخال الإسم ثلاثي")]
        [Display(Name = "الإسم ثلاثي")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "الرجاء إدخال الرقم القومي")]
        [Display(Name = "الرقم القومي")]
       public string NationalId { get; set; }



        [Required   (ErrorMessage = "الرجاء إدخال الإيميل")]
       [StringLength(50, ErrorMessage = "الايميل غير صالح")]
        [Display(Name = "E-mail : الايميل")]
        public string Email { get; set; }

        [Required   (ErrorMessage ="الرجاء إدخال رقم الهاتف")]
        [StringLength(15, ErrorMessage = "رقم الهاتف يجب أن يكون أقل من 15 رقم.")]
        [RegularExpression("^0\\d{9,14}$", ErrorMessage = "رقم الهاتف يجب أن يبدأ بصفر ويحتوي على أرقام فقط.")]
        [Display(Name = "رقم الهاتف")]
        public string PhoneNumber { get; set; }

        //[Required(ErrorMessage = "الرجاء الدخال اسم الكلية الخاصه بالموظف")]
        //[Display(Name = "القسم")]
        //public String Department { get; set; }


      //  [StringLength(80, ErrorMessage = "العنوان طويل للغايه")]
        [Display(Name = "العنوان")]
        public string? Address { get; set; }

        [Required (ErrorMessage = "الرجاء الدخال دور الموظف في السيستم")]
        [Display(Name = "الدور")]
        public string Role { get; set; }






    }
}
