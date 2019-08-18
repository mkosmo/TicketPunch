namespace TicketPunch.Core.Crypto
{
    public interface ISigner
    {
        byte[] Sign(string input);
        bool Verify(string input, byte[] signature);
    }
}