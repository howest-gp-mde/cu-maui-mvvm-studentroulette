using Mde.Mvvm.StudentRoulette.Domain.Models;

namespace Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces
{
    public interface IStudentService
    {
        public Task<Student> GetById(Guid id);
        public Task<ICollection<Student>> GetAll();
        public Task<Student> SaveOrUpdate(Student student);
    }
}
