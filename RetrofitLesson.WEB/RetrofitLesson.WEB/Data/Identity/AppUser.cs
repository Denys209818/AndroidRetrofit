using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RetrofitLesson.WEB.Data.Identity
{
    public class AppUser : IdentityUser<long>
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; }
        [Required, StringLength(100)]
        public string SecondName { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [Required, StringLength(100)]
        public string Photo { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
