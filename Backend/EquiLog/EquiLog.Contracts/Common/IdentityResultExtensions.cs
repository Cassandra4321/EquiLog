using Microsoft.AspNetCore.Identity;

namespace EquiLog.Contracts.Common
{
    public static class IdentityResultExtensions
    {
        public static ServiceResult ToServiceResult(this IdentityResult result, string ? prefix = null)
        {
            if (result.Succeeded)
            {
                return ServiceResult.Ok();
            }

            var error = string.Join("; ", result.Errors.Select(e => e.Description));
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                error = $"{prefix}: error";    
            }
            return ServiceResult.Fail(error);
        }
    }
}
