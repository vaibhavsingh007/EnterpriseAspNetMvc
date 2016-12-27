using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDev.Data.Base;
using WebDev.Data.Repositories;
using WebDev.Data.SqlCe.Base;
using WebDev.Models;

namespace WebDev.Data.SqlCe.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        /// <summary>
        /// Note that the Addresses are lazy here.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Person> GetPersons()
        {
            return GetDbSet<Person>().AsEnumerable();
        }

        /// <summary>
        /// Eager loads Addresses and converts EF proxies back to POCOs.
        /// </summary>
        /// <returns></returns>
        public List<Person> GetPersonsWithAddresses()
        {
            List<Person> retval = null;
            List<Address> unproxiedAddresses = null;

            // This will avoid sending the proxies to the layer using it.
            // Proxies are not good for Serializers or UI.
            var persons = GetDbSet<Person>().Include("Addresses").ToList();
            retval = new List<Person>();
            unproxiedAddresses = new List<Address>();

            foreach (var person in persons)
            {
                var currentPerson = UnProxy<Person>(person);

                foreach (var address in person.Addresses)
                {
                    unproxiedAddresses.Add(UnProxy<Address>(address));
                }

                currentPerson.Addresses = unproxiedAddresses.ToList();
                retval.Add(currentPerson);

                unproxiedAddresses.Clear();
            }

            return retval;
        }

        public Person GetPersonById(int id)
        {
            return GetDbSet<Person>().FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Returns the unproxied Addresses by Person's ID.
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public List<Address> GetAddressesByPersonId(int personId)
        {
            List<Address> retval = new List<Address>();

            // Unproxying required.
            foreach (var address in GetPersonById(personId).Addresses)
            {
                retval.Add(UnProxy<Address>(address));
            }

            return retval;

        }
    }
}
