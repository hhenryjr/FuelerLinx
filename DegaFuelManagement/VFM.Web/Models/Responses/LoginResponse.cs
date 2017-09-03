using Degatech.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Responses
{
    public class LoginResponse<T> : BaseResponse
    {
        public List<T> Items { get; set; }
    }
}