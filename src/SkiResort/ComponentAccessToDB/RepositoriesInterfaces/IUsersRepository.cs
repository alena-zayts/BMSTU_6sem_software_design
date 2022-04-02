using System;
using System.Collections.Generic;
using SkiResort.ComponentBL.ModelsBL;

namespace SkiResort.ComponentAccessToDB.RepositoriesInterfaces
{
    public interface IUsersRepository
    {
        List<UserBL> GetList();
        UserBL GetById(uint user_id);
        void Add(UserBL user);
        void Update(UserBL user); 
        void Delete(UserBL user); 
    }
}
