using IDS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDS.Controllers
{
    public class TicketController : Controller
    {


        private readonly AppDbContext _context;
        private readonly IEnumerable<Patient> Patients;
        private readonly IEnumerable<string> PatientsIds;
        private readonly IEnumerable<Ticket> tickets;



        public TicketController(AppDbContext context)
        {
            _context = context;
            Patients = _context.patients.ToList();
            PatientsIds = Patients.Select(p => p.PatientId).ToList();
            tickets = _context.Tickets.ToList();


        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateTicketForNewPatient()
        {
            return RedirectToAction("CreateTicketForNewPatient", "Reception");
        }


        public async Task<IActionResult> CreateTicketForExistingPatient(string id)
        {
            return RedirectToAction("CreateTicketForExistingPatient", "Reception");


        }
        // Creation of tickets is of reception controller acrtions 

        [HttpPost]
        public async Task<IActionResult> ShowFullTicket(string id)
        {
            return RedirectToAction("ShowFullTicket", "Reception" , new {id = id});

        }




        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                    .Include(t => t.MedicalHistory)
                    .Include(t => t.Patient)
                    .Include(t => t.ReferredTo)
                    .FirstOrDefaultAsync(t => t.TicketId == id); // ✅ Use FirstOrDefaultAsync

            string pId = ticket?.PatientId; // Step 2: Now it's safe to access PatientId


            if (ticket == null)
            {
                TempData["Error"] = "عذرا ; هذه التذكره غير موجوده";
            return RedirectToAction("ShowPatientProfile", "Patient", new { id = pId });
            }
            // ✅ Null Safety Checks
            var ticketVm = new TicketVM
            {


                // Patient Properties (Null check added)
                PatientId = ticket.PatientId,

                Name = ticket.Patient?.Name ?? "N/A",
                Address = ticket.Patient?.Address ?? "N/A",
                profession = ticket.Patient?.profession ?? "N/A",
                phoneNumber = ticket.Patient?.phoneNumber ?? "N/A",
                Gender = ticket.Patient?.Gender ?? "N/A",
                Age = ticket.Patient?.Age ?? 0,

                // Ticket Properties

                TicketID = ticket.TicketId,
                AppointmentDate = ticket.AppointmentDate,
                ChiefComlant = ticket.ChiefComlant,
                PrevisionalDiagnosis = ticket.PrevisionalDiagnosis,
                NextDate = ticket.NextDate,
                Status = ticket.Status,
                IsValid = ticket.IsValid,



        

                // Medical History Properties (Null check added)
                HeartTrouble = ticket.MedicalHistory?.HeartTrouble ?? false,
                Hyperttention = ticket.MedicalHistory?.Hyperttention ?? false,
                Pregnancy = ticket.MedicalHistory?.Pregnancy ?? false,
                Diabetes = ticket.MedicalHistory?.Diabetes ?? false,
                Hepatitis = ticket.MedicalHistory.Hepatitis,
                AIDs = ticket.MedicalHistory?.AIDs ?? false,
                Tuberculosis = ticket.MedicalHistory?.Tuberculosis ?? false,
                Allergies = ticket.MedicalHistory?.Allergies ?? false,
                Anemia = ticket.MedicalHistory?.Anemia ?? false,
                Rheumatism = ticket.MedicalHistory?.Rheumatism ?? false,
                RadTherapy = ticket.MedicalHistory?.RadTherapy ?? false,
                Haemophilia = ticket.MedicalHistory?.Haemophilia ?? false,
                AspirinIntake = ticket.MedicalHistory?.AspirinIntake ?? false,
                KidneyTroubles = ticket.MedicalHistory?.KidneyTroubles ?? false,
                Asthma = ticket.MedicalHistory?.Asthma ?? false,
                HayFever = ticket.MedicalHistory?.HayFever ?? false,
                MedicalHistoryText = ticket.MedicalHistory?.MedicalHistoryText ?? "N/A",

                // Referred To Properties (Null check added)
                Oral = ticket.ReferredTo?.Oral ?? false,
                RemovableProsth = ticket.ReferredTo?.RemovableProsth ?? false,
                Operative = ticket.ReferredTo?.Operative ?? false,
                Endodontic = ticket.ReferredTo?.Endodontic ?? false,
                Ortho = ticket.ReferredTo?.Ortho ?? false,
                CrownAndBridge = ticket.ReferredTo?.CrownAndBridge ?? false,
                Surgery = ticket.ReferredTo?.Surgery ?? false,
                Pedo = ticket.ReferredTo?.Pedo ?? false,
                XRay = ticket.ReferredTo?.XRay ?? false
            };

            return View(ticketVm);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id , TicketVM old)
        {


            

            //foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //{
            //    Console.WriteLine("Badrrrrrrrrr" + error.ErrorMessage);
            //    Console.WriteLine(error.ToString());
            //}


            var ticket = await _context.Tickets
               .Include(t => t.MedicalHistory)
               .Include(t => t.Patient)
               .Include(t => t.ReferredTo)
               .FirstOrDefaultAsync(t => t.TicketId == id); // ✅ Use FirstOrDefaultAsync

            string pId = ticket?.PatientId; // Step 2: Now it's safe to access PatientId
            
                if (ticket == null)
                {
                TempData["Error"] = "فشل التعديل, الرجاء مراجعه البيانات و المحاوله مره أخري";
                return View(old);
                //  return NotFound();
            }

            if (ModelState.IsValid)
            {

                // ✅ Null Safety Checks

                // Editable patient properties
                ticket.PatientId = old.PatientId;

                // Ticket Properties
                // ticket.PatientId = old.PatientId;
                ticket.AppointmentDate = old.AppointmentDate;
                ticket.ChiefComlant = old.ChiefComlant;
                ticket.PrevisionalDiagnosis = old.PrevisionalDiagnosis;
                ticket.NextDate = old.NextDate;
                ticket.Status = old.Status;
                ticket.IsValid = old.IsValid;


                // Medical History Properties (Null check added)
                ticket.MedicalHistory.HeartTrouble = old.HeartTrouble;
                ticket.MedicalHistory.Hyperttention = old.Hyperttention;
                ticket.MedicalHistory.Pregnancy = old.Pregnancy;
                ticket.MedicalHistory.Diabetes = old.Diabetes;
                ticket.MedicalHistory.Hepatitis = old.Hepatitis;
                ticket.MedicalHistory.AIDs = old.AIDs;
                ticket.MedicalHistory.Tuberculosis = old.Tuberculosis;
                ticket.MedicalHistory.Allergies = old.Allergies;
                ticket.MedicalHistory.Anemia = old.Anemia;
                ticket.MedicalHistory.Rheumatism = old.Rheumatism;
                ticket.MedicalHistory.RadTherapy = old.RadTherapy;
                ticket.MedicalHistory.Haemophilia = old.Haemophilia;
                ticket.MedicalHistory.AspirinIntake = old.AspirinIntake;
                ticket.MedicalHistory.KidneyTroubles = old.KidneyTroubles;
                ticket.MedicalHistory.Asthma = old.Asthma;
                ticket.MedicalHistory.HayFever = old.HayFever;
                ticket.MedicalHistory.MedicalHistoryText = old.MedicalHistoryText ?? "N/A";

                // Referred To Properties (Null check added)
                ticket.ReferredTo.Oral = old.Oral;
                ticket.ReferredTo.RemovableProsth = old.RemovableProsth;
                ticket.ReferredTo.Operative = old.Operative;
                ticket.ReferredTo.Endodontic = old.Endodontic;
                ticket.ReferredTo.Ortho = old.Ortho;
                ticket.ReferredTo.CrownAndBridge = old.CrownAndBridge;
                ticket.ReferredTo.Surgery = old.Surgery;
                ticket.ReferredTo.Pedo = old.Pedo;
                ticket.ReferredTo.XRay = old.XRay;


              //  _context.Update(ticket);
               await _context.SaveChangesAsync();
                TempData["success"] = "تم تعديل التذكرة بنجاح";
                return RedirectToAction("ShowPatientProfile", "Patient", new { id = pId });
            }
            TempData["Error"] = "فشل التعديل,الرجاء مراجعه البيانات و المحاوله مره أخري";
            return View(old);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {

            var ticket = await _context.Tickets
        .Include(t => t.MedicalHistory)
        .Include(t => t.Patient)
        .Include(t => t.ReferredTo)
        .FirstOrDefaultAsync(t => t.TicketId == id); // ✅ Use FirstOrDefaultAsync

            string pId = ticket?.PatientId; // Step 2: Now it's safe to access PatientId


            if (!tickets.Select(t => t.TicketId).Contains(id))
            {

                TempData["success"] = "عذرا  هذه التذكره غير موجودة";
                return RedirectToAction("ShowPatientProfile", "Patient", new { id = pId });

                //  return RedirectToAction(nameof(Index));
            }



            if (ticket == null)
            {
                TempData["success"] = "فشل الحذف, الرجاء المحاوله مره أخري";
                return RedirectToAction("ShowPatientProfile", "Patient", new { id = pId });


                //  return NotFound();
            }

            _context.Tickets.Remove(ticket);
            _context.SaveChangesAsync();
            TempData["Success"] = "تم حذف التذكرة بنجاح";
            return RedirectToAction("ShowPatientProfile", "Patient", new { id = pId });



        }

    }
}
