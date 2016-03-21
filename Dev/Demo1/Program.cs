using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using Microsoft.ServiceBus;


namespace Demo1
{
    class Program
    {
        static void Main(string[] args)
        {
            string eventHubName = "eventhubstreamtemperature";
            string eventHubNamespace = "eumar-azurepa";
            string sharedAccessPolicyName = "DevicePolicy";
            string sharedAccessPolicyKey = Constants.PoliceKey;


            Random MyRandom = new Random();

            //Service Bus Init
            var settings = new MessagingFactorySettings()
            {
                TokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(sharedAccessPolicyName, sharedAccessPolicyKey),
                TransportType = TransportType.Amqp
            };

            var factory = MessagingFactory.Create(ServiceBusEnvironment.CreateServiceUri("sb", eventHubNamespace, ""), settings);
            EventHubClient client = factory.CreateEventHubClient(eventHubName);

            List<Task> tasks = new List<Task>();

            //Création d'une variable liste pour stocker mes objets 
            List<Device> MyDevices = new List<Device>();

            List<string> Liste = new List<string>();
            Liste.Add("device1");
            Liste.Add("device2");
            Liste.Add("device3");
            Liste.Add("device4");
            Liste.Add("device5");


            while (!Console.KeyAvailable)
            {
                MyDevices.Clear();

                MyDevices.Add(new Device(1, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Tunis"));
                MyDevices.Add(new Device(2, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "France"));
                MyDevices.Add(new Device(3, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Algerie"));
                MyDevices.Add(new Device(4, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Sousse"));
                MyDevices.Add(new Device(5, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Allemagne"));


                for (int j = 0; j < 5; j++)
                {


                    var Device = MyDevices[j];

                    Device.Time = DateTime.Now;

                    // Serialize to JSON
                    var serializedString = JsonConvert.SerializeObject(Device);
                    Console.WriteLine(serializedString);
                    EventData data = new EventData(Encoding.UTF8.GetBytes(serializedString))
                    {
                        PartitionKey = Device.DeviceID.ToString()
                    };

                    // Send the metric to Event Hub
                    tasks.Add(client.SendAsync(data));

                    //client.SendAsync(data);

                }
                Thread.Sleep(1500);
            }


        }
    }
}
