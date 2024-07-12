using Microsoft.AspNetCore.Mvc;
using Models.Customer.Entities;
using Teste_Crud_Mvc.Services.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Teste_Crud_Mvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(
            ILogger<CustomerController> logger,
            ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        public IActionResult Upsert([FromRoute] string id)
        {
            if (!string.IsNullOrEmpty(id))
                return View(_customerService.GetById(id));

            return View();
        }

        public IActionResult List()
        {
            var result = _customerService.List();
            if (!result.Success)
                ViewBag.ErrorMessage = result.Message;

            return View(result);
        }

        public IActionResult Save([FromForm] Customer data)
        {
            var result = string.IsNullOrEmpty(data?.Id) ?
                _customerService.Create(data) :
                _customerService.Update(data);
            if (result.Success)
                return Redirect("/");

            ViewBag.ErrorMessage = result.Message;
            return View("Upsert");
        }

        public IActionResult Delete([FromRoute] string id)
        {
            var result = _customerService.Delete(id);
            if (!result.Success)
                ViewBag.ErrorMessage = result.Message;

            return Redirect("/");
        }
    }
}
