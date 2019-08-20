using System;
namespace TicketPunch.Core
{
    public interface ILicenseBuilder<T> where T : IIdentity
    {
        ILicenseBuilder<T> WithIdentity(T identity);
        ILicenseBuilder<T> WithExpirationDate(DateTime dt);
        License<T> License();
    }
}