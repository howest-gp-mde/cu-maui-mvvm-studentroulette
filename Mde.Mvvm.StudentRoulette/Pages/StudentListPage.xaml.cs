using Mde.Mvvm.StudentRoulette.Domain.Models;
using Mde.Mvvm.StudentRoulette.ViewModels;

namespace Mde.Mvvm.StudentRoulette.Pages;

public partial class StudentListPage : ContentPage
{
	public StudentListPage(StudentListViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    protected override void OnAppearing()
    {
        StudentListViewModel viewmodel = BindingContext as StudentListViewModel;
        viewmodel.RefreshListCommand?.Execute(null);
        base.OnAppearing();
    }

    private void lstStudents_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        Student student = e.Item as Student;
        StudentListViewModel viewmodel = BindingContext as StudentListViewModel;
        viewmodel.EditStudentCommand?.Execute(student);
    }
}