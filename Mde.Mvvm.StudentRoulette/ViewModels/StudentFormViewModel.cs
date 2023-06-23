using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using System.Windows.Input;

namespace Mde.Mvvm.StudentRoulette.ViewModels
{
    [QueryProperty(nameof(SelectedStudent), nameof(SelectedStudent))]
    public partial class StudentFormViewModel : ObservableObject
    {
        private Student selectedStudent;
        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set 
            {
                selectedStudent = value;

                //Sync Bindable Properties with incoming student
                if (selectedStudent != null)
                {
                    PageTitle = "Edit student";
                    Mantra = selectedStudent.Mantra;
                    NumberOfTimesChosen = selectedStudent.TimesChosen;
                    IsPresent = selectedStudent.IsPresent;
                    FirstName = selectedStudent.FirstName;
                    MiddleName = selectedStudent.MiddleName;
                    LastName = selectedStudent.LastName;
                    Birthday = selectedStudent.Birthday;
                }
                else
                {
                    PageTitle = "Add student";
                    Mantra = default;
                    NumberOfTimesChosen = default;
                    IsPresent = default;
                    FirstName = default;
                    MiddleName = default;
                    LastName = default;
                    Birthday = DateTime.Now;
                }
            }
        }

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                SetProperty(ref pageTitle, value);
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

        private DateTime birthday;
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
            Student student;

            if (SelectedStudent == null)
            {
                student = new Student();
            }
            else
            {
                student = SelectedStudent;
            }

            student.FirstName = FirstName;
            student.MiddleName = MiddleName;
            student.LastName = LastName;
            student.Birthday = Birthday;
            student.Mantra = Mantra;
            student.TimesChosen = NumberOfTimesChosen;
            student.IsPresent = IsPresent;

            if (student.Id.Equals(Guid.Empty))
            {
                await studentService.Add(student);
            }
            else
            {
                await studentService.Update(student);
            }
            
            await Shell.Current.GoToAsync("//StudentListPage");

        });

    }
}
