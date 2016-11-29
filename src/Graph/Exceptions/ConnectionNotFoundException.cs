using System;

namespace Graph.Exceptions
{
    public class ConnectionNotFoundException : Exception 
    {
        public ConnectionNotFoundException() { }

        public ConnectionNotFoundException(string message) : base (message)
        {

        }
    }
}
