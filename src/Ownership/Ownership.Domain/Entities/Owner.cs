using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ownership.Domain.Entities
{
    public class Owner
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string? Email { get; private set; }
        public string? Phone { get; private set; }

        protected Owner() { }

        public static Owner Create(string name, string? email, string? phone)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required.");
            return new Owner
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Email = email,
                Phone = phone
            };
        }

        public void Update(string? name, string? email, string? phone)
        {
            if (!string.IsNullOrWhiteSpace(name)) Name = name.Trim();
            Email = email;
            Phone = phone;
        }
    }
}

