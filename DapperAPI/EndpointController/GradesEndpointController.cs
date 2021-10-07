using System.Collections.Generic;
using DapperAPI.QueryController;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.EndpointController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesEndpointController : ControllerBase
    {
        private readonly IStudentQueryController _studentQueryController;

        public GradesEndpointController(IStudentQueryController studentQueryController)
        {
            _studentQueryController = studentQueryController;
        }

        // GET: api/Grade
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /*
        // GET: api/Grade/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }*/
        
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return _studentQueryController.GetGrade(id).Result.ToString();
        }

        // POST: api/Grade
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Grade/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Grade/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
