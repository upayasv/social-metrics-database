using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UpayaWebApp
{
    public partial class PartnerAdmin_VModel : PartnerAdmin
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
    }
}