using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mde.Mvvm.StudentRoulette.Domain.Models
{
    public record class Student
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public int TimesChosen { get; set; }
        public string Mantra { get; set; }
        public bool IsPresent { get; set; }
        public string FullName
        {
            get
            {
                List<string> namepieces = new List<string>();
                if (!string.IsNullOrWhiteSpace(FirstName)) namepieces.Add(FirstName);
                if (!string.IsNullOrWhiteSpace(MiddleName)) namepieces.Add(MiddleName);
                if (!string.IsNullOrWhiteSpace(LastName)) namepieces.Add(LastName);

                return string.Join(" ", namepieces);
            }
        }
    }
}