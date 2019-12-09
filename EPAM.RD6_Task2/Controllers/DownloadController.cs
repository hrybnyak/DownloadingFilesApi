using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
namespace EPAM.RD6_Task2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly DownloadingFilesService _service = new DownloadingFilesService();
        [HttpGet]
        [Route("sync")]
        public ActionResult DownloadFilesSync()
        {
            try
            {
                TimeSpan time = _service.DownloadFiles();
                _logger.Info("User called synchronous controller and it finished in " + time.TotalMilliseconds + " ms.");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("async")]
        public async Task<ActionResult> DownloadFilesAsync()
        {
            try
            {
                TimeSpan time = await _service.DownloadFilesAsync();
                _logger.Info("User called asynchronous controller and it finished in " + time.TotalMilliseconds + " ms.");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("parallel")]
        public ActionResult DownloadFilesParallel()
        {
            try
            {
                TimeSpan time = _service.DownloadFilesParallel();
                _logger.Info("User called parallel controller and it finished in " + time.TotalMilliseconds + " ms.");
                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}