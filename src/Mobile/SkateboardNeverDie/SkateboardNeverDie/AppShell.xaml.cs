using SkateboardNeverDie.Views;
using Xamarin.Forms;

namespace SkateboardNeverDie
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewSkaterPage), typeof(NewSkaterPage));
            Routing.RegisterRoute(nameof(SkaterTricksPage), typeof(SkaterTricksPage));
            Routing.RegisterRoute(nameof(SkaterDetailPage), typeof(SkaterDetailPage));
            Routing.RegisterRoute(nameof(NewTrickPage), typeof(NewTrickPage));
            Routing.RegisterRoute(nameof(TrickDetailPage), typeof(TrickDetailPage));

            //CurrentItem = CurrentItem.Items[1];
            tabBar.CurrentItem = tabItemPrincipal;
        }
    }
}
