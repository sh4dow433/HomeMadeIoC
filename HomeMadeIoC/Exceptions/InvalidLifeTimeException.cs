using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Exceptions;

public class InvalidLifeTimeException : Exception
{
    public InvalidLifeTimeException()
        : base($"A singleton can only depend on other singletons.")
    {

    }
}
