using Lab7.Controllers;
using Lab7.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Tests
{
    public class UnitTests
    {
        [Fact]
        public void TestIncorrectData()
        {
            var data = new InputData
            {
                X0 = new[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057 },
                A = new[,] {
                    {1.0, -1.0, -1.0, 0.0, 0.0, 0.0, 0.0},
                    {0.0, 0.0, 1.0, -1.0, -1.0, 0.0, 0.0},
                    {0.0, 0.0, 0.0, 0.0, 1.0, -1.0, -1.0}
                },
                B = new[] { 0.0, 0.0, 0.0 },
                Measurability = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                Tolerance = new[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020 },
                Lower = new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                Upper = new[] { 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0 }
            };

            var controller = new BalanceController();
            var result = controller.PostAsync(data).Result;

            Assert.Equal("error", result.Type);
        }

        [Fact]
        public void TestCollectiveVersion()
        {
            var data = new InputData
            {
                X0 = new [] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991 },
                A = new [,] {
                    {1.0, -1.0, -1.0, 0.0, 0.0, 0.0, 0.0},
                    {0.0, 0.0, 1.0, -1.0, -1.0, 0.0, 0.0},
                    {0.0, 0.0, 0.0, 0.0, 1.0, -1.0, -1.0}
                },
                B = new [] { 0.0, 0.0, 0.0 },
                Measurability = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                Tolerance = new[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020 },
                Lower = new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                Upper = new[] { 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0 }
            };

            var expected = new[] { 10.05561, 3.01447, 7.04114, 1.98225, 5.05888, 4.06726, 0.99163 };

            var controller = new BalanceController();
            var result = controller.PostAsync(data).Result;
            var resultData = result.Data as OutputData;

            Assert.Equal("result", result.Type);

            for (var i = 0; i < resultData?.X.Length; i++)
            {
                Assert.Equal(expected[i], resultData.X[i], 3);
            }
        }

        [Fact]
        public void TestOddVersion()
        {
            var data = new InputData
            {
                X0 = new[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991, 6.666 },
                A = new[,] {
                    {1.0, -1.0, -1.0, 0.0, 0.0, 0.0, 0.0, -1.0},
                    {0.0, 0.0, 1.0, -1.0, -1.0, 0.0, 0.0, 0.0},
                    {0.0, 0.0, 0.0, 0.0, 1.0, -1.0, -1.0, 0.0}
                },
                B = new[] { 0.0, 0.0, 0.0 },
                Measurability = new[] { 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0 },
                Tolerance = new[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 },
                Lower = new[] { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                Upper = new[] { 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0, 10000.0 }
            };

            var expected = new[] { 10.540, 2.836, 6.973, 1.963, 5.009, 4.020, 0.989, 0.731 };

            var controller = new BalanceController();
            var result = controller.PostAsync(data).Result;
            var resultData = result.Data as OutputData;

            Assert.Equal("result", result.Type);

            for (var i = 0; i < resultData?.X.Length; i++)
            {
                Assert.Equal(expected[i], resultData.X[i], 2);
            }
        }
    }
}
