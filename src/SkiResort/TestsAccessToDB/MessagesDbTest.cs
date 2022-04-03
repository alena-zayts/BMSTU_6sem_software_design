using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

using ProGaudi.Tarantool.Client;

using ComponentBL.ModelsBL;
using ComponentBL.RepositoriesInterfaces;


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

			var box = Box.Connect("ski_admin:Tty454r293300@localhost:3301").GetAwaiter().GetResult();

			_context = new ContextTarantool(box);
		}

        [Fact]
        public async Task Test_All()
        {
            IMessagesRepository rep = new TarantoolMessagesRepository(_context);

            //start testing 
            Assert.Empty(await rep.GetList());
            Task.Delay(1000).GetAwaiter().GetResult(); //вариант 2


            // add correct
            MessageBL added_message1 = new MessageBL(1, 1, 0, "text1");
            await rep.Add(added_message1);
            MessageBL added_message2 = new MessageBL(2, added_message1.sender_id, 2, "text2");
            await rep.Add(added_message2);
            MessageBL added_message3 = new MessageBL(3, 2, added_message2.checked_by_id, "text3");
            await rep.Add(added_message3);


            // add already existing
            await Assert.ThrowsAsync<MessageDBException>(() => rep.Add(added_message1));

			// get_by_ids correct
			var got_by_sender_id = await rep.GetListBySenderId(added_message1.sender_id);
            Assert.Equal(2, got_by_sender_id.Count);
            Assert.Equal(added_message1, got_by_sender_id[0]);
            Assert.Equal(added_message2, got_by_sender_id[1]);

            got_by_sender_id = await rep.GetListBySenderId(0);
            Assert.Empty(got_by_sender_id);

            var got_by_checker_id = await rep.GetListByCheckerId(added_message2.checked_by_id);
            Assert.Equal(2, got_by_checker_id.Count);
            Assert.Equal(added_message2, got_by_checker_id[0]);
            Assert.Equal(added_message3, got_by_checker_id[1]);

            got_by_checker_id = await rep.GetListByCheckerId(added_message1.checked_by_id);
            Assert.Single(got_by_checker_id);
            Assert.Equal(added_message1, got_by_checker_id[0]);

            //get list 
            var list = await rep.GetList();
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
            Assert.Empty(await rep.GetList());
        }
    }
}
