using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xrm_aspnet_2017.Models;

namespace xrm_aspnet_2017.Services
{
    public class StudentManager : IStudentManager {
       

        public string GetStudentInfo(Student s) {

            return String.Format("Имя: {0}, Фамилия: {1}. Был зачислен {2}",
                s.FirstMidName, s.LastName, s.EnrollmentDate.ToShortDateString());

        }
    }
}
