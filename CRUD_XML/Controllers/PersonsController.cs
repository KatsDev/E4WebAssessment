using CRUD_XML.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_XML.Controllers
{
    public class PersonsController : Controller
    {
        // GET: Persons

        PersonRepository _PersonRepository = new PersonRepository();

        public ActionResult Index()
        {
            return View();
        }

        [Route("Persons/SavePeron/{person}")]
        [HttpPost]
        public JsonResult SavePerson(PersonsModel person)
        {
            try
            {
                _PersonRepository.SavePerson(person);
                return Json(person, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Persons/AddNewPerson/{id?}")]
        public ActionResult AddNewPerson(int? id)
        {
            if (id.HasValue)
            {
                if (id > 0)
                {
                    var data = _PersonRepository.GetPersonByID(id.Value);
                    return this.PartialView("_PersonMaintain", data);
                }
                else
                {
                    return this.PartialView("_PersonMaintain", new PersonsModel());
                }
            }
            else
            {
                return this.PartialView("_PersonMaintain", new PersonsModel());
            }
        }

        [Route("Persons/GetPersonByID/{id}")]
        public JsonResult GetPersonByID(int id)
        {
            try
            {
                var pm = new PersonsModel();
                pm = _PersonRepository.GetPersonByID(id);
                return Json(pm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public JsonResult GetPersons()
        {
            try
            {
                var personList = new List<PersonsModel>();
                personList = _PersonRepository.GetPersons();
                return Json(personList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Persons/DeletePerson/{id}")]
        [HttpPost]
        public void DeletePerson(int id)
        {
            try
            {
                _PersonRepository.DeletePersonsModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Route("Persons/UpdatePerson/{person}")]
        [HttpPost]
        public void UpdatePerson(PersonsModel person)
        {
            try
            {
                _PersonRepository.EditPersonsModel(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}