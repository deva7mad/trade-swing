using ErrorOr;
using MediatR;
using TradeSwing.Application.Services;

namespace TradeSwing.Application.Authentication.Commands.Register;

public record RegisterCommand(string FirstName, string LastName, string Email, string Mobile, string Password) : IRequest<ErrorOr<AuthenticationResult>>;