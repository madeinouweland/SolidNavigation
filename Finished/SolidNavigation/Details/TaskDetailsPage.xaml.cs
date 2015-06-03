using SolidNavigation.Navigation;
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

            if (Target is TaskDetailsTarget) {
                var taskId = ((TaskDetailsTarget)Target).TaskId;
                ViewModel = new TaskDetailsPageViewModel(taskId);
            }

            if (Target is CommentTarget) {
                var taskId = ((CommentTarget)Target).TaskId;
                var commentId = ((CommentTarget)Target).CommentId;
                ViewModel = new TaskDetailsPageViewModel(taskId, commentId);
            }

            NavInfo.Text = "parameter: " + e.Parameter;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e) {
            ((Frame)Window.Current.Content).GoBack();
        }
    }
}
