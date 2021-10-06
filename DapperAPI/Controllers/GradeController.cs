using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperAPI.ProductMaster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IStudentProvider _studentProvider;
        private readonly IStudentRepository _studentRepository;

        public GradeController(IStudentProvider studentProvider, IStudentRepository studentRepository)
        {
            _studentProvider = studentProvider;
            _studentRepository = studentRepository;
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
            return _studentProvider.GetGrade(id).Result.ToString();
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
