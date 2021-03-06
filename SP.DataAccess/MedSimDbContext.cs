namespace SP.DataAccess
{
    using Data.Interfaces;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Newtonsoft.Json;
    using NLog;
    using SP.Metadata;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Threading.Tasks;

    public partial class MedSimDbContext : IdentityDbContext<Participant, AspNetRole,
        Guid, AspNetUserLogin,AspNetUserRole, AspNetUserClaim>
    {
        public MedSimDbContext()
            : base("DefaultConnection")
        {
        }

        static MedSimDbContext()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MedSimDbContext, Configuration>());
        }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<CandidatePrereading> CandidatePrereadings { get; set; }
        public virtual DbSet<Culture> Cultures { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseActivity> CourseActivities { get; set; }
        public virtual DbSet<CourseDay> CourseDays { get; set; }
        public virtual DbSet<CourseFacultyInvite> CourseFacultyInvites { get; set; }
        public virtual DbSet<CourseFormat> CourseFormats { get; set; }
        public virtual DbSet<CourseHangfireJob> CourseHangfireJobs { get; set; }
        public virtual DbSet<CourseParticipant> CourseParticipants { get; set; } 
        public virtual DbSet<CourseScenarioFacultyRole> CourseScenarioFacultyRoles { get; set; }
        public virtual DbSet<CourseSlot> CourseSlots { get; set; }
        public virtual DbSet<CourseSlotActivity> CourseSlotActivities { get; set; }
        public virtual DbSet<CourseSlotManikin> CourseSlotManikins { get; set; }
        public virtual DbSet<CourseSlotPresenter> CourseSlotPresenters { get; set; }
        public virtual DbSet<CourseType> CourseTypes { get; set; }
        public virtual DbSet<CourseTypeDepartment> CourseTypeDepartments { get; set; }
        public virtual DbSet<CourseTypeScenarioRole> CourseTypeScenarioRoles { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<FacultyScenarioRole> FacultyScenarioRoles { get; set; }
        public virtual DbSet<HotDrink> HotDrinks { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<Manikin> Manikins { get; set; }
        public virtual DbSet<ManikinManufacturer> ManikinManufacturers { get; set; }
        public virtual DbSet<ManikinModel> ManikinModels { get; set; }
        public virtual DbSet<ManikinService> ManikinServices { get; set; }
        public virtual DbSet<ProfessionalRole> ProfessionalRoles { get; set; }
        public virtual DbSet<ProfessionalRoleInstitution> ProfessionalRoleInstitutions { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Scenario> Scenarios { get; set; }
        public virtual DbSet<ScenarioResource> ScenarioResources { get; set; }
        public virtual DbSet<AspNetUserRole> UserRoles { get; set; }

        /*
#region overrides //overriding to allow access without referencing aspnet.identity.entityframework assembly

public override IDbSet<Participant> Users
{
    get
    {
        return base.Users;
    }

    set
    {
        base.Users = value;
    }
}
#endregion //ovrerides
    */


        public static MedSimDbContext Create()
        {
            return new MedSimDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configuration.LazyLoadingEnabled = false;
            //Configuration.ProxyCreationEnabled = false;

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Add(new FixedLengthAttributeConvention());

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.CourseSlotActivities)
                .WithOptional(e => e.Activity)
                .HasForeignKey(e => e.ActivityId);

            modelBuilder.Entity<Culture>()
                .HasMany(e => e.Institutions)
                .WithRequired(e => e.Culture)
                .HasForeignKey(e => e.LocaleCode)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseSlotActivities)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseSlotManikins)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseSlotPresenters)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.CourseDays)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.HangfireJobs)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.FacultyInvites)
                .WithRequired(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseActivity>()
                .HasMany(e => e.CourseSlots)
                .WithOptional(e => e.Activity)
                .HasForeignKey(e=>e.ActivityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseActivity>()
                .HasMany(e => e.ActivityChoices)
                .WithRequired(e => e.CourseActivity)
                .HasForeignKey(e => e.CourseActivityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseFormat>()
                .HasMany(e => e.CourseSlots)
                .WithRequired(e => e.CourseFormat)
                .HasForeignKey(e => e.CourseFormatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseFormat>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.CourseFormat)
                .HasForeignKey(e=>e.CourseFormatId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlot>()
                .HasMany(e => e.CourseSlotPresenters)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlot>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlot>()
                .HasMany(e => e.CourseSlotActivities)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseSlot>()
                .HasMany(e => e.CourseSlotManikins)
                .WithRequired(e => e.CourseSlot)
                .HasForeignKey(e => e.CourseSlotId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseType>()
                .HasMany(e => e.CandidatePrereadings)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId);

            modelBuilder.Entity<CourseType>()
                .HasMany(e => e.CourseActivities)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId);

            modelBuilder.Entity<CourseType>()
                .HasMany(e => e.CourseFormats)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseType>()
                .HasMany(e => e.CourseTypeDepartments)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId);

            modelBuilder.Entity<CourseType>()
                .HasMany(e => e.CourseTypeScenarioRoles)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e=>e.CourseTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseType>()
                .HasOptional(e => e.InstructorCourse)
                .WithMany()
                .HasForeignKey(e => e.InstructorCourseId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CourseType>()
                .HasMany(e => e.Scenarios)
                .WithRequired(e => e.CourseType)
                .HasForeignKey(e => e.CourseTypeId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.CourseTypeDepartments)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.Department)
                .HasForeignKey(e=>e.DefaultDepartmentId);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.OutreachCourses)
                .WithOptional(e => e.OutreachingDepartment)
                .HasForeignKey(e => e.OutreachingDepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Scenarios)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Manikins)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacultyScenarioRole>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.FacultyScenarioRole)
                .HasForeignKey(e => e.FacultyScenarioRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FacultyScenarioRole>()
                .HasMany(e => e.CourseTypeScenarioRoles)
                .WithRequired(e => e.FacultyScenarioRole)
                .HasForeignKey(e => e.FacultyScenarioRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HotDrink>()
                .HasMany(e => e.Participants)
                .WithOptional(e => e.DrinkPreference)
                .HasForeignKey(e => e.DrinkPreferenceId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Institution>()
                .Property(e => e.LocaleCode)
                .IsFixedLength();

            modelBuilder.Entity<Institution>()
                .HasMany(e => e.Departments)
                .WithRequired(e => e.Institution)
                .HasForeignKey(e=>e.InstitutionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Institution>()
                .HasMany(e => e.ProfessionalRoleInstitutions)
                .WithRequired(e => e.Institution)
                .HasForeignKey(e => e.InstitutionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manikin>()
                .HasMany(e => e.ManikinServices)
                .WithRequired(e => e.Manikin)
                .HasForeignKey(e => e.ManikinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Manikin>()
                .HasMany(e => e.CourseSlotManikins)
                .WithRequired(e => e.Manikin)
                .HasForeignKey(e => e.ManikinId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManikinManufacturer>()
                .HasMany(e => e.ManikinModels)
                .WithRequired(e => e.Manufacturer)
                .HasForeignKey(e => e.ManufacturerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ManikinModel>()
                .HasMany(e => e.Manikins)
                .WithRequired(e => e.Model)
                .HasForeignKey(e => e.ModelId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participant>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.Participant)
                .HasForeignKey(e => e.ParticipantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participant>()
                .HasMany(e => e.CourseScenarioFacultyRoles)
                .WithRequired(e => e.Participant)
                .HasForeignKey(e => e.ParticipantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participant>()
                .HasMany(e => e.CourseSlotPresentations)
                .WithRequired(e => e.Participant)
                .HasForeignKey(e => e.ParticipantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participant>()
                .HasMany(e => e.CourseInvites)
                .WithRequired(e => e.Faculty)
                .HasForeignKey(e => e.ParticipantId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfessionalRole>()
                .HasMany(e => e.Participants)
                .WithRequired(e => e.ProfessionalRole)
                .HasForeignKey(e => e.DefaultProfessionalRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfessionalRole>()
                .HasMany(e => e.CourseParticipants)
                .WithRequired(e => e.ProfessionalRole)
                .HasForeignKey(e => e.ProfessionalRoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProfessionalRole>()
                .HasMany(e => e.ProfessionalRoleInstitutions)
                .WithRequired(e => e.ProfessionalRole)
                .HasForeignKey(e => e.ProfessionalRoleId);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.Courses)
                .WithRequired(e => e.Room)
                .HasForeignKey(e => e.RoomId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.FacultyMeetings)
                .WithOptional(e => e.FacultyMeetingRoom)
                .HasForeignKey(e => e.FacultyMeetingRoomId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Scenario>()
                .HasMany(e => e.CourseSlotScenarios)
                .WithOptional(e => e.Scenario)
                .HasForeignKey(e=>e.ScenarioId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        /*
        private static void buildAuth(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Roles)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Claims)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Logins)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetRole>()
                .HasKey(e=>e.Id)
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUserClaim>()
                .HasKey(e => e.Id);

            //suspect chances of the oauth provider keys overlapping is pretty remote
            modelBuilder.Entity<AspNetUserLogin>()
                .HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId});

            modelBuilder.Entity<AspNetUserRole>()
                .HasKey(e => new { e.RoleId, e.UserId });

        }


        private SanitizeStringProperties _sanitizeHtml { get; set; }
        private SanitizeStringProperties SanitizeHtml
        {
            get { return _sanitizeHtml ?? (_sanitizeHtml = new SanitizeStringProperties()); }
        }
        */

        public override Task<int> SaveChangesAsync()
        {
            BeforeSave();

            try
            {
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                var de = new DetailedEntityValidationException(e);
                throw de;
            }
        }

        public override int SaveChanges()
        {
            //not sanitizing for the time being - at that point we will need a wysywig editor
            //SanitizeHtml.ForEntities(ChangeTracker);
            BeforeSave();

            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var de = new DetailedEntityValidationException(e);
                throw de;
            }

        }
        private void BeforeSave()
        {
            var entries = ChangeTracker.Entries();
            log(entries);
            SetTimeTracking(entries);
        }
        private static ILogger _logger = LogManager.GetCurrentClassLogger();
        private void log(IEnumerable<DbEntityEntry> entries)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            _logger.Info(()=> "save operation for:" + 
                string.Join("\r\n",
                    (from e in entries
                    where e.State != EntityState.Unchanged
                    select $"\r\n{e.State} entity type({e.Entity.GetType().Name}):\r\n{JsonConvert.SerializeObject(e.Entity, Formatting.Indented, settings)}")));
        }
        private void SetTimeTracking(IEnumerable<DbEntityEntry> entries)
        {
            var nowUtc = DateTime.UtcNow;
            /*
            foreach (var ent in ChangeTracker.Entries())
            {
                System.Diagnostics.Debug.WriteLine(ent);
            }
            */
            foreach (var ent in entries)
            {
                var t = ent.Entity.GetType();
                if (t.GetInterface(nameof(IModified)) != null)
                {
                    var im = (IModified)ent.Entity;
                    im.Modified = nowUtc;
                }
                else if (t == typeof(Course))//not IModified
                {
                    var c = (Course)ent.Entity;
                    if (ent.State == EntityState.Added)
                    {
                        c.CourseDatesLastModified = c.FacultyMeetingDatesLastModified = nowUtc;
                    }
                    else if (ent.State == EntityState.Modified)
                    {
                        if (ent.Property(nameof(c.StartFacultyUtc)).IsModified)
                        {
                            c.CourseDatesLastModified = nowUtc;
                        }
                        else
                        {
                            ent.Property(nameof(c.CourseDatesLastModified)).IsModified = false;
                        }
                        if (ent.Property(nameof(c.FacultyMeetingUtc)).IsModified)
                        {
                            c.FacultyMeetingDatesLastModified = nowUtc;
                        }
                        else
                        {
                            ent.Property(nameof(c.FacultyMeetingDatesLastModified)).IsModified = false;
                        }
                        c.Version++;
                    }
                    /*
                    else if (ent.State == EntityState.Modified && t.GetInterface(nameof(IAdminApproved))!=null)
                    {
                        var aa = (IAdminApproved)ent.Entity;
                        if (!aa.AdminApproved)
                        {
                            ent.Property(nameof(aa.AdminApproved)).IsModified = false;
                        }
                    }
                    */
                }
                if (t.GetInterface(nameof(Data.Interfaces.ICreated)) != null)
                {
                    var ic = (Data.Interfaces.ICreated)ent.Entity;
                    if (ent.State == EntityState.Added)
                    {
                        ic.CreatedUtc = nowUtc;
                    }
                    else if (ent.State == EntityState.Modified)
                    {
                        ent.Property(nameof(ic.CreatedUtc)).IsModified = false;
                    }
                }
            }
        }
    }

    [Serializable]
    public class DetailedEntityValidationException : DbEntityValidationException
    {
        public DetailedEntityValidationException(DbEntityValidationException ve)
            : base(string.Join(new string('-',10) + "\r\n", ve.EntityValidationErrors.Select(ev=>string.Join("\r\n",ev.ValidationErrors.Select(e=>e.ErrorMessage)))), ve.EntityValidationErrors)
        {
        }
    }
}
