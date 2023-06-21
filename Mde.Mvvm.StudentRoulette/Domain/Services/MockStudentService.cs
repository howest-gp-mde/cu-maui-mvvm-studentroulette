using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;

namespace Mde.Mvvm.StudentRoulette.Domain.Services
{
    public class MockStudentService : IStudentService
    {
        private static readonly List<Student> students = new List<Student>()
        {
            new Student() { Id = Guid.NewGuid(), FirstName = "Randy", LastName = "Ohm", IsPresent = true },
            new Student() { Id = Guid.NewGuid(), FirstName = "Roel", LastName = "Ette", IsPresent = true }
        };
        public Task<Student> Add(Student student)
        {
            student.Id = Guid.NewGuid();

            students.Add(student);

            return Task.FromResult(student);
        }

        public Task<ICollection<Student>> GetAll()
        {
            ICollection<Student> allStudents = students;
            return Task.FromResult(allStudents);
        }

        public Task<Student> GetById(Guid id)
        {
            Student student = students.FirstOrDefault(student => student.Id == id);
            return Task.FromResult(student);
        }

        public async Task<Student> Update(Student student)
        {
            Student existingStudent = await GetById(student.Id);

            if (existingStudent is null) 
                throw new ArgumentException("The student you're trying to update does not have a correct id.");

            student.Id = existingStudent.Id;
            students.Remove(existingStudent);
            students.Add(student);

            return student;
        }

        public Task<Student> ChooseRandom()
        {
            if (students.Count == 0) return null;

            Random random = new Random();
            int randomIndex = random.Next(0, students.Count);

            Student chosenStudent = students[randomIndex];
            chosenStudent.TimesChosen++;

            return Task.FromResult(chosenStudent);
        }
    }
}
