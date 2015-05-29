using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation.Tasks {
    public sealed partial class TasksPage {
        public TasksPageViewModel ViewModel { get; set; }
        public TasksPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            var id = int.Parse(e.Parameter.ToString());
            ViewModel = new TasksPageViewModel(id);

            NavInfo.Text = "parameter: " + e.Parameter;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e) {
            ((Frame)Window.Current.Content).GoBack();
        }
    }
}
