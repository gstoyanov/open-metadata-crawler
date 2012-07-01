using System;

namespace OpenMetadataCrawler
{
    /// <summary>
    /// A simple factory that can create web requests. Used to support easier
    /// unit testing.
    /// </summary>
    public class WebRequestFactory
    {
        /// <summary>
        /// Creates a new web request object initialized with the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The created web request object.</returns>
        public virtual IWebRequest Create( string uri )
        {
            if ( uri == null )
            {
                throw new ArgumentNullException( "uri" );
            }

            return this.Create( new Uri( uri ) );
        }

        /// <summary>
        /// Creates a new web request object initialized with the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The created web request object.</returns>
        public virtual IWebRequest Create( Uri uri )
        {
            if ( uri == null )
            {
                throw new ArgumentNullException( "uri" );
            }

            return new WebRequestWrapper( uri );
        }
    }
}
