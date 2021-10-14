using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using SkateboardNeverDie.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    public class SkatersViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();
        //private Skater _selectedSkater;
        private bool _canAddSkater;

        public SkatersViewModel()
        {
            Title = "Skaters";
            Skaters = new ObservableCollection<Skater>();
            LoadSkatersCommand = new Command(async () => await ExecuteLoadSkatersCommand());
            AddSkaterCommand = new Command(OnAddSkater);

            //SkaterTapped = new Command<Skater>(OnSkaterSelected);
        }

        public ObservableCollection<Skater> Skaters { get; }
        public Command LoadSkatersCommand { get; }
        public Command AddSkaterCommand { get; }
        //public Command<Skater> SkaterTapped { get; }
        public bool CanAddSkater
        {
            get => _canAddSkater;
            set => SetProperty(ref _canAddSkater, value);
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

        public void OnAppearing()
        {
            IsBusy = true;
            //SelectedSkater = null;
        }

        private async Task ExecuteLoadSkatersCommand()
        {
            IsBusy = true;

            try
            {
                Skaters.Clear();
                var skatersHateoasResult = await _skateboardNeverDieApi.GetSkatersAsync();

                CanAddSkater = skatersHateoasResult.HasLink(Skater.Rels.Create);

                foreach (var skater in skatersHateoasResult.Data.Results)
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

        private async void OnAddSkater(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewSkaterPage));
        }

        //async void OnSkaterSelected(Skater skater)
        //{
        //    if (skater == null)
        //        return;

        //    // This will push the ItemDetailPage onto the navigation stack
        //    await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={skater.Id}");
        //}
    }
}