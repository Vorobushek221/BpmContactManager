using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BpmContactManager.Models.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddServerOffset(this DateTime dateTime)
        {
            return dateTime.ToLocalTime().AddHours(GlobalConstants.ServerOffsetFromUtc);
        }
    }
}