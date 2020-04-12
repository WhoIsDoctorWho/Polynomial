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

            Polynomial bigger = new Polynomial(new double[] { 73, 1, 1, 1, 1, 1 });
            Polynomial expectedResult2 = new Polynomial(new double[] { 73, 2, 3, 1, 5, 6 });
            Assert.AreEqual(expectedResult2, polynom + bigger);

            Polynomial smaller = new Polynomial(new double[] { 1, 1 });
            Polynomial expectedResult3 = new Polynomial(new double[] { 2, 3, 1, 5, 6 });
            Assert.AreEqual(expectedResult3, polynom + smaller);

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom + null; });
        }
        [Test]
        public void TestSubstraction()
        {
            Polynomial sameSize = new Polynomial(new double[] { 1, 1, 1, 1, 1 });
            Polynomial expectedResult1 = new Polynomial(new double[] { 0, 1, -1, 3, 4 });
            Assert.AreEqual(expectedResult1, polynom - sameSize);

            Polynomial bigger = new Polynomial(new double[] { 73, 1, 1, 1, 1, 1 });
            Polynomial expectedResult2 = new Polynomial(new double[] { -73, 0, 1, -1, 3, 4 });
            Assert.AreEqual(expectedResult2, polynom - bigger);

            Polynomial smaller = new Polynomial(new double[] { 1, 1 });
            Polynomial expectedResult3 = new Polynomial(new double[] { 1, 2, 0, 3, 4 });
            Assert.AreEqual(expectedResult3, polynom - smaller);

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom - null; });
        }
        [Test]
        public void TestMultiplication()
        {
            Polynomial sameSize = new Polynomial(new double[] { 1, 1, 1, 1, 1 });
            Polynomial expectedResult1 = new Polynomial(new double[] { 5, 9, 9, 11, 12, 7, 3, 3, 1 });
            Assert.AreEqual(expectedResult1, polynom * sameSize);            

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom * null; });
        }


    }
}