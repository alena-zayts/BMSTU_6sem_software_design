using System.Collections.Generic;
using System.Threading.Tasks;

using ComponentBL.ModelsBL;


namespace ComponentBL.RepositoriesInterfaces
{
    public interface IMessagesRepository
    {
        Task<List<MessageBL>> GetList();
        Task Add(MessageBL message);
        Task Update(MessageBL message); 
        Task Delete(MessageBL message);
        Task<List<MessageBL>> GetListBySenderId(uint sender_id);
        Task<List<MessageBL>> GetListByCheckerId(uint checked_by_id);
    }
}
