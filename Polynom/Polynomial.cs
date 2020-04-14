using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Polynom
{
    public class Polynomial
    {        
        public SortedDictionary<long, double> Nodes { get; set; }        
        public long Degree 
        {
            get
            {
                if (Nodes?.Keys == null || Nodes.Keys.Count == 0)
                    return 0;
                return Nodes.Keys.Max();
            }
        }
        public double this[long degree]
        {            
            get 
            {
                if (!Nodes.ContainsKey(degree))
                    return 0;    
                return Nodes[degree];
            }
            set => Nodes[degree] = value;           
        }
        private delegate double Operation(double d1, double d2);
        public Polynomial() {
            Nodes = new SortedDictionary<long, double>();
        }        
        public Polynomial(double[] coefficients) : this()
        {            
            for (long i = 0; i < coefficients.Length; i++)
            {
                if (coefficients[i] != 0)
                    Nodes[i] = coefficients[i];
            }
        }
        public Polynomial(SortedDictionary<long, double> nodes)
        {
            Nodes = nodes;
        }
        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            return SumOfPolynomials(p1, p2, (a, b) => a + b);
        }
        
        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            return SumOfPolynomials(p1, p2, (a, b) => a - b);
        }
        public static Polynomial operator *(Polynomial p1, Polynomial p2)
        {
            if (p1 == null || p2 == null)
                throw new ArgumentException("Cannot multiply these values");

            Polynomial result = new Polynomial();

            foreach (KeyValuePair<long, double> kvp1 in p1.Nodes)
            {
                foreach (KeyValuePair<long, double> kvp2 in p2.Nodes)
                {
                    long key = kvp1.Key + kvp2.Key;
                    double value = kvp1.Value * kvp2.Value;
                    result[key] += value;
                }
            }                                   
            
            return new Polynomial(result.Nodes.Values.Where(value => value != 0).ToArray());
        }
        public static Polynomial operator *(Polynomial p1, double number)
        {
            if (p1 == null)
                throw new ArgumentException("Cannot add these values");
            
            Polynomial result = new Polynomial(
                new SortedDictionary<long, double>(p1.Nodes
                .Select(node => new { key = node.Key, value = node.Value * number }) 
                .Where(node => node.value != 0)
                .ToDictionary(node => node.key, node => node.value)));

            return result;
        }
        public static Polynomial operator *(double number, Polynomial p1)
        {         
            return p1 * number;
        }
        public Polynomial Integrate()
        {
            Polynomial result = new Polynomial(
                new SortedDictionary<long, double>(Nodes
                .Select(node => new { key = node.Key + 1, value = node.Value/(node.Key+1) })
                .ToDictionary(node => node.key, node => node.value)));               
            return result;
        }
        public double Integrate(double lowerBound, double upperBound)
        {
            Polynomial integral = Integrate();
            return integral.Function(upperBound) - integral.Function(lowerBound);
        }
        private Polynomial Derivative()
        {
            Polynomial result = new Polynomial(
                new SortedDictionary<long, double>(Nodes
                .Where(node => node.Key > 0)
                .Select(node => new { key = node.Key - 1, value = node.Value * node.Key })
                .ToDictionary(node => node.key, node => node.value)));
            return result;
        }
        public Polynomial Derivative(int derivarives = 1)
        {
            Polynomial result = Derivative();
            for(int i = 1; i < derivarives && result.Nodes.Any(); i++)
            {
                result = result.Derivative();
            }
            return result;
        }
        public double Derivative(double x, int derivarives = 1)
        {
            return Derivative(derivarives).Function(x);
        }
        public double Function(double x)
        {
            double result = 0;
            foreach (KeyValuePair<long, double> node in Nodes) 
            {
                result += node.Value * Math.Pow(x, node.Key);
            }
            return result;
        }
        public void Print()
        {
            Console.WriteLine(this);
        }
        public override bool Equals(object obj)
        {
            Polynomial toCheck = obj as Polynomial;
            if (toCheck == null || Degree != toCheck.Degree)
                return false;            
            bool result = Nodes.SequenceEqual(toCheck.Nodes);
            return result;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("f(x) = ", Nodes.Count * 8); // f(x) = ax^m + bx^n + cx^k

            if(Nodes.Count == 0) // f(x) = 0
                return sb.Append(0).ToString();

            var reversedNodes = Nodes.Reverse();
            long minKey = Nodes.Keys.Min();
            foreach (KeyValuePair<long, double> node in reversedNodes)
            {
                if (node.Key != 0 && node.Key != 1)
                    sb.Append($"{node.Value}x^{node.Key}");
                else if(node.Key == 1)
                    sb.Append(node.Value + "x");
                else 
                    sb.Append(node.Value);
                if (node.Key != minKey)
                    sb.Append(" + ");
            }
            return sb.ToString();
        }
        private static Polynomial SumOfPolynomials(Polynomial p1, Polynomial p2, Operation operation)
        {
            if (p1 == null || p2 == null || operation == null)
                throw new ArgumentException("Cannot add these values");
            Polynomial result = new Polynomial();

            var keys = p1.Nodes.Keys.Union(p2.Nodes.Keys).ToList();
            foreach (long key in keys)
            {
                double value = operation.Invoke(p1[key], p2[key]);
                if (value != 0)
                    result[key] = value;
            }
            return result;
        }

    }
}
