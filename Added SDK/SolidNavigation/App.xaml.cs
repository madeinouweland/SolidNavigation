using System;
using SolidNavigation.Tasks;
using SolidNavigation.Lists;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SolidNavigation.Details;
using SolidNavigation.Navigation;
using SolidNavigation.Sdk;

namespace SolidNavigation
{
    sealed partial class App : Application
    {
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;
        public App()
        {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            this.Suspending += OnSuspending;

            Router.Current.Scheme = "solidnavigation://";
            Router.Current.AddRoute("tasks/{taskid}/comments", typeof(TaskDetailsPage), typeof(CommentTarget));
            Router.Current.AddRoute("tasks/{taskid}", typeof(TaskDetailsPage), typeof(TaskDetailsTarget));
            Router.Current.AddRoute("lists/{listid}", typeof(TasksPage), typeof(TaskListTarget));
            Router.Current.AddRoute("", typeof(ListsPage), typeof(HomeTarget));
        }

        private void InitFrame()
        {
            var frame = Window.Current.Content as Frame;
            if (frame == null) {
                frame = new Frame();
                Window.Current.Content = frame;
            }

            SuspensionManager.RegisterFrame(frame, "AppFrame");

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) => {
                if (frame.CanGoBack) {
                    frame.GoBack();
                }
            };
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            InitFrame();

            if (args.Kind == ActivationKind.Protocol) {
                var pa = args as ProtocolActivatedEventArgs;
                var id = pa.Uri.ToString().Split('/')[2];
                ((Frame)Window.Current.Content).Navigate(typeof(TasksPage), id);
            }
            else {

                ((Frame)Window.Current.Content).Navigate(typeof(ListsPage));
            }
            Window.Current.Activate();
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            InitFrame();

            if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                await SuspensionManager.RestoreAsync();
            }
            else {
                if (e.Arguments != "") {
                    ((Frame)Window.Current.Content).Navigate(typeof(TasksPage), e.Arguments);
                }
                else {
                    ((Frame)Window.Current.Content).Navigate(typeof(ListsPage));
                }
            }

            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            await SuspensionManager.SaveAsync();

            deferral.Complete();
        }
    }
}