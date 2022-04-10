﻿using Xunit;
using BL;
using Ninject;
using BL.Models;
using System.Threading.Tasks;
using BL.Exceptions;
using System.Collections.Generic;


namespace TestsBL
{
    public class Messages
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();
            Facade facade = new(ninjectKernel.Get<IRepositoriesFactory>());

            await TestUsers.Create();

            Message sentMessage1 = await facade.SendMessageAsync(TestUsers.authorizedID, "test text 1");
            Message sentMessage2 = await facade.SendMessageAsync(TestUsers.authorizedID, "test text 2");
            Assert.Equal(Message.MessageCheckedByNobody, sentMessage1.CheckedByID);
            Assert.Equal(Message.MessageCheckedByNobody, sentMessage2.CheckedByID);
            await Assert.ThrowsAsync<PermissionsException>(() => facade.SendMessageAsync(TestUsers.unauthorizedID, "test text 0"));


            Message readMessage1 = await facade.MarkMessageReadByUserAsync(TestUsers.skiPatrolID, sentMessage1.MessageID);
            Assert.Equal(TestUsers.skiPatrolID, readMessage1.CheckedByID);
            await Assert.ThrowsAsync<MessageException>(() => facade.MarkMessageReadByUserAsync(TestUsers.skiPatrolID, sentMessage1.MessageID));
            await Assert.ThrowsAsync<PermissionsException>(() => facade.MarkMessageReadByUserAsync(TestUsers.authorizedID, sentMessage2.MessageID));
            await Assert.ThrowsAsync<PermissionsException>(() => facade.MarkMessageReadByUserAsync(TestUsers.unauthorizedID, sentMessage2.MessageID));

            Message updatedMessage2 = new(sentMessage2.MessageID, sentMessage2.SenderID, sentMessage2.CheckedByID, "another text");
            await facade.AdminUpdateMessageAsync(TestUsers.adminID, updatedMessage2);

            List<Message> messages = await facade.GetMessagesAsync(TestUsers.skiPatrolID, 0u, Facade.UNLIMITED);
            Assert.Equal(2, messages.Count);
            Assert.Contains(readMessage1, messages);
            Assert.Contains(updatedMessage2, messages);

            foreach (var message in messages)
            {
                await facade.AdminDeleteMessageAsync(TestUsers.adminID, message);
            }

            messages = await facade.GetMessagesAsync(TestUsers.adminID, 0u, Facade.UNLIMITED);
            Assert.Empty(messages);

        }
    }
}