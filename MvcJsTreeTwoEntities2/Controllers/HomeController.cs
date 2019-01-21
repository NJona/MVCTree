using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcJsTreeTwoEntities2.Data;
using MvcJsTreeTwoEntities2.Models;
using Newtonsoft.Json;

namespace MvcJsTreeTwoEntities2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            foreach (var role in _context.Role)
            {
                nodes.Add(new TreeViewNode{id = role.Id.ToString(), parent = "#", text = role.Name});
            }

            foreach (var legimitation in _context.Legimitation)
            {
                nodes.Add(new TreeViewNode{id = legimitation.ParentId + "-" + legimitation.Id, parent = legimitation.ParentId.ToString(), text = legimitation.Name});
            }

            ViewBag.Json = JsonConvert.SerializeObject(nodes);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int numberRoles, int numberLegimitationsPerChild, string buttonType)
        {
            if (buttonType == "Generate")
            {
                if (ModelState.IsValid)
                {
                    for (var parentIndex = 0; parentIndex < numberRoles; parentIndex++)
                    {
                        var role = new Role()
                        {
                            Name = "Role"
                        };
                        _context.Add(role);
                        _context.SaveChanges();
                        var parentId = role.Id;

                        for (var childIndex = 0; childIndex < numberLegimitationsPerChild; childIndex++)
                        {
                            var treeDataChild = new Legimitation()
                            {
                                Name = "Legimitation",
                                ParentId = parentId
                            };
                            _context.Add(treeDataChild);
                            _context.SaveChanges();
                        }
                    }
                }
            }
            else if (buttonType == "DeleteAll")
            {
                var roles = from o in _context.Role
                    select o;
                foreach (var role in roles)
                {
                    _context.Role.Remove(role);
                }

                var legimitations = from o in _context.Legimitation
                    select o;
                foreach (var legimitation in legimitations)
                {
                    _context.Legimitation.Remove(legimitation);
                }

                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
