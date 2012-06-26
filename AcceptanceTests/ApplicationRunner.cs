using System.Threading;

namespace AcceptanceTests
{
    internal class ApplicationRunner
    {
        public const string SniperId = "sniper";
        public const string SniperPassword = "sniper";
//        private AuctionSniperDriver driver;


        public void StartBiddingIn(FakeAuctionServer auction)
        {
            var thread = new Thread(StartApplication);
            thread.Start();
//            driver = new AuctionSniperDriver(1000);
//            driver.ShowsSniperStatus(StatusJoining);
        }

        public void ShowsSniperHasLostAuction()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
//            if (driver != null)
//            {
//                driver.Dispose();
//            }
        }

        private void StartApplication()
        {
            AuctionSniper.App.Main();

        }
    }
}