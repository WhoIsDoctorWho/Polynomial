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
        public void TestAddition()
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
        public void TestSubstion()
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
        public void TestMultiplication()
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
        public void TestMultOnNumber()
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

            Polynomial nullPolynomial = null;
            Assert.Throws(typeof(ArgumentException), () => { Polynomial test = nullPolynomial * 42.1; });
        }         
        [Test]
        public void TestToString()
        {
            Assert.AreEqual("f(x) = 5x^4 + 4x^3 + 2x + 1", polynom.ToString());
            Assert.AreEqual("f(x) = 0", (new Polynomial()).ToString());            
        }
        [Test]
        public void TestFunction()
        {
            Assert.AreEqual(polynom.Function(-10), libPolynom.ValueAt(-10));
            Assert.AreEqual(polynom.Function(0), libPolynom.ValueAt(0));
            Assert.AreEqual(polynom.Function(999), libPolynom.ValueAt(999));
        }
        [Test]
        public void TestIntegral()
        {            
            Assert.AreEqual(polynom.Integrate(0, 10), libPolynom.Integral(0, 10));            
            Assert.AreEqual(polynom.Integrate(-10, 10), libPolynom.Integral(-10, 10));
        }
        [Test]
        public void TestDerivative()
        {                    
            Assert.True(PolynomialEquality(polynom.Derivative(), 
                libPolynom.GetDerivative() as LibPolynomial));
            Assert.True(PolynomialEquality(polynom.Derivative(2),
                libPolynom.GetDerivative().GetDerivative() as LibPolynomial));

            double x = 73;
            Assert.AreEqual(polynom.Derivative(x), libPolynom.GetDerivative().ValueAt(x));
            Assert.AreEqual(polynom.Derivative(x, 3), 
                libPolynom.GetDerivative().GetDerivative().GetDerivative().ValueAt(x));
        }

        private static bool PolynomialEquality(Polynomial p1, LibPolynomial p2)
        {            
            if (p1 == null || p2 == null || p1.Degree != p2.Degree)
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