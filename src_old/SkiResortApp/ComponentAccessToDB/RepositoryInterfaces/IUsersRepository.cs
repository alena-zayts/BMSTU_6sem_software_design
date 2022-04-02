using System;
using System.Collections.Generic;
using SkiResortApp.ComponentAccessToDB.DBModels;


namespace SkiResortApp.ComponentAccessToDB.RepositoryInterfaces
{
    public interface IUsersRepository
    {
        List<UserDB> GetList();
        UserDB GetById(uint user_id);
        void Add(UserDB user);
        void Update(UserDB user); 
        void Delete(UserDB user); 
    }
}
