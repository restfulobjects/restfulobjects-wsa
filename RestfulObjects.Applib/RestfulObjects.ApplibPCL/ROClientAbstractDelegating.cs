using RestfulObjects.Spec;

namespace RestfulObjects.Applib
{
    /// <summary>
    /// Implementation that delegates to an underlying ROClient.
    /// </summary>
    /// <remarks>
    /// The intent is to be able to easily wrap non-threadsafe implementations of ROClient, 
    /// returning a new instance of the underlying client for each request.  For threadsafe
    /// implementations, there is no need to use this class (or can use and simply return the
    /// same underlying ROClient each time).
    /// </remarks>
    public abstract class ROClientAbstractDelegating : ROClientAbstract
    {
        protected ROClientAbstractDelegating(string baseUrl) : base(baseUrl)
        {
        }

        public override T Execute<T>(HttpMethod httpMethod, RORequest roRequest, object args = null)
        {
            return UnderlyingROClient().Execute<T>(httpMethod, roRequest, args);
        }

        /// <summary>
        /// Return an ROClient to delegate to.  For non-threadsafe implementations, should instantiate
        /// a new client each time called.  For threadsafe implementations, can always return the
        /// same client.
        /// </summary>
        /// <returns></returns>
        protected abstract ROClient UnderlyingROClient();
    }

}
