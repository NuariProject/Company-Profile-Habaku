using CMS_Dashboard_v1.Models;
using CMS_Dashboard_v1.Models.ModelForm;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CMS_Dashboard_v1.Service
{
    public class GeneralService
    {
        public async Task<IList<DropdownModel>> DropdownMenu()
        {
            Dammy getvalue = new Dammy();
            var Value =  await getvalue.MenuList();

            return Value;
        }

        public List<DropdownModel> Dropdown()
        {
            Dammy getvalue = new Dammy();
            var Value = getvalue.StatusList();

            return Value;
        }
    }
}
