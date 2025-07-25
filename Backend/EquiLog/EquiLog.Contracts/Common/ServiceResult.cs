namespace EquiLog.Contracts.Common
{
    public record ServiceResult(bool Success, string? ErrorMessage)
    {
        public static ServiceResult Ok() => new(true, null);
        public static ServiceResult Fail(string message) => new(false, message);    
    }
}
