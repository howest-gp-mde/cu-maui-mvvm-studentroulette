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
        public async Task<Student> Add(Student student)
        {
            List<Student> students = (await GetAll()).ToList();
            bool studentExists = students.Any(search =>search.Id == student.Id);

            if (!studentExists)
            {
                student.Id = Guid.NewGuid();
                students.Add(student);
            }
            else
            {
                throw new ArgumentException("The student you're trying to update already exists.");
            }

            await WriteStudents(students);
            return student;
        }

        public async Task<Student> Update(Student student)
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
                throw new ArgumentException("The student you're trying to update does not have a correct id.");
            }

            await WriteStudents(students);
            return student;
        }

        private void EnsureFileExists(string targetFile)
        {
            if (!File.Exists(targetFile))
            {
                File.WriteAllText(targetFile, JsonSerializer.Serialize(new List<Student>()));
            }
        }

        private async Task WriteStudents(List<Student> students)
        {
            string serializedStudents = JsonSerializer.Serialize(students);
            await File.WriteAllTextAsync(targetFile, serializedStudents);
        }

        public async Task<Student> ChooseRandom()
        {
            var students = (await GetAll())
                .Where(student => student.IsPresent)
                .ToList();

            if (students.Count == 0) return null;

            Random random = new Random();
            int randomIndex = random.Next(0, students.Count);

            Student chosenStudent = students[randomIndex];
            chosenStudent.TimesChosen++;

            await WriteStudents(students);
            return chosenStudent;
        }
    }
}
