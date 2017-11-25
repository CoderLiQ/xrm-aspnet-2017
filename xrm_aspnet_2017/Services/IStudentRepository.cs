using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xrm_aspnet_2017.Models;

namespace xrm_aspnet_2017.Services
{
    interface IStudentRepository
    {
        ICollection<Student> GetAllStudents();
    }
}
