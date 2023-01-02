using ClassroomAPI.Models;

namespace ClassroomAPI.Services
{
    public interface IStudentService
    {
        //Funcionalidades da API
        //*Representações assíncronas com Task<> (lista)
        Task<IEnumerable<Student>> GetStudents(); //mostra lista dos alunos
                                                  //(IEnumerable é usado para mostrar lista)
        Task<Student> GetStudent(int id);         //mostra aluno por Id
        Task<IEnumerable<Student>> GetStudentsByName(string Name); //mostra aluno por nome
        Task CreateStudent(Student student); //Cria
        Task UpdateStudent(Student student); //Atualiza informações
        Task DeleteStudent(Student student); //Elimina
    }
}
