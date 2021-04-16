﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.IO;

using GTGrimServer.Database.Controllers;
using GTGrimServer.Filters;
using GTGrimServer.Controllers;
using GTGrimServer.Services;
using GTGrimServer.Models;
using GTGrimServer.Database.Tables;

namespace GTGrimServer.Helpers
{
    /// <summary>
    /// Handles profile related requests.
    /// </summary>
    [ApiController]
    [Authorize]
    [PDIClient]
    [Route("[controller]")]
    [Produces("application/xml")]
    public class UserController : GrimControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserDBManager _userDb;

        public UserController(PlayerManager players, ILogger<UserController> logger, UserDBManager userDb)
            : base(players)
        {
            _logger = logger;
            _userDb = userDb;
        }

        /// <summary>
        /// GT5 user fetch
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId:long}.xml")]
        public ActionResult Get(long userId)
        {
            var user = new UserProfile();
            return Ok(user);
        }

        /// <summary>
        /// GT6 user fetch
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userName}.xml")]
        public ActionResult Get(string userName)
        {
            var currentPlayer = Player;
            if (currentPlayer is null)
            {
                _logger.LogWarning("Unable to get current player for host '{host}'", Request.Host);
                return Unauthorized();
            }

            if (currentPlayer?.Data?.PSNName?.Equals(userName) is true)
            {
                var user = UserProfile.FromDatabaseObject(currentPlayer.Data);
                return Ok(user);
            }
            else
            {
                // TODO: Get other user
                _logger.LogWarning("Got unexpected profile request for '{userName}'", userName);
                return BadRequest();
            }
        }
    }
}
