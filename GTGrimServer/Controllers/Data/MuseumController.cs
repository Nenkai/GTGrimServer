﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.IO;

namespace GTGrimServer.Controllers
{
    [ApiController]
    [Route("/data2/[controller]")]
    [Produces("application/xml")]
    public class MuseumController : ControllerBase
    {
        private readonly ILogger<MuseumController> _logger;

        public MuseumController(ILogger<MuseumController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{server}/{region}/museum_{fileRegion}_{arg}.xml")]
        public async Task GetMuseum(string server, string region, string fileRegion, string arg)
        {
            if (arg.StartsWith("l_") && arg.Length > 2 && int.TryParse(arg.AsSpan(2), out int listId))
                await GetMuseumList(server, region, fileRegion, listId);
            else if (arg.Length > 0 && int.TryParse(arg, out int itemId))
                await GetMuseumItem(server, region, fileRegion, itemId);
            else
            {
                // Handle issue
            }
            
        }

        private async Task GetMuseumList(string server, string region, string fileRegion, int listId)
        {
            string museumListFile = $"Resources/data2/museum/{server}/{region}/museum_{fileRegion}_l_{listId}.xml";
            if (!System.IO.File.Exists(museumListFile))
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            using var fs = System.IO.File.OpenRead(museumListFile);
            await fs.CopyToAsync(Response.Body);
        }

        public async Task GetMuseumItem(string server, string region, string fileRegion, int itemId)
        {
            string museumItemFile = $"Resources/data2/museum/{server}/{region}/museum_{fileRegion}_{itemId}.xml";
            if (!System.IO.File.Exists(museumItemFile))
            {
                Response.StatusCode = StatusCodes.Status404NotFound;
                return;
            }

            using var fs = System.IO.File.OpenRead(museumItemFile);
            await fs.CopyToAsync(Response.Body);
        }
    }
}
