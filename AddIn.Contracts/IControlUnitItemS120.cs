using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddIn.Contracts
{
    public interface IControlUnitItemS120
    {
        string Name { get; }
        List<IDriveItemS120> Drives { get; }

        void AddDriveS120(IDriveItemS120 drive);
    }
}
