using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Services.Interface
{
    public interface IGetDataByFormIdService
    {
        Task<PassportResidentVM?> GetPassportResidentVMByFormIdAsync(string formId);
    }
}
