using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDev.Models;

namespace WebDev.Data.Repositories
{
    public interface IPersonRepository
    {
        /// <summary>
        /// Note that the Addresses are lazy here.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Person> GetPersons();

        /// <summary>
        /// Eager loads Addresses and converts EF proxies back to POCOs.
        /// </summary>
        /// <returns></returns>
        List<Person> GetPersonsWithAddresses();

        /// <summary>
        /// Returns the unproxied Addresses by Person's ID.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        List<Address> GetAddressesByPersonId(int personId);

        Person GetPersonById(int id);

        // Other relevant methods.
    }
}
