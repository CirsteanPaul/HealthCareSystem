using Healthcare.Application.Test.Commands;
using Mapster;
using WebApi.Controllers;

namespace WebApi.Mappings;

public class TestMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTestRequest, CreateTestRequestCommand>()
            .MapToConstructor(true);
    }
}