using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using API.Models.Messages;

namespace API.Interfaces
{
    public interface IMessageService
    {
        Task<string> GetIdFromUsernameAsync(string username);
        Task<PayloadResult<Message>> SendMessageAsync(string senderId, string receiverUsername, SendMessageModel model);
        Task<IEnumerable<Message>> GetUserSentMessages(string username);
        Task<IEnumerable<Message>> GetUserReceivedMessages(string username);
        Task<bool> SetMessageAsViewed(string username);
        Task<int> NewMessagesCount(string username);

    }

}