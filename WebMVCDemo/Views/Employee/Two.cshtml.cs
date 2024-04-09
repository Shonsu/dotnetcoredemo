using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebMVCDemo.Views.Employee
{
    public class Two : PageModel
    {
        private readonly ILogger<Two> _logger;

        public Two(ILogger<Two> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}