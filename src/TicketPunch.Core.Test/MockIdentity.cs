using TicketPunch.Core;

namespace TicketPunch.Core.Test
{
    public class MockIdentity : IIdentity
    {   

        public string Name { get; set; } = "TEST-NAME";

        public bool Verify() => Name == "TEST-NAME";
    }
}