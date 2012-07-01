using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace OpenMetadataCrawler
{
    internal class WebRequestWrapper : IWebRequest
    {
        private readonly HttpWebRequest webRequest;

        public WebRequestWrapper( Uri uri )
        {
            this.webRequest = ( HttpWebRequest )WebRequest.Create( uri );
        }

        public string UserAgent
        {
            get
            {
                return this.webRequest.UserAgent;
            }

            set
            {
                this.webRequest.UserAgent = value;
            }
        }

        [SuppressMessage(
            "Microsoft.Reliability",
            "CA2000:Dispose objects before losing scope",
            Justification = @"The client of the web request is supposed to use
            the response object and then dispose it." )]
        public IWebResponse GetResponse()
        {
            WebResponse response = this.webRequest.GetResponse();

            return new WebResponseWrapper( ( HttpWebResponse )response );
        }
    }
}
