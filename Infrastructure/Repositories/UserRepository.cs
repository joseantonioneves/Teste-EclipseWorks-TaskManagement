using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users; // Pode ser substituído por uma base de dados, como Entity Framework

        public UserRepository()
        {
            _users = new List<User>(); // Este é apenas um exemplo com dados em memória
        }

        public User GetById(Guid userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }

        public User GetByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users;
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Update(User user)
        {
            var existingUser = GetById(user.Id);
            if (existingUser != null)
            {
                existingUser = user; // Aqui pode ser feita uma lógica de atualização mais refinada
            }
        }

        public void Remove(Guid userId)
        {
            var user = GetById(userId);
            if (user != null)
            {
                _users.Remove(user);
            }
        }
    }
}
