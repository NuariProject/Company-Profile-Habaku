using CMS_Dashboard_v1.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMS_Dashboard_v1.Service
{
    public class GeneralService
    {
        public List<DropdownModel> DropdownStatus()
        {
            Dammy getvalue = new Dammy();
            var Value = getvalue.StatusList();

            return Value;
        }
    }
}
