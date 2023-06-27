﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using System.Windows.Input;

namespace Mde.Mvvm.StudentRoulette.ViewModels
{
    public partial class StudentFormViewModel : ObservableObject
    {
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
            Student student = new Student();

            student.FirstName = FirstName;
            student.MiddleName = MiddleName;
            student.LastName = LastName;
            student.Birthday = Birthday;
            student.Mantra = Mantra;
            student.TimesChosen = NumberOfTimesChosen;
            student.IsPresent = IsPresent;

            await studentService.Add(student);
            
            await Shell.Current.GoToAsync("//StudentListPage");

        });

    }
}
