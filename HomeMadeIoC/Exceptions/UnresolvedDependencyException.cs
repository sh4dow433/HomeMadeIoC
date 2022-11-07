﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Exceptions;

public class UnresolvedDependencyException : Exception
{
    public UnresolvedDependencyException(string type)
        : base($"Type {type} couldn't be resolved.")
    {
    }
}
