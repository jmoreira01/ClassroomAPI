using ClassroomAPI.Models;
using ClassroomAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassroomAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Aqui define-se os endpoints da API com os Verbos, onde está istanciada a base de dados
    public class StudentsController : ControllerBase
    {
        private IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        /*
         Task - Porque é uma operação assíncrona
        ActionResult - permite o response da API
        IAsyncEnumerable - para devolver uma lista em modo assíncrono dos alunos
         */
        [HttpGet] // GET ALL STUDENTS FROM LIST

        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudents()
        {
            try
            {
                var students = await _studentService.GetStudents();
                return Ok(students);
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        [HttpGet("StudentByName")] // GET STUDENT BY NAME
        public async Task<ActionResult<IAsyncEnumerable<Student>>> 
            GetStudentsByName([FromQuery]string name) 
            //vai buscar o input do nome para iniciar a busca (usa-se FromQuery
            //porque o parametro da API requere Query string "Name")
        {
            try
            {
                var students = await _studentService.GetStudentsByName(name);

                if (students == null)
                    return NotFound($"{name} Not Found!");

                return Ok(students);
            }
            catch
            {
                return BadRequest("Invalid Request");
            }
        }

        [HttpGet("{id:int}", Name = "GetStudent")] // GET STUDENT BY ID
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            try
            {
                var students = await _studentService.GetStudent(id);

                if (students == null)
                    return NotFound($"ID:{id} Not Found!");

                return Ok(students);

            }
            catch
            {

                return BadRequest("Invalid Request");
            }

        }

        [HttpPost] // Student Creation
        public async Task<ActionResult> Create(Student student)
        {
            try
            {
                await _studentService.CreateStudent(student);
                return CreatedAtRoute(nameof(GetStudent), new { id = student.Id }, student);
                // https://learn.microsoft.com/pt-br/dotnet/api/system.web.http.apicontroller.createdatroute?view=aspnet-webapi-5.2
                // (201 Created) confirma a criação de um recurso com o CreatedAtRoute() method em POST.
                // no HttpGet {id} temos de definir o parâmetro Name para que a rota seja acedida: [HttpGet("{id}", Name = "GetStudent")]

            }
            catch
            {

                return BadRequest("Invalid Request");
            }
        }

        [HttpPut("{id:int}")] //Update by ID
        public async Task<ActionResult> Edit(int id, [FromBody] Student student)
        {
            try
            {

                if (student.Id == id)
                {
                    await _studentService.UpdateStudent(student);
                    return Ok($"Student ID:{id} sucessful updated!");
                }
                else
                {
                    return BadRequest("Data Error!");
                }

            }
            catch
            {

                return BadRequest("Invalid Request");
            }
        }

        [HttpDelete("{id:int}")] // DELETE BY ID
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var student = await _studentService.GetStudent(id);

                if (student != null)
                {
                    await _studentService.DeleteStudent(student);
                    return Ok($"Student ID:{id} sucessful deleted!");
                }
                else
                {
                    return BadRequest("Not Found!");
                }

            }
            catch
            {

                return BadRequest("Invalid Request");
            }
        }

    }
}
