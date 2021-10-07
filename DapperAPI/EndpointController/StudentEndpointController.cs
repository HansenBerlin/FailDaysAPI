using System.Collections.Generic;
using System.Threading.Tasks;
using DapperAPI.Models;
using DapperAPI.QueryController;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.EndpointController
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

        

        // POST api/<ProductController>
        [HttpPost]
        public async Task Post([FromBody] Student student)
        {
            await studentQueryController.Create(student);
        }
        
        /*
        // GET: api/Product
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
