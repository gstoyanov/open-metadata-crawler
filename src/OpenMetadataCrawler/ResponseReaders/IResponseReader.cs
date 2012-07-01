using System;
using System.Collections.Generic;

namespace OpenMetadataCrawler
{
    /// <summary>
    /// An object that can read metadata from an HTTP response returned from a
    /// remote server.
    /// </summary>
    public interface IResponseReader
    {
        /// <summary>
        /// Determines whether this instance can read the specified HTTP web
        /// response. This method shouldn't throw any exceptions.
        /// </summary>
        /// <param name="webResponse">The HTTP web response.</param>
        /// <returns>
        /// True if this instance can read the specified HTTP response;
        /// otherwise, false.
        /// </returns>
        bool CanRead( IWebResponse webResponse );

        /// <summary>
        /// Reads the specified HTTP response and returns all metadata that this
        /// instance can extract. This method shouldn't throw any exceptions.
        /// </summary>
        /// <param name="webResponse">The HTTP response.</param>
        /// <returns>A collection with the extracted metadata.</returns>
        IEnumerable<MetadataItem> Read( IWebResponse webResponse );
    }
}
