﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Xml.Serialization;
using System.IO;

using GTGrimServer.Sony;
using GTGrimServer.Models;

namespace GTGrimServer.Helpers
{
    /// <summary>
    /// Handles profile related requests.
    /// </summary>
    [ApiController]
    [Route("/ap/[controller]/")]
    [Produces("application/xml")]
    public class ProfileController : ControllerBase
    {
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(ILogger<ProfileController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<GrimResult> Post()
        {
            GrimRequest requestReq = await GrimRequest.Deserialize(Request.Body);
            if (requestReq is null)
            {
                // Handle
                return GrimResult.FromInt(0);
            }

            return GrimResult.FromInt(1);
        }

    }
}