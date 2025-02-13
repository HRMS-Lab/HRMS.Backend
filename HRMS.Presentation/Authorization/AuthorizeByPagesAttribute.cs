using HRMS.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace HRMS.Presentation.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class AuthorizeByPagesAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        private readonly string? _uiName;
        private readonly int _uiId;
        private readonly bool _allowOverride;

        public AuthorizeByPagesAttribute(int uiId, bool allowOverride = false, string? uiName = null)
        {
            _uiName = uiName;
            _uiId = uiId;
            _allowOverride = allowOverride;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.Filters.Count(f => f is AuthorizeByPagesAttribute) > 1 && _allowOverride)
            {
                return;
            }

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var jwtDecoder = context.HttpContext.RequestServices.GetRequiredService<JwtDecoder>();
                var dbContext = context.HttpContext.RequestServices.GetRequiredService<DataContext>();

                var userClaims = jwtDecoder.DecodeJwt(token);

                var UIs = await dbContext?.UserInterfaces
                .Include(ui => ui.Roles)
                .ThenInclude(r => r.SecurityGroups)
                .Where(ui => ui.Roles.Any(r => r.SecurityGroups.Any(sg => sg.Users.Any(u => u.UserID == userClaims.Id) && sg.Active == true)) && ui.Active)
                .Select(ui => ui.UIActualId)
                .ToListAsync();

                if (UIs != null && UIs.Contains(_uiId))
                {
                    return;
                }
            }

            context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
        }
    }
}
