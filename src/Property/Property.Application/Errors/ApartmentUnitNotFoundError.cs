using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Application.Errors
{
    public class ApartmentUnitNotFoundError(string message) : Error (message)
    {
    }
}
