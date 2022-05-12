using Xunit;
using BL;
using Ninject;
using BL.Models;
using System.Threading.Tasks;
using BL.Exceptions.PermissionExceptions;
using BL.Exceptions.MessageExceptions;
using System.Collections.Generic;


namespace TestsBL
{
    public class TestMessages
    {
        [Fact]
        public async Task Test1()
        {
            IKernel ninjectKernel = new StandardKernel();
            ninjectKernel.Bind<IRepositoriesFactory>().To<IoCRepositoriesFactory>();
            Facade facade = new(ninjectKernel.Get<IRepositoriesFactory>());

            await TestUsersCreator.Create();

            uint messageID1 = await facade.SendMessageAsync(TestUsersCreator.authorizedID, "test text 1");
            Message sentMessage1 = await facade.AdminGetMessageByIDAsync(TestUsersCreator.adminID, messageID1);
            uint messageID2 = await facade.SendMessageAsync(TestUsersCreator.authorizedID, "test text 2");
            Message sentMessage2 = await facade.AdminGetMessageByIDAsync(TestUsersCreator.adminID, messageID2);

            Assert.Equal(Message.MessageCheckedByNobody, sentMessage1.CheckedByID);
            Assert.Equal(Message.MessageCheckedByNobody, sentMessage2.CheckedByID);
            await Assert.ThrowsAsync<PermissionException>(() => facade.SendMessageAsync(TestUsersCreator.unauthorizedID, "test text 0"));


            Message readMessage1 = await facade.MarkMessageReadByUserAsync(TestUsersCreator.skiPatrolID, sentMessage1.MessageID);
            Assert.Equal(TestUsersCreator.skiPatrolID, readMessage1.CheckedByID);
            await Assert.ThrowsAsync<MessageCheckingException>(() => facade.MarkMessageReadByUserAsync(TestUsersCreator.skiPatrolID, sentMessage1.MessageID));
            await Assert.ThrowsAsync<PermissionException>(() => facade.MarkMessageReadByUserAsync(TestUsersCreator.authorizedID, sentMessage2.MessageID));
            await Assert.ThrowsAsync<PermissionException>(() => facade.MarkMessageReadByUserAsync(TestUsersCreator.unauthorizedID, sentMessage2.MessageID));

            Message updatedMessage2 = new(sentMessage2.MessageID, sentMessage2.SenderID, sentMessage2.CheckedByID, "another text");
            await facade.AdminUpdateMessageAsync(TestUsersCreator.adminID, updatedMessage2.MessageID, updatedMessage2.SenderID, updatedMessage2.CheckedByID, updatedMessage2.Text);

            List<Message> messages = await facade.GetMessagesAsync(TestUsersCreator.skiPatrolID, 0u, Facade.UNLIMITED);
            Assert.Equal(2, messages.Count);
            Assert.Contains(readMessage1, messages);
            Assert.Contains(updatedMessage2, messages);

            foreach (var message in messages)
            {
                await facade.AdminDeleteMessageAsync(TestUsersCreator.adminID, message.MessageID);
            }

            messages = await facade.GetMessagesAsync(TestUsersCreator.adminID, 0u, Facade.UNLIMITED);
            Assert.Empty(messages);

        }
    }
}