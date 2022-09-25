using System;
using System.Collections.Generic;

#nullable disable

namespace EfCore.Models
{
    public partial class UserInfo
    {
        public UserInfo()
        {
            IpInfos = new HashSet<IpInfo>();
        }

        public int UserInfoId { get; set; }
        public string EmpNo { get; set; }
        public string CmpCode { get; set; }

        public virtual ICollection<IpInfo> IpInfos { get; set; }
    }
}
