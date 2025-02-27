using AddIn.Contracts;
using Siemens.Engineering.HW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPADrv_DriveSettings
{
    public class DriveItemS120 : IDriveItemS120
    {
        public string Name { get; }
        public DeviceItem DeviceItem { get; }

        public DriveItemS120(string name, DeviceItem deviceItem)
        {
            Name = name;
            DeviceItem = deviceItem;
        }
    }
}
