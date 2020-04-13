using System;
using System.Collections.Generic;
using System.Linq;

namespace Polynom
{
    public class Polynomial
    {        
        public SortedDictionary<long, double> Nodes { get; set; }        
        public long Degree { get => Nodes.Keys.Max(); }
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

            Polynomial result = new Polynomial();            

            result.Nodes = new SortedDictionary<long, double>(p1.Nodes
                .Select(node => new { key = node.Key, value = node.Value * number })
                .ToDictionary(node => node.key, node => node.value));

            return result;
        }
        public static Polynomial operator *(double number, Polynomial p1)
        {         
            return p1 * number;
        }
        public void Print()
        {
            var reversedNodes = Nodes.Reverse();
            foreach (var node in reversedNodes)
            {
                Console.Write($"{node.Value}x^{node.Key} ");
                if (node.Key != Nodes.Keys.Min())
                    Console.Write("+ ");
                else
                    Console.Write("= 0");
            }
        }
        public override bool Equals(object obj)
        {
            Polynomial toCheck = obj as Polynomial;
            if (toCheck == null || Degree != toCheck.Degree)
                return false;            
            bool result = Nodes.SequenceEqual(toCheck.Nodes);
            return result;
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
