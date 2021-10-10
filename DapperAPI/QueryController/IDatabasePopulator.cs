using System.Threading.Tasks;

namespace DapperAPI.QueryController
{
    public interface IDatabasePopulator
    {
        Task CreateStudents();
    }
}