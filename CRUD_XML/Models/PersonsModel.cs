using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CRUD_XML.Models
{
    public class PersonsModel
    {
        public PersonsModel()
        {
            this.ID = 0;
            this.FirstName = null;
            this.LastName = null;
            this.ContactNumber = null;
        }

        public PersonsModel(int id, string firstName, string lastName, string contactNumber)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ContactNumber = contactNumber;
        }

        public int ID { get; set; }
        
        [Required(ErrorMessage = "First Name Is Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Contact Number Is Required")]
        public string ContactNumber { get; set; }
    }
}