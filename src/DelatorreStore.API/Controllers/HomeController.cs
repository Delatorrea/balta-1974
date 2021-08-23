using System;
using Microsoft.AspNetCore.Mvc;

namespace DelatorreStore.API.Controllers
{
    public class HomeController : Controller
    {   
        [HttpGet]
        [Route("")]
        public object Get()
        {
            return new { version = "0.0.1" };
        }

        [HttpGet]
        [Route("error")]
        public string Error()
        {   
            throw new Exception("Erro erro ora ora");
            return "erro";
        }
    }
}