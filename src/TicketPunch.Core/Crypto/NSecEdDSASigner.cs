using System;
using NSec.Cryptography;

namespace TicketPunch.Core.Crypto
{
    public class NSecEdDSASigner : ISigner
    {   
        private Key _privateKey;
        private PublicKey _publicKey;

        public NSecEdDSASigner(Key privateKey) {
            _privateKey = privateKey;
            _publicKey = privateKey.PublicKey;
        }

        public NSecEdDSASigner(PublicKey publicKey) {
            _privateKey = null;
            _publicKey = publicKey;
        }

        public byte[] Sign(string input)
        {
            if (_privateKey == null) {
                throw new InvalidOperationException("Cannot sign if private key is not loaded.");
            }

            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            return SignatureAlgorithm.Ed25519.Sign(_privateKey, inputData);
        }

        public bool Verify(string input, byte[] signature)
        {
            byte[] inputData = System.Text.Encoding.UTF8.GetBytes(input);
            return SignatureAlgorithm.Ed25519.Verify(_publicKey, inputData, signature);
        }
    }
}