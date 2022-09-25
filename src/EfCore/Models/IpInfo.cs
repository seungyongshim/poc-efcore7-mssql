using System;
using System.Collections.Generic;

#nullable disable

namespace EfCore.Models
{
    public partial class IpInfo
    {
        public int IpInfoId { get; set; }
        public string IpAddress { get; set; }
        public short? GrantSend { get; set; }
        public int? UserInfoId { get; set; }
        public bool? PermissionYn { get; set; }
        public bool? UseYn { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
