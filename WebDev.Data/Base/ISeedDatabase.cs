using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Data.Base
{
    public interface ISeedDatabase
    {
        /// <summary>
        /// The seeder. Check usage for more info.
        /// </summary>
        void Seed();
    }
}
