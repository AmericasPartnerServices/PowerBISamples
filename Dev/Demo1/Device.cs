using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1
{
    class Device
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
}
