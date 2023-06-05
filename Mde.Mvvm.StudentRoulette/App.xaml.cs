namespace Mde.Mvvm.StudentRoulette;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
