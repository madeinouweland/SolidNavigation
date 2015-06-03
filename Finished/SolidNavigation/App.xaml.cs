using System;
using SolidNavigation.Details;
using SolidNavigation.Lists;
using SolidNavigation.Navigation;
using SolidNavigation.Sdk;
using SolidNavigation.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SolidNavigation {
    sealed partial class App : Application {
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;
        public App() {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            this.Suspending += OnSuspending;

            Router.Current.Scheme = "solidnavigation://";
            Router.Current.AddRoute("tasks/{taskid}/comments", typeof(TaskDetailsPage), typeof(CommentTarget));
            Router.Current.AddRoute("tasks/{taskid}", typeof(TaskDetailsPage), typeof(TaskDetailsTarget));
            Router.Current.AddRoute("lists/{listid}", typeof(TasksPage), typeof(TaskListTarget));
            Router.Current.AddRoute("", typeof(ListsPage), typeof(HomeTarget));
        }

        private void InitFrame() {
            var frame = Window.Current.Content as Frame;
            if (frame == null) {
                frame = new Frame();
                Window.Current.Content = frame;
                SuspensionManager.RegisterFrame(frame, "AppFrame");
            }
        }

        protected override void OnActivated(IActivatedEventArgs args) {
            base.OnActivated(args);

            InitFrame();

            if (args.Kind == ActivationKind.Protocol) {
                var protocolArgs = (ProtocolActivatedEventArgs)args;
                NavigateService.Current.Navigate(protocolArgs.Uri + "");
            }

            Window.Current.Activate();
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e) {
            InitFrame();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                await SuspensionManager.RestoreAsync();
            } else {
                NavigateService.Current.Navigate(e.Arguments);
            }

            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();

            await SuspensionManager.SaveAsync();

            deferral.Complete();
        }
    }
}