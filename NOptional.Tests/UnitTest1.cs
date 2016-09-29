using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NOptional.Samples
{
    [TestClass]
    public class Playground
    {
        [TestMethod]
        public void Play()
        {
            Optional<Message> grafik = Message.ReceiveUnsafe();
            

        }
    }

    public class Message
    {
        public static Message ReceiveUnsafe() => null;

        private Message(string message)
        {
            
        }
    }
}
