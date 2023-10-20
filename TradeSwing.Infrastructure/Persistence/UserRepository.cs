using TradeSwing.Application.Persistence;
using TradeSwing.Domain.Entities;

namespace TradeSwing.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<UserEntity> Users = new(); 
    public void AddUser(UserEntity userEntity)
    {
        Users.Add(userEntity);
    }

    public UserEntity? GetUserByMobile(string mobile)
    {
        return Users.SingleOrDefault(u => u.Mobile == mobile);
    }

    public UserEntity? GetUserEmail(string email)
    {
        return Users.SingleOrDefault(u => u.Email == email);
    }
}