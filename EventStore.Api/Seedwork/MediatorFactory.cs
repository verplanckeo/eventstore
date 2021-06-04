using Autofac;
using EventStore.Application.Mediator;
using MediatR;

namespace EventStore.Api.Seedwork
{
    /// <summary>
    /// Factory used to add features in the future for mediator scopes
    /// </summary>
    public class MediatorFactory : IMediatorFactory
    {
        private ILifetimeScope _lifetimeScope;

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="lifetimeScope"></param>
        public MediatorFactory(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        /// <summary>
        /// Create mediatorscope instance
        /// </summary>
        /// <returns></returns>
        public IMediatorScope CreateScope()
        {
            return _lifetimeScope.Resolve<IMediatorScope>();
        }
    }
}