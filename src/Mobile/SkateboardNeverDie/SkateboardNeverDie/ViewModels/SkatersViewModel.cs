using SkateboardNeverDie.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class SkatersViewModel : BaseViewModel
    {
        //private Skater _selectedSkater;

        public ObservableCollection<Skater> Skaters { get; }
        public Command LoadSkatersCommand { get; }
        //public Command AddItemCommand { get; }
        //public Command<Skater> SkaterTapped { get; }

        public SkatersViewModel()
        {
            Title = "Skaters";
            Skaters = new ObservableCollection<Skater>();
            LoadSkatersCommand = new Command(async () => await ExecuteLoadSkatersCommand());

            //SkaterTapped = new Command<Skater>(OnSkaterSelected);
            //AddItemCommand = new Command(OnAddItem);
        }

        async Task ExecuteLoadSkatersCommand()
        {
            IsBusy = true;

            try
            {
                Skaters.Clear();
                var skaters = await SkateboardNeverDieApi.GetSkatersAsync();

                foreach (var skater in skaters.Data.Results)
                    Skaters.Add(skater);
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
            //SelectedSkater = null;
        }

        //public Skater SelectedSkater
        //{
        //    get => _selectedSkater;
        //    set
        //    {
        //        SetProperty(ref _selectedSkater, value);
        //        OnSkaterSelected(value);
        //    }
        //}

        //private async void OnAddItem(object obj)
        //{
        //    await Shell.Current.GoToAsync(nameof(NewItemPage));
        //}

        //async void OnSkaterSelected(Skater skater)
        //{
        //    if (skater == null)
        //        return;

        //    // This will push the ItemDetailPage onto the navigation stack
        //    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={skater.Id}");
        //}
    }
}