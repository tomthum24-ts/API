using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Common.ClaimUser
{
    public interface IUserSessionInfo
    {
        /// <summary>
        /// Gets current UserId or null.
        /// It can be null if no user logged in.
        /// </summary>
        int? ID { get; }


        string Name { get; }
        string UserName { get; }

    }
}
