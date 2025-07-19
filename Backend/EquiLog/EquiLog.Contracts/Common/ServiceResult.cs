namespace EquiLog.Contracts.Common
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }

        public static ServiceResult Ok() => new ServiceResult { Success = true };
        public static ServiceResult Fail(string message) => new ServiceResult { Success = false, ErrorMessage = message };
    }
}
