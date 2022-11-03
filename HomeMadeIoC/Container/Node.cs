using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeMadeIoC.Container;

internal class Node
{
    public Type? Abstraction { get; set; } = null;
    public Type Implementation { get; set; }
    public LifeTime LifeTime { get; set; }

    public Node()
    {

    }

    public List<Type> RequiredTypes { get; private set; }

}

internal class Connection
{
    public Node Left { get; set; }
    public Node Right { get; set; }
}

internal class DependencyGraph
{
    public List<Node> Nodes { get; set; }
    public List<Connection> Connections { get; set; }

}

