using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Diagnostics.Tracing;
using Windows.Storage;
using System.Diagnostics;
using System.Threading;
using System.Net;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Samples.EventHubConsumer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // I've generated this Shared Access Signature which, at the time of upload, has not been revoked and hence which anyone can use.
        // Please be aware that my own system will consume any message that is sent using this token, and although my system will ignore
        // these messages, you should never-the-less be responsible with it and only use it to prove that you are getting the correct
        // Http response messages. I reserve the right to revoke this SAS at any time, which I'll probably only do if dev bandwidth
        // gets too big.


        //string sas = "SharedAccessSignature sr=https%3a%2f%2feumariothub.servicebus.windows.net&sig=Ro4AspaQtWFnYmkcxNcPAGnmwoeJU9llzUsZauSYPZg%3d&se=1460220109&skn=Default";
        string serviceNamespace = "eumariothub";
        string hubName = "devicemonitoring";
        EventHubSasClient sasClient = null;

        public MainPage()
        {
            this.InitializeComponent();
          
        }


        static uint GetExpiry(uint tokenLifetimeInSeconds)
        {
            const long ticksPerSecond = 1000000000 / 100; // 1 tick = 100 nano seconds</code>

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = DateTime.Now.ToUniversalTime() - origin;

            return ((uint)(diff.Ticks / ticksPerSecond)) + tokenLifetimeInSeconds;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            sasClient = new EventHubSasClient(string.Empty, serviceNamespace, hubName, "");

            EventListener informationListener = new StorageFileEventListener("MyListenerInformation");
            informationListener.EnableEvents(MetroEventSource.Log, EventLevel.Informational);

            MetroEventSource.Log.Info("Tracking Started");

            DeviceController.DoWork(MetroEventSource.Log, sasClient, x=> { LabelMessage.Text =  x + LabelMessage.Text; } );
        }

        
    }

}
