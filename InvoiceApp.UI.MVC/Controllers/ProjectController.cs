using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.ViewModels.Project;
using InvoiceApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _projectService.GetAll());
        }

        public IActionResult Add()
        {
            return View(model: new ProjectViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProjectViewModel projectViewModel)
        {
            var result = await _projectService.Add(projectViewModel);

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

            return View(projectViewModel);
        }

        public async Task<IActionResult> Delete(int projectId)
        {
            var vm = await _projectService.Get(projectId);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ProjectViewModel projectViewModel)
        {
            var result = await _projectService.Delete(projectViewModel);

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

            return View(projectViewModel);
        }

        public async Task<IActionResult> Update(int projectId)
        {
            var vm = await _projectService.Get(projectId);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProjectViewModel projectViewModel)
        {
            var result = await _projectService.Update(projectViewModel);

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

            return View(projectViewModel);
        }
    }
}
