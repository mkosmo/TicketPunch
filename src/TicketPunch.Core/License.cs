using System;
using System.Text;
using Newtonsoft.Json;

namespace TicketPunch.Core
{
    public class License<TIdentity> where TIdentity : IIdentity
    {
        public Guid Id { get; internal set; }
        public DateTime CreationDate { get; internal set; }
        public DateTime? ExpireDate { get; internal set; }
        public TIdentity Identity { get; internal set; }

        public string Finalize(ISigner signer)
        {
            var json = ToJSON();
            var data = Base64Encode(json);

            var signature = signer.Sign(data);

            StringBuilder sb = new StringBuilder();
            sb.Append(data);
            sb.Append('.');
            sb.Append(System.Convert.ToBase64String(signature));

            return sb.ToString();
        }

        public string ToJSON() {
            return JsonConvert.SerializeObject(this);
        }

        private static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        internal bool Verify()
        {
            if (ExpireDate != null)
            {
                if(DateTime.Now >= ExpireDate)
                {
                    return false;
                }
            }

            return Identity.Verify();
        }
    }
}