using System;
using System.ComponentModel.DataAnnotations;

namespace Messaging.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        public int UserIDFrom { get; set; }
        public int UserIDTo { get; set; }

        public string Content { get; set; }
        public DateTime MessageUTCCreatedDate { get; set; }
        public bool SeenByUserTo { get; set; }

        public Message() { }
    }
}
