using TechTalk.SpecFlow;

namespace AcceptanceTests
{
    [Binding]
    public class AuctionEndToEndDefinitions
    {
        private static readonly FakeAuctionServer Auction = new FakeAuctionServer("item-54321");
        private static readonly ApplicationRunner Application = new ApplicationRunner();

        [Given(@"I have an Auction selling an item")]
        public void GivenIHaveAnAuctionSellingAnItem()
        {
            Auction.StartSellingItem();
        }

        [Given(@"The sniper has started to bid in that auction")]
        public void GivenTheSniperHasStartedToBidInThatAuction()
        {
            Application.StartBiddingIn(Auction);
        }

        [Given(@"The auction receives a join request")]
        public void GivenTheAuctionReceivesAJoinRequest()
        {
            Auction.HasReceivedJoinRequestFromSniper();
        }

        [When(@"The auction announces that it is closed")]
        public void WhenTheAuctionAnnouncesThatItIsClosed()
        {
            Auction.AnnounceClose();
        }

        [Then(@"The sniper will show that it has lost the auction")]
        public void ThenTheSniperWillShowThatItHasLostTheAuction()
        {
            Application.ShowsSniperHasLostAuction();
        }

        [AfterScenario]
        public static void AfterScenario()
        {
            Auction.Stop();
            Application.Stop();
        }
    }
}