using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WilliamsWeb1.Models
{
    public class Chats
    {
        public virtual List<Message> Messages { get; set; }
        public string ClientID { get; set; }
        public virtual ApplicationUser Client { get; set; }
        [Key]
        public int ChatId { get; set; }
        public string Subject { get; set; }
    }
    public class Message
    {
        public int ChatID { get; set; }
        public virtual Chats Chat { get; set; }
        public int MessageID { get; set; }
        public string Sender { get; set; }
        public string mess { get; set; }
        public Message() { }
    }
}