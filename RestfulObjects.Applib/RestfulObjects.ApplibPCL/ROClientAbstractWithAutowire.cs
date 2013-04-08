using RestfulObjects.Spec;

namespace RestfulObjects.Applib
{
    public abstract class ROClientAbstractWithAutowire : ROClientAbstract
    {
        protected ROClientAbstractWithAutowire(string baseUrl)
            : base(baseUrl)
        {
        }

        /// <summary>
        /// implementation of mandatory hook method that autowires any returned representations
        /// with itself if the representation declares that it is ROClientAware.
        /// </summary>
        public override T Execute<T>(HttpMethod httpMethod, RORequest roRequest, object args = null)
        {
            var response = DoExecute<T>(httpMethod, roRequest, args);

            var roClientAware = response as ROClientAware;
            if (roClientAware != null)
            {
                roClientAware.ROClient = this;
            }
            return response;
        }

        public abstract T DoExecute<T>(HttpMethod httpMethod, RORequest roRequest, object args = null)
            where T : JsonRepr, new();

    }

}
