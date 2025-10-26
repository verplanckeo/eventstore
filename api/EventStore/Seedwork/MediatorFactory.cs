using Autofac;
using EventStore.Application.Mediator;

namespace EventStore.Seedwork
{
    public class MediatorFactory : IMediatorFactory
    {
        private ILifetimeScope _lifetimeScope;

        public MediatorFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }
        public IMediatorScope CreateScope()
        {
            return _lifetimeScope.Resolve<IMediatorScope>();
        }
    }
}