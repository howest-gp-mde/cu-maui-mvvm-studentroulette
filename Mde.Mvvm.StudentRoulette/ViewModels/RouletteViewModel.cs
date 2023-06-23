using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Mvvm.StudentRoulette.Domain.Services.Interfaces;
using System.Windows.Input;

namespace Mde.Mvvm.StudentRoulette.ViewModels
{
    public partial class RouletteViewModel : ObservableObject
    {
        private readonly IStudentService studentService;

        private string chosenStudentName;

        public string ChosenStudentName
        {
            get { return chosenStudentName; }
            set 
            { 
                SetProperty(ref chosenStudentName, value);
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


        public RouletteViewModel(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        public ICommand ChooseRandomStudentCommand => new Command(async () =>
        {
            var chosenStudent = await studentService.ChooseRandom();

            if (chosenStudent is not null)
            {
                ChosenStudentName = chosenStudent.FullName;
                NumberOfTimesChosen = chosenStudent.TimesChosen;                
            }

            else
            {
                ChosenStudentName = "nobody";
                NumberOfTimesChosen = 0;
            }
        });
    }
}
