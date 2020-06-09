using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Models
{
    public class Chat
    {

        public Chat()
        {
            Messages = new List<Messages>();
            Users = new List<ChatUser>();
        }

        public int Id { get; set; }

        public ICollection<Messages> Messages { get; set; }

        public ICollection<ChatUser> Users { get; set; }
        public string Name { get; set; }
        public ChatType Type { get; set; }
    }
}
