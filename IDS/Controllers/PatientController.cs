using IDS.Models;
using IDS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDS.Controllers
{
    public class PatientController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IEnumerable<Patient> Patients;
        private readonly IEnumerable<string> PatientsIds;
        private readonly IEnumerable<Ticket> tickets;

        public PatientController(AppDbContext context) 
        {
            _context = context;
            Patients = _context.patients.ToList();
            PatientsIds = Patients.Select(p => p.PatientId).ToList();
            tickets = _context.Tickets.ToList();
        }

        // Show all patients
        public async Task<IActionResult> Index()
        {
            var patients = await _context.patients.ToListAsync();
            return View(patients);
        }

        // Partial view for AJAX or modal loading
        [HttpGet]
        public async Task<IActionResult> ShowAll()
        {

            ViewBag.controllerName = "Patient";
            ViewBag.entity = "مريض";
            ViewBag.theEntity = "المريض";
            ViewBag.pluralEntity = "مرضي";
            ViewBag.thePluralEntity = "المرضي";
            ViewBag.placeHolder = "بدر أحمد";

            var patients = await _context.patients.ToListAsync();
            return PartialView("_ShowAllPatients", patients);
        }

    


        public async Task<IActionResult> ShowPatientProfile(string id)
        {
            ViewBag.controllerName = "Ticket";
            ViewBag.entity = "تذكرة";
            ViewBag.theEntity = "التذكرة";
            ViewBag.pluralEntity = "تذاكر";
            ViewBag.thePluralEntity = "التذكرة";
            ViewBag.placeHolder = "171155";

            if (id == null)
            {
                return NotFound();
            }

            if (!Patients.Select(t => t.PatientId).Contains(id))
            {
                TempData["Error"] = "عذرا , هذا المريض غير موجود";
                return RedirectToAction(nameof(Index));
            }

            PatientProfileVm ppv = new PatientProfileVm();

            Patient patient = await _context.patients
                .AsNoTracking()
                .Include(p => p.Tickets)
                .ThenInclude(t => t.ReferredTo)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            ppv.PatientId = patient.PatientId;
            ppv.Name = patient.Name;
            ppv.Address = patient.Address;
            ppv.phoneNumber = patient.phoneNumber;
            ppv.Gender = patient.Gender;
            ppv.Age = patient.Age;
            ppv.Tickets = patient.Tickets.ToList();

            return View("PatientProfile", ppv);

        }

        // Details
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.patients
                .Include(p => p.Tickets)
                .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // Create GET
        public IActionResult Create()
        {
            return View();
        }

        // Create POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,Name,Address,profession,phoneNumber,Gender,Age")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // Edit GET
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var patient = await _context.patients
        .AsNoTracking()
        .FirstOrDefaultAsync(p => p.PatientId == id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PatientId,Name,Address,profession,phoneNumber,Gender,Age")] Patient patient)
        {

            if (id != patient.PatientId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    TempData["success"] = "تم تعديل بيانات المريض بنجاح";
                    // Make sure no tracked entity with the same key is causing issues
                    var local = _context.patients.Local.FirstOrDefault(e => e.PatientId == patient.PatientId);
                    if (local != null)
                    {
                        _context.Entry(local).State = EntityState.Detached;
                    }

                    // Now attach the posted patient and mark as modified
                    _context.Attach(patient);
                    _context.Entry(patient).State = EntityState.Modified;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.PatientId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction("Index", "Admin", new { load = "Patient/ShowAll" });

            }

            TempData["Error"] = "فشل التعديل,الرجاء مراجعه البيانات و المحاوله مره أخري";
            return View(patient);
        }

        // Delete GET
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //        return NotFound();

        //    var patient = await _context.patients
        //        .FirstOrDefaultAsync(m => m.PatientId == id);

        //    if (patient == null)
        //        return NotFound();

        //    return View(patient);
        //}

        // Delete POST


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            if (id == null)
            {
                TempData["Error"] = "فشل الحذف,الرجاء مراجعه البيانات و المحاوله مره أخري";

                return RedirectToAction("Index", "Admin", new { load = "Patient/ShowAll" });
            }

       

            var patient = await _context.patients.FindAsync(id);
            if (patient != null)
            {
                _context.patients.Remove(patient);
                await _context.SaveChangesAsync();
            }

            TempData["success"] = "تم حذف المريض من قواعد البيانات بنجاح";

            // Redirect to main view and pass which partial to load  (as a query string)
            return RedirectToAction("Index", "Admin" , new { load = "Patient/ShowAll" });
        }


        public async Task<IActionResult> Search(string keyword)
        {
            IEnumerable<IDS.Models.Patient> patients;
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                patients = _context.patients.Where(u => u.Name.Contains(keyword));
            }
            else
            {
                patients = _context.patients;
            }
            return PartialView("_PatientsSearchResults", patients.ToList());
        }

        // Helper
        private bool PatientExists(string id)
        {
            return _context.patients.Any(e => e.PatientId == id);
        }
    }
}
