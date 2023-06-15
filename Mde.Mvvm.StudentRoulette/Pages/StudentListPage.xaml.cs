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
        (BindingContext as StudentListViewModel).RefreshList?.Execute(null);
        base.OnAppearing();
    }
}