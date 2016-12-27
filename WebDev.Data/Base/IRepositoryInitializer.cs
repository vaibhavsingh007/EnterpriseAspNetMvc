using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Data.Base
{
    public interface IRepositoryInitializer
    {
        /// <summary>
        /// To be used for initialization of the backing repositories.
        /// </summary>
        void Initialize();
    }
}
