using Mapster;
using TradeSwing.Application.Services;
using TradeSwing.Contracts.Authentication;

namespace TradeSwing.APIs.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.UserEntity);
    }
}