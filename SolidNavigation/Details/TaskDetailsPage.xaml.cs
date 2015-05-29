using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation.Details {
    public sealed partial class TaskDetailsPage {
        public TaskDetailsPageViewModel ViewModel { get; set; }
        public TaskDetailsPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            var id = int.Parse(e.Parameter.ToString());
            ViewModel = new TaskDetailsPageViewModel(id);

            NavInfo.Text = "parameter: " + e.Parameter;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e) {
            ((Frame)Window.Current.Content).GoBack();
        }
    }
}
