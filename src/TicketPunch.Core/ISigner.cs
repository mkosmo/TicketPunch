namespace TicketPunch.Core
{
    public interface ISigner
    {
        byte[] Sign(string input);
    }
}