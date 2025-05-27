using QuanLiHoChieu.Services.Interface;

namespace QuanLiHoChieu.Services
{
    public class RootAdminService : IRootAdminService
    {
        private readonly string _rootUsername;

        public RootAdminService(IConfiguration config)
        {
            _rootUsername = config["RootAdmin:Username"]
                ?? throw new InvalidOperationException("Root admin username is not configured."); ;
        }

        public bool IsRootAdmin(string username) =>
            string.Equals(_rootUsername, username, StringComparison.OrdinalIgnoreCase);
    }
}
