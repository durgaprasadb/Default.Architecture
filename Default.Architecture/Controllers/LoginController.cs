﻿using Default.Architecture.Authentication;
using Domain;
using Domain.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Default.Architecture.Controllers
{
    [Route("api/login")]
    public class LoginController : Controller
    {
        IAuthenticator<ICredential> authenticator;
        public LoginController(IAuthenticator<ICredential> authenticator)
        {
            this.authenticator = authenticator;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CredentialDto user)
        {
            return Json(await authenticator.Login(user).Select(token => new { token }));
        }
    }
}
