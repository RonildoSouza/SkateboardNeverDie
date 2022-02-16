using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class TricksViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();

        public TricksViewModel()
        {
            Title = "Tricks";
            Tricks = new ObservableCollection<Trick>();
            LoadTricksCommand = new Command(async () => await ExecuteLoadTricksCommand());
        }

        public ObservableCollection<Trick> Tricks { get; }
        public Command LoadTricksCommand { get; }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async Task ExecuteLoadTricksCommand()
        {
            IsBusy = true;

            try
            {
                Tricks.Clear();
                var tricksHateoasResult = await _skateboardNeverDieApi.GetTricksAsync();

                foreach (var trick in tricksHateoasResult.Data.Results)
                    Tricks.Add(trick);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}