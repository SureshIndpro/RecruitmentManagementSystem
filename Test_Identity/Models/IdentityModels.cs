using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Test_Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Field can't be empty")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Field can't be empty")]
        public string Lastname { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("Test_individual", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        //public System.Data.Entity.DbSet<Test_Identity.Models.ApplicationUser> ApplicationUsers { get; set; }
        public System.Data.Entity.DbSet<Test_Identity.Models.RoleViewModel> RoleViewModels { get; set; }

        public System.Data.Entity.DbSet<Test_Identity.Models.Skills> Skills { get; set; }

        public System.Data.Entity.DbSet<Test_Identity.Models.Job> Jobs { get; set; }
        public DbSet<CandModels> candidatesModels { get; set; }
        public DbSet<InterviewerModel> interviewerModels { get; set; }
        public DbSet<InterviewModels> roundInterviews { get; set; }
    }
}