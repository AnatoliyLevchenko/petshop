using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.BLL.Infrastructure
{
    /// <summary>
    /// Represents exception for vslidation errors.
    /// </summary>
    public class ValidationException : Exception
    {
        public string Property { get; protected set; }
        public ValidationException(string msg, string prop) : base(msg)
        {
            Property = prop;
        }
    }
}
