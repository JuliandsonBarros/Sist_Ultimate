using Sisat.Models;

namespace Sisat.ViewModels
{
    public class ChatViewModel : BaseViewModel 
    {
        public Chat Chat { get; set; }

        public List<Chat> Chats { get; set; }

        public ChatViewModel()
        {
            Chat = new Chat();
            Chats = new List<Chat>();
        }
    }
}

