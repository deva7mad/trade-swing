using TradeSwing.Domain.Entities;

namespace TradeSwing.Application.Persistence;

public interface IUserRepository
{
    void AddUser(UserEntity userEntity);
    UserEntity? GetUserByMobile(string mobile);
    UserEntity? GetUserEmail(string email);
}