using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xrm_aspnet_2017.Models;

namespace xrm_aspnet_2017.Services
{
    public interface IStudentManager
    {
        /// <summary>
        /// Выводит инфо о студенте
        /// </summary>
        /// <returns></returns>
        string GetStudentInfo(Student s);
    }
}
