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
}