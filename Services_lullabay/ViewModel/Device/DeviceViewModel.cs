using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services_lullabay.Enum.DeviceEnum;

namespace Services_lullabay.ViewModel.Device
{
    public class DeviceViewModel
    {
        
        public string UDID { get; set; }
        public long StartDateTime { get; set; }
        public string Location { get; set; }
        public DeviceEnumType DeviceType { get; set; }
        public long? EndDateTime { get; set; }
    }
}
