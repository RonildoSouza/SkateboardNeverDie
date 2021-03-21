using SkateboardNeverDie.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class TricksViewModel : BaseViewModel
    {
        //private Skater _selectedSkater;

        public ObservableCollection<Trick> Tricks { get; }
        public Command LoadTricksCommand { get; }

        public TricksViewModel()
        {
            Title = "Tricks";
            Tricks = new ObservableCollection<Trick>();
            LoadTricksCommand = new Command(async () => await ExecuteLoadTricksCommand());
        }

        async Task ExecuteLoadTricksCommand()
        {
            IsBusy = true;

            try
            {
                Tricks.Clear();
                var tricks = await SkateboardNeverDieApi.GetTricksAsync();

                foreach (var trick in tricks.Data.Results)
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

        public void OnAppearing()
        {
            IsBusy = true;
        }
    }
}