using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Samples.EventHubConsumer
{
    public class Device
    {
        public int DeviceID { get; set; }
        public int Vibration { get; set; }
        public int Voltage { get; set; }
        public int Current { get; set; }
        public int Wind_speed { get; set; }
        public int Wind_direction { get; set; }
        public int Device_direction { get; set; }
        public int Generator_speed { get; set; }
        public int Windpower { get; set; }
        public int Electrical_power { get; set; }
        public int Device_location_longitude { get; set; }
        public int Device_location_latitude { get; set; }
        public string Device_location { get; set; }

        public DateTime Time { get; set; }

        public Device(int mDeviceID, int mVibration, int mVoltage, int mCurrent, int mWind_speed, int mWind_direction, int mDevice_direction, int mGenerator_speed, int mWindpower, int mElectrical_power, int mDevice_location_longitude, int mDevice_location_latitude, string mDevice_location)
        {
            this.DeviceID = mDeviceID;
            this.Vibration = mVibration;
            this.Voltage = mVoltage;
            this.Current = mCurrent;
            this.Wind_speed = mWind_speed;
            this.Wind_direction = mWind_direction;
            this.Device_direction = mDevice_direction;
            this.Generator_speed = mGenerator_speed;
            this.Windpower = mWindpower;
            this.Electrical_power = mElectrical_power;
            this.Device_location_longitude = mDevice_location_longitude;
            this.Device_location_latitude = mDevice_location_latitude;
            this.Device_location = mDevice_location;
            this.Time = System.DateTime.Now;
        }
    }
    public class DeviceController
    {
        public static async void DoWork(MetroEventSource logger, EventHubSasClient eventHub, Action<string> action)
        {

            Random MyRandom = new Random();

            List<Task<HttpResponseMessage>> tasks = new List<Task<HttpResponseMessage>>();

            //Création d'une variable liste pour stocker mes objets 
            List<Device> MyDevices = new List<Device>();

            List< KeyValuePair<string, string>> listKeys = new List<KeyValuePair<string,string>>();
            listKeys.Add(new KeyValuePair<string, string>("device1", "SharedAccessSignature sr=https%3a%2f%2feumariothub.servicebus.windows.net%2fdevicemonitoring%2fpublishers%2f1%2fmessages&sig=Ro4AspaQtWFnYmkcxNcPAGnmwoeJU9llzUsZauSYPZg%3d&se=1460220109&skn=Default"));
            listKeys.Add(new KeyValuePair<string, string>("device2", "SharedAccessSignature sr=https%3a%2f%2feumariothub.servicebus.windows.net%2fdevicemonitoring%2fpublishers%2f2%2fmessages&sig=7qUQGP3ytBju8amBLHnPojWlhPhAMIi4iJrQsf3jXVM%3d&se=1465172326&skn=Default"));
            listKeys.Add(new KeyValuePair<string, string>("device3", "SharedAccessSignature sr=https%3a%2f%2feumariothub.servicebus.windows.net%2fdevicemonitoring%2fpublishers%2f3%2fmessages&sig=imW6JFIi9uDOOr%2bJlDmVQajAmZbmfL7MKkpJ724JqHc%3d&se=1465172448&skn=Default"));
            listKeys.Add(new KeyValuePair<string, string>("device4", "SharedAccessSignature sr=https%3a%2f%2feumariothub.servicebus.windows.net%2fdevicemonitoring%2fpublishers%2f4%2fmessages&sig=vN2Gn8hqr24Vxy1%2fLFL4Pg0Ot3P2%2bjocC5q6UIRhAlQ%3d&se=1465172404&skn=Default"));
            listKeys.Add(new KeyValuePair<string, string>("device5", "SharedAccessSignature sr=https%3a%2f%2feumariothub.servicebus.windows.net%2fdevicemonitoring%2fpublishers%2f5%2fmessages&sig=VJ7n59h%2by5xJXAPlXOwjv9QG1qNGZILfZwf2nsbPN10%3d&se=1465172478&skn=Default"));

            int count = 0;
            while (count < 50000)
            {
                try
                {
                    count++;

                    MyDevices.Clear();

                    MyDevices.Add(new Device(1, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Tunis"));
                    //MyDevices.Add(new Device(2, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "France"));
                    //MyDevices.Add(new Device(3, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Algerie"));
                    //MyDevices.Add(new Device(4, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Sousse"));
                    //MyDevices.Add(new Device(5, MyRandom.Next(0, 25), MyRandom.Next(0, 600), MyRandom.Next(0, 10), MyRandom.Next(0, 35), MyRandom.Next(0, 359), MyRandom.Next(0, 359), MyRandom.Next(0, 500), MyRandom.Next(0, 2000), MyRandom.Next(0, 2000), MyRandom.Next(-180, 180), MyRandom.Next(-90, 90), "Allemagne"));

                    for (int j = 0; j < 1; j++)
                    {
                        var Device = MyDevices[j];
                        // Serialize to JSON
                        var serializedString = JsonConvert.SerializeObject(Device);
                        var key = listKeys[j].Value;

                        Device.Time = DateTime.Now;
                        eventHub.DeviceName = Device.DeviceID.ToString();
                        eventHub.SharedAccessSignature = key;

                        Task<HttpResponseMessage> lastTask = eventHub.SendMessageAsync(serializedString);

                        await lastTask;
                        tasks.Add(lastTask);

                        logger.Info(serializedString);

                        while (!lastTask.IsCompleted)
                        {
                            System.Threading.Tasks.Task.Delay(0).Wait();
                        }
                        try
                        {
                            HttpResponseMessage response = lastTask.Result;

                            string result = await response.Content.ReadAsStringAsync();

                            logger.Info(result);

                            action(response .StatusCode.ToString()+ "/n" + DateTime.Now.ToString("hh:mm:ss mmm"));
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }
                        //client.SendAsync(data);
                    }
                }
                catch (Exception ex) {
                    try
                    {
                        System.Diagnostics.Debug.Write(ex.Message);
                        logger.Error(ex.Message);
                    }
                    catch
                    {
                       
                    }
                }
            }
        }
    }
}