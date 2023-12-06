using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_lullabay.ViewModel.Device
{
    public class Dashboard
    {
        public int Alltest { get; set; }
        public int testcomplete { get; set; }
        public int testleftover { get; set; }
        public float android_percentage { get; set; }
        public float ios_percentage { get; set; }
    }
}
