using Mde.Mvvm.StudentRoulette.ViewModels;

namespace Mde.Mvvm.StudentRoulette.Pages;

public partial class StudentListPage : ContentPage
{
	public StudentListPage(StudentListViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}