using System;

namespace TicketPunch.Core
{
    public class LicenseBuilder<T> : ILicenseBuilder<T> where T : IIdentity
    {   
        private readonly License<T> _license;

        internal LicenseBuilder() {
            _license = new License<T>();
            _license.Id = Guid.NewGuid();
            _license.CreationDate = DateTime.UtcNow;
        }

        public ILicenseBuilder<T> WithIdentity(T identity)
        {
            _license.Identity = identity;
            return this;
        }

        public ILicenseBuilder<T> WithExpirationDate(DateTime dt)
        {
            _license.ExpireDate = dt;
            return this;
        }

        public License<T> License()
        {
            return _license;
        }
    }

}