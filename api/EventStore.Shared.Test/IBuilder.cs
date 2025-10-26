using System;
using System.Linq.Expressions;

namespace EventStore.Shared.Test
{
    public interface IBuilder<T> where T : class
    {
        T Item { get; }

        IBuilder<T> With<TProp>(Expression<Func<T, TProp>> expression, TProp value);

        T Build();
    }
}