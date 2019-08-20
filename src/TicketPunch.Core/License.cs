using System;
using System.Text;
using Newtonsoft.Json;
using TicketPunch.Core.Crypto;

namespace TicketPunch.Core
{
    public sealed class License<TIdentity> where TIdentity : IIdentity
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public TIdentity Identity { get; set; }

        internal License() { }

        public string ToJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string Sign(ISigner signer)
        {
            var json = ToJSON();
            var data = Utilites.Base64EncodeString(json);

            var signature = signer.Sign(data);

            StringBuilder sb = new StringBuilder();
            sb.Append(data);
            sb.Append('.');
            sb.Append(System.Convert.ToBase64String(signature));
            return sb.ToString();
        }

        public static License<TIdentity> Load(ISigner verifier, string licenseString)
        {
            var sections = licenseString.Split('.');
            if (sections.Length != 2) { throw new InvalidLicenseException(); }

            var rawLicense = sections[0];
            var signature = System.Convert.FromBase64String(sections[1]);

            if (!verifier.Verify(rawLicense, signature))
            {
                throw new InvalidLicenseException();
            }

            return FromJSON(Utilites.Base64DecodeString(rawLicense));
        }

        public static License<TIdentity> FromJSON(string json)
        {
            var license = JsonConvert.DeserializeObject<License<TIdentity>>(json);

            // Verify required properties are not null
            if (license.Id == null) { throw new InvalidLicenseException(); }
            if (license.CreationDate == null) { throw new InvalidLicenseException(); }

            return license;
        }

        public static ILicenseBuilder<TIdentity> Issue() {
            return new LicenseBuilder<TIdentity>();
        }
    }
}