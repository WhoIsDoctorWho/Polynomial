using System;
using System.Collections.Generic;

namespace Polynom
{
    class Program
    {
        static void Main(string[] args)
        {
            Polynomial p = new Polynomial(new double[] { 1, 2, 3, 4, 5 });
            p.Print();
        }
    }
}
