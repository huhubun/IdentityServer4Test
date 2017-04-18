using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Admin.Models.ClientViewModels
{
    public class ClientViewModel : Client
    {
        [Display(Name = "客户端名称")]
        public new string ClientName { get; set; }
    }
}
