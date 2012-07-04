using System;
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

        public IWebResponse GetResponse()
        {
            WebResponse response = this.webRequest.GetResponse();

            return new WebResponseWrapper( ( HttpWebResponse )response );
        }
    }
}
