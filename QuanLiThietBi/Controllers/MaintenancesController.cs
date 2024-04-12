using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLiThietBi.Application.Interfaces;
using QuanLiThietBi.Infrastructure.Repositories;

namespace QuanLiThietBi.Controllers
{
    public class MaintenancesController : Controller
    {
        private readonly IRepository<TblMaintenance> _maintenanceRepository;
        // GET: MaintenancesController
        public MaintenancesController(IRepository<TblMaintenance> maintenanceRepository)
        {
            _maintenanceRepository = maintenanceRepository;
        }
        public async Task<IActionResult> Index()
        {
            var maintenances = await _maintenanceRepository.GetAll();
            return View(maintenances);
        }

        // GET: MaintenancesController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var maintenance = await _maintenanceRepository.GetByID(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // GET: MaintenancesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MaintenancesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId, MaintenanceDate, Description, Status, AssignTo, CompletionDate")] TblMaintenance maintenance)
        {
            if (ModelState.IsValid)
            {
                maintenance.Status = 0; //Đang bảo trì
                _maintenanceRepository.Add(maintenance);
                return RedirectToAction(nameof(Index));
            }
            return View(maintenance);
        }

        // GET: MaintenancesController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var maintenance = await _maintenanceRepository.GetByID(id);
            if (maintenance == null)
            {
                return NotFound();
            }
            return View(maintenance);
        }

        // POST: MaintenancesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaintenanceId, ProductId, MaintenanceDate, Description, Status, AssignTo, CompletionDate")] TblMaintenance maintenance)
        {
            if (id != maintenance.MaintenanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _maintenanceRepository.Update(maintenance);
                return RedirectToAction(nameof(Index));
            }
            return View(maintenance);
        }

        // GET: MaintenancesController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var maintenance = await _maintenanceRepository.GetByID(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            return View(maintenance);
        }

        // POST: MaintenancesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _maintenanceRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
