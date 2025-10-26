namespace EventStore.Core.DddSeedwork
{
    /* Why do we <out T> to this interface? This is to denote that the type T is covariant

        In C#, covariance and contravariance enable implicit reference conversion for array types, delegate types, and generic type arguments.
        Covariance preserves assignment compatibility and contravariance reverses it.
        > Assignment compatibility.
            string str = "test";  
        > An object of a more derived type is assigned to an object of a less derived type.
            object obj = str;  

        > Covariance.
            IEnumerable<string> strings = new List<string>();  
        > An object that is instantiated with a more derived type argument
          is assigned to an object instantiated with a less derived type argument.
          Assignment compatibility is preserved.
            IEnumerable<object> objects = strings;  

        > Contravariance.
          Assume that the following method is in the class:
            static void SetObject(object o) { }
            Action<object> actObject = SetObject;  
        > An object that is instantiated with a less derived type argument
          is assigned to an object instantiated with a more derived type argument.
          Assignment compatibility is reversed.
            Action<string> actString = actObject; 
    */

    /// <summary>
    /// Specify a DDD entity must have an ID property of type <see cref="IEntityId"/>.
    /// </summary>
    /// <typeparam name="TIdentity"></typeparam>
    public interface IEntity<out TIdentity> where TIdentity: IEntityId
    {
        /// <summary>
        /// Identity property
        /// </summary>
        TIdentity Id { get; }
    }
}