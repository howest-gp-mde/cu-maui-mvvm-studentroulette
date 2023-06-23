using Mde.Mvvm.StudentRoulette.ViewModels;

namespace Mde.Mvvm.StudentRoulette.Pages;

public partial class StudentFormPage : ContentPage
{
	public StudentFormPage(StudentFormViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}