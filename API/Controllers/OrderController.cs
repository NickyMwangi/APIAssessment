using Business.IProcesses;
using Business.IProcesses.shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.WebPages;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController :  BaseController<OrderController, IOrderProcess>
    {
        private readonly IOptionsProcess _options;
        private readonly ILogger<OrderController> _logger;
        public OrderController(IOrderProcess context, IOptionsProcess options, ILogger<OrderController> logger) : base(context)
        {
            _options = options;
            _logger = logger;
        }


        [HttpGet]
        [Route("all")]
        public IActionResult Get()
        {
            var data = context.FilterQuery("");
            return Ok(data);

        }

        [HttpGet]
        [Route("sales")]
        public IActionResult OrderItems([FromQuery] int perPage, int page)
        {
            var data = context.CitySales(perPage, page);
            return Ok(data);

        }

        [HttpGet]
        [Route("late")]
        public IActionResult LateDelivery([FromQuery] int perPage, int page)
        {
            var data = context.LateDelivery(perPage, page);
            return Ok(data);

        }

        [HttpGet]
        [Route("early")]
        public IActionResult EarlyDelivery([FromQuery] int perPage, int page)
        {
            var data = context.EarlyDelivery(perPage, page);
            return Ok(data);

        }
        [HttpGet]
        [Route("combined")]
        public IActionResult CombinedDelivery([FromQuery] int perPage, int page)
        {
            var data = context.CombinedDelivery(perPage, page);
            return Ok(data);

        }

        [HttpPost]
        [Route("timer")]
        [AllowAnonymous]
        public IActionResult checkTime([FromBody] string CurrTime)
        {
            var data = context.TimeCheck(CurrTime, "");
            return Ok(data);

        }
    }
}
