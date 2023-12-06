using Microsoft.AspNetCore.Mvc;
using Services_lullabay.DTO;
using Services_lullabay.ViewModel.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_lullabay.IServices
{
    public interface IDeviceServices
    {
        public int create(AddDevice add);
        public UpdateDevice update(UpdateDevice update);
        public List<DeviceViewModel> getall();
        public List<GetByUdid> getbyudid(string udid);
        public Dashboard dashboard();
        public List<LocationCount> location();

    }
}
