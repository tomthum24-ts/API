using API.Extension;
using BaseCommon.Common.MethodResult;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Attributes
{

    public class SQLInjectionCheckOperationAttribute : ActionFilterAttribute
    {
        private string[] _arrPropertiesEx = null;
        private string[] _default = new string[] { "Keyword", "Id" };
        private bool _isCheckAllStringProperties = false;
        private string[] _ignore = new string[] { "TableName", "DsColName" };

        public SQLInjectionCheckOperationAttribute(string[] arrPropertiesEx = null, bool isCheckAllStringProperties = false)
        {
            _arrPropertiesEx = arrPropertiesEx;
            _isCheckAllStringProperties = isCheckAllStringProperties;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context != null)
            {
                foreach (var argument in context.ActionArguments)
                {
                    if (argument.Value != null)
                    {
                        foreach (var prop in argument.Value.GetType().GetProperties())
                        {
                            if (_isCheckAllStringProperties)
                            {
                                if (prop.PropertyType == typeof(string))
                                {
                                    var val = prop.GetValue(argument.Value, null);

                                    if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                                    {
                                        continue;
                                    }

                                    if (!_ignore.Contains(prop.Name) && SecurityHelper.CheckForSQLInjection(val.ToString()))
                                    {
                                        var errorResult = new ErrorResult
                                        {
                                            ErrorCode = CommonErrors.InvalidFormat,
                                            ErrorMessage = ErrorHelpers.GetCommonErrorMessage(CommonErrors.InvalidFormat)
                                        };

                                        throw new BaseException(errorResult, (int)HttpStatusCode.BadRequest);
                                    }
                                }
                            }
                            else
                            {
                                var val = prop.GetValue(argument.Value, null);

                                if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                                {
                                    continue;
                                }

                                if ((_default.Contains(prop.Name) ||
                                (_arrPropertiesEx != null && _arrPropertiesEx.Contains(prop.Name)))
                                && SecurityHelper.CheckForSQLInjection(val.ToString()))
                                {
                                    var errorResult = new ErrorResult
                                    {
                                        ErrorCode = CommonErrors.InvalidFormat,
                                        ErrorMessage = ErrorHelpers.GetCommonErrorMessage(CommonErrors.InvalidFormat)
                                    };

                                    throw new BaseException(errorResult, (int)HttpStatusCode.BadRequest);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

