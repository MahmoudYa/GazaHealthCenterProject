using GazaHealthCenter_2.Data;
using GazaHealthCenter_2.Objects.Models.MedicalLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GazaHealthCenter_2.Services.MedicalLocation
{
    public class MedicalLocationService : AService
    {
        public MedicalLocationService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public List<MedicalLocationModel> GetAllLocations() => UnitOfWork.Select<MedicalLocationModel>().ToList();

        public MedicalLocationModel? GetLocationById(long id) => UnitOfWork.Get<MedicalLocationModel>(id);

        public void AddLocation(MedicalLocationModel location)
        {
            UnitOfWork.Insert(location);
            UnitOfWork.Commit();
        }

        public void UpdateLocation(MedicalLocationModel location)
        {
            UnitOfWork.Update(location);
            UnitOfWork.Commit();
        }

        public void DeleteLocation(long id)
        {
            MedicalLocationModel? location = UnitOfWork.Get<MedicalLocationModel>(id);
            if (location != null)
            {
                UnitOfWork.Delete(location);
                UnitOfWork.Commit();
            }
        }
    }
}
