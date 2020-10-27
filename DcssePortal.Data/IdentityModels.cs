using DcssePortal.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;


namespace DcssePortal.Data
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public System.Data.Entity.DbSet<DcssePortal.Model.Student> Students { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Complaints> Complaints { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Content> Contents { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Course> Courses { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.CoursesScheme> CoursesSchemes { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.DateSheet> DateSheets { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Noticeboard> Noticeboards { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Enrollment> Enrollments { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Faculty> Faculties { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Feedback> Feedbacks { get; set; }

        public System.Data.Entity.DbSet<DcssePortal.Model.Result> Results { get; set; }
    }
}