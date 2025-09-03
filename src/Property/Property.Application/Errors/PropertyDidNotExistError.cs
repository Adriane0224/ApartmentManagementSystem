using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Application.Errors
{
    public class PropertyDidNotExistError(string message) : Error (message)
    {
    }
}
