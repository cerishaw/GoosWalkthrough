using System.Collections.Concurrent;
using Matrix;
using Matrix.Xmpp;
using Matrix.Xmpp.Client;
using NUnit.Framework;

namespace AcceptanceTests
{
    internal class FakeAuctionServer
    {
        public const string ItemIdAsLogin = "auction-%s";
        public const string AuctionResource = "Auction";
        public const string XmppHostName = "localhost";
        private const string AuctionPassword = "auction";

        private readonly string itemId;
        private readonly XmppClient xmppClient;
        private readonly SingleMessageListener messageListener = new SingleMessageListener();
        private Jid chatId;

        public FakeAuctionServer(string itemId)
        {
            this.itemId = itemId;
            xmppClient = new XmppClient {XmppDomain = XmppHostName};
        }

        public void StartSellingItem()
        {
            xmppClient.SetUsername(string.Format(ItemIdAsLogin, itemId));
            xmppClient.Password = AuctionPassword;
            xmppClient.SetResource(AuctionResource);
            xmppClient.OnMessage += xmppClient_OnMessage;
            xmppClient.Open();
        }

        private void xmppClient_OnMessage(object sender, MessageEventArgs e)
        {
            chatId = e.Message.From;
            messageListener.ProcessMessage(e.Message);
        }

        public void HasReceivedJoinRequestFromSniper()
        {
            messageListener.ReceivesAMessage();
        }

        public void AnnounceClose()
        {
            xmppClient.Send(new Message(chatId, MessageType.chat, ""));
        }

        public void Stop()
        {
            xmppClient.Close();
        }

        private class SingleMessageListener
        {
            private readonly BlockingCollection<Message> messages = new BlockingCollection<Message>(1);

            public void ProcessMessage(Message message)
            {
                messages.Add(message);
            }

            public void ReceivesAMessage()
            {
                Message result;
                const int timeout = 5000; //5 seconds
                messages.TryTake(out result, timeout);
                Assert.That(result, Is.Not.Null);
            }
        }
    }
}