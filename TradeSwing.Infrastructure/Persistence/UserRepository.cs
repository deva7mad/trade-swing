using TradeSwing.Domain.Entities;
using TradeSwing.Application.Persistence;

namespace TradeSwing.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<UserEntity> Users = []; 
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