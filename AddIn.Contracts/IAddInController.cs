﻿using Siemens.Engineering.HW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddIn.Contracts
{
    public interface IAddInController
    {
        List<IControlUnitItemS120> GetControlUnitsS120();
        List<IDriveItemG120> GetDriveItemG120();
        void AddControlUnitS120(string controlUnitName);
        void AddDriveToControlUnitS120(string controlUnitName, string driveName, DeviceItem deviceItem);
        void AddDriveG120(string driveName, DeviceItem deviceItem);
        void HandleSelectedDrive(IDriveItemS120 drive); // UI’den gelen seçimi işleyebilir
        string ReadParameter(DeviceItem drive); // UI’den gelen seçimi işleyebilir
    }
}
