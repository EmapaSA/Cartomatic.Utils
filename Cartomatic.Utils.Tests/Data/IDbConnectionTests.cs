using System;
using System.Data;
using AwesomeAssertions;
using NUnit.Framework;

namespace Cartomatic.Utils.Data.Tests
{
    [TestFixture]
    public class IDbConnectionTests
    {
        [Test]
        public void ConnectionClose_WhenCalled_ProperlyClosesConnectionWithoutDispose()
        {
            var conn = new Connection();
            conn.Open();
            var stateAfterOpen = conn.State;

            conn.CloseConnection(dispose: false);
            var stateAfterClose = conn.State;

            stateAfterOpen.Should().Be(ConnectionState.Open);
            stateAfterClose.Should().Be(ConnectionState.Closed);
        }

        [Test]
        public void ConnectionClose_WhenCalled_ProperlyClosesAndDisposesConnection()
        {
            var conn = new Connection();
            conn.Open();
            var stateAfterOpen = conn.State;
            var disposeAfterOpen = conn.Disposed;

            conn.CloseConnection(dispose: true);
            var stateAfterClose = conn.State;
            var disposeAfterClose = conn.Disposed;

            stateAfterOpen.Should().Be(ConnectionState.Open);
            stateAfterClose.Should().Be(ConnectionState.Closed);

            disposeAfterOpen.Should().Be(false);
            disposeAfterClose.Should().Be(true);
        }

        class Connection : IDbConnection, IDisposable
        {
            public bool Disposed { get; set; }
        
            public void Dispose()
            {
                Disposed = true;
            }

            public IDbTransaction BeginTransaction()
            {
                throw new NotImplementedException();
            }

            public IDbTransaction BeginTransaction(IsolationLevel il)
            {
                throw new NotImplementedException();
            }

            public void Close()
            {
                State = ConnectionState.Closed;
            }

            public void ChangeDatabase(string databaseName)
            {
                throw new NotImplementedException();
            }

            public IDbCommand CreateCommand()
            {
                throw new NotImplementedException();
            }

            public void Open()
            {
                State = ConnectionState.Open;
            }

            public string ConnectionString { get; set; }
            public int ConnectionTimeout { get; private set; }
            public string Database { get; private set; }
            public ConnectionState State { get; private set; }
        }
    }
}
