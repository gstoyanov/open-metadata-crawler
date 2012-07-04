using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;

namespace OpenMetadataCrawler
{
    /// <summary>
    /// An interface for the web response object.
    /// </summary>
    public interface IWebResponse : IDisposable
    {
        /// <summary>
        /// Gets a value indicating whether this response was obtained from the
        /// cache.
        /// </summary>
        /// <returns>
        /// true if the response was taken from the cache; otherwise, false.
        /// </returns>
        bool IsFromCache
        {
            get;
        }

        /// <summary>
        /// Gets a value indicating whether both client and server were
        /// authenticated.
        /// </summary>
        /// <returns>
        /// true if mutual authentication occurred; otherwise, false.
        /// </returns>
        bool IsMutuallyAuthenticated
        {
            get;
        }

        /// <summary>
        /// Gets the cookies that are associated with this response.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Net.CookieCollection"/> that contains the
        /// cookies that are associated with this response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        CookieCollection Cookies
        {
            get;
        }

        /// <summary>
        /// Gets the headers that are associated with this response from the
        /// server.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Net.WebHeaderCollection"/> that contains the
        /// header information returned with the response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        WebHeaderCollection Headers
        {
            get;
        }

        /// <summary>
        /// Gets the length of the content returned by the request.
        /// </summary>
        /// <returns>
        /// The number of bytes returned by the request. Content length does not
        /// include header information.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        long ContentLength
        {
            get;
        }

        /// <summary>
        /// Gets the method that is used to encode the body of the response.
        /// </summary>
        /// <returns>
        /// A string that describes the method that is used to encode the body
        /// of the response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string ContentEncoding
        {
            get;
        }

        /// <summary>
        /// Gets the content type of the response.
        /// </summary>
        /// <returns>
        /// A string that contains the content type of the response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string ContentType
        {
            get;
        }

        /// <summary>
        /// Gets the character set of the response.
        /// </summary>
        /// <returns>
        /// A string that contains the character set of the response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string CharacterSet
        {
            get;
        }

        /// <summary>
        /// Gets the name of the server that sent the response.
        /// </summary>
        /// <returns>
        /// A string that contains the name of the server that sent the
        /// response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string Server
        {
            get;
        }

        /// <summary>
        /// Gets the last date and time that the contents of the response were
        /// modified.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.DateTime"/> that contains the date and time
        /// that the contents of the response were modified.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        DateTime LastModified
        {
            get;
        }

        /// <summary>
        /// Gets the status of the response.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Net.HttpStatusCode"/> values.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        HttpStatusCode StatusCode
        {
            get;
        }

        /// <summary>
        /// Gets the status description returned with the response.
        /// </summary>
        /// <returns>
        /// A string that describes the status of the response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string StatusDescription
        {
            get;
        }

        /// <summary>
        /// Gets the version of the HTTP protocol that is used in the response.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Version"/> that contains the HTTP protocol
        /// version of the response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        Version ProtocolVersion
        {
            get;
        }

        /// <summary>
        /// Gets the URI of the Internet resource that responded to the request.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Uri"/> that contains the URI of the Internet
        /// resource that responded to the request.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        Uri ResponseUri
        {
            get;
        }

        /// <summary>
        /// Gets the method that is used to return the response.
        /// </summary>
        /// <returns>
        /// A string that contains the HTTP method that is used to return the
        /// response.
        /// </returns>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string Method
        {
            get;
        }

        /// <summary>
        /// Gets the stream that is used to read the body of the response from
        /// the server. This <see cref="Stream"/> object is a copy of the
        /// original response stream that supports <see cref="Stream.Seek"/>, so
        /// clients can use it without affecting other clients. Every time this
        /// method is called the position of the stream is returned back to the
        /// beginning of the stream.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.IO.Stream"/> containing the body of the
        /// response.
        /// </returns>
        /// <exception cref="T:System.Net.ProtocolViolationException">
        /// There is no response stream.
        /// </exception>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        [SuppressMessage(
            "Microsoft.Design",
            "CA1024:UsePropertiesWhereAppropriate",
            Justification = @"A property wouldn't be appropriated, because this
            method performs a relatively slow operation the first time it is
            called. Furthermore, it restarts the position of the stream on every
            call. Moreover, this interface is trying to look similar to the
            interface of the System.Net.HttpWebResponse class to look more
            familiar to developers using it." )]
        Stream GetResponseStream();

        /// <summary>
        /// Gets the contents of a header that was returned with the response.
        /// </summary>
        /// <returns>
        /// The contents of the specified header.
        /// </returns>
        /// <param name="headerName">
        /// The header value to return.
        /// </param>
        /// <exception cref="T:System.ObjectDisposedException">
        /// The current instance has been disposed.
        /// </exception>
        string GetResponseHeader( string headerName );
    }
}
