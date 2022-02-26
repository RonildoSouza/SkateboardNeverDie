using SkateboardNeverDie.Models;
using SkateboardNeverDie.ViewModels;
using System;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class SkaterTricksPage : ContentPage
    {
        readonly SkaterTricksViewModel _viewModel;

        public SkaterTricksPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new SkaterTricksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (sender == null || !(sender is CheckBox))
                return;

            var checkBox = (CheckBox)sender;
            var checkBoxId = checkBox.AutomationId;
            var trick = (Trick)checkBox.BindingContext;

            if (checkBoxId == "Natural Stance")
                checkBoxId = _viewModel.CreateSkater.NaturalStance.ToString();

            Enum.TryParse(checkBoxId, true, out StanceType variation);
            _viewModel.AddSkaterTrick(trick.Id, variation);
        }
    }
}