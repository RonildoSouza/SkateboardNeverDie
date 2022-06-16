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
    public class TricksViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();
        private bool _canAddTrick;
        private int _pageSize = 10;
        private bool _hasNextPage;

        public TricksViewModel()
        {
            Title = "Tricks";
            Tricks = new ObservableCollection<Trick>();
            LoadTricksCommand = new Command(async () => await ExecuteLoadTricksCommand());
            AddTrickCommand = new Command(OnAddTrick);
            TrickTapped = new Command<Trick>(OnTrickSelected);
            PropertyChanged += (_, __) =>
            {
                if (__.PropertyName == nameof(DashboardViewModel.IsLogged))
                    CanAddTrick = !CanAddTrick;
            };
        }

        public ObservableCollection<Trick> Tricks { get; }
        public Command LoadTricksCommand { get; }
        public Command AddTrickCommand { get; }
        public Command<Trick> TrickTapped { get; }
        public bool CanAddTrick
        {
            get => _canAddTrick;
            set => SetProperty(ref _canAddTrick, value);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        private async void OnAddTrick() => await Shell.Current.GoToAsync(nameof(NewTrickPage));

        private async Task ExecuteLoadTricksCommand()
        {
            IsBusy = true;

            try
            {
                Tricks.Clear();
                var tricksHateoasResult = await _skateboardNeverDieApi.GetTricksAsync(pageSize: _pageSize);

                CanAddTrick = tricksHateoasResult.HasLink(Trick.Rels.Create);
                _hasNextPage = tricksHateoasResult.HasLink(Trick.Rels.Next);
                Tricks.TryAddRange(tricksHateoasResult.Data.Results);
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

        private async void OnTrickSelected(Trick trick)
        {
            if (trick == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(TrickDetailPage)}?{nameof(TrickDetailViewModel.TrickId)}={trick.Id}");
        }

        protected override async Task ItemsThresholdReached()
        {
            if (IsBusy || !_hasNextPage)
                return;

            try
            {
                var nextPage = (Tricks.Count / _pageSize) + 1;
                var tricksHateoasResult = await _skateboardNeverDieApi.GetTricksAsync(nextPage, _pageSize);

                CanAddTrick = tricksHateoasResult.HasLink(Trick.Rels.Create);
                _hasNextPage = tricksHateoasResult.HasLink(Trick.Rels.Next);
                Tricks.TryAddRange(tricksHateoasResult.Data.Results);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}