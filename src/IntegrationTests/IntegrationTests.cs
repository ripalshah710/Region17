using NUnit.Framework;
using region4.ObjectModel.Commerce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class UnitTests
    {
        [Test]
        public void UnitTestHappyPath()
        {
            Assert.IsTrue (true);

        }

        [Test]
        public void UnitTestSadPath()
        {
            Assert.IsFalse (false);
        }

        [Test]
        public void UnitTestAuthorizePayment()
        {
            var basePaymentMethod = new BasePaymentMethod();
            string message = "Returned";
            Assert.IsTrue(basePaymentMethod.AuthorizePayment(out message));

        }

        [Test]
        public void UnitTestValidatePOByDistrict()
        {
            var basePaymentMethod = new BasePaymentMethod();
            Assert.IsFalse(basePaymentMethod.ValidatePOByDistrict());


        }
    }
}
