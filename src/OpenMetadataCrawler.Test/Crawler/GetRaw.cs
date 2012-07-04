using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace OpenMetadataCrawler.Test
{
    [TestFixture]
    public class GetRaw : CrawlerTest
    {
        private readonly IEnumerable<MetadataItem> sampleResults1 = new[]
        {
            new MetadataItem( "name1", "value1" ),
            new MetadataItem( "name2", "value2" ),
        };

        private readonly IEnumerable<MetadataItem> sampleResults2 = new[]
        {
            new MetadataItem( "name3", "value3" ),
            new MetadataItem( "name4", "value4" ),
        };

        private void AddReader( IEnumerable<MetadataItem> results )
        {
            var reader = new Mock<IResponseReader>();
            reader
                .Setup( r => r.CanRead( this.webResponseMock.Object ) )
                .Returns( true );
            reader
                .Setup( r => r.Read( this.webResponseMock.Object ) )
                .Returns( results );

            this.crawler.AddReader( reader.Object );
        }

        [Test]
        public void EmptyCollectionIsReturnedIfTheReaderDoesntExtractAnything()
        {
            this.AddReader( Enumerable.Empty<MetadataItem>() );

            IEnumerable<MetadataItem> metadata = this.crawler.GetRaw( TestUri );

            metadata.Should().BeEmpty();
        }

        [Test]
        public void EmptyCollectionIsReturnedWithoutReaders()
        {
            IEnumerable<MetadataItem> metadata = this.crawler.GetRaw( TestUri );

            metadata.Should().BeEmpty();
        }

        [Test]
        public void IfTheReaderReturnsNullTheMethodShouldntFail()
        {
            this.AddReader( null );

            IEnumerable<MetadataItem> metadata = this.crawler.GetRaw( TestUri );

            metadata.Should().BeEmpty();
        }

        [Test]
        public void ResultsFromASingleReaderAreReturned()
        {
            this.AddReader( this.sampleResults1 );

            IEnumerable<MetadataItem> metadata = this.crawler.GetRaw( TestUri );

            metadata.Should().BeEquivalentTo( this.sampleResults1 );
        }

        [Test]
        public void ResultsFromMultipleReadersShouldBeMerged()
        {
            this.AddReader( this.sampleResults1 );
            this.AddReader( this.sampleResults2 );

            IEnumerable<MetadataItem> metadata = this.crawler.GetRaw( TestUri );

            metadata.Should().BeEquivalentTo(
                this.sampleResults1.Concat( this.sampleResults2 ) );
        }

        [Test]
        public void WebRequestIsSendToTheUri()
        {
            this.crawler.GetRaw( TestUri );

            this.webRequestFactoryMock.Verify( f =>
                f.Create( It.Is<Uri>( uri => uri.Equals( new Uri( TestUri ) ) ) ),
                Times.Once() );
        }

        [Test]
        public void WhenThereIsANameCollisionTheFirstReaderWins()
        {
            this.AddReader( this.sampleResults1 );

            IEnumerable<MetadataItem> secondReaderResults = this.sampleResults2.Concat( new[]
            {
                new MetadataItem( "name1", "different value" ),
            } );

            this.AddReader( secondReaderResults );

            IEnumerable<MetadataItem> metadata = this.crawler.GetRaw( TestUri );

            // the itme added with concat to the second result set should be
            // ignored
            metadata.Should().BeEquivalentTo(
                this.sampleResults1.Concat( this.sampleResults2 ) );
        }
    }
}
