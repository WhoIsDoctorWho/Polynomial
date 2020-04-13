using NUnit.Framework;
using Polynom;
using System;

namespace PolynomialTests
{
    public class Tests
    {
        private Polynomial polynom;
        [SetUp]
        public void Setup()
        {
            polynom = new Polynomial(new double[] { 1, 2, 0, 4, 5 }); // 5x^4 + 4x^3 + 0x^2 + 2x + 1
        }

        [Test]
        public void TestAddition()
        {
            Polynomial sameSize = new Polynomial(new double[] { 1, 1, 1, 1, 1 });
            Polynomial expectedResult1 = new Polynomial(new double[] { 2, 3, 1, 5, 6 });
            Assert.AreEqual(expectedResult1, polynom + sameSize);

            Polynomial bigger = new Polynomial(new double[] { 1, 1, 1, 1, 1, 73 });
            Polynomial expectedResult2 = new Polynomial(new double[] { 2, 3, 1, 5, 6, 73 });
            Assert.AreEqual(expectedResult2, polynom + bigger);

            Polynomial smaller = new Polynomial(new double[] { 1, 1 });
            Polynomial expectedResult3 = new Polynomial(new double[] { 2, 3, 0, 4, 5 });
            Assert.AreEqual(expectedResult3, polynom + smaller);

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom + null; });
        }
        [Test]
        public void TestSubstraction()
        {
            Polynomial sameSize = new Polynomial(new double[] { 1, 1, 1, 1, 1 });
            Polynomial expectedResult1 = new Polynomial(new double[] { 0, 1, -1, 3, 4 });
            Assert.AreEqual(expectedResult1, polynom - sameSize);

            Polynomial bigger = new Polynomial(new double[] { 1, 1, 1, 1, 1, 73 });
            Polynomial expectedResult2 = new Polynomial(new double[] { 0, 1, -1, 3, 4, -73 });
            Assert.AreEqual(expectedResult2, polynom - bigger);

            Polynomial smaller = new Polynomial(new double[] { 1, 1 });
            Polynomial expectedResult3 = new Polynomial(new double[] { 0, 1, 0, 4, 5 });
            Assert.AreEqual(expectedResult3, polynom - smaller);

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom - null; });
        }
        [Test]
        public void TestMultiplication()
        {
            Polynomial sameSize = new Polynomial(new double[] { 1, 1, 1, 1, 1 });
            Polynomial expectedResult = new Polynomial(new double[] { 1, 3, 3, 7, 12, 11, 9, 9, 5 });
            Assert.AreEqual(expectedResult, polynom * sameSize);

            //  { 1, 2, 0, 4, 5 }

            Polynomial bigger = new Polynomial(new double[] { 1, 1, 1, 1, 1, 73 });
            Polynomial expectedResult2 = new Polynomial(new double[] { 1, 3, 3, 7, 12, 84, 155, 9, 297, 365 });
            Assert.AreEqual(expectedResult2, polynom * bigger);

            Polynomial smaller = new Polynomial(new double[] { 1, 1 });
            Polynomial expectedResult3 = new Polynomial(new double[] { 1, 3, 2, 4, 9, 5 });
            Assert.AreEqual(expectedResult3, polynom * smaller);

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom * null; });
        }


    }
}