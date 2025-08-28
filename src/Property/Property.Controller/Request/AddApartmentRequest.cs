using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Property.Controller.Request
{
    public class AddApartmentRequest
    {
        [Required]
        public string UnitNumber { get; set; } = null!;
    }
}
