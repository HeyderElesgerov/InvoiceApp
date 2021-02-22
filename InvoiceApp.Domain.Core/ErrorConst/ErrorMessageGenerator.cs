using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Core.ErrorConst
{
    public class ErrorMessageGenerator
    {
        public static string Generate(string fieldName, string message)
        {
            return $"{fieldName}, {message}";
        }
    }
}
