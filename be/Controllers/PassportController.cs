using System;
using lavax711.Models;
using Microsoft.AspNetCore.Mvc;

namespace lavax711.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassportController:ControllerApiBase
    {
        [HttpPost("login")]
        public JsonResult Login(LoginRequest req)
        {
            if(req.email == "lavax711@qq.com" && req.password == "123456")
            {
                return Output(new LoginResponse
                {
                    token = "123456789",
                    username = "lavax711",
                    email = req.email,
                    avatar = "https://ng-alain.com/assets/img/logo-color.svg"
                });
            }
            throw new Exception("无效用户");
        }
    }
}