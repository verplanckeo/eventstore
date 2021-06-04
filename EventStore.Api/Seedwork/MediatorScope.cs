#nullable enable
using System.Threading;
using System.Threading.Tasks;
using EventStore.Application.Mediator;
using MediatR;

namespace EventStore.Api.Seedwork
{
    /// <summary>
    /// Mediator scope
    /// </summary>
    public class MediatorScope : IMediatorScope
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// CTor
        /// </summary>
        /// <param name="mediator"></param>
        public MediatorScope(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync(object notification, CancellationToken cancellationToken = default)
        {
            await _mediator.Publish(notification, cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNotification"></typeparam>
        /// <param name="notification"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            await _mediator.Publish(notification, cancellationToken);
        }

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }

        public async Task<object?> SendAsync(object request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}