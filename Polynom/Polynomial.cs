using System;
using System.Linq;

namespace Polynom
{
    public class Polynomial
    {
        public double[] Coefficients { get; set; } // maybe should use list
        public long Degree { get => Coefficients.Length; }
        public double this[long degree]
        {
            get => Coefficients[degree];
            set => Coefficients[degree] = value;
        }
        public Polynomial() {}
        public Polynomial(long degree)
        {
            Coefficients = new double[degree + 1];
        }
        public Polynomial(double[] coefficients)
        {
            Coefficients = coefficients;
        }
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            Polynomial result = new Polynomial();

            throw new NotImplementedException();

            return result;
        }
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            Polynomial result = new Polynomial();

            throw new NotImplementedException();

            return result;
        }
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            Polynomial result = new Polynomial();

            throw new NotImplementedException();

            return result;
        }
        public static Polynomial operator *(Polynomial p1, double number)
        {
            Polynomial result = new Polynomial();

            throw new NotImplementedException();

            return result;
        }
        public static Polynomial operator *(double number, Polynomial p1)
        {
            throw new NotImplementedException();
            return p1 * number;
        }
        public void Print()
        {
            throw new NotImplementedException();
        }
        public override bool Equals(object obj)
        {
            Polynomial toCheck = obj as Polynomial;
            if (toCheck == null || Degree != toCheck.Degree)
                return false;

            return Coefficients.SequenceEqual(toCheck.Coefficients);            
        }

    }
}
