﻿using AddIn.Contracts;
using Siemens.Engineering.HW;
using Siemens.Engineering.MC.DriveConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QPADrv_DriveSettings
{
    public class DriveItemG120 : IDriveItemG120
    {
        public string Name { get; }
        public IEnumerable<DeviceItem> DriveObject { get; }

        public DriveItemG120(string name, IEnumerable<DeviceItem> driveObject)
        {
            Name = name;
            DriveObject = driveObject;
        }
    }
}
