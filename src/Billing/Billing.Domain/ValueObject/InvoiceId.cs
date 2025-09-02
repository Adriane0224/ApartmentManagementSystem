using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Domain.ValueObject
{
    public readonly struct InvoiceId
    {
        public Guid Value { get; }
        public InvoiceId(Guid value) => Value = value;
        public override string ToString() => Value.ToString();
        public static implicit operator Guid(InvoiceId id) => id.Value;
        public static implicit operator InvoiceId(Guid value) => new(value);
    }
}
