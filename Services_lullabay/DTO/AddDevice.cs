using Services_lullabay.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services_lullabay.Enum.DeviceEnum;

namespace Services_lullabay.DTO
{
    public class AddDevice
    {
        public string UDID { get; set; }
        public long StartDateTime { get; set; }
        public string Location { get; set; }
        public DeviceEnumType DeviceType { get; set; }
    }
}
