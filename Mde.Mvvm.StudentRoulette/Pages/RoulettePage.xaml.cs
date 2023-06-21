using Mde.Mvvm.StudentRoulette.ViewModels;

namespace Mde.Mvvm.StudentRoulette.Pages;

public partial class RoulettePage : ContentPage
{
	public RoulettePage(RouletteViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    protected override void OnAppearing()
    {
		var viewmodel = BindingContext as RouletteViewModel;
		viewmodel?.ChooseRandomStudentCommand?.Execute(null);
    }
}