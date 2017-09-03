using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VFM.Web.Models.Responses
{
    public class ItemsResponse<Type> : SuccessResponse
    {
        public List<Type> Items { get; set; } = new List<Type>();
    }
}