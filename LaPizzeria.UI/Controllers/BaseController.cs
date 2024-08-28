﻿using LaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace LaPizzeria.UI.Controllers
{
    public class BaseController : Controller
    {
        public UserModel CurrentUser
        {
            get
            {
                if (User.Claims.Count() > 0)
                {
                    string userData = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.UserData).Value;
                    return JsonSerializer.Deserialize<UserModel>(userData);
                }
                return null;
            }
        }
    }
}
