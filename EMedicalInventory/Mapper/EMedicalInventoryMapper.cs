using AutoMapper;
using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;

namespace EMedicalInventory.Mapper
{
    public class EMedicalInventoryMapper : Profile
    {
        public EMedicalInventoryMapper() {
            CreateMap<Obat, ObatViewModel>().ReverseMap();
        }
       
    }
}
