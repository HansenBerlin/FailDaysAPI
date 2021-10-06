using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperAPI.ProductMaster
{
    public interface IStudentProvider
    {
        Task<IEnumerable<Student>> GetDatasets();
        Task<double> GetGrade(int studentId);
    }
}