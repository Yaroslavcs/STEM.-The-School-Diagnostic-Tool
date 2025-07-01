using System;
using System.Collections.Generic;
using System.Linq;
using project.Database.Data;
using project.Database.Models;

namespace SeedData
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Schools.Any())
            {
                context.Schools.AddRange(
                    new School { Name = "Ліцей №1" },
                    new School { Name = "Гімназія STEM" }
                );
                context.SaveChanges();
            }

            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Role { Name = "Student" },
                    new Role { Name = "Teacher" },
                    new Role { Name = "Deputy" },
                    new Role { Name = "Director" },
                    new Role { Name = "Admin" }
                );
                context.SaveChanges();
            }

            if (!context.Subjects.Any())
            {
                context.Subjects.AddRange(
                    new Subject { Name = "Science" },
                    new Subject { Name = "Technology" },
                    new Subject { Name = "Engineering" },
                    new Subject { Name = "Mathematics" }
                );
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                var school = context.Schools.First();
                var studentRole = context.Roles.First(r => r.Name == "Student");
                var teacherRole = context.Roles.First(r => r.Name == "Teacher");
                var directorRole = context.Roles.First(r => r.Name == "Director");
                context.Users.AddRange(
                    new User { Username = "student1", FullName = "Іван Петренко", Email = "student1@school.com", PasswordHash = "studenthash", RoleId = studentRole.Id, SchoolId = school.Id, Grade = 7 },
                    new User { Username = "teacher1", FullName = "Олена Коваль", Email = "teacher1@school.com", PasswordHash = "teacherhash", RoleId = teacherRole.Id, SchoolId = school.Id, Grade = 0 },
                    new User { Username = "director1", FullName = "Петро Директор", Email = "director1@school.com", PasswordHash = "directorhash", RoleId = directorRole.Id, SchoolId = school.Id, Grade = 0 }
                );
                context.SaveChanges();
            }

            if (!context.Surveys.Any())
            {
                var survey = new Survey
                {
                    Title = "STEM Семантичний диференціал",
                    DateCreated = DateTime.UtcNow,
                    Questions = new List<Question>
                    {
                        new Question { Text = "Мені цікава наука", STEMLetter = "S" },
                        new Question { Text = "Я люблю технології", STEMLetter = "T" },
                        new Question { Text = "Мені подобається інженерія", STEMLetter = "E" },
                        new Question { Text = "Математика для мене важлива", STEMLetter = "M" }
                    }
                };
                context.Surveys.Add(survey);
                context.SaveChanges();
            }
        }
    }
} 