using System;
using System.Collections.Generic;

namespace OpenMetadataCrawler
{
    /// <summary>
    /// The base crawler object. It can extract metadata from an url using a
    /// collection of response readers. This object is responsible for making
    /// the actual web request and calling all of its readers in the order they
    /// were added. Every reader's CanRead method is called with the web
    /// response and if it returns true then it's Read method is called to read
    /// the actual data. The data from all readers is aggregated, but if more
    /// than one reader returns a value for a given key, the **first** one wins.
    /// </summary>
    public class Crawler
    {
        private readonly List<IResponseReader> responseReaders
            = new List<IResponseReader>();

        private readonly WebRequestFactory webRequestFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Crawler"/> class using
        /// the specified webRequestFactory. The factory is a simple object that
        /// can create web requests.
        /// </summary>
        /// <param name="webRequestFactory">The web request factory.</param>
        public Crawler( WebRequestFactory webRequestFactory )
        {
            this.webRequestFactory = webRequestFactory;
        }

        /// <summary>
        /// Adds the reader to this crawler object. This means that the reader
        /// will be called for all subsequent web requests. This method returns
        /// the instance of this crawler to support method chaining.
        /// </summary>
        /// <param name="responseReader">The response reader.</param>
        /// <returns>
        /// Returns this instance to support method chaining.
        /// </returns>
        public Crawler AddReader( IResponseReader responseReader )
        {
            this.responseReaders.Add( responseReader );

            return this;
        }

        /// <summary>
        /// This overload is the same as GetRaw( Uri uri ), but accepts the uri
        /// parameter as string instead of as an instance of the Uri class.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// A collection with all metadata items that were extracted from the
        /// given URI.
        /// </returns>
        public IEnumerable<MetadataItem> GetRaw( string uri )
        {
            return this.GetRaw( new Uri( uri ) );
        }

        /// <summary>
        /// Makes a web request to the specified URI and returns a collection
        /// with all metadata items that were extracted using the registered
        /// readers. Note that if you don't add any readers before making the
        /// request (or if no data was found) this method will return an empty
        /// collection.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// A collection with all metadata items that were extracted from the
        /// given URI.
        /// </returns>
        public IEnumerable<MetadataItem> GetRaw( Uri uri )
        {
            IWebRequest request = this.webRequestFactory.Create( uri );
            using ( IWebResponse response = request.GetResponse() )
            {
                var results = new List<MetadataItem>();

                foreach ( IResponseReader reader in this.responseReaders )
                {
                    if ( reader.CanRead( response ) )
                    {
                        IEnumerable<MetadataItem> readerResults = reader.Read( response );
                        CollectItems( results, readerResults );
                    }
                }

                return results;
            }
        }

        private static void CollectItems(
            List<MetadataItem> results,
            IEnumerable<MetadataItem> readerResults )
        {
            if ( readerResults == null )
            {
                return;
            }

            foreach ( MetadataItem newItem in readerResults )
            {
                // names are unique and older items take precedence
                bool exists = results.Exists( m => m.Name == newItem.Name );
                if ( !exists )
                {
                    results.Add( newItem );
                }
            }
        }
    }
}
