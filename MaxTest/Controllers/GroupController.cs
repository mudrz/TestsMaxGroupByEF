using System.Collections.Generic;
using System.Linq;
using MaxTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MaxTest.Controllers
{

    [Route("api/[controller]")]
    public class GroupController : Controller
    {
        private readonly LiteContext _context;
        private readonly ILogger<GroupController> _logger;
        private readonly EventId EVENT_ID = new EventId(444, "GroupController");

        public GroupController(LiteContext context, ILogger<GroupController> logger)
        {
            _context = context;
            _logger = logger;
        }
        private void Log(string message)
        {
            _logger.LogCritical(EVENT_ID, $"``````````````````````````````````````````");
            _logger.LogCritical(EVENT_ID, message);
            _logger.LogCritical(EVENT_ID, $"``````````````````````````````````````````");
        }
        private void Log(int route)
        {
            _logger.LogCritical(EVENT_ID, $"``````````````````````````````````````````");
            _logger.LogCritical(EVENT_ID, $"ROUTE {route}");
            _logger.LogCritical(EVENT_ID, $"``````````````````````````````````````````");
        }
        /// <summary>
        /// Separate query to DB is launched for each parent to pull children
        /// </summary>
        [HttpGet("1")]
        public IActionResult Get1()
        {
            Log(1);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                Bars = p.Children
                        .GroupBy(c => c.Group)
                        .Select(g => new BarVm
                        {
                            Group = g.Key,
                            GroupCount = g.Count()
                        }),
            }));
        }
        /// <summary>
        /// Separate query to DB is launched for each parent to pull children
        /// </summary>
        [HttpGet("2")]
        public IActionResult Get2()
        {
            Log(2);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                Bars = p.Children
                        .Select(c => c.Group)
                        .GroupBy(g => g)
                        .Select(g => new BarVm
                        {
                            Group = g.Key,
                            GroupCount = g.Count()
                        }),
                ChildrenCount = p.Children.Count,
            }));
        }
        /// <summary>
        /// Separate query to DB is launched for each parent to pull children
        /// </summary>
        [HttpGet("3")]
        public IActionResult Get3()
        {
            Log(3);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                Bars = p.Children
                        .Select(c => c.Group)
                        .GroupBy(g => g)
                        .Select(g => new BarVm
                        {
                            Group = g.Key,
                        }),
            }));
        }

        /// <summary>
        /// Separate query to DB is launched for each parent to pull children
        /// </summary>
        [HttpGet("4")]
        public IActionResult Get4()
        {
            Log(4);
            return Ok(_context.Parents.Select(p => new
            {
                Groups = p.Children.Select(c => c.Group).ToList(),
                Parent = p
            }).Select(s => new FooVm
            {
                ParentId = s.Parent.Id,
                Bars = s.Groups
                        .GroupBy(g => g)
                        .Select(g => new BarVm
                        {
                            Group = g.Key,
                        }),
            }));
        }
        /// <summary>
        /// All children props are pulled in memory and then grouped
        /// expected
        /// </summary>
        [HttpGet("5")]
        public IActionResult Get5()
        {
            Log(5);
            return Ok(_context.Children
                            .GroupBy(g => g.Group)
                            .Select(g => new BarVm
                            {
                                Group = g.Key,
                                GroupCount = g.Count()
                            })
            );
        }
        /// <summary>
        /// Only Child.Gruop is pulled in memory and then grouped
        /// expected
        /// </summary>
        [HttpGet("6")]
        public IActionResult Get6()
        {
            Log(6);
            return Ok(_context.Children
                            .Select(c => c.Group)
                            .GroupBy(g => g)
                            .Select(g => new BarVm
                            {
                                Group = g.Key,
                                GroupCount = g.Count()
                            })
            );
        }
    }
}