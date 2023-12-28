using System;

namespace Thresh.Core.Data
{
    [Serializable]
    internal class EntityException : Exception
    {
        internal EntityException()
        {
        }

        internal EntityException(string message)
            : base(message)
        {
        }

        internal EntityException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    
    [Serializable]
    internal class EventException : Exception
    {
        public EventException()
        {
        }

        public EventException(string message)
            : base(message)
        {
        }

        public EventException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    
    [Serializable]
    internal class DefinitionException : Exception
    {
        internal DefinitionException()
        {
        }
        internal DefinitionException(string message)
            : base(message)
        {
        }

        internal DefinitionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    
    [Serializable]
    internal class ModuleException : Exception
    {
        public ModuleException()
        {
        }
        public ModuleException(string message)
            : base(message)
        {
        }

        public ModuleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
    
    [Serializable]
    public class INIException : Exception
    {
        public INIException()
        {
        }
        public INIException(string message)
            : base(message)
        {
        }

        public INIException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}