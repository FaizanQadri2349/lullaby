using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services_lullabay.Data;
using Services_lullabay.DTO;
using Services_lullabay.Helper;
using Services_lullabay.IServices;
using Services_lullabay.Models;
using Services_lullabay.ViewModel.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Services_lullabay.Services
{
    public class DeviceServices : IDeviceServices
    {
        private readonly LulabayDbContext _db;

        public DeviceServices(LulabayDbContext db)
        {
            _db = db;
        }
        public int create(AddDevice add)
        {
            if (add.UDID == null) return 0;
            if (add.Location==null) return 0; 
            
           
            Device model = new Device
            {
                UDID = add.UDID,
                StartDateTime = add.StartDateTime,
                Location = add.Location,
                DeviceType = add.DeviceType,
            };
            _db.devices.Add(model);
            _db.SaveChanges();
            return model.Id;
           
        }

       

        public UpdateDevice update(UpdateDevice update)
        {
            var existingModel = _db.devices.Find(update.Id);
            if(existingModel != null)
            {
                existingModel.EndDateTime = update.EnddateTime;

            }

            _db.SaveChanges();
            return update;
        }
        public List<DeviceViewModel> getall()
        {
            var distinctUdIds = _db.devices.Select(u => u.UDID).Distinct().ToList();
            var uniqueRecords = new List<DeviceViewModel>();
            foreach (var udId in distinctUdIds)
            {
                var latestRecord = _db.devices.Where(u => u.UDID == udId).OrderBy(u => u.Id).FirstOrDefault();
                if (latestRecord != null)
                {
                    //uniqueRecords.Add(latestRecord);
                    var deviceViewModel = new DeviceViewModel
                    {
                        
                       
                        UDID = latestRecord.UDID,
                        StartDateTime=latestRecord.StartDateTime,
                        Location = latestRecord.Location,
                        DeviceType = latestRecord.DeviceType,
                        EndDateTime = latestRecord.EndDateTime,
                    };
                    uniqueRecords.Add(deviceViewModel);
                }
            }
            return uniqueRecords;
        }

        public List<GetByUdid> getbyudid(string udid)
        {
            
            var uniqueRecords = new List<GetByUdid>();
            
                var data = _db.devices.Where(x => x.UDID == udid);
            if (data != null && data.Any())
            {
                foreach (var record in data)
                {

                    var getByUdid = new GetByUdid
                    {
                        UDID = record.UDID,
                        StartDateTime = record.StartDateTime,
                        Location = record.Location,
                        DeviceType = record.DeviceType,
                        EndDateTime = record.EndDateTime,
                    };
                    uniqueRecords.Add(getByUdid);
                }
            }
            return uniqueRecords;

        }
        public Dashboard dashboard()
        {
          // var uniqueRecords = new List<Dashboard>();
            var alltest = _db.devices.Count();
            var testcomplete = _db.devices.Where(x => x.EndDateTime !=null).Count();
            var leftover = _db.devices.Where(x => x.EndDateTime == null).Count();
            var totalai = _db.devices.Where(x => x.DeviceType == Enum.DeviceEnum.DeviceEnumType.Android || x.DeviceType == Enum.DeviceEnum.DeviceEnumType.Ios).Count();
            var andr = _db.devices.Where(x => x.DeviceType == Enum.DeviceEnum.DeviceEnumType.Android).Count();
            var iosr = _db.devices.Where(x => x.DeviceType == Enum.DeviceEnum.DeviceEnumType.Ios).Count();
            float andrper = (float)andr / totalai * 100;
            float ios = (float)iosr / totalai * 100;
            var result = new Dashboard
            {
                Alltest = alltest,
                testcomplete = testcomplete,
                testleftover = leftover,
                android_percentage = andrper,
                ios_percentage = ios
            };
           // uniqueRecords.Add(result);

            return result;

        }

        public List<LocationCount> location()
        {
            var locationCounts = _db.devices.GroupBy(x => x.Location.ToLower())
               .Select(group => new LocationCount
               {
                   Location = group.Key,
                   Count = group.Count()
               }).ToList();

            return locationCounts;
        }
    }
}
