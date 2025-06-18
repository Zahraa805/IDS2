using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IDS.Models;
using System.Security.Claims;
using IDS.Models;
namespace IDS.Controllers
{


    //[Authorize(Roles = "Admin")]

    //  [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {


        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;

        }
        public IActionResult Index()
        {
            
            ViewBag.controllerName = "Roles";

            ViewBag.entity = "دور";
            ViewBag.theEntity = "الدور";
            ViewBag.pluralEntity = "أدوار";
            ViewBag.thePluralEntity = "الأدوار";
            ViewBag.placeHolder = "مراجع";
            return PartialView("_ShowAllRoles" , _roleManager.Roles.ToList());
        }



        //  public IActionResult Create() { return View(); }



        [HttpPost]

        public async Task<IActionResult> Create(RoleVm vm)
        {
            if (ModelState.IsValid)
            {
                IdentityRole roleModel = new IdentityRole();
                roleModel.Name = vm.Name;
                var result = await _roleManager.CreateAsync(roleModel);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", result.Errors.FirstOrDefault().Description);
                    }
                    return View();
                }
            }
            else
            {
                //  ModelState.AddModelError(");
                return RedirectToAction(nameof(Index));
            }

        }


        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null) return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return View(role);
        }

        // GET: Roles/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null) return NotFound();
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null) return NotFound();
        //    return View(new RoleVm { Id = role.Id, Name = role.Name });
        //}

        // POST: Roles/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(RoleVm vm)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(vm.Id);
                if (role == null) return NotFound();

                role.Name = vm.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Roles/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null) return NotFound();
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null) return NotFound();
        //    return View(role);
        //}

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
