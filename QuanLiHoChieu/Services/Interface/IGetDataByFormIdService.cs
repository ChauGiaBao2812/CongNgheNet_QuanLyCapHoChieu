using QuanLiHoChieu.Models.ViewModels;

namespace QuanLiHoChieu.Services.Interface
{
    public interface IGetDataByFormIdService
    {
        Task<PassportResidentVM?> GetPassportResidentVMByFormIdAsync(string formId);
        Task<PassportFormReviewVM?> GetPassportVMByFormIdAsync(string formId);

    }
}
