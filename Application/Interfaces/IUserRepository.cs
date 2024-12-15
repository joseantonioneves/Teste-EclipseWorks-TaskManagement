using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        User GetById(Guid userId);
        User GetByEmail(string email);
        IEnumerable<User> GetAllUsers();
        void Add(User user);
        void Update(User user);
        void Remove(Guid userId);
    }
}
