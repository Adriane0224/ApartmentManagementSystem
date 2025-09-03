using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Domain.Entities
{
    public class OwnerUnit
    {
        public Guid Id { get; private set; }
        public Guid OwnerId { get; private set; }
        public Guid UnitId { get; private set; }

        protected OwnerUnit() { }
        private OwnerUnit(Guid ownerId, Guid unitId)
        {
            Id = Guid.NewGuid();
            OwnerId = ownerId;
            UnitId = unitId;
        }

        public static OwnerUnit Assign(Guid ownerId, Guid unitId)
        {
            if (ownerId == Guid.Empty) throw new ArgumentException("OwnerId required.");
            if (unitId == Guid.Empty) throw new ArgumentException("UnitId required.");
            return new OwnerUnit(ownerId, unitId);
        }
    }
}

