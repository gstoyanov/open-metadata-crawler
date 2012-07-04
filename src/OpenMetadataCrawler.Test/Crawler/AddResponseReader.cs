using System;
using System.Collections.Generic;
using Moq;
using Moq.Sequences;
using NUnit.Framework;

namespace OpenMetadataCrawler.Test
{
    [TestFixture]
    public class AddResponseReader : CrawlerTest
    {
        private List<Mock<IResponseReader>> readers;

        public override void SetUp()
        {
            base.SetUp();

            readers = new List<Mock<IResponseReader>>();
        }

        private void AddReader( bool? canRead = null )
        {
            var readerMock = new Mock<IResponseReader>();
            this.readers.Add( readerMock );

            if ( canRead.HasValue )
            {
                readerMock
                    .Setup( r => r.CanRead( this.webResponseMock.Object ) )
                    .Returns( canRead.Value );
            }

            this.crawler.AddReader( readerMock.Object );

            return;
        }

        private void VerifyReadCalled( Times times )
        {
            this.readers.ForEach( readerMock => readerMock.Verify(
                r => r.Read( this.webResponseMock.Object ), times ) );
        }

        private void VerifyCanReadCalled( Times times )
        {
            this.readers.ForEach( readerMock => readerMock.Verify(
                r => r.CanRead( this.webResponseMock.Object ), times ) );
        }

        [Test]
        public void MultipleReadersCanReadIsCalled()
        {
            this.AddReader();
            this.AddReader();
            this.AddReader();

            this.crawler.GetRaw( TestUri );

            this.VerifyCanReadCalled( Times.Once() );
        }

        [Test]
        public void MultipleReadersReadIsCalledWhenCanReadReturnsTrue()
        {
            this.AddReader( true );
            this.AddReader( true );
            this.AddReader( true );

            this.crawler.GetRaw( TestUri );

            this.VerifyReadCalled( Times.Once() );
        }

        [Test]
        public void MultipleReadersReadNotCalledWhenCanReadReturnsFalse()
        {
            this.AddReader( false );
            this.AddReader( false );
            this.AddReader( false );

            this.crawler.GetRaw( TestUri );

            this.VerifyReadCalled( Times.Never() );
        }

        [Test]
        public void ReadersAreCalledInTheOrderTheyAreAdded()
        {
            this.AddReader( true );
            this.AddReader( true );
            this.AddReader( true );

            using ( Sequence.Create() )
            {
                this.readers[ 0 ].Setup( r => r.Read( this.webResponseMock.Object ) ).InSequence();
                this.readers[ 1 ].Setup( r => r.Read( this.webResponseMock.Object ) ).InSequence();
                this.readers[ 2 ].Setup( r => r.Read( this.webResponseMock.Object ) ).InSequence();

                this.crawler.GetRaw( TestUri );
            }
        }

        [Test]
        public void SingleReaderCanReadIsCalled()
        {
            this.AddReader();

            this.crawler.GetRaw( TestUri );

            this.VerifyCanReadCalled( Times.Once() );
        }

        [Test]
        public void SingleReaderReadIsCalledWhenCanReadReturnsTrue()
        {
            this.AddReader( true );

            this.crawler.GetRaw( TestUri );

            this.VerifyReadCalled( Times.Once() );
        }

        [Test]
        public void SingleReaderReadNotCalledWhenCanReadReturnsFalse()
        {
            this.AddReader( false );

            this.crawler.GetRaw( TestUri );

            this.VerifyReadCalled( Times.Never() );
        }
    }
}
