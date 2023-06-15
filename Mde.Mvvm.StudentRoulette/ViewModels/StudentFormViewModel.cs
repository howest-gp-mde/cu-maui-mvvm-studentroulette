using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using System.Windows.Input;
using static Microsoft.Maui.ApplicationModel.Permissions;

namespace Mde.Mvvm.StudentRoulette.ViewModels
{
    [QueryProperty(nameof(Student), nameof(Student))]
    public partial class StudentFormViewModel : ObservableObject
    {
        private Student student;

        public Student Student
        {
            get { return student; }
            set 
            {
                //Set the private field
                student = value;

                //Update vm props for UI refresh
                Mantra = value.Mantra;
                NumberOfTimesChosen = value.TimesChosen;
                IsPresent = value.IsPresent;
                FirstName = value.FirstName;
                MiddleName = value.MiddleName;
                LastName = value.LastName;
                Birthday = value.Birthday;
            }
        }


        /* 1. If the property has changed, SetProperty will notify the changes to the View */
        private string mantra;
        public string Mantra
        {
            get { return mantra; }
            set 
            {
                SetProperty(ref mantra, value);
            }
        }

        private int numberOfTimesChosen;
        public int NumberOfTimesChosen
        {
            get { return numberOfTimesChosen; }
            set 
            { 
                SetProperty(ref numberOfTimesChosen, value);
            }
        }

        private bool isPresent;
        public bool IsPresent
        {
            get { return isPresent; }
            set
            {
                SetProperty(ref isPresent, value);
            }
        }

        /* 2. Example of why you might need the long notation:
         * if the FirstName, MiddleName or LastName changes, we immediately want to notify
         * that the FullName has changed, too.*/
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set 
            { 
                if (SetProperty(ref firstName, value))
                {
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private string middleName;
        public string MiddleName
        {
            get { return middleName; }
            set 
            {
                if (SetProperty(ref middleName, value))
                {
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (SetProperty(ref lastName, value))
                {
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        private DateTime birthday = new DateTime(2000, 1, 1);
        private readonly IStudentService studentService;

        public DateTime Birthday
        {
            get { return birthday; }
            set
            {
                SetProperty(ref birthday, value);
            }
        }

        /* 3. The result of FullName needs extra processing.
         * Other properties notify that this has changed */
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

        public StudentFormViewModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            //todo: validation
            Student = Student with
            {
                FirstName = FirstName,
                MiddleName = MiddleName,
                LastName = LastName,
                Birthday = Birthday,
                Mantra = Mantra,
                TimesChosen = NumberOfTimesChosen,
                IsPresent = IsPresent
            };

            await studentService.SaveOrUpdate(student);
            await Shell.Current.GoToAsync("//StudentListPage");
        });
    }
}
