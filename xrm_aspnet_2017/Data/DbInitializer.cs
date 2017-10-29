using xrm_aspnet_2017.Models;
using System;
using System.Linq;

namespace xrm_aspnet_2017.Data {
    public static class DbInitializer {
        public static void Initialize(UniversityContext context) {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Students.Any()) {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstMidName="Дезмоднд",LastName="Майлс",EnrollmentDate=DateTime.Parse("2017-09-01")},
            new Student{FirstMidName="Альтаир",LastName="Ла-Ахад",EnrollmentDate=DateTime.Parse("2017-09-01")},
            new Student{FirstMidName="Эцио",LastName="Аудиторе",EnrollmentDate=DateTime.Parse("2017-09-01")},
            new Student{FirstMidName="Коннор",LastName="Кенуэй",EnrollmentDate=DateTime.Parse("2017-09-01")},
            new Student{FirstMidName="Хэйтем",LastName="Кенуэй",EnrollmentDate=DateTime.Parse("2017-09-01")},
            new Student{FirstMidName="Катерина",LastName="Сфорца",EnrollmentDate=DateTime.Parse("2017-09-01")}
            };
            foreach (Student s in students) {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1,Title="ASP.NET CORE",Credits=3},
            new Course{CourseID=2,Title="Java",Credits=3},
            new Course{CourseID=3,Title="Front End",Credits=5}
            };
            foreach (Course c in courses) {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1,Grade=Grade.Отлично},
            new Enrollment{StudentID=1,CourseID=2,Grade=Grade.Хорошо},
            new Enrollment{StudentID=1,CourseID=3,Grade=Grade.Удовлетворительно},
            new Enrollment{StudentID=2,CourseID=1,Grade=Grade.Отлично},
            new Enrollment{StudentID=2,CourseID=2,Grade=Grade.Неудовлетворительно},
            new Enrollment{StudentID=3,CourseID=3},
            new Enrollment{StudentID=4,CourseID=1},
            new Enrollment{StudentID=4,CourseID=2},
            new Enrollment{StudentID=5,CourseID=2},
            new Enrollment{StudentID=6,CourseID=1}
            };
            foreach (Enrollment e in enrollments) {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
