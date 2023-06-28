using BaseCommon.Authorization;
using BaseCommon.Common.ClaimUser;
using BaseCommon.Common.Enum;
using BaseCommon.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCommon.Filters
{
    public class AuthorizeGroupCheckOperationFilter : IAsyncAuthorizationFilter
    {
        #region fields
        private const string Controller = nameof(Controller);
        private readonly IUserSessionInfo _userSessionInfo;
        private readonly EAuthorizeType _authorizeType;
        private readonly IPermissionChecker _permissionChecker;
        private readonly string _crudName;
        private readonly IMemoryCache _memoryCache;

        public AuthorizeGroupCheckOperationFilter(IUserSessionInfo userSessionInfo,
            EAuthorizeType authorizeType,
            string crudName,
            IPermissionChecker permissionChecker,
            IMemoryCache memoryCache)
        {
            _userSessionInfo = userSessionInfo;
            _authorizeType = authorizeType;
            _crudName = crudName;
            _permissionChecker = permissionChecker;
            _memoryCache = memoryCache;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            bool skipAuthorization = controllerActionDescriptor.MethodInfo.IsDefined(typeof(AllowAnonymousAttribute), true);

            if (skipAuthorization || _authorizeType == EAuthorizeType.Everyone) return ;

            if (_userSessionInfo.ID == null)
            {
                context.Result = new ForbidResult(); return;
            }
            else
            {
                if (_authorizeType == EAuthorizeType.AuthorizedUsers)
                {
                    return;
                }

                if (_authorizeType == EAuthorizeType.MusHavePermission)
                {
                    var dataCache = _memoryCache.Get(AuthorSetting.NamePermission);
                    var result = ((IEnumerable)dataCache).Cast<string>().ToList();
                    //Phải có ít nhất 1 quyền
                    if (result == null)
                    {
                        context.Result = new ForbidResult();
                        return;
                    }

                    string controllerName = controllerActionDescriptor.ControllerName + Controller;
                    string actionName = !string.IsNullOrEmpty(_crudName) ? _crudName : controllerActionDescriptor.MethodInfo.Name;
                    if (!_permissionChecker.IsGrantedAsync(result, controllerName, actionName))
                    {
                         context.Result = new ForbidResult();
                    }
                }
            }

        }

        #endregion fields
    }
}
