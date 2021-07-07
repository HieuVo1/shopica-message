using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MESSAGE_SERVICE_NET.Models
{
    public class Attachments: BaseModel
    {
        public int Id { get; set; }
        public int Message_id { get; set; }
        public string Thumb_url { get; set; }
        public string File_url { get; set; }
        public Messages Message { get; set; }
    }
}
