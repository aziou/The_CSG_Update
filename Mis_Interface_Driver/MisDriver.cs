using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Driver_Interface;
namespace Mis_Interface_Driver
{
    public abstract class MisDriver:BaseDriver
    {
        public enum SoftType_Code : uint
        {
            CL3000S = 1,
            CL3000G = 2,
            CL3000F = 3,
            CL3000DV80 = 4,

        }

        public enum Function_Code : uint
        {
            GetSelectTimeData = 1,
            UpDataToOracle = 2,
            DeleteDataFromOracle = 3,
            OutPutMisInfoToReport = 4,

        }
    }
}
