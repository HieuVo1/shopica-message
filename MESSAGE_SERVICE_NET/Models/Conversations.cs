using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Models
{
    public class Conversations: BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image_url { get; set; }
        public int Creator_id { get; set; }
        public ICollection<Messages> Messages { get; set; }
        public ICollection<Participants> Participants { get; set; }
    }
}
