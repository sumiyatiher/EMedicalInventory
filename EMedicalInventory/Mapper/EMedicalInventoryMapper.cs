using AutoMapper;
using EMedicalInventory.Models;
using EMedicalInventory.ViewModels.Obat;
using EMedicalInventory.ViewModels.Request;

namespace EMedicalInventory.Mapper
{
    public class EMedicalInventoryMapper : Profile
    {
        public EMedicalInventoryMapper() {
            CreateMap<Obat, ObatViewModel>().ReverseMap();
            CreateMap<RequestObat,RequestObatViewModel>().ReverseMap();
        }
       
    }
}
