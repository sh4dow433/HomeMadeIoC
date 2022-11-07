using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Exceptions;

public class InvalidScopeException : Exception
{
    public InvalidScopeException()
        : base($"A singleton can only depend on other singletons.")
    {

    }
}
