using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Exceptions
{
    public class RapsAppException : Exception
    {
        public RapsAppException(string message):base(message)
        {

        }

        public RapsAppException(string message, Exception innerException):base(message,innerException)
        {

        }
    }
}
