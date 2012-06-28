using System.Threading;
using White.Core;

namespace AcceptanceTests
{
    internal class ApplicationRunner
    {
        private const string ApplicationPath = @"C:\GitHub\GoosWalkthrough\AuctionSniper\bin\Debug\AuctionSniper.exe";
        private const string ApplicationName = "AuctionSniper";

        public const string SniperId = "sniper";
        public const string SniperPassword = "sniper";
        private const string StatusJoining = "Joining...";
        private const string StatusLostAuction = "Auction Lost";

        private Application applicationUnderTest;
        private AuctionSniperDriver driver;


        public void StartBiddingIn(FakeAuctionServer auction)
        {
            var thread = new Thread(StartApplication);
            thread.Start();
            driver = new AuctionSniperDriver(1000, ApplicationName);
            driver.ShowsSniperStatus(StatusJoining);
        }

        public void ShowsSniperHasLostAuction()
        {
            driver.ShowsSniperStatus(StatusLostAuction);
        }

        public void Stop()
        {
            if (applicationUnderTest != null)
            {
                applicationUnderTest.Kill();
            }
        }

        private void StartApplication()
        {
            applicationUnderTest =
                Application.Launch(ApplicationPath);
        }
    }
}