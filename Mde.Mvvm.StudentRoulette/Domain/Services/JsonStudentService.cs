using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using System.Text.Json;

namespace Mde.Mvvm.StudentRoulette.Domain.Services
{
    public class JsonStudentService : IStudentService
    {
        private readonly string targetFile = $"{FileSystem.AppDataDirectory}/students.json";
        public async Task<ICollection<Student>> GetAll()
        {
            EnsureFileExists(targetFile);

            string savedSerialized = await File.ReadAllTextAsync(targetFile);
            List<Student> savedStudents = JsonSerializer.Deserialize<List<Student>>(savedSerialized);
            
            return savedStudents;
        }

        public async Task<Student> GetById(Guid id)
        {
            List<Student> students = (await GetAll()).ToList();
            Student existingStudent = students.FirstOrDefault(search =>
            {
                return search.Id == id;
            });

            return existingStudent;
        }

        public async Task<Student> SaveOrUpdate(Student student)
        {
            List<Student> students = (await GetAll()).ToList();
            Student existingStudent = students.FirstOrDefault(search =>
            {
                return search.Id == student.Id;
            });

            if (existingStudent != null)
            {
                students.Remove(existingStudent);
                student.Id = existingStudent.Id;
                students.Add(student);
            }
            else
            {
                student.Id = Guid.NewGuid();
                students.Add(student);
            }

            string serializedStudents = JsonSerializer.Serialize(students);
            await File.WriteAllTextAsync(targetFile, serializedStudents);
            return student;
        }

        private void EnsureFileExists(string targetFile)
        {
            if (!File.Exists(targetFile))
            {
                File.WriteAllText(targetFile, JsonSerializer.Serialize(new List<Student>()));
            }
        }
    }
}
