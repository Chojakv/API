using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Messages;
using Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    [Authorize]
    public class MessageService : IMessageService
    {
        private readonly DataContext _dataContext;

        public MessageService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<PayloadResult<Message>> SendMessageAsync(string senderId, SendMessageModel model)
        {
            var receiverId = await GetIdFromUsernameAsync(model.Username);

            var senderUsername = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == senderId);
            
            if (receiverId == null)
                return new PayloadResult<Message>
                {
                    Errors = new[] {$"There is no user with username '{model.Username}'"}
                };
            if (senderUsername.UserName == model.Username)
            {
                return new PayloadResult<Message>
                {
                    Errors = new[] {$"U can't message yourself"}
                };
            }
            
            var sender = await _dataContext.Mailboxes.FirstOrDefaultAsync(x => x.UserId == senderId && x.Type == MailboxType.Sent);
            var receiver = await _dataContext.Mailboxes.FirstOrDefaultAsync(x => x.UserId == receiverId && x.Type == MailboxType.Received);

            var message = new Message
            {
                Subject = model.Subject,
                Content = model.Content,
                ReceiverId = receiverId,
                ReceiverUsername = model.Username,
                IsViewed = false,
                SenderId = senderUsername.Id,
                SenderUsername = senderUsername.UserName,
                SendDate = DateTime.UtcNow,
                SentMailboxId = sender.Id,
                ReceivedMailboxId = receiver.Id
            };

            await _dataContext.Messages.AddAsync(message);

            var success = await _dataContext.SaveChangesAsync() > 0;

            if (!success)
                return new PayloadResult<Message>
                {
                    Errors = new[] {"Could not save changes."},
                    Success = false
                };

            return new PayloadResult<Message>
            {
                Payload = message
            };
        }
        
        public async Task<Message> GetById(Guid messageId)
        {
            return await _dataContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
        }

        public async Task<IEnumerable<Message>> GetUserSentMessages(string username)
        {
            var sender = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            return await _dataContext.Messages.Where(x => x.SenderId == sender.Id).OrderByDescending(x=>x.SendDate).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetUserReceivedMessages(string username)
        {
            var receiver = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == username);

            await SetMessageAsViewed(receiver.UserName);
            
            return await _dataContext.Messages.Where(x => x.ReceiverId == receiver.Id).OrderByDescending(x=>x.SendDate).ToListAsync();
        }
        private async Task SetMessageAsViewed(string username)
        {
            var user = await GetIdFromUsernameAsync(username);
            var messages = await _dataContext.Messages.Where(x => x.ReceiverId == user).ToListAsync();

            foreach (var message in messages.Where(message => message.IsViewed == false))
            {
                message.IsViewed = true;
                _dataContext.Messages.Update(message);
            }

            await _dataContext.SaveChangesAsync();
        }
        public async Task<int> NewMessagesCount(string username)
        {
            var unreadMessages = 0;
            var user = await GetIdFromUsernameAsync(username);
            var messages = await _dataContext.Messages.Where(x => x.ReceiverId == user).ToListAsync();
        
            foreach (var message in messages.Where(message => message.IsViewed == false))
            {
                unreadMessages++;
            } 
            return unreadMessages;
        }

        private async Task<string> GetIdFromUsernameAsync(string username)
        {
            return (await _dataContext.Users.Where(x => x.UserName == username).FirstOrDefaultAsync())?.Id;
        }
    }
}