using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddIn.Contracts
{
    public interface IAddInController
    {
        List<IControlUnitItem> GetControlUnits();
        void AddControlUnit(string controlUnitName);
        void AddDriveToControlUnit(string controlUnitName, string driveName);
        void HandleSelectedDrive(IDriveItem drive); // UI’den gelen seçimi işleyebilir
        void WriteLogText(); // UI’den gelen seçimi işleyebilir
    }
}
