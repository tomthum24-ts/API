using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCommon.Model
{
    public class ActionModel : DisposableModel
    {
        public ActionModel(Action action)
        {
            Action = action;
        }

        public string Id { get; set; } = Guid.NewGuid().ToString("N");
        public Action Action { get; set; }
    }
}
