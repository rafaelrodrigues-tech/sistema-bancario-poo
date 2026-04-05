using System;

namespace SistemaBancario.Entities.Exceptions
{
    class DomainException:ApplicationException
    {
        public DomainException(string message):base(message)
        { 
        }
    }
}
