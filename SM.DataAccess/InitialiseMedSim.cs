﻿using SM.DataAccess.Enums;
using System;
using System.Data.Entity;

namespace SM.DataAccess
{
    class InitialiseMedSim : DropCreateDatabaseAlways<MedSimDbContext>// DropCreateDatabaseIfModelChanges<MedSimDbContext>
    {
        protected override void Seed(MedSimDbContext context)
        {
            var nz = new Country { Code = "NZ", Name = "New Zealand" };
            context.Countries.Add(nz);
            string[] locales = new string[] { "en-NZ", "en-AU", "en-GB" };
            for (byte i = 0; i < locales.Length; i++)
            {
                context.CountryLocaleCodes.Add(new CountryLocaleCode { Country = nz, LocaleCode = locales[i], Preference = i });
            }
            var starship = new Institution { Id = Guid.NewGuid(), Country = nz, Name="Starship" };
            context.Institutions.Add(starship);
            var ced = new Department { Id = Guid.NewGuid(), Institution = starship, Name = "CED" };
            context.Departments.Add(ced);
            var simProgram = new Department { Id = Guid.NewGuid(), Institution = starship, Name = "Simulation Programme" };
            context.Departments.Add(simProgram);

            var picu = new Department {Id = Guid.NewGuid(), Institution = starship, Name = "PICU" };
            context.Departments.Add(ced);

            var consultantRole = new ProfessionalRole { Id = Guid.NewGuid(), Category = ProfessionalCategory.Medical, Description = "Consulant" };
            context.ProfessionalRoles.Add(consultantRole);
            var nursingRole = new ProfessionalRole { Id = Guid.NewGuid(), Category = ProfessionalCategory.Medical, Description = "Clinical Charge Nurse" };
            context.ProfessionalRoles.Add(nursingRole);
            var techRole = new ProfessionalRole { Id = Guid.NewGuid(), Category = ProfessionalCategory.Medical, Description = "Simulation Technician" };
            context.ProfessionalRoles.Add(techRole);

            nz.ProfessionalRoles.Add(consultantRole);
            nz.ProfessionalRoles.Add(nursingRole);
            nz.ProfessionalRoles.Add(techRole);


            //todo add Mike,Denish,Becks & check trish phone no and 2nd email 

            var trish = new Participant { Id = Guid.NewGuid(), Email = "trishw@adhb.govt.nz", FullName = "Trish Wood", PhoneNumber = "999 9999 99", ProfessionalRole = nursingRole, Department=ced };
            context.Users.Add(trish);

            var brent = new Participant { Id = Guid.NewGuid(), Email = "brentm@adhb.govt.nz", AlternateEmail = "mcshagery@yahoo.com.au",FullName = "Brent McSharry", PhoneNumber = "999 9999 99", ProfessionalRole = consultantRole, Department=picu };
            context.Users.Add(brent);

            context.SaveChanges();
            var laerdal = new ManequinManufacturer { Id = Guid.NewGuid(), Name = "Laerdal" };
            context.ManequinManufacturers.Add(laerdal);
            var junior = new Manequin { Id = Guid.NewGuid(), Department = ced, Description = "Sim Junior", Manufacturer=laerdal };
            context.Manequins.Add(junior);
            var crm = new CourseType { Id = Guid.NewGuid(), Abbrev = "CRM", DaysDuration = 1, Description = "Crisis Resourse Managment", EmersionCategory = Emersion.Emersive, IsInstructorCourse = false };
            context.CourseTypes.Add(crm);
            crm.Departments.Add(ced);

            //NB alter so that resources belong to each department!!
            var slides = new CourseTeachingResource { Id = Guid.NewGuid(), Name = "Slide Set", ResourceFilename = @"C:\whatever\Slides.ppt" };
            context.CourseTeachingResources.Add(slides);
            var didactic = new CourseSlot { Id = Guid.NewGuid(), Day = 1, MinutesDuration = 20, MaximumFaculty = 1, MinimumFaculty = 1, Name = "Didactic Lecture", Order = 1 };
            didactic.DefaultResources.Add(slides);
            didactic.CourseTypes.Add(crm);
            context.CourseEvents.Add(didactic);

            var sim1 = new ScenarioSlot { Id = Guid.NewGuid(), Day = 1, MinutesDuration = 40, Order = 2 };
            sim1.CourseTypes.Add(crm);
            context.CourseScenarios.Add(sim1);

            var ld = new ScenarioRoleDescription { Id = Guid.NewGuid(), Description = "Lead Debrief" };
            var ad = new ScenarioRoleDescription { Id = Guid.NewGuid(), Description = "Assistant Debrief" };
            var lt = new ScenarioRoleDescription { Id = Guid.NewGuid(), Description = "Lead Tech" };
            var at = new ScenarioRoleDescription { Id = Guid.NewGuid(), Description = "Assistant Tech" };
            var d = new ScenarioRoleDescription { Id = Guid.NewGuid(), Description = "Director" };
            var r = new ScenarioRoleDescription { Id = Guid.NewGuid(), Description = "Runner" };
            crm.ScenarioRoles.Add(ld);
            crm.ScenarioRoles.Add(ad);
            crm.ScenarioRoles.Add(lt);
            crm.ScenarioRoles.Add(at);
            crm.ScenarioRoles.Add(d);
            crm.ScenarioRoles.Add(r);

            var coffee = new CourseSlot { Id = Guid.NewGuid(), Day = 1, MinutesDuration =20, MaximumFaculty = 0, MinimumFaculty = 0, Name = "Coffee break", Order = 3 };
            coffee.CourseTypes.Add(crm);
            context.CourseEvents.Add(coffee);

            var c = new Course { Id = Guid.NewGuid(), CourseType = crm, Department = ced, FacultyNoRequired = 5, StartTime = DateTime.Now.AddDays(14) };
            var c2 = new Course { Id = Guid.NewGuid(), CourseType = crm, Department = picu, FacultyNoRequired = 5, StartTime = DateTime.Now.AddDays(28) };
            var c0 = new Course { Id = Guid.NewGuid(), CourseType = crm, Department = ced, FacultyNoRequired = 5, StartTime = DateTime.Now.AddDays(-30) };
            context.Courses.AddRange(new Course[] { c, c0, c2 });

            foreach (var t in new Course[] { c, c2, c0 })
            {
                var cp = new CourseParticipant { Participant = trish, IsConfirmed = true, IsFaculty = true, Course = t, DepartmentId = trish.DefaultDepartmentId, ProfessionalRoleId = trish.DefaultProfessionalRoleId };
                context.CourseParticipants.Add(cp);
                var cp2 = new CourseParticipant { Participant = brent, IsConfirmed = true, IsFaculty = true, Course = t, DepartmentId = brent.DefaultDepartmentId, ProfessionalRoleId = brent.DefaultProfessionalRoleId };
                context.CourseParticipants.Add(cp2);
            }

            context.SaveChanges();

            var s = new Scenario { Id = Guid.NewGuid(), Complexity = Difficulty.Easy, Description = "boy falls down well", EmersionCategory = Emersion.Emersive, CourseType = crm, Manequin = junior,
                Department = ced };

            var s2 = new Scenario
            {
                Id = Guid.NewGuid(),
                Complexity = Difficulty.Moderate,
                Description = "boy has fever",
                EmersionCategory = Emersion.Emersive,
                CourseType = crm,
                Manequin = junior,
                Department = picu
            };

            var proforma = new ScenarioResource { Id = Guid.NewGuid(), Name = "proforma", ResourceFilename = @"C:\whatever.doc", Scenario=s };

            c.Scenarios.Add(s);
            c2.Scenarios.Add(s2);
            c0.Scenarios.Add(s);

            context.SaveChanges();

            var pres = new CourseSlotPresenter { Course = c, CourseSlot = didactic, Presenter = trish };

            var role = new ScenarioFacultyRole { Course = c, FacultyMember = trish, Role = ld, Scenario = s };

            context.CourseSlotPresenters.Add(pres);

            context.ScenarioFacultyRoles.Add(role);

            context.SaveChanges();

        }
    }
}
