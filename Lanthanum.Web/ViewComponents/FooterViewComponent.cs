using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Views.Shared
{
    [ViewComponent(Name = "FooterViewComponent")]
    public class FooterViewComponent : ViewComponent
    {

        public FooterViewComponent()
        {

        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var FooterTabItems = new List<FooterTabItem>()
            {
                new FooterTabItem{ Id = 1, Name = "ItemCompanyInfo1", Category = "CompanyInfo", Link="#", IsDisplaying = true },
                new FooterTabItem{ Id = 2, Name = "ItemCompanyInfo2", Category = "CompanyInfo", Link="#", IsDisplaying = true },
                new FooterTabItem{ Id = 3, Name = "ItemCompanyInfo3", Category = "CompanyInfo", Link="#", IsDisplaying = true },
                new FooterTabItem{ Id = 4, Name = "ItemContributors1", Category = "Contributors", Link="#", IsDisplaying = true },
                new FooterTabItem{ Id = 5, Name = "ItemContributors2", Category = "Contributors", Link="#", IsDisplaying = true },
                new FooterTabItem{ Id = 6, Name = "ItemNewsletter1", Category = "Newsletter", Link="#", IsDisplaying = true }
            };
            return View("Footer.cshtml", FooterTabItems);
        }
    }
}
