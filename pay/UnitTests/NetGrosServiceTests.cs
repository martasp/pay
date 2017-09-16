using Microsoft.VisualStudio.TestTools.UnitTesting;
using pay.Services;

namespace UnitTests
{
    [TestClass]
    public class NetGrosServiceTests
    {
        [TestMethod]
        public void CalculateTest()
        {
            float[] data =       { 282.1f, 335.3f,  760f, 761f };
            float[] dataResult = { 310.00f, 380.00f, 1000.00f, 1001.32f };
            for (int i = 0; i < data.Length; i++)
            {
                var Expected = dataResult[i];
                var Actual = NetGrossCalculator.CalculateGross(data[i]);
                Assert.AreEqual(Expected, Actual);
            }
        }
    }
}
