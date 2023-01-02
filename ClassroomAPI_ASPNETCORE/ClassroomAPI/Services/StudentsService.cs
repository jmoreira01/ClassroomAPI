using ClassroomAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassroomAPI.Services
{
    public class StudentsService : IStudentService
    {
        private readonly MyDbContext _context;

        public StudentsService(MyDbContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(Student student)
        {
            _context.Students.Add(student); // primeiro vai à Base de dados.
            await _context.SaveChangesAsync(); // Depois grava na Base de dados

        }

        public async Task UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified; //Vai à entidade, e altera.
            await _context.SaveChangesAsync(); // Depois grava na Base de dados
        }

        public async Task DeleteStudent(Student student)
        {
            _context.Students.Remove(student); //Vai à entidade, e remove.
            await _context.SaveChangesAsync(); // Depois grava na Base de dados
        }

        public async Task<Student> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsByName(string name)
        {
            IEnumerable<Student> students;
            if (!string.IsNullOrWhiteSpace(name)) //não pode ser nullo nem em branco
            {
                //Vai buscar todos os alunos filtrados pelo nome
                students = await _context.Students.Where(x => x.Name.Contains(name)).ToListAsync();
            }
            else
            {    
                //Vai buscar todos os alunos sem filtro
                students = await GetStudents();
            }
            return students;
        }

    }
}
