using System;

namespace Graph.Exceptions
{
    public class NodeNotFoundException : Exception 
    {
        public NodeNotFoundException() { }

        public NodeNotFoundException(string message) : base(message) { }
    }
}
