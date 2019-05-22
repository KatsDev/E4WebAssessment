using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace CRUD_XML.Models
{
    public class PersonRepository : IPersonRepository
    {
        //try catch will throw any exceptions

        //List of persons
        private List<PersonsModel> allPersons;
        //XML Document
        private XDocument PersonsData;
        //FilePath
        private string fileName = "~/App_Data/PersonData.xml";

        //Constructor
        public PersonRepository()
        {
            try
            {
                //Lets create a persons list
                allPersons = new List<PersonsModel>();
                //Get the data from the data source
                if (File.Exists(HttpContext.Current.Server.MapPath(this.fileName)))
                {
                    PersonsData = XDocument.Load(HttpContext.Current.Server.MapPath(this.fileName));
                    var Persons = from t in PersonsData.Descendants("Item")
                                  select new PersonsModel((int)t.Element("ID"),
                                  t.Element("FirstName").Value,
                                  t.Element("LastName").Value,
                                  t.Element("ContactNumber").Value);
                    //Add the data to the list that was created at the top
                    allPersons.AddRange(Persons.ToList<PersonsModel>());
                }
                else
                {
                    using (XmlWriter writer = XmlWriter.Create(HttpContext.Current.Server.MapPath(this.fileName)))
                    {
                        writer.WriteStartElement("Users");
                        //writer.WriteStartElement("Item");
                        //writer.WriteElementString("ID", "1");
                        //writer.WriteElementString("FirstName", "Faheem Kathrada");
                        //writer.WriteElementString("LastName", "Kathrada");
                        //writer.WriteElementString("ContactNumber", "0739131826");
                        //writer.WriteEndElement();
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get all data from the data source
        public List<PersonsModel> GetPersons()
        {
            try
            {
                return allPersons;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Save person to the data file
        public void SavePerson(PersonsModel Person)
        {
            try
            {
                Person.ID = (int)(from S in PersonsData.Descendants("Item") orderby (int)S.Element("ID") descending select (int)S.Element("ID")).FirstOrDefault() + 1;
                PersonsData.Root.Add(new XElement("Item", new XElement("ID", Person.ID),
                    new XElement("FirstName", Person.FirstName),
                    new XElement("LastName", Person.LastName),
                    new XElement("ContactNumber", Person.ContactNumber)));
                //Save the file with the new data
                PersonsData.Save(HttpContext.Current.Server.MapPath(this.fileName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get person by id
        public PersonsModel GetPersonByID(int id)
        {
            try
            {
                //Find the person by their ID from the data source
                return allPersons.Find(item => item.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Edit a persons data
        public void EditPersonsModel(PersonsModel Person)
        {
            try
            {

                //Find the person by their ID from the data source
                XElement node = PersonsData.Root.Elements("Item").Where(i => (int)i.Element("ID") == Person.ID).FirstOrDefault();

                //Update the information below
                node.SetElementValue("FirstName", Person.FirstName);
                node.SetElementValue("LastName", Person.LastName);
                node.SetElementValue("ContactNumber", Person.ContactNumber);
                //Save the file with the updated data
                PersonsData.Save(HttpContext.Current.Server.MapPath(this.fileName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete a person by id
        public void DeletePersonsModel(int id)
        {
            try
            {
                //Find the person by their ID from the data source and delete
                PersonsData.Descendants("Users").Elements("Item").Where(i => (int)i.Element("ID") == id).Remove();
                //Save the file with the updated data
                PersonsData.Save(HttpContext.Current.Server.MapPath(this.fileName));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}