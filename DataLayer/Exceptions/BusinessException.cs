using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Exceptions
{
    class BusinessException : Exception
    {
        public BusinessException(string message) : base(message)
        {
        }
    }
}
