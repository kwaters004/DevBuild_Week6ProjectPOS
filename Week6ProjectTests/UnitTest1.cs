using System;
using Xunit;
using Week6Project;
using System.Collections.Generic;

namespace Week6ProjectTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(3, 4, 12)]
        [InlineData(3.99, 3, 11.97)]
        [InlineData(2.99, 6, 17.94)]
        [InlineData(.99, 2, 1.98)]

        public void LineTotalTest(decimal price, int quantity, decimal expected)
        {
            Assert.Equal(expected, Product.LineTotal(price, quantity));
        }

        [Fact]
        public void GrandTotalTest()
        {
            Dictionary<string, int> list = new Dictionary<string, int>
            {
                {"Cheeseburger", 3 }, // Cheeseburger price is 3.99
                {"Triple Cheeseburger", 4 } // Triple Cheeseburger price is 7.79
            };
            Assert.Equal(43.13m, Product.GrandTotal(list));
        }
        
        [Fact]
        public void GrandTotalTest2()
        {
            Dictionary<string, int> list = new Dictionary<string, int>
            {
                {"Cheeseburger", 4 } // Cheeseburger price is 3.99
            };
            Assert.Equal(15.96m, Product.GrandTotal(list));
        }


        [Theory]
        [InlineData(10, 10.6)]
        [InlineData(15, 15.9)]
        [InlineData(9.99, 10.59)]
        [InlineData(12.59, 13.35)]
        [InlineData(2.79, 2.96)]
        [InlineData(100.16, 106.17)]

        public void TaxTotalTest(decimal total, decimal output)
        {
            Assert.Equal(output, Product.TaxTotal(total));
        }



        [Theory]
        [InlineData(15, 20, 5)]
        [InlineData(29.89, 30, .11)]
        [InlineData(8.79, 10, 1.21)]
        [InlineData(17.75, 20.75, 3)]

        public void DetermineChangeTest(decimal total, decimal tendered, decimal output)
        {
            Assert.Equal(output, Program.DetermineChange(total, tendered));
        }

        [Theory]
        [InlineData("12")]
        [InlineData("5")]
        public void CheckIntProductTest(string input)
        {
            Assert.True(Validator.CheckIntProduct(input));
        }

        [Theory]
        [InlineData("20")]
        [InlineData("14")]
        [InlineData("0")]

        public void CheckIntProductTest2(string input)
        {
            Assert.False(Validator.CheckIntProduct(input));
        }

        




    }
}
