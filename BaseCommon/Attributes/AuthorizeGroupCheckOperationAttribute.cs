using BaseCommon.Filters;
using BaseCommon.Model;
using Microsoft.AspNetCore.Mvc;

namespace BaseCommon.Attributes
{
    public class AuthorizeGroupCheckOperationAttribute : TypeFilterAttribute
    {
        public AuthorizeGroupCheckOperationAttribute(EAuthorizeType authorizeType, string crudName = "") : base(typeof(AuthorizeGroupCheckOperationFilter))
        {
            Arguments = new object[] { authorizeType, crudName };
        }
    }
}
