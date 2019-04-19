using System;

namespace Domain.Entities
{
    public class OrganizationDetail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LocationHQ { get; set; }
        public string Phone { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}