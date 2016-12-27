using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDev.Data.Repositories;
using WebDev.DomainModel.Abstract;
using WebDev.Models;
using WebDev.Utils.ExceptionHandlers;

namespace WebDev.DomainModel.Concrete
{
    /// <summary>
    /// This is the PersonHandler implementation of the Domain Driven Design.
    /// </summary>
    public class PersonHandler : IPersonHandler
    {

        private IPersonRepository _personRepo;

        public PersonHandler(IPersonRepository personRepo)
        {
            _personRepo = personRepo;
        }

        public IEnumerable<Person> GetAllPersons()
        {
            IEnumerable<Person> retval = null;

            try
            {
                retval = _personRepo.GetPersonsWithAddresses();
            }
            catch (Exception ex)
            {
                ExceptionFacade.LogException(ex, Constants.LogDir, true);
            }

            return retval;
        }

        public Person GetPersonById(int id)
        {
            Person retval = null;

            try
            {
                retval = _personRepo.GetPersonById(id);
            }
            catch (Exception ex)
            {
                ExceptionFacade.LogException(ex, Constants.LogDir, true);
            }

            return retval;
        }

        public List<Address> GetAddressesByPersonId(int personId)
        {
            List<Address> retval = null;

            try
            {
                retval = _personRepo.GetAddressesByPersonId(personId);
            }
            catch (Exception ex)
            {
                ExceptionFacade.LogException(ex, Constants.LogDir, true);
            }

            return retval;
        }
    }
}
