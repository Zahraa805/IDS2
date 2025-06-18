using System.ComponentModel.DataAnnotations;

namespace IDS.Models
{
    public class RoleVm
    {

        public string? Id { get; set; }
        [Display (Name= "الدور" )]
        public string Name { get; set; }
    }
}
