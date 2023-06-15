﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Mvvm.StudentRoulette.ViewModels
{
    public class StudentListViewModel : ObservableObject
    {
		private ObservableCollection<Student> students;
        private readonly IStudentService studentService;

        public ObservableCollection<Student> Students
        {
			get { return students; }
			set 
			{ 
				SetProperty(ref students, value);
			}
		}

        public StudentListViewModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        public ICommand RefreshList => new Command(async () =>
        {
            var students = await studentService.GetAll();
            Students = new ObservableCollection<Student>(students);
        });



	}
}
