using Project.TakuGames.Model.Helpers;
using System;
namespace Project.TakuGames.Model.Exceptions
{
    public class ConflictException : ApplicationException
    {
        public ConflictException() : base("Los datos enviados generaran un conflicto con otra entidad")
        {
        }
        public ConflictException(string message) : base(message)
        {
        }
    }

    public class NotFoundException : ApplicationException
    {
        public NotFoundException() : base("No se encontró la entidad")
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class BadRequestException : ApplicationException
    {
        public ComponentError ComponentError { get; set; }

        public BadRequestException() : base("No paso las validaciones")
        {
        }
        public BadRequestException(string message) : base(message)
        {
        }
        public BadRequestException(ComponentError componentError) : base("No paso las validaciones")
        {
            ComponentError = componentError;
        }
    }
}
