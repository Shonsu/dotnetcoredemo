using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Signers;
using WebMVCDemo.Services;


namespace WebMVCDemo.Controllers
{

    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IAuthorizationService _authorizationService;

        public EmployeeController(IEmployeeService employeeService, IAuthorizationService authorizationService)
        {
            this._employeeService = employeeService;
            this._authorizationService = authorizationService;

        }

        // GET: /<controller>/
        [AllowAnonymous]
        public IActionResult Index()
        {
            // ViewBag.Something = "Something";
            ViewData["Name"] = new List<string>() { "CS 4540", "students" };
            ViewData["Surname"] = new List<string>() { "cat", "elephant" };

            return View(_employeeService.GetEmployees());
        }
        public async Task<IActionResult> Details(int id, int a, int b)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }
            ViewBag.A = a;
            ViewBag.B = b;
            return View(_employeeService.GetEmployee(id));
        }

        // Action: Display Form: doGet()
        [Authorize(Policy = "IsAdmin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Action: Process form Submission: doPost()
        [Authorize(Policy = "IsAdmin")]
        [HttpPost]
        public IActionResult Add(EmployeeDto employee)
        {
            Console.WriteLine(employee);
            _employeeService.AddEmployee(new Models.Employee(employee.FirstName, employee.LastName, employee.DateHired, employee.Department));
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, id, "CanAccessEmployee");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }
            ViewBag.Supervisors = _employeeService.GetEmployees()
                .Select(e => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem($"{e.FirstName} {e.LastName}", e.Id.ToString()))
                .ToList();
            return View(_employeeService.GetEmployee(id));
        }

        // Action: Process form Submission: doPost()
        [HttpPost]
        public async Task<IActionResult> UpdateAsync(EmployeeDto employee)
        {
            var authResult = await _authorizationService.AuthorizeAsync(User, employee.Id, "CanAccessEmployee");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }
            Console.WriteLine(employee);
            _employeeService.UpdateEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            System.Console.WriteLine("test kutwa");
            return View();
        }

        [HttpGet]
        public IActionResult Two(int id1, int id2)
        {
            var emp1 = _employeeService.GetEmployee(id1);
            var emp2 = _employeeService.GetEmployee(id2);
            return View((emp1, emp2));
        }
    }
}
