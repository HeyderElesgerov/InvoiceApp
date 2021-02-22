using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clientService.GetAll());
        }

        public IActionResult Add()
        {
            return View(model: new ClientViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ClientViewModel clientViewModel)
        {
            var result = await _clientService.Add(clientViewModel);

            if (result.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    string propertyName = error.PropertyName?.Split('.')?.LastOrDefault();
                    if (propertyName == null) propertyName = "";

                    ModelState.AddModelError(propertyName, error.ErrorMessage);
                }
            }

            return View(clientViewModel);
        }

        public async Task<IActionResult> Delete(int clientId)
        {
            var vm = await _clientService.Get(clientId);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ClientViewModel clientViewModel)
        {
            var result = await _clientService.Delete(clientViewModel);

            if (result.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
            }

            return View(clientViewModel);
        }

        public async Task<IActionResult> Update(int clientId)
        {
            var vm = await _clientService.Get(clientId);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClientViewModel clientViewModel)
        {
            var result = await _clientService.Update(clientViewModel);

            if (result.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    string propertyName = error.PropertyName?.Split('.')?.LastOrDefault();
                    if (propertyName == null) propertyName = "";

                    ModelState.AddModelError(propertyName, error.ErrorMessage);
                }
            }

            return View(clientViewModel);
        }
    }
}
