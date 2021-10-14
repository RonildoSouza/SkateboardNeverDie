using SkateboardNeverDie.Models;
using SkateboardNeverDie.ViewModels;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class NewSkaterPage : ContentPage
    {
        public Skater Skater { get; set; }

        public NewSkaterPage()
        {
            InitializeComponent();
            BindingContext = new NewSkaterViewModel();
        }
    }
}