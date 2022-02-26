using SkateboardNeverDie.Extensions;
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
        private Skater _selectedSkater;
        private bool _canAddSkater;
        private int _pageSize = 10;
        private bool _hasNextPage;

        public SkatersViewModel()
        {
            Title = "Skaters";
            Skaters = new ObservableCollection<Skater>();
            LoadSkatersCommand = new Command(async () => await ExecuteLoadSkatersCommand());
            AddSkaterCommand = new Command(OnAddSkater);
            SkaterTapped = new Command<Skater>(OnSkaterSelected);
            PropertyChanged += (_, __) =>
            {
                if (__.PropertyName == nameof(DashboardViewModel.IsLogged))
                    CanAddSkater = !CanAddSkater;
            };
        }

        public ObservableCollection<Skater> Skaters { get; }
        public Command LoadSkatersCommand { get; }
        public Command AddSkaterCommand { get; }
        public Command<Skater> SkaterTapped { get; }
        public bool CanAddSkater
        {
            get => _canAddSkater;
            set => SetProperty(ref _canAddSkater, value);
        }
        public Skater SelectedSkater
        {
            get => _selectedSkater;
            set
            {
                SetProperty(ref _selectedSkater, value);
                OnSkaterSelected(value);
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedSkater = null;
        }

        private async Task ExecuteLoadSkatersCommand()
        {
            IsBusy = true;

            try
            {
                Skaters.Clear();
                var skatersHateoasResult = await _skateboardNeverDieApi.GetSkatersAsync(pageSize: _pageSize);

                CanAddSkater = skatersHateoasResult.HasLink(Skater.Rels.Create);
                _hasNextPage = skatersHateoasResult.HasLink(Skater.Rels.Next);
                Skaters.TryAddRange(skatersHateoasResult.Data.Results);
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

        private async void OnAddSkater() => await Shell.Current.GoToAsync(nameof(NewSkaterPage));

        private async void OnSkaterSelected(Skater skater)
        {
            if (skater == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(SkaterDetailPage)}?{nameof(SkaterDetailViewModel.SkaterId)}={skater.Id}");
        }

        protected override async Task ItemsThresholdReached()
        {
            if (IsBusy || !_hasNextPage)
                return;

            IsBusy = true;

            try
            {
                var nextPage = (Skaters.Count / _pageSize) + 1;
                var skatersHateoasResult = await _skateboardNeverDieApi.GetSkatersAsync(nextPage, _pageSize);

                CanAddSkater = skatersHateoasResult.HasLink(Skater.Rels.Create);
                _hasNextPage = skatersHateoasResult.HasLink(Skater.Rels.Next);
                Skaters.TryAddRange(skatersHateoasResult.Data.Results);
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