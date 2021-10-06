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
    public class StudentController : ControllerBase
    {
        private readonly IStudentProvider _studentProvider;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentProvider studentProvider,
            IStudentRepository studentRepository)
        {
            this._studentProvider = studentProvider;
            this._studentRepository = studentRepository;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _studentProvider.GetDatasets();
        }

        

        // POST api/<ProductController>
        [HttpPost]
        public async Task Post([FromBody] Student student)
        {
            await _studentRepository.Create(student);
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
