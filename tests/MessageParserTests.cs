using Microsoft.VisualStudio.TestTools.UnitTesting;
using Topdev.Twitch.Chat.Helpers;

namespace Topdev.Twitch.Chat.Client.Tests
{
    [TestClass]
    public class MessageParserTests
    {
        [TestMethod]
        public void TestTryParseValidPrivateMessage()
        {
            var messageParser = new MessageParser();
            var canParse = messageParser.TryParsePrivateMessage(":test!some@someHost.cz PRIVMSG #someChannel :some random test", out var msg);

            Assert.IsTrue(canParse);
            Assert.AreEqual(msg.User, "test");
            Assert.AreEqual(msg.Channel, "someChannel");
            Assert.AreEqual(msg.Host, "someHost.cz");
            Assert.AreEqual(msg.Text, "some random test");
        }

        [TestMethod]
        public void TestTryParseInvalidPrivateMessage()
        {
            var messageParser = new MessageParser();
            var canParse = messageParser.TryParsePrivateMessage(":test!!!some@someHost.cz PRIMSG #someChannel ;some random test", out var msg);

            Assert.IsFalse(canParse);
        }
    }
}
