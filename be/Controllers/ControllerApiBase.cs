using Microsoft.AspNetCore.Mvc;

namespace lavax711.Controllers
{
    public abstract class ControllerApiBase:ControllerBase
    {
        public JsonResult Output(object data,string msg="ok")
        {
            return new JsonResult(new 
            {
                msg,
                data
            });
        }
    }
}