using NUnit.Framework;
using Polynom;
using System;
using System.Linq;
using LibPolynomial = Extreme.Mathematics.Curves.Polynomial;

namespace PolynomialTests
{
    public class Tests
    {
        private Polynomial polynom;
        private LibPolynomial libPolynom;        
        
        [SetUp]
        public void Setup()
        {
            double[] values = new double[] { 1, 2, 0, 4, 5 };
            polynom = new Polynomial(values); // 5x^4 + 4x^3 + 0x^2 + 2x + 1
            libPolynom = new LibPolynomial(values); 
        }
        [Test]
        public void TestAdditionWithLib()
        {
            double[] sameSizeArr = new double[] { 17, 1, -13, 1, 0 };
            Polynomial sameSize = new Polynomial(sameSizeArr);
            LibPolynomial libSameSize = new LibPolynomial(sameSizeArr);            
            Assert.True(PolynomialEquality(sameSize + polynom, libSameSize + libPolynom));

            double[] biggerArr = new double[] { 1, 1, 1, 1, 1, 73 };
            Polynomial bigger = new Polynomial(biggerArr);
            LibPolynomial libBigger = new LibPolynomial(biggerArr);
            Assert.True(PolynomialEquality(bigger + polynom, libBigger + libPolynom));

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom + null; });
        }
        [Test]
        public void TestSubstionWithLib()
        {
            double[] sameSizeArr = new double[] { 17, 1, -13, 1, 0 };
            Polynomial sameSize = new Polynomial(sameSizeArr);
            LibPolynomial libSameSize = new LibPolynomial(sameSizeArr);
            Assert.True(PolynomialEquality(sameSize - polynom, libSameSize - libPolynom));

            double[] biggerArr = new double[] { 1, 1, 1, 1, 1, 73 };
            Polynomial bigger = new Polynomial(biggerArr);
            LibPolynomial libBigger = new LibPolynomial(biggerArr);
            Assert.True(PolynomialEquality(bigger - polynom, libBigger - libPolynom));

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom - null; });
        }
        [Test]
        public void TestMultiplicationWithLib()
        {
            double[] sameSizeArr = new double[] { 17, 1, -13, 1, 0 };
            Polynomial sameSize = new Polynomial(sameSizeArr);
            LibPolynomial libSameSize = new LibPolynomial(sameSizeArr);
            Assert.True(PolynomialEquality(sameSize * polynom, libSameSize * libPolynom));

            double[] biggerArr = new double[] { 1, 1, 1, 1, 1, 73 };
            Polynomial bigger = new Polynomial(biggerArr);
            LibPolynomial libBigger = new LibPolynomial(biggerArr);
            Assert.True(PolynomialEquality(bigger * polynom, libBigger * libPolynom));

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom * null; });
        }
        [Test]
        public void TestMultOnNumberWithLib()
        {
            double positive = 73;
            double negative = -73;
            double zero = 0;
            double[] values = new double[] { 1, 2, 0, 4, 5 };
            LibPolynomial onPositive = new LibPolynomial(values.Select(x => x * positive).ToArray());
            LibPolynomial onNegative = new LibPolynomial(values.Select(x => x * negative).ToArray());
            LibPolynomial onZero = new LibPolynomial(values.Select(x => x * zero).ToArray());

            Assert.True(PolynomialEquality(polynom * positive, onPositive));
            Assert.True(PolynomialEquality(polynom * negative, onNegative));
            Assert.True(PolynomialEquality(polynom * zero, onZero));

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

            Polynomial bigger = new Polynomial(new double[] { 1, 1, 1, 1, 1, 73 });
            Polynomial expectedResult2 = new Polynomial(new double[] { 1, 3, 3, 7, 12, 84, 155, 9, 297, 365 });
            Assert.AreEqual(expectedResult2, polynom * bigger);

            Polynomial smaller = new Polynomial(new double[] { 1, 1 });
            Polynomial expectedResult3 = new Polynomial(new double[] { 1, 3, 2, 4, 9, 5 });
            Assert.AreEqual(expectedResult3, polynom * smaller);

            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = polynom * null; });
        }
        [Test]
        public void TestToString()
        {
            Assert.AreEqual("f(x) = 5x^4 + 4x^3 + 2x + 1", polynom.ToString());
            Assert.AreEqual("f(x) = 0", (new Polynomial()).ToString());
        }

        private static bool PolynomialEquality(Polynomial p1, LibPolynomial p2)
        {            
            if (p1.Degree != p2.Degree)
                return false;
            for (int i = 0; i < p1.Degree; i++)
            {
                if (p1[i] != p2[i])
                    return false;
            }
            return true;
        }
    }
}