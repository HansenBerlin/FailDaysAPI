using System.Collections.Generic;
using System.Threading.Tasks;
using DapperAPI.Models;

namespace DapperAPI.QueryController
{
    public interface ICourseQueryController
    {
        Task<IEnumerable<Course>> GetAll();
    }
}