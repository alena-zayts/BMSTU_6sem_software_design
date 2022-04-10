using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL.Models;
using BL.IRepositories;


using AccessToDB.RepositoriesTarantool;
using AccessToDB.Exceptions;
using AccessToDB;



namespace Tests
{
	public class MessagesDbTest
	{
		TarantoolContext _context;
		private readonly ITestOutputHelper output;

		public MessagesDbTest(ITestOutputHelper output)
		{
			this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new TarantoolContext(connection_string);
        }

        [Fact]
        public async Task Test_All()
        {
            IMessagesRepository rep = new TarantoolMessagesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetMessagesAsync());
            Task.Delay(1000).GetAwaiter().GetResult(); //вариант 2


            // add correct
            Message added_message1 = new Message(1, 1, 0, "text1");
            await rep.AddMessageAsync(added_message1);
            Message added_message2 = new Message(2, added_message1.SenderID, 2, "text2");
            await rep.AddMessageAsync(added_message2);
            Message added_message3 = new Message(3, 2, added_message2.CheckedByID, "text3");
            await rep.AddMessageAsync(added_message3);


            // add already existing
            await Assert.ThrowsAsync<MessageException>(() => rep.AddMessageAsync(added_message1));

			// get_by_ids correct
			var got_by_sender_id = await rep.GetMessagesBySenderIdAsync(added_message1.SenderID);
            Assert.Equal(2, got_by_sender_id.Count);
            Assert.Equal(added_message1, got_by_sender_id[0]);
            Assert.Equal(added_message2, got_by_sender_id[1]);

            got_by_sender_id = await rep.GetMessagesBySenderIdAsync(0);
            Assert.Empty(got_by_sender_id);

            var got_by_checker_id = await rep.GetMessagesByCheckerIdAsync(added_message2.CheckedByID);
            Assert.Equal(2, got_by_checker_id.Count);
            Assert.Equal(added_message2, got_by_checker_id[0]);
            Assert.Equal(added_message3, got_by_checker_id[1]);

            got_by_checker_id = await rep.GetMessagesByCheckerIdAsync(added_message1.CheckedByID);
            Assert.Single(got_by_checker_id);
            Assert.Equal(added_message1, got_by_checker_id[0]);

            //get list 
            var list = await rep.GetMessagesAsync();
            Assert.Equal(3, list.Count);
            Assert.Equal(added_message1, list[0]);
            Assert.Equal(added_message2, list[1]);
            Assert.Equal(added_message3, list[2]);

            // delete correct
            await rep.DeleteMessageAsync(added_message1);
            await rep.DeleteMessageAsync(added_message2);
            await rep.DeleteMessageAsync(added_message3);


			// delete not existing
			await Assert.ThrowsAsync<MessageException>(() => rep.DeleteMessageAsync(added_message1));

            // end tests - empty getlist
            Assert.Empty(await rep.GetMessagesAsync());


            Message tmp2 = await rep.AddMessageAutoIncrementAsync(added_message1);
            Assert.True(1 == tmp2.MessageID);
            Message tmp3 = await rep.AddMessageAutoIncrementAsync(added_message1);
            Assert.True(2 == tmp3.MessageID);
            await rep.DeleteMessageAsync(tmp2);
            await rep.DeleteMessageAsync(tmp3);
            Assert.Empty(await rep.GetMessagesAsync());
        }
    }
}
