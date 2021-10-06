using System.Threading.Tasks;

namespace DapperAPI.ProductMaster
{
    public interface IStudentRepository
    {
        Task Create(Student student);
    }
}