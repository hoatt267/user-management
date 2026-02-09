using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagementApp.Domain.Enums;

namespace UserManagementApp.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public Role Role { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User(string fullName, string email, Role role)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            Role = role;
            IsActive = true;
        }

        public void UpdateUser(string fullName, string email, Role role)
        {
            FullName = fullName;
            Email = email;
            Role = role;
        }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}