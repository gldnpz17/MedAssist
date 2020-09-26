namespace EventAggregatorLibrary
{
    //A simple self-made event aggregator(in case third party libraries aren't allowed).

    /// <summary>
    /// The EventAggregator class.
    /// Groups events together and makes them easily accessible.
    /// </summary>
    public interface IEventAggregator
    {
        /// <summary>
        /// Gets an event of type T.
        /// </summary>
        /// <typeparam name="T">The event type.</typeparam>
        /// <returns>The requested event's instance</returns>
        T GetEvent<T>() where T : AggregateEventBase;
    }
}