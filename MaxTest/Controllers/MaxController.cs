using System.Collections.Generic;
using System.Linq;
using MaxTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MaxTest.Controllers
{

    [Route("api/[controller]")]
    public class MaxController : Controller
    {
        private readonly LiteContext _context;
        private readonly ILogger<MaxController> _logger;
        private readonly EventId EVENT_ID = new EventId(555, "MaxController");
        public MaxController(LiteContext context, ILogger<MaxController> logger)
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
        [HttpGet("1")]
        public IActionResult Get1()
        {
            Log(1);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                SumChildId = p.Children.Sum(c => c.Id),
                AverageChildId = p.Children.Average(c => c.Id),
                MaxChildId = p.Children.Max(c => c.Id),
            }));
        }

        [HttpGet("2")]
        public IActionResult Get2()
        {
            Log(2);
            return Ok(_context.Parents
                            .Select(p => new
                            {
                                MaxChildId = p.Children.Max(c => c.Id),
                                AverageChildId = p.Children.Average(c => c.Id),
                                Parent = p
                            })
                            .Select(g => new FooVm
                            {
                                ParentId = g.Parent.Id,
                                SumChildId = g.Parent.Children.Sum(c => c.Id),
                                AverageChildId = g.AverageChildId,
                                MaxChildId = g.MaxChildId,
                            })
                    );
        }

        [HttpGet("3")]
        public IActionResult Get3()
        {
            Log(3);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                SumChildId = p.Children.Sum(c => c.Id),
                MaxChildId = p.Children.Count == 0 ? 0 : p.Children.Max(c => c.Id),
                AverageChildId = p.Children.Count == 0 ? 0 : p.Children.Average(c => c.Id)
            }));
        }
        [HttpGet("4")]
        public IActionResult Get4()
        {
            Log(4);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                SumChildId = p.Children.Sum(c => c.Id),
                MaxChildId = p.Children.Count == 0 ? 0 : p.Children.Max(c => (int?)c.Id) ?? 77,
                AverageChildId = p.Children.Count == 0 ? 0 : p.Children.Average(c => (int?)c.Id) ?? 55,
            }));
        }
        [HttpGet("5")]
        public IActionResult Get5()
        {
            Log(5);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                SumChildId = p.Children.Sum(c => c.Id),
                MaxChildId = p.Children.Max(c => (int?)c.Id) ?? 77,
                AverageChildId = p.Children.Average(c => (int?)c.Id) ?? 55,
            }));
        }



        [HttpGet("6")]
        public IActionResult Get6()
        {
            Log(6);
            return Ok(_context.Parents.Select(p => new FooVm
            {
                ParentId = p.Id,
                SumChildId = p.Children.Sum(c => c.Id),
                MaxChildId = p.Children.Max(c => (int?)c.Id) ?? 77,
                Bars = p.Children
                        .GroupBy(c => c.Group)
                        .Select(g => new BarVm
                        {
                            Group = g.Key,
                            GroupCount = g.Count()
                        }),
                ChildrenCount = p.Children.Count,

                AverageChildId = p.Children.Average(c => (int?)c.Id) ?? 55,
            }));
        }
        [HttpGet("7")]
        public IActionResult Get7()
        {
            Log(7);
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
        [HttpGet("8")]
        public IActionResult Get8()
        {
            Log(8);
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
        [HttpGet("9")]
        public IActionResult Get9()
        {
            Log(9);
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
        [HttpGet("10")]
        public IActionResult Get10()
        {
            Log(10);
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
    }
}