using Microsoft.AspNetCore.Identity;

namespace RetrofitLesson.WEB.Data.Identity
{
    public class AppRole : IdentityRole<long>
    {
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
    }
}
