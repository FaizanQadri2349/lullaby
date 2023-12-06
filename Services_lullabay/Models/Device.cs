using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services_lullabay.Enum.DeviceEnum;

namespace Services_lullabay.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string UDID { get; set; }
        public long StartDateTime { get; set; }
        public string Location { get; set; }
        public DeviceEnumType DeviceType { get; set; }
        public long? EndDateTime { get; set; }
        
    }
}

