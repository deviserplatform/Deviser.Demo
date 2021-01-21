using System;

namespace Deviser.Demo.Admin.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public Country Nationality { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveBadgeClass => IsActive ? "badge-primary" : "badge-secondary";
    }
}