using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trul.Application.UI.Core.Tasks;
using Trul.Application.UI.Task;

namespace Trul.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IHomeTask homeTask;

        public HomeController(IHomeTask homeTask) {
            this.homeTask = homeTask;
        }

        public ActionResult Index() {
            var model = homeTask.Index();
            return View(model);
        }
    }
}
