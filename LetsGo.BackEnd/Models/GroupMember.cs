using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsGo.BackEnd.Models
{
    public class GroupMember
    {
        public Guid Id { get; set; }
        public string UserFullName { get; set; }
        public string UserAltFullName { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageURL { get; set; }
        public Guid? CityId { get; set; }
        public string JobTitle { get; set; }
        public string WorkPlace { get; set; }
        public String GroupRole { get; set; }
    }
}
