

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseCommon.Authorization
{
    public class PermissionChecker : IPermissionChecker
    {
        public bool IsGranted(string controllerName, string actionName)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsGrantedAsync(string controllerName, string actionName)
        {
            throw new System.NotImplementedException();
        }

        public bool IsGrantedAsync(IEnumerable<string> listOfPermission, string controllerName, string actionName)
        {
            if (listOfPermission == null || !listOfPermission.Any()) return false;

            return listOfPermission.Contains(controllerName + "." + actionName); return true;
        }
    }
}
