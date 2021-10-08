using System.Collections.Generic;
using System.Threading.Tasks;
using DapperAPI.Models;
using DapperAPI.QueryController;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentEndpointController : ControllerBase
    {
        private readonly IStudentQueryController studentQueryController;

        public StudentEndpointController(IStudentQueryController studentQueryController)
        {
            this.studentQueryController = studentQueryController;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await studentQueryController.GetDatasets();
        }
        
        [HttpPost]
        public async Task Post([FromBody] Student student)
        {
            await studentQueryController.Create(student);
        }
    }
}
