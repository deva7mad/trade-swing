
namespace TradeSwing.Application.Services;

public interface IAuthenticationService
{
    AuthenticationResult Login(string mobile, string password);
    AuthenticationResult Register(string firstName, string lastName, string email, string mobile, string password);
}