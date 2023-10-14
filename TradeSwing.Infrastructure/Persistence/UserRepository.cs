using TradeSwing.Application.Persistence;
using TradeSwing.Domain.Entities;

namespace TradeSwing.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> Users = new(); 
    public void AddUser(User user)
    {
        Users.Add(user);
    }

    public User? GetUserByMobile(string mobile)
    {
        return Users.SingleOrDefault(u => u.Mobile == mobile);
    }

    public User? GetUserEmail(string email)
    {
        return Users.SingleOrDefault(u => u.Email == email);
    }
}