using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Attributes
{
    public class ExtractCustomHeaderAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            const string HeaderKeyName = "FilterHeaderKey";
            context.HttpContext.Request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue);
            if (context.HttpContext.Items.ContainsKey(HeaderKeyName))
            {
                context.HttpContext.Items[HeaderKeyName] = headerValue;
            }
            else
            {
                context.HttpContext.Items.Add(HeaderKeyName, $"{headerValue}-received");
            }
        }
    }
}
