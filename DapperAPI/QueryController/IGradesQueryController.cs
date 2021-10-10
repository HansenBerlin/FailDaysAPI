using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperAPI.QueryController
{
    public interface IGradesQueryController
    {
        Task<IEnumerable<string>> GetCategorys();
        Task UpdateGrade(decimal grade, int gradeId);
        Task CreateGradesForStudent(int studentId);


    }
}