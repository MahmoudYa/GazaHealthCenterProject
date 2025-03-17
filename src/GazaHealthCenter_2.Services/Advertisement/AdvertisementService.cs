using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects.Models.Advertisment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Services.Advertisement
{
    public class AdvertisementService : AService
    {
        public AdvertisementService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        // Get all advertisements
        public List<AdvertisementModel> GetAllAdvertisements()
        {
            List<AdvertisementModel> advertisements = UnitOfWork.Select<AdvertisementModel>().ToList();
            return advertisements;
        }

        // Get advertisement by ID
        public AdvertisementModel? GetAdvertisementById(long id)
        {
            return UnitOfWork.Get<AdvertisementModel>(id);
        }

        // Add new advertisement
        public void AddAdvertisement(AdvertisementModel advertisement)
        {
            UnitOfWork.Insert(advertisement);
            UnitOfWork.Commit();
        }

        // Update existing advertisement
        public void UpdateAdvertisement(AdvertisementModel advertisement)
        {
            UnitOfWork.Update(advertisement);
            UnitOfWork.Commit();
        }

        // Delete advertisement
        public void DeleteAdvertisement(long id)
        {
            AdvertisementModel? advertisement = UnitOfWork.Get<AdvertisementModel>(id);
            if (advertisement != null)
            {
                UnitOfWork.Delete(advertisement);
                UnitOfWork.Commit();
            }
        }
    }
}

