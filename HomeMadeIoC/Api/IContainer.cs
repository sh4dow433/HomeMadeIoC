using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Api;

public interface IContainer
{
    public T GetService<T>();
    public object GetService(string name);
}
