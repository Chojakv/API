using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Messages;
using Domain.Domain;

namespace Application.Interfaces
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