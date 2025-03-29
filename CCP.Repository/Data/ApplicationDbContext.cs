using CCP.Repositori.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCP.Repositori.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Child> Children { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<HealthMetric> HealthMetrics { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<NutritionalIntake> NutritionalIntakes { get; set; }
        public DbSet<SleepPattern> SleepPatterns { get; set; }
        public DbSet<PhysicalActivity> PhysicalActivities { get; set; }
        public DbSet<ExpertAvailability> ExpertAvailabilities { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Refund> Refunds { get; set; }
        public DbSet<RefundPolicySetting> RefundPolicySettings { get; set; }


    }
}
