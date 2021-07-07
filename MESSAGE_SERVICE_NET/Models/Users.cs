using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Models
{
    public class Users : BaseModel
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Image { get; set; }

        //collection navigation properties
        public ICollection<Participants> Participants { get; set; }
    }
}
