﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac.Integration.WebApi;
using JamesAlcaraz.NlayeredAppDemo.Application.InfrastructureServices.Authentication;
using JamesAlcaraz.NlayeredAppDemo.WebApi.Models;
using JamesAlcaraz.NlayeredAppDemo.WebApi.Repository;
using Microsoft.AspNet.Identity;

namespace JamesAlcaraz.NlayeredAppDemo.WebApi.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController()
        {
            
        }
        public AccountController(IAuthenticationService authService)
        {
            _authenticationService = authService;
        }

        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserRegistrationInput userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserRegistrationResult result = await _authenticationService.RegisterUser(userModel);

            IHttpActionResult errorResult = GetErrorResult(result);

            if (errorResult != null)
            {
                return errorResult;
            }

            return Ok(result);
        }

        private IHttpActionResult GetErrorResult(UserRegistrationResult result)
        {
            if (result == null)
                return InternalServerError();

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                    return BadRequest();

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
