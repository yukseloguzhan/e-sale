using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.ViewComponents
{
    public class ESaleFeaturesViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            return View();
        }



    }
}
