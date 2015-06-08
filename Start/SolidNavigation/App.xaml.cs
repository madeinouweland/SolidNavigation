using System;
using SolidNavigation.Tasks;
using SolidNavigation.Lists;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SolidNavigation {
    sealed partial class App : Application {
        public static Microsoft.ApplicationInsights.TelemetryClient TelemetryClient;
        public App() {
            TelemetryClient = new Microsoft.ApplicationInsights.TelemetryClient();

            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        private void InitFrame() {
            var frame = Window.Current.Content as Frame;
            if (frame == null) {
                frame = new Frame();
                Window.Current.Content = frame;
            }

            SystemNavigationManager.GetForCurrentView().BackRequested += (s, e) => {
                if (frame.CanGoBack) {
                    frame.GoBack();
                }
            };
        }

        protected override void OnActivated(IActivatedEventArgs args) {
            base.OnActivated(args);

            InitFrame();

            ((Frame)Window.Current.Content).Navigate(typeof(ListsPage));
            Window.Current.Activate();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            InitFrame();

            ((Frame)Window.Current.Content).Navigate(typeof(ListsPage));

            Window.Current.Activate();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e) {
            var deferral = e.SuspendingOperation.GetDeferral();

            // await SuspensionManager.SaveAsync();

            deferral.Complete();
        }
    }
}