using Xunit;
using Xunit.Abstractions;
using NSec.Cryptography;
using System;
using TicketPunch.Core.Crypto;

namespace TicketPunch.Core.Test
{
    public class TestLicense
    {   

        private readonly ITestOutputHelper output;

        public TestLicense(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void TestLicenseSerialization()
        {
            var license = new License<MockIdentity>();
            license.CreationDate = DateTime.Now;
            license.ExpireDate = DateTime.Now.AddYears(1);
            license.Id = Guid.NewGuid();
            license.Identity = new MockIdentity();

            var json = license.ToJSON();
            output.WriteLine(json);
        }

        [Fact]
        public void TestLicenseDeserialization()
        {
            var data = @"
            {
                ""Id"":""a43d938b-4db0-4d36-a14d-cdc965528fc4"",
                ""CreationDate"": ""2019-08-18T14:32:16.7196531-05:00"",
                ""ExpireDate"":""2020-08-18T14:32:16.7196531-05:00"",
                ""Identity"":{
                    ""Name"":""TEST-NAME""
                }
            }";

            var license = License<MockIdentity>.FromJSON(data);
        }

        [Fact]
        public void TestMismatchingIdentity()
        {
            var data = @"
            {
                ""Id"":""a43d938b-4db0-4d36-a14d-cdc965528fc4"",
                ""CreationDate"": ""2019-08-18T14:32:16.7196531-05:00"",
                ""ExpireDate"":""2020-08-18T14:32:16.7196531-05:00"",
                ""Identity"":{
                    ""Name"":""BAD-MAN""
                }
            }";

            var license = License<MockIdentity>.FromJSON(data);
            Assert.False(license.Identity.Verify());
        }

        [Fact]
        public void TestValidSignature() {
            using( var sigKey = Key.Create(SignatureAlgorithm.Ed25519) ) {
                var signer = new NSecEdDSASigner(sigKey);
                var license = new License<MockIdentity>();
                license.CreationDate = DateTime.Now;
                license.ExpireDate = DateTime.Now.AddYears(1);
                license.Id = Guid.NewGuid();
                license.Identity = new MockIdentity();

                var signedLicense = license.Sign(signer);
                output.WriteLine(signedLicense);
                var validatedLicense = License<MockIdentity>.Load(signer, signedLicense);
                Assert.True(validatedLicense.Identity.Verify());
            }
        }
    }
}