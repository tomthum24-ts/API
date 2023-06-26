using Microsoft.AspNetCore.Authorization;

namespace BaseCommon.Attributes
{
    public class HasCredentialAttribute : AuthorizeAttribute
    {
        public string RoleID { get; set; }
        //protected override bool AuthorizeCore()
        //{
        //    return true;
        //}
    }
}
