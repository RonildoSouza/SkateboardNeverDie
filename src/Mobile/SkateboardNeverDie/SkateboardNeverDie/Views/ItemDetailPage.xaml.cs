using SkateboardNeverDie.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SkateboardNeverDie.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}