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

        //Instantiate the Repository that will be used
        PersonRepository _PersonRepository = new PersonRepository();

        public ActionResult Index()
        {
            return View();
        }

        //Attribute Route
        [Route("Persons/SavePeron/{person}")]
        [HttpPost]
        public JsonResult SavePerson(PersonsModel person)
        {
            try
            {
                //Save person method
                _PersonRepository.SavePerson(person);
                return Json(person, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Attribute Route
        [Route("Persons/AddNewPerson/{id?}")]
        public ActionResult AddNewPerson(int? id)
        {
            //If there is a person get their data and return to the partial view
            if (id.HasValue)
            {
                if (id > 0)
                {
                    var data = _PersonRepository.GetPersonByID(id.Value);
                    return this.PartialView("_PersonMaintain", data);
                }
                else
                {
                    //If no person return a new instance of a person
                    return this.PartialView("_PersonMaintain", new PersonsModel());
                }
            }
            else
            {
                return this.PartialView("_PersonMaintain", new PersonsModel());
            }
        }

        //Attribute Route
        //Get person by their ID
        [Route("Persons/GetPersonByID/{id}")]
        public JsonResult GetPersonByID(int id)
        {
            try
            {
                //New instance of person
                var pm = new PersonsModel();
                //Load the data into the instance of the person
                pm = _PersonRepository.GetPersonByID(id);
                return Json(pm, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get all people
        [HttpGet]
        public JsonResult GetPersons()
        {
            try
            {
                //New list of person instance
                var personList = new List<PersonsModel>();
                //Get the list of persons and load into the list created above
                personList = _PersonRepository.GetPersons();
                //Return the data in JSON format
                return Json(personList, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        //Delete a person
        [Route("Persons/DeletePerson/{id}")]
        [HttpPost]
        public void DeletePerson(int id)
        {
            try
            {
                //Delete the person
                _PersonRepository.DeletePersonsModel(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Edit a person
        [Route("Persons/UpdatePerson/{person}")]
        [HttpPost]
        public void UpdatePerson(PersonsModel person)
        {
            try
            {
                //Update the person
                _PersonRepository.EditPersonsModel(person);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}