using System;
using System.Linq.Expressions;
using System.Reflection;

namespace EventStore.Shared.Test
{
    public class GenericBuilder<T> : IBuilder<T> where T : class
    {
        public T Item { get; private set; }

        public GenericBuilder()
        {
            Item = Activator.CreateInstance<T>();
        }

        public GenericBuilder<T> SetDefaults(Action<T> action)
        {
            action(Item);
            return this;
        }

        public GenericBuilder<T> SetDefaults(Func<T> func)
        {
            Item = func();
            return this;
        }

        public IBuilder<T> With<TProp>(Expression<Func<T, TProp>> expression, TProp value)
        {
            var prop = typeof(T).GetProperty(((MemberExpression) expression.Body).Member.Name, BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty);
            prop.SetValue(Item, value, null);

            return this;
        }

        public virtual T Build()
        {
            return Item;
        }
    }
}