using System;
using DAL.Entities;
using System.Data.Entity;
using DAL.Identity.Entities;
using DAL.Identity.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DAL.EF
{
   

    public class WorkPlaceContext : DbContext
    {

        public WorkPlaceContext(string connectionString)
            : base("name=WorkPlaceContext")
        {
        }
        static WorkPlaceContext()
        {
            Database.SetInitializer((new DbInitializaer()));
        }

        public virtual DbSet<Vacancy> Vacancies { get; set; }
        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<Resume> Resumes { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Heading> Headings { get; set; }
        public virtual DbSet<User> UsersProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vacancy>()
                .HasMany(e => e.Offers)
                .WithRequired(e => e.Vacancy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resume>()
                .Property(e => e.Payment)
                .IsFixedLength();

            modelBuilder.Entity<Resume>()
                .HasMany(e => e.Offers)
                .WithRequired(e => e.Resume)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Resume>()
                .HasMany(e => e.Experiences)
                .WithRequired(e => e.Resume)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Heading>()
                .HasMany(e => e.Vacancy)
                .WithRequired(e => e.Heading)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Heading>()
                .HasMany(e => e.Resumes)
                .WithRequired(e => e.Heading)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Vacancies)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Resumes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

        }
    }

    public class DbInitializaer : DropCreateDatabaseIfModelChanges<WorkPlaceContext>
    {
        protected override void Seed(WorkPlaceContext context)
        {
            base.Seed(context);

            #region Roles

            ApplicationRole rolAdmin = new ApplicationRole { Name = "admin" };
            ApplicationRole role1 = new ApplicationRole { Name = "employer" };
            ApplicationRole role2 = new ApplicationRole { Name = "worker" };

            ApplicationRoleManager roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            roleManager.Create(rolAdmin);
            roleManager.Create(role1);
            roleManager.Create(role2);
            #endregion

            #region Users

            var userAdmin = new ApplicationUser { Email = "admin@gmail.com", UserName = "admin@gmail.com" };
            var userEmployer = new ApplicationUser { Email = "employer@gmail.com", UserName = "employer@gmail.com" };
            var userWorker = new ApplicationUser { Email = "worker@gmail.com", UserName = "worker@gmal.com" };
            var userWorker2 = new ApplicationUser { Email = "worker2@gmail.com", UserName = "worker2@gmal.com" };
            var userWorker3 = new ApplicationUser { Email = "worker3@gmail.com", UserName = "worker3@gmal.com" };

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            userManager.Create(userAdmin, "adminpassword");
            userManager.Create(userEmployer, "employerpassword");
            userManager.Create(userWorker, "workerpassword");
            userManager.Create(userWorker2, "workerpassword");
            userManager.Create(userWorker3, "workerpassword");

            userManager.AddToRole(userAdmin.Id, "admin");
            userManager.AddToRole(userEmployer.Id, "employer");
            userManager.AddToRole(userWorker.Id, "worker");
            userManager.AddToRole(userWorker2.Id, "worker");
            userManager.AddToRole(userWorker3.Id, "worker");

            var profileAdmin = new User { Id = userAdmin.Id, Surname = "AdminSurname", Name = "AdminName" };
            var profileEmployer = new User { Id = userEmployer.Id, Surname = "EmployerSurname", Name = "EmployerName" };
            var profileWorker = new User { Id = userWorker.Id, Surname = "WorkerSurname", Name = "WorkerName" };
            var profileWorker2 = new User { Id = userWorker2.Id, Surname = "Worker2Surname", Name = "Worker2Name" };
            var profileWorker3 = new User { Id = userWorker3.Id, Surname = "Worker3Surname", Name = "Worker3Name" };

            context.UsersProfiles.Add(profileAdmin);
            context.UsersProfiles.Add(profileEmployer);
            context.UsersProfiles.Add(profileWorker);
            context.UsersProfiles.Add(profileWorker2);
            context.UsersProfiles.Add(profileWorker3);
            #endregion

            #region Rubrics

            var head1 = new Heading { Id = 1, Name = "IT" };
            var head2 = new Heading { Id = 2, Name = "HR специалисты - Бизнес-тренеры" };
            var head3 = new Heading { Id = 3, Name = "Автобизнес - Сервисное обслуживание" };
            var head4 = new Heading { Id = 4, Name = "Административный персонал - Водители - Курьеры" };
            var head5 = new Heading { Id = 5, Name = "Банки - Инвестиции - Лизинг" };

            context.Headings.Add(head1);
            context.Headings.Add(head2);
            context.Headings.Add(head3);
            context.Headings.Add(head4);
            context.Headings.Add(head5);
            #endregion

            #region Resumes

            var resume1 = new Resume
            {
                Id = 1,
                Title = "Junior QA Engineer",
                Name = "Алексей",
                Birthday = new DateTime(1990, 1, 10),
                Phone = "256-587-1546",
                Email = "KarenMDeberry@armyspy.com",
                Portfolio = "https://github.com/xiaoshi316",
                Payment = "400$",
                Skills = "Programming languages: Java, C++, Visual Basic.\nDevelopment environment: IntelliJ IDEA, UIPath, AutomationAnywhere\n\nFrameWork: Selenium\n\nVCS : Git",
                UserId = profileWorker.Id,
                HeadingId = head1.Id,
            };
            var resume2 = new Resume
            {
                Id = 2,
                Title = "Junior Software Engineer, Support Engineer",
                Name = "Ruslan",
                Birthday = new DateTime(1995, 5, 10),
                Phone = "205-324-5857",
                Email = "NikitaVagin@armyspy.com",
                Portfolio = "https://github.com/javlonsodikov",
                Payment = "800$",
                Skills = "За время работы на ЗМК \"Запорожсталь\" приобрел солидный опыт написания и оптимизации SQL-запросов СУБД Oracle, также есть много разработок клиент-серверного ПО на Delphi под СУБД Interbase (Fierbird/Yaffi - полная разработка БД с нуля с написанием хранимых процедур, функций, триггеров и т.п.), большой опыт использования c модифицированным мною кодом FastReport, есть опыт создания своих Delphi-компонент для IDE Delphi, также большой опыт в написании VB-скриптов в MS-Excel и MS-Word  с использованием ADO/ODBC (СУБД Oracle/IB). Опыт работы с приложением Toad. Также был админом SQL-серверов Yaffil/Firebird, других специализированных серверов предприятия.",
                UserId = profileWorker.Id,
                HeadingId = head2.Id,
            };
            var resume3 = new Resume
            {
                Id = 3,
                Title = "Программист С++, JS, PHP, Delphi, SQL",
                Name = "Вячеслав",
                Birthday = new DateTime(1990, 1, 10),
                Phone = "256-587-1546",
                Email = "KarenMDeberry@armyspy.com",
                Portfolio = "https://github.com/xiaoshi316",
                Payment = "400$",
                Skills = "Programming languages: Java, C++, Visual Basic.\nDevelopment environment: IntelliJ IDEA, UIPath, AutomationAnywhere\n\nFrameWork: Selenium\n\nVCS : Git",
                UserId = profileWorker2.Id,
                HeadingId = head3.Id,
            };

            context.Resumes.Add(resume1);
            context.Resumes.Add(resume2);
            context.Resumes.Add(resume3);
            #endregion

            #region ResumesExperience

            var experience1 = new Experience
            {
                Id = 1,
                Company = "Playtech",
                LastPosition = "Java/javascript developer",
                StartDate = new DateTime(2013, 10, 1),
                ResumeId = resume1.Id,
            };
            var experience2 = new Experience
            {
                Id = 2,
                Company = "IT company",
                LastPosition = "C# developer ",
                StartDate = new DateTime(2011, 03, 1),
                FinishDate = new DateTime(2013, 10, 1),
                ResumeId = resume1.Id,
            };
            var experience3 = new Experience
            {
                Id = 3,
                Company = "Private enterprise",
                LastPosition = "PHP programmer",
                StartDate = new DateTime(2007, 04, 1),
                FinishDate = new DateTime(2010, 06, 1),
                ResumeId = resume2.Id,
            };

            context.Experiences.Add(experience1);
            context.Experiences.Add(experience2);
            context.Experiences.Add(experience3);
            #endregion

            #region Careers

            var career1 = new Vacancy
            {
                Id = 1,
                Title = "QA Engineer",
                Company = "Codeminders",
                City = "Киев",
                ContName = "Codeminders",
                ContPhone = "207-698-2959",
                Desctiption = "Based in the US since 2004,Codeminders develops software products for high-tech companies located predominantly in the Silicon Valley of California. While we specialize in a broad range of applications, our primary focus is on modern technologies such as social networks, mobile applications, video conference systems, cloud computing, etc. We have a constantly growing team of top-level specialists with proven ability to professionally design and deliver a diverse spectrum of projects. Codemindersis not a typical outsourcing company. Our clients select us primarily because we master the most challenging and diverse projects delivering them successfully and on schedule.",
                HeadingId = head1.Id,
                UserId = profileEmployer.Id,
            };
            var career2 = new Vacancy
            {
                Id = 2,
                Title = "C++ Developper",
                Company = "CompanyName",
                City = "Черкассы",
                ContName = "ContactName",
                ContPhone = "207-698-2959",
                Desctiption = "Based in the US since 2004,Codeminders develops software products for high-tech companies located predominantly in the Silicon Valley of California. While we specialize in a broad range of applications, our primary focus is on modern technologies such as social networks, mobile applications, video conference systems, cloud computing, etc. We have a constantly growing team of top-level specialists with proven ability to professionally design and deliver a diverse spectrum of projects. Codemindersis not a typical outsourcing company. Our clients select us primarily because we master the most challenging and diverse projects delivering them successfully and on schedule.",
                HeadingId = head1.Id,
                UserId = profileEmployer.Id,
            };

            context.Vacancies.Add(career1);
            context.Vacancies.Add(career2);
            #endregion

            #region Offers

            var offer1 = new Offer
            {
                Id = 1,
                ResumeId = resume1.Id,
                VacancyId = career1.Id,
                DateSend = new DateTime(2018, 5, 15),
                Checked = false
            };
            var offer2 = new Offer
            {
                Id = 2,
                ResumeId = resume1.Id,
                VacancyId = career2.Id,
                DateSend = new DateTime(2018, 5, 10),
                Checked = false
            };

            context.Offers.Add(offer1);
            context.Offers.Add(offer2);
            #endregion

            context.SaveChanges();
        }
    }
}