using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using BL.Models;
using BL.IRepositories;


using ComponentAccessToDB.RepositoriesTarantool;
using ComponentAccessToDB;



namespace Tests
{
	public class MessagesDbTest
	{
		ContextTarantool _context;
		private readonly ITestOutputHelper output;

		public MessagesDbTest(ITestOutputHelper output)
		{
			this.output = output;

            string connection_string = "ski_admin:Tty454r293300@localhost:3301";
            _context = new ContextTarantool(connection_string);
        }

        [Fact]
        public async Task Test_All()
        {
            IMessagesRepository rep = new TarantoolMessagesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetMessagesAsync());
            Task.Delay(1000).GetAwaiter().GetResult(); //вариант 2


            // add correct
            MessageBL added_message1 = new MessageBL(1, 1, 0, "text1");
            await rep.Add(added_message1);
            MessageBL added_message2 = new MessageBL(2, added_message1.SenderID, 2, "text2");
            await rep.Add(added_message2);
            MessageBL added_message3 = new MessageBL(3, 2, added_message2.CheckedByID, "text3");
            await rep.Add(added_message3);


            // add already existing
            await Assert.ThrowsAsync<MessageDBException>(() => rep.Add(added_message1));

			// get_by_ids correct
			var got_by_sender_id = await rep.GetListBySenderId(added_message1.SenderID);
            Assert.Equal(2, got_by_sender_id.Count);
            Assert.Equal(added_message1, got_by_sender_id[0]);
            Assert.Equal(added_message2, got_by_sender_id[1]);

            got_by_sender_id = await rep.GetMessagesBySenderIdAsync(0);
            Assert.Empty(got_by_sender_id);

            var got_by_checker_id = await rep.GetListByCheckerId(added_message2.CheckedByID);
            Assert.Equal(2, got_by_checker_id.Count);
            Assert.Equal(added_message2, got_by_checker_id[0]);
            Assert.Equal(added_message3, got_by_checker_id[1]);

            got_by_checker_id = await rep.GetListByCheckerId(added_message1.CheckedByID);
            Assert.Single(got_by_checker_id);
            Assert.Equal(added_message1, got_by_checker_id[0]);

            //get list 
            var list = await rep.GetMessagesAsync();
            Assert.Equal(3, list.Count);
            Assert.Equal(added_message1, list[0]);
            Assert.Equal(added_message2, list[1]);
            Assert.Equal(added_message3, list[2]);

            // delete correct
            await rep.Delete(added_message1);
            await rep.Delete(added_message2);
            await rep.Delete(added_message3);


			// delete not existing
			await Assert.ThrowsAsync<MessageDBException>(() => rep.Delete(added_message1));

            // end tests - empty getlist
            Assert.Empty(await rep.GetMessagesAsync());


            MessageBL tmp2 = await rep.AddAutoIncrement(added_message1);
            Assert.True(1 == tmp2.MessageID);
            MessageBL tmp3 = await rep.AddAutoIncrement(added_message1);
            Assert.True(2 == tmp3.MessageID);
            await rep.Delete(tmp2);
            await rep.Delete(tmp3);
            Assert.Empty(await rep.GetMessagesAsync());
        }
    }
}
