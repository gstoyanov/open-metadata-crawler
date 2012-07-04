using System;
using Moq;
using NUnit.Framework;

namespace OpenMetadataCrawler.Test
{
    [TestFixture]
    public class CrawlerTest
    {
        [SetUp]
        public virtual void SetUp()
        {
            this.webRequstMock = new Mock<IWebRequest>();
            this.webResponseMock = new Mock<IWebResponse>();
            this.webRequestFactoryMock = new Mock<WebRequestFactory>();

            this.webRequstMock
                .Setup( wr => wr.GetResponse() )
                .Returns( this.webResponseMock.Object );

            webRequestFactoryMock
                .Setup( f => f.Create( It.IsAny<Uri>() ) )
                .Returns( this.webRequstMock.Object );

            this.crawler = new Crawler( webRequestFactoryMock.Object );
        }

        protected const string TestUri = "http://example.com";

        protected Crawler crawler;
        protected Mock<WebRequestFactory> webRequestFactoryMock;
        protected Mock<IWebRequest> webRequstMock;
        protected Mock<IWebResponse> webResponseMock;
    }
}
