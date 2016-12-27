using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using WebDev.DomainModel.Abstract;
using WebDev.Models;
using WebDev.Web.Models;
using System.Web.UI;

namespace WebDev.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IPersonHandler _personHandler;

        public HomeController(IServiceLocator serviceLocator, IPersonHandler personHandler)
            : base(serviceLocator)
        {
            _personHandler = personHandler;
        }

        public ActionResult Index()
        {
            IEnumerable<Person> persons = _personHandler.GetAllPersons();

            // The controller is responsible for populating the ViewModel.
            // This is synonyous to the MVVM pattern. Note that the view is strongly typed.
            List<PersonListVM> personVMs = new List<PersonListVM>();

            foreach (Person person in persons)
            {
                personVMs.Add(new PersonListVM
                {
                    Person = person as Person,
                    AddressCount = person.Addresses.Count()
                });
            }

            return View(personVMs);
        }

        // Cache the result on the client for optimization and vary by id.
        // Configure SqlCacheDependency to vary by SQL updates.
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.Client, VaryByParam = "id")]
        public PartialViewResult PersonDetails(int id)
        {
            Person person = _personHandler.GetPersonById(id);

            return PartialView("_PersonDetails", person);
        }

        [HttpPost]
        public JsonResult GetPersonAddresses(int personId)
        {
            var addresses = _personHandler.GetAddressesByPersonId(personId);
            return Json(addresses, JsonRequestBehavior.AllowGet);
        }

    }
}
