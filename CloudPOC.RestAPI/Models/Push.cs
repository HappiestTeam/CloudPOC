using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudASPNETWebApi.Models
{
    public partial class Push
    {
        public virtual int cloudMesageId { get; set; }
        public virtual string cloudMessage { get; set; }
        public virtual bool result { get; set; }
    }
}