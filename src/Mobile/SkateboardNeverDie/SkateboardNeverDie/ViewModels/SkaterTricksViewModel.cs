using Newtonsoft.Json;
using SkateboardNeverDie.Extensions;
using SkateboardNeverDie.Models;
using SkateboardNeverDie.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SkateboardNeverDie.ViewModels
{
    [QueryProperty(nameof(SkaterJSON), nameof(SkaterJSON))]
    public class SkaterTricksViewModel : BaseViewModel
    {
        private readonly ISkateboardNeverDieApi _skateboardNeverDieApi = DependencyService.Get<ISkateboardNeverDieApi>();
        private string _skaterJSON;
        private string _skaterFullName;
        private int _pageSize = 10;
        private bool _hasNextPage;

        public SkaterTricksViewModel()
        {
            Title = "Skater Tricks";
            Tricks = new ObservableCollection<Trick>();
            LoadTricksCommand = new Command(async () => await ExecuteLoadTricksCommand());
            SaveCommand = new Command(OnSave, CanExecuteSave);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
            CancelCommand = new Command(OnCancel);
        }

        public ObservableCollection<Trick> Tricks { get; }
        public Command LoadTricksCommand { get; }
        public Command SaveCommand { get; set; }
        public Command CancelCommand { get; }
        public string SkaterJSON
        {
            get => _skaterJSON;
            set
            {
                _skaterJSON = Uri.UnescapeDataString(value ?? string.Empty);
                SetProperty(ref _skaterJSON, value);

                if (!string.IsNullOrEmpty(_skaterJSON))
                {
                    CreateSkater = JsonConvert.DeserializeObject<CreateSkater>(_skaterJSON);
                    SkaterFullName = $"{CreateSkater.FirstName} {CreateSkater.LastName}".Trim().ToUpper();
                }
            }
        }
        public CreateSkater CreateSkater { get; private set; }
        public string SkaterFullName
        {
            get => _skaterFullName;
            set
            {
                SetProperty(ref _skaterFullName, value);
            }
        }

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
                var tricksHateoasResult = await _skateboardNeverDieApi.GetTricksAsync(pageSize: _pageSize);

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

        private async void OnSave()
        {
            await _skateboardNeverDieApi.PostSkatersAsync(CreateSkater);
            await Shell.Current.GoToAsync("../..", false);
        }

        private bool CanExecuteSave()
        {
            return CreateSkater?.SkaterTricks?.Any() ?? false;
        }

        private async void OnCancel() => await Shell.Current.GoToAsync("../..", false);

        internal void AddSkaterTrick(Guid trickId, StanceType variation)
        {
            var skaterTrick = new CreateSkater.SkaterTrick { TrickId = trickId };

            if (CreateSkater.SkaterTricks.Any(_ => _.TrickId == trickId))
            {
                skaterTrick = CreateSkater.SkaterTricks.First(_ => _.TrickId == trickId);
                CreateSkater.SkaterTricks.RemoveAll(_ => _.TrickId == trickId);
            }

            // Add or remove variations
            if (skaterTrick.Variations.Contains(variation))
                skaterTrick.Variations.Remove(variation);
            else
                skaterTrick.Variations.Add(variation);

            // Add or remove skater trick
            if (!skaterTrick.Variations.Any())
                CreateSkater.SkaterTricks.RemoveAll(_ => _.TrickId == skaterTrick.TrickId);
            else
                CreateSkater.SkaterTricks.Add(skaterTrick);

            OnPropertyChanged(nameof(CreateSkater));
        }

        protected override async Task ItemsThresholdReached()
        {
            if (IsBusy || !_hasNextPage)
                return;

            IsBusy = true;

            try
            {
                var nextPage = (Tricks.Count / _pageSize) + 1;
                var tricksHateoasResult = await _skateboardNeverDieApi.GetTricksAsync(nextPage, _pageSize);

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
    }
}