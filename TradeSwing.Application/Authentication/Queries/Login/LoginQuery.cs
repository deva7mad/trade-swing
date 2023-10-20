using ErrorOr;
using MediatR;
using TradeSwing.Application.Services;

namespace TradeSwing.Application.Authentication.Queries.Login;

public record LoginQuery(string Mobile, string Password) : IRequest<ErrorOr<AuthenticationResult>>;