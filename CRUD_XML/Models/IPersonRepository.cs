using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_XML.Models
{
    public interface IPersonRepository
    {
        List<PersonsModel> GetPersons();
        PersonsModel GetPersonByID(int id);
        void SavePerson(PersonsModel Person);
        void EditPersonsModel(PersonsModel Person);
        void DeletePersonsModel(int id);
    }
}
