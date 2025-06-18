using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Math;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using IDS.Migrations;
using IDS.Models;
using IDS.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlTypes;
using System.Net.Sockets;
using System.Runtime.Intrinsics.X86;
using System.Security.Principal;

namespace IDS.Controllers
{
    public class ReceptionController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IEnumerable<Patient> Patients;
        private readonly IEnumerable<string> PatientsIds;
        private readonly IEnumerable<Ticket> tickets;



        public ReceptionController(AppDbContext context)
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
        public async Task<IActionResult> ShowFullTicket(string id)
        {
            TempData["New"] = false;

            if (id == null)
            {
                return NotFound();
            }

            if (!tickets.Select(t => t.TicketId).Contains(id))
            {
                TempData["Error"] = "عذرا , هذه التذكره غير موجوده";
                return RedirectToAction(nameof(Index));
            }



            /////////////Important notes About null
            #region
            ////////////////Important about null : 
            // var tickets = _context.Tickets; // ✅ This is an IQueryable<Ticket>, not a list! This does not retrieve any data immediately.    
            //  The query only executes when you iterate over it(e.g., using .ToList(), .FirstOrDefault(), etc.).

            //ToList() returns a list(empty or filled)
            //FirstOrDefault() returns a single object(or null if nothing is found)


            // What Happens When There’s No Match?
            //Query                                                         Return Type                                            What It Returns If No Match?
            //_context.Tickets.Where(e => e.Id == id);                      IQueryable<Ticket>                                     An empty queryable(not null)
            //_context.Tickets.Where(e => e.Id == id).ToList();             List<Ticket>                                           An empty list[]

            //_context.Tickets.Select(e => e.Id);                           IQueryable<int>                                        Empty queryable(not null)
            //_context.Tickets.Select(e => e.Id).ToList();                  List<int> Empty                                        list[](not null)

            //_context.Tickets.FirstOrDefault(e => e.Id == id);             Ticket ?                                                (nullable)null
            //_context.Tickets.SingleOrDefault(e => e.Id == id);            Ticket ?                                                (nullable)null(if no match), Exception(if multiple matches)

            #endregion



            #region
            //,, important about db ;loading , queries and data reader
            //  .ToList() retrieves all tickets from the database and loads them into memory.
            // .FirstOrDefault(t => t.TicketId == id) filters the list in memory to find the first matching record



            // ✅ Fix: Remove.ToList() and use FirstOrDefaultAsync() instead.
            // When you use.ToList(), EF keeps the DataReader open while it loads all records.
            //If your database has 100,000 tickets, all of them are loaded into memory even though you only need one!
            //This wastes memory and slows down performance.
            //It increases database load and can lead to higher RAM usage in large systems.

            //Missing await for database operations
            //Since you're inside an async action, database queries should use await with FirstOrDefaultAsync() to avoid blocking the thread.
            #endregion


            // Wrong code 
            //var ticket = _context.Tickets
            //.AsNoTracking()

            //.Include(t => t.MedicalHistory)
            //.Include(t => t.Patient)
            //.Include(t => t.ReferredTo)
            //.ToList()
            //.FirstOrDefault(t => t.TicketId == id); // ✅ Use ToList()

            var ticket = await _context.Tickets
                .AsNoTracking()
                .Include(t => t.MedicalHistory)
                .Include(t => t.Patient)
                .Include(t => t.ReferredTo)
                .FirstOrDefaultAsync(t => t.TicketId == id); // ✅ Use FirstOrDefaultAsync

            if (ticket == null)
            {
                return NotFound();
            }

            // ✅ Null Safety Checks
            var ticketVm = new TicketVM
            {
                // Ticket Properties
                PatientId = ticket.PatientId,
                AppointmentDate = ticket.AppointmentDate,
                ChiefComlant = ticket.ChiefComlant,
                PrevisionalDiagnosis = ticket.PrevisionalDiagnosis,

                // Patient Properties (Null check added)
                Name = ticket.Patient?.Name ?? "N/A",
                Address = ticket.Patient?.Address ?? "N/A",
                profession = ticket.Patient?.profession ?? "N/A",
                phoneNumber = ticket.Patient?.phoneNumber ?? "N/A",
                Gender = ticket.Patient?.Gender ?? "N/A",
                Age = ticket.Patient?.Age ?? 0,

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

            return View("Ticket", ticketVm);
        }



        // In .NET, Task is a class in the System.Threading.
        // Tasks namespace used to represent asynchronous operations. 
        public async Task<IActionResult> CreateTicketForNewPatient()
        {
            TempData["New"] = true;

            return View("ReceptionTicket");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicketForNewPatient(TicketVM ticket)
        {

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["Errors"] = errors;


            //foreach (var error in errors)
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}

            if (PatientsIds.Contains(ticket.PatientId))
            {
                TempData["Error"] = "عذرا , هذا المريض مسجل بالفعل";
                return View("ReceptionTicket", ticket);
            }

            if (ModelState.IsValid)
            {
                Patient patient = new Patient();
                Ticket newTicket = new Ticket(_context);


                patient.PatientId = ticket.PatientId;
                patient.Name = ticket.Name;
                patient.Address = ticket.Address;
                patient.profession = ticket.profession;
                patient.phoneNumber = ticket.phoneNumber;
                patient.Gender = ticket.Gender;
                patient.Age = ticket.Age;



                var referredTo = new ReferredTo
                {
                    Oral = ticket.Oral,
                    RemovableProsth = ticket.RemovableProsth,
                    Operative = ticket.Operative,
                    Endodontic = ticket.Endodontic,
                    Ortho = ticket.Ortho,
                    CrownAndBridge = ticket.CrownAndBridge,
                    Surgery = ticket.Surgery,
                    Pedo = ticket.Pedo,
                    XRay = ticket.XRay,
                };


                var medicalHistory = new MedicalHistory
                {
                    HeartTrouble = ticket.HeartTrouble,
                    Hyperttention = ticket.Hyperttention,
                    Pregnancy = ticket.Pregnancy,
                    Diabetes = ticket.Diabetes,
                    Hepatitis = ticket.Hepatitis,
                    AIDs = ticket.AIDs,
                    Tuberculosis = ticket.Tuberculosis,
                    Allergies = ticket.Allergies,
                    Anemia = ticket.Anemia,
                    Rheumatism = ticket.Rheumatism,
                    RadTherapy = ticket.RadTherapy,
                    Haemophilia = ticket.Haemophilia,
                    AspirinIntake = ticket.AspirinIntake,
                    KidneyTroubles = ticket.KidneyTroubles,
                    Asthma = ticket.Asthma,
                    HayFever = ticket.HayFever,
                    MedicalHistoryText = ticket.MedicalHistoryText,
                };


                // Yes! When you call _context.SaveChanges(), the database generates the identity value for Id and
                // then EF Core automatically updates the Id property of the medicalHistory object with the generated
                // value.
                // So, after calling _context.SaveChanges(), you can access medicalHistory.Id,
                // and it will contain the new database- generated identity value.
                _context.AddRange(patient, referredTo, medicalHistory);
                _context.SaveChanges();

                newTicket.PatientId = patient.PatientId;
                newTicket.MedicalHistoryId = medicalHistory.Id;
                newTicket.ReferredToId = referredTo.Id;
                newTicket.AppointmentDate = ticket.AppointmentDate;
                

                _context.Add(newTicket);

                var ticketAccountacy = new TicketAccountancy
                {
                    TicketId = newTicket.TicketId,
                    ReceptionEmpId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value

                };
                _context.Add(ticketAccountacy);

                await _context.SaveChangesAsync();

                TempData["Success"] = "تم تسجيل بيانات المريض بنجاح و تحويل التذكرة لعيادة التشخيص";
                TempData["TicketId"] = newTicket.TicketId;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "عذرا , حدث خطأ اثناء تسجيل البيانات";
                return View("ReceptionTicket", ticket);
            }
        }





        [HttpGet]
        public async Task<IActionResult> CreateTicketForExistingPatient(string id)
        {
            TempData["New"] = false;
            if (id == null)
            {
                return NotFound();
            }
            if (!PatientsIds.Contains(id))
            {
                TempData["Error"] = "عذرا , هذا المريض  غير موجود";
                return RedirectToAction(nameof(Index));
            }

            var patient = Patients.FirstOrDefault(p => p.PatientId == id);

            var ticket = new TicketVM();
            ticket.PatientId = patient.PatientId;
            ticket.Name = patient.Name;
            ticket.Address = patient.Address;
            ticket.profession = patient.profession;
            ticket.phoneNumber = patient.phoneNumber;
            ticket.Gender = patient.Gender;
            ticket.Age = patient.Age;

            return View("ReceptionTicket", ticket);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicketForExistingPatient(TicketVM ticket)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            TempData["Errors"] = errors;


            if (ModelState.IsValid)
            {
                Ticket newTicket = new Ticket(_context);

                var referredTo = new ReferredTo
                {
                    Oral = ticket.Oral,
                    RemovableProsth = ticket.RemovableProsth,
                    Operative = ticket.Operative,
                    Endodontic = ticket.Endodontic,
                    Ortho = ticket.Ortho,
                    CrownAndBridge = ticket.CrownAndBridge,
                    Surgery = ticket.Surgery,
                    Pedo = ticket.Pedo,
                    XRay = ticket.XRay,
                };


                var medicalHistory = new MedicalHistory
                {
                    HeartTrouble = ticket.HeartTrouble,
                    Hyperttention = ticket.Hyperttention,
                    Pregnancy = ticket.Pregnancy,
                    Diabetes = ticket.Diabetes,
                    Hepatitis = ticket.Hepatitis,
                    AIDs = ticket.AIDs,
                    Tuberculosis = ticket.Tuberculosis,
                    Allergies = ticket.Allergies,
                    Anemia = ticket.Anemia,
                    Rheumatism = ticket.Rheumatism,
                    RadTherapy = ticket.RadTherapy,
                    Haemophilia = ticket.Haemophilia,
                    AspirinIntake = ticket.AspirinIntake,
                    KidneyTroubles = ticket.KidneyTroubles,
                    Asthma = ticket.Asthma,
                    HayFever = ticket.HayFever,
                    MedicalHistoryText = ticket.MedicalHistoryText,
                };

                // Yes! When you call _context.SaveChanges(), the database generates the identity value for Id and
                // then EF Core automatically updates the Id property of the medicalHistory object with the generated
                // value.
                // So, after calling _context.SaveChanges(), you can access medicalHistory.Id,
                // and it will contain the new database- generated identity value.
                _context.AddRange(referredTo, medicalHistory);
                _context.SaveChanges();

                newTicket.PatientId = ticket.PatientId;
                newTicket.MedicalHistoryId = medicalHistory.Id;
                newTicket.ReferredToId = referredTo.Id;
                newTicket.AppointmentDate = ticket.AppointmentDate;

                _context.Add(newTicket);

                var ticketAccountacy = new TicketAccountancy
                {
                    TicketId = newTicket.TicketId,
                    ReceptionEmpId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value

                };
                _context.Add(ticketAccountacy);
                await _context.SaveChangesAsync();

                TempData["Success"] = "تم تسجيل بيانات المريض بنجاح و تحويل التذكرة لعيادة التشخيص";
                TempData["TicketId"] = newTicket.TicketId;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["Error"] = "عذرا , حدث خطأ اثناء تسجيل البيانات";
                return View("ReceptionTicket", ticket);
            }
        }


     


    


    }
}
