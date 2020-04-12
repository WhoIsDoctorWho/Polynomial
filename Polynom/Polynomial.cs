using System;

namespace Polynom
{
    public class Polynomial
    {
        public double[] Coefficients { get; set; } // maybe should use list
        public double this[int degree]
        {
            get => Coefficients[degree];
            set => Coefficients[degree] = value;
        }
        public Polynomial() {}
        public Polynomial(int degree)
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

    }
}
