using System.Linq;
using System.Threading;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems;
using White.Core.UIItems.WindowItems;

namespace AcceptanceTests
{
    internal class AuctionSniperDriver
    {
        private readonly Application applicationUnderTest;
        private readonly int timeout;

        public AuctionSniperDriver(int timeout, string applicationName)
        {
            Thread.Sleep(timeout);
            applicationUnderTest = Application.Attach(applicationName);
            this.timeout = timeout;
        }

        public void ShowsSniperStatus(string statusText)
        {
            Window window = applicationUnderTest.GetWindows().Single();
            var statusLabel = window.Get<Label>("Status");

            Assert.AreEqual(statusText, statusLabel.Text);
        }
    }
}