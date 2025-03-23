using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageQueueGUI
{
    public static class UserSession
    {
        public static string Username { get; set; } = "";
        public static Guid AppId { get; set; } = Guid.Empty;
    }
}
