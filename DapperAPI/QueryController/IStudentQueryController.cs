using System.Collections.Generic;
using System.Threading.Tasks;
using DapperAPI.Models;

namespace DapperAPI.QueryController
{
    public interface IStudentQueryController
    {
        Task<IEnumerable<Student>> GetDatasets();
        Task<double> GetGrade(int studentId);
        Task Create(Student student);
        Task<IEnumerable<Grade>> GetGradeObjectPerStudent(int studentId);
        Task DeleteAllRows(string tableName);

    }
}