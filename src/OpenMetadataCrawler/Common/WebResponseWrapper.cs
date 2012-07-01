using System;
using System.IO;
using System.Net;

namespace OpenMetadataCrawler
{
    internal sealed class WebResponseWrapper : IWebResponse, IDisposable
    {
        private readonly HttpWebResponse httpWebResponse;
        private MemoryStream responseStreamCopy;

        public WebResponseWrapper( HttpWebResponse httpWebResponse )
        {
            if ( httpWebResponse == null )
            {
                throw new ArgumentNullException( "httpWebResponse" );
            }

            this.httpWebResponse = httpWebResponse;
        }

        public bool IsFromCache
        {
            get
            {
                return this.httpWebResponse.IsFromCache;
            }
        }

        public bool IsMutuallyAuthenticated
        {
            get
            {
                return this.httpWebResponse.IsMutuallyAuthenticated;
            }
        }

        public CookieCollection Cookies
        {
            get
            {
                return this.httpWebResponse.Cookies;
            }
        }

        public WebHeaderCollection Headers
        {
            get
            {
                return this.httpWebResponse.Headers;
            }
        }

        public long ContentLength
        {
            get
            {
                return this.httpWebResponse.ContentLength;
            }
        }

        public string ContentEncoding
        {
            get
            {
                return this.httpWebResponse.ContentEncoding;
            }
        }

        public string ContentType
        {
            get
            {
                return this.httpWebResponse.ContentType;
            }
        }

        public string CharacterSet
        {
            get
            {
                return this.httpWebResponse.CharacterSet;
            }
        }

        public string Server
        {
            get
            {
                return this.httpWebResponse.Server;
            }
        }

        public DateTime LastModified
        {
            get
            {
                return this.httpWebResponse.LastModified;
            }
        }

        public HttpStatusCode StatusCode
        {
            get
            {
                return this.httpWebResponse.StatusCode;
            }
        }

        public string StatusDescription
        {
            get
            {
                return this.httpWebResponse.StatusDescription;
            }
        }

        public Version ProtocolVersion
        {
            get
            {
                return this.httpWebResponse.ProtocolVersion;
            }
        }

        public Uri ResponseUri
        {
            get
            {
                return this.httpWebResponse.ResponseUri;
            }
        }

        public string Method
        {
            get
            {
                return this.httpWebResponse.Method;
            }
        }

        public Stream GetResponseStream()
        {
            if ( this.responseStreamCopy == null )
            {
                this.responseStreamCopy = new MemoryStream();
                Stream responseStream = this.httpWebResponse
                    .GetResponseStream();

                responseStream.CopyTo( this.responseStreamCopy );
            }

            this.responseStreamCopy.Seek( 0, SeekOrigin.Begin );
            return this.responseStreamCopy;
        }

        public string GetResponseHeader( string headerName )
        {
            return this.httpWebResponse.GetResponseHeader( headerName );
        }

        public void Dispose()
        {
            if ( this.responseStreamCopy != null )
            {
                this.responseStreamCopy.Dispose();
            }
        }
    }
}
