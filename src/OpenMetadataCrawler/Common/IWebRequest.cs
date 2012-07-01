using System;
using System.Diagnostics.CodeAnalysis;

namespace OpenMetadataCrawler
{
    /// <summary>
    /// An interface for the web request object.
    /// </summary>
    public interface IWebRequest
    {
        /// <summary>
        /// Gets or sets the user agent string that will be send with this
        /// request.
        /// </summary>
        string UserAgent
        {
            get;
            set;
        }

        /// <summary>
        /// Sends the request to the remote server and returns the response
        /// object.
        /// </summary>
        /// <returns>
        /// The response of this request.
        /// </returns>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = @"A property wouldn't be appropriated, because this
            method performs a relatively slow operation and also this interface
            is trying to look similar to the interface of the
            System.Net.HttpWebRequest class to look more familiar to developers
            using it." )]
        IWebResponse GetResponse();
    }
}
