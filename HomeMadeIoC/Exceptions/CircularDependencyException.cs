﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Exceptions;

public class CircularDependencyException : Exception
{
    public CircularDependencyException() 
        : base($"There is a circular dependency between the classes in your hierarchy")
    {
    }
}
