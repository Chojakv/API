using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Messages;
using Domain.Domain;

namespace Application.Interfaces
{
    public interface IMessageService
    {
        Task<Message> GetById(Guid messageId);
        Task<PayloadResult<Message>> SendMessageAsync(string senderId, SendMessageModel model);
        Task<IEnumerable<Message>> GetUserSentMessages(string username);
        Task<IEnumerable<Message>> GetUserReceivedMessages(string username);
        Task<int> NewMessagesCount(string username);
    }

}