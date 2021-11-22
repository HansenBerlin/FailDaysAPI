using System.Collections.Generic;
using System.Threading.Tasks;
using DapperAPI.Models;

namespace DapperAPI.QueryController
{
    public interface IGradesQueryController
    {
        Task<IEnumerable<string>> GetCategorys();
        Task UpdateGrade(decimal grade, int gradeId);
        Task CreateGradesForStudent(int studentId);
        Task<IEnumerable<Grade>> GetGradeObjectPerStudent(int studentId);
    }
}