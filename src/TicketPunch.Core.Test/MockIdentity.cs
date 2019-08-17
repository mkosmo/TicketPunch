using TicketPunch.Core;

namespace TicketPunch.Core.Test
{
    public class MockIdentity : IIdentity
    {   

        public string Name => "TEST-NAME";

        public bool Verify()
        {
            return true;
        }
    }
}