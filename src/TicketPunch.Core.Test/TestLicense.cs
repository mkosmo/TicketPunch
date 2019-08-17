using Xunit;
using Xunit.Abstractions;
using System;

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
        public void UnitTestLicense()
        {
            var license = new License<MockIdentity>();
            license.CreationDate = DateTime.Now;
            license.Id = Guid.NewGuid();
            var json = license.ToJSON();
            output.WriteLine(json);
        }
    }
}