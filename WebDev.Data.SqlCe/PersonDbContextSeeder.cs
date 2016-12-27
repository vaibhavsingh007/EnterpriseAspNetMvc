using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebDev.Data.Base;
using WebDev.Models;

namespace WebDev.Data.SqlCe
{
    public partial class PersonDbContext : ISeedDatabase
    {
        public void Seed()
        {
            CreateSomePersonsAndCountries();
        }

        /// <summary>
        /// Spawns some dummy data (persons with addresses).
        /// </summary>
        private void CreateSomePersonsAndCountries()
        {
            string[] personNames = new string[] { "James", "Adams", "Jill", "Mark" };
            string[] personCountries = new string[] { "USA", "India", "UK", "Denmark" };
            Person newPerson = null;
            Dictionary<int, Address> addresses = new Dictionary<int, Address>();

            for (int i = 0; i < personNames.Count(); i++)
            {
                newPerson = new Person
                {
                    FirstName = personNames[i],
                    Age = 20 + i,
                    HomeTown = ((char)(65 + i)).ToString(),
                    LastName = "Anderson",
                    Addresses = new List<Address>()
                };

                AddAddresses(newPerson, addresses);
                Persons.Add(newPerson);
            }
        }

        /// <summary>
        /// Populates the person entity's navigational property, Addresses.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="addressesDict">Dictionary to maintain and use unique addresses.</param>
        private void AddAddresses(Person person, IDictionary<int, Address> addressesDict)
        {
            // A randomized seeder logic that dynamically adds different number
            //..of addresses for a person.
            Random random = new Random();
            int addressCount = random.Next(1, 4);

            Address address = null;

            for (int num = 1; num <= addressCount; num++)
            {
                // Check if the current address was already spawned.
                if (addressesDict.ContainsKey(num))
                {
                    address = addressesDict[num];
                }
                else
                {
                    address = new Address
                    {
                        Street = "Street " + num,
                        City = "City " + num,
                        State = "State " + num
                        //ZipCode = 100 + num
                    };

                    addressesDict.Add(num, address);
                }

                // Add to the person entity's navigational property.
                person.Addresses.Add(address);
            }
        }
    }
}
