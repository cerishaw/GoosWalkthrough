using System.Collections.Concurrent;
using NUnit.Framework;
using jabber;
using jabber.client;
using jabber.protocol.client;

namespace AcceptanceTests
{
    internal class FakeAuctionServer
    {
        public const string ItemIdAsLogin = "auction-%s";
        public const string AuctionResource = "Auction";
        public const string XmppHostName = "localhost";
        private const string AuctionPassword = "auction";

        private readonly string itemId;
        private readonly JabberClient xmppClient;
        private readonly SingleMessageListener messageListener = new SingleMessageListener();
        private JID chatId;

        public FakeAuctionServer(string itemId)
        {
            this.itemId = itemId;
            xmppClient = new JabberClient {Server = XmppHostName};
        }

        public void StartSellingItem()
        {
            xmppClient.User = string.Format(ItemIdAsLogin, itemId);
            xmppClient.Password = AuctionPassword;
            xmppClient.Resource = AuctionResource;
            xmppClient.OnMessage += xmppClient_OnMessage;
            xmppClient.Connect();
        }

        private void xmppClient_OnMessage(object sender, Message msg)
        {
            chatId = msg.From;
            messageListener.ProcessMessage(msg);
        }

        public void HasReceivedJoinRequestFromSniper()
        {
            messageListener.ReceivesAMessage();
        }

        public void AnnounceClose()
        {
            xmppClient.Message(MessageType.chat, chatId.User, "close");
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