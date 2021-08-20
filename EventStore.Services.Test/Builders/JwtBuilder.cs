using EventStore.Infrastructure.Seedwork;
using EventStore.Shared.Test;

namespace EventStore.Services.Test.Builders
{
    public class JwtBuilder : GenericBuilder<Jwt>
    {
        public JwtBuilder()
        {
            SetDefaults(() => new Jwt{ Issuer = "EventStore.Services.Test", Key = "This-Is-A-Very-Secure-UnitTest-Key-That-Is-Even-Using-A-Number-1", ValidFor = 3600 });
        }
    }
}