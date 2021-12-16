using System;
using System.Collections.Generic;
using System.Text;

namespace Project.TakuGames.Model.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public string ErrorCode { get; set; }
        public BusinessException() : base()
        {
        }
        public BusinessException(string message) : base(message)
        {
        }
    }
}
