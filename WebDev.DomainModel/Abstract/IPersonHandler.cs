using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDev.Models;

namespace WebDev.DomainModel.Abstract
{
    public interface IPersonHandler
    {
        /// <summary>
        /// Returns all persons with eagerly loaded addresses.
        /// </summary>
        /// <returns>Pure POCOs.</returns>
        IEnumerable<Person> GetAllPersons();

        /// <summary>
        /// Returns Addresses for the person.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        List<Address> GetAddressesByPersonId(int personId);

        /// <summary>
        /// Returns person by the id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Person GetPersonById(int id);
    }
}
