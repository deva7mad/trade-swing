using TradeSwing.Domain.Entities;

namespace TradeSwing.Application.Persistence;

public interface IUserRepository
{
    void AddUser(User user);
    User? GetUserByMobile(string mobile);
    User? GetUserEmail(string email);
}