using System.ComponentModel.DataAnnotations;


namespace IDS.Models.ViewModels
{
    public class RoleVm
    {

        public string? Id { get; set; }
        [Display (Name= "الدور" )]
        public string Name { get; set; }
    }
}
