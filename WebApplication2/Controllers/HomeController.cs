using System.Text;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    //https://stackoverflow.com/questions/6290053/setting-access-control-allow-origin-in-asp-net-mvc-simplest-possible-method
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowCrossSiteJson]
        public JsonResult GetScript()
        {
            //keep following line, this works
            //var str = "<script type='text/javascript'>alert('happy')</script>";
            var builder = new StringBuilder();
            builder.Append("<script type='text/javascript'>");
            builder.Append("$(document).on('click', '#hook', function (e) { "
                + "var tag = document.evaluate('.//div/h1', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;"
                + "var html = tag.innerHTML;"
                + "$.ajax({type: 'POST',url: 'http://localhost:54738/Home/GetData', data: { 'productname': html}});"
                + "})");
            builder.Append("</script>");
            //todo, encode the string utf8
            var str = builder.ToString();
            return Json(new { error = false, script = str });
        }


        //if this method gets hit, it means we manage to get extracted html back from vendors
        [HttpPost]
        [AllowCrossSiteJson]
        public JsonResult GetData(string productname)
        {
            return Json(new { error = false });
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}