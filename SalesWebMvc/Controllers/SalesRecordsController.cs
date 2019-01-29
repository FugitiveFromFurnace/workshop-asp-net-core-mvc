using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService) => this._salesRecordService = salesRecordService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? minDate, DateTime? maxDate)
        {
            var tupla = ValidateDate(minDate, maxDate);

            ViewData["minDate"] = tupla.minDate.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = tupla.maxDate.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateAsync(minDate, maxDate);  

            return View(result);
        }


        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            var tupla = ValidateDate(minDate, maxDate);

            ViewData["minDate"] = tupla.minDate.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = tupla.maxDate.ToString("yyyy-MM-dd");

            var result = await _salesRecordService.FindByDateGroupingAsync(minDate, maxDate);

            return View(result);
        }

        private (DateTime minDate, DateTime maxDate) ValidateDate(DateTime? minDate, DateTime? maxDate) => (minDate ?? new DateTime(DateTime.Now.Year, 1, 1), maxDate ?? DateTime.Now);
    }
}