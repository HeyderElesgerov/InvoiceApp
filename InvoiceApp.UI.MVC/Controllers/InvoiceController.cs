using ClosedXML.Excel;
using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.ViewModels.Invoice;
using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using InvoiceApp.UI.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IClientService _clientService;
        private readonly IProjectService _projectService;
        private static IEnumerable<InvoiceViewModel> _invoiceViewModelsToExport;

        public InvoiceController(IInvoiceService invoiceService, IClientService clientService, IProjectService projectService)
        {
            _invoiceService = invoiceService;
            _clientService = clientService;
            _projectService = projectService;
        }

        public async Task<IActionResult> Index()
        {
            FilteredInvoicesViewModel filteredInvoicesViewModel = await GetFilteredInvoicesVM(null, null, null, null);
            return View(filteredInvoicesViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FilteredInvoicesViewModel filteredInvoicesViewModel)
        {
            int? projectId = filteredInvoicesViewModel.SelectedProjectId;
            int? clientId = filteredInvoicesViewModel.SelectedClientId;
            DateTime? from = filteredInvoicesViewModel.From;
            DateTime? to = filteredInvoicesViewModel.To;

            return View(await GetFilteredInvoicesVM(from, to, projectId, clientId));
        }

        public async Task<IActionResult> Add()
        {
            return View(await GetCreateInvoiceVM(new InvoiceViewModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateInvoiceVM createInvoiceVM)
        {
            var result = await _invoiceService.Add(createInvoiceVM.InvoiceViewModel);

            if (result.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach(var error in result.Errors)
            {
                string propertyName = error.PropertyName.Split('.').LastOrDefault();
                if (propertyName == null) propertyName = "";

                ModelState.AddModelError(propertyName, error.ErrorMessage);
            }

            return View(await GetCreateInvoiceVM(createInvoiceVM.InvoiceViewModel));
        }

        public async Task<IActionResult> Update(int id)
        {
            var vm = _invoiceService.Get(id);

            if (vm == null)
                return NotFound();

            return View(await GetCreateInvoiceVM(vm));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CreateInvoiceVM createInvoiceVM)
        {
            var result = await _invoiceService.Update(createInvoiceVM.InvoiceViewModel);

            if (result.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                string propertyName = error.PropertyName.Split('.').LastOrDefault();
                if (propertyName == null) propertyName = "";

                ModelState.AddModelError(propertyName, error.ErrorMessage);
            }

            return View(await GetCreateInvoiceVM(createInvoiceVM.InvoiceViewModel));
        }

        public IActionResult Excel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Invoices");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Invoice Date";
                worksheet.Cell(currentRow, 3).Value = "Due Date";
                worksheet.Cell(currentRow, 4).Value = "Client";
                worksheet.Cell(currentRow, 5).Value = "Project";
                worksheet.Cell(currentRow, 6).Value = "Net Amount";
                worksheet.Cell(currentRow, 7).Value = "Tax Amount";
                worksheet.Cell(currentRow, 8).Value = "Status";

                foreach (var invoice in _invoiceViewModelsToExport)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = invoice.Id;
                    worksheet.Cell(currentRow, 2).Value = invoice.InvoiceDate;
                    worksheet.Cell(currentRow, 3).Value = invoice.DueDate;
                    worksheet.Cell(currentRow, 4).Value = invoice.ClientName;
                    worksheet.Cell(currentRow, 5).Value = invoice.ProjectName;
                    worksheet.Cell(currentRow, 6).Value = invoice.NetAmount;
                    worksheet.Cell(currentRow, 7).Value = invoice.TaxAmount;
                    worksheet.Cell(currentRow, 8).Value = invoice.Status;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "invoices.xlsx");
                }
            }
        }

        private async Task<CreateInvoiceVM> GetCreateInvoiceVM(InvoiceViewModel invoiceViewModel)
        {
            var invoiceVM = new CreateInvoiceVM(invoiceViewModel);
            invoiceVM.SetProjects(await _projectService.GetAll());
            invoiceVM.SetClients(await _clientService.GetAll());

            return invoiceVM;
        }
        private async Task<FilteredInvoicesViewModel> GetFilteredInvoicesVM(DateTime? from, DateTime? to, int? projectId, int? clientid)
        {
            var invoices = await _invoiceService.GetAllIncludingClientAndProject(projectId, clientid, from, to);
            _invoiceViewModelsToExport = invoices;
            var clients = await _clientService.GetAll();
            var projects = await _projectService.GetAll();
            decimal net = _invoiceService.CalculateTotalNetAmount(invoices);
            decimal tax = _invoiceService.CalculateTotalTaxAmount(invoices);

            FilteredInvoicesViewModel filteredInvoicesViewModel = new FilteredInvoicesViewModel(invoices, net, tax, from, to, clientid, clients, projectId, projects);

            return filteredInvoicesViewModel;
        }
    }
}
