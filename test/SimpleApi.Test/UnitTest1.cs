using System;
using System.Linq;
using SimpleApi.Controllers;
using Xunit;

namespace SimpleApi.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ReturnFiveResults()
        {
            var weatherController = new WeatherForecastController();

            var val = weatherController.Get();
            Assert.Equal(6, val.Count());
        }
    }
}
