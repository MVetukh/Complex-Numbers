//using System.Numerics;

using ComplexLib;

Complex c0 = new Complex(1, 1);
Complex c1 = new Complex(2, -2);

Console.WriteLine(Complex.Exp(Complex.Log(c0)));
Console.WriteLine(Complex.Pow(Complex.Pow(c0, c1), 1 / c1));
Console.WriteLine(Complex.Pow(Complex.Sqrt(c0), 2));
Console.WriteLine(Complex.Acos(Complex.Cos(c0)));
Console.WriteLine(Complex.Asin(Complex.Sin(c0)));
Console.WriteLine(Complex.Atan(Complex.Tan(c0)));
Console.WriteLine(Complex.Acotan(Complex.Cotan(c0)));

//Console.WriteLine(Complex.Sin(c0));
//Console.WriteLine(Complex.Cos(c0));
//Console.WriteLine(Complex.Tan(c0));
//Console.WriteLine(Complex.Cot(c0));
//Console.WriteLine(Complex.Arcsin(c0));
//Console.WriteLine(Complex.Arccos(c0));
//Console.WriteLine(Complex.Arctg(c0));
//Console.WriteLine(Complex.Arcctg(c0));
//Console.WriteLine(Complex.Sh(c0));
//Console.WriteLine(Complex.Ch(c0));
//Console.WriteLine(Complex.Th(c0));
//Console.WriteLine(Complex.Cth(c0));
//Console.WriteLine(Complex.Pow(c0, 2));
//Console.WriteLine(Complex.Sqrt(c0));
//Console.WriteLine(Complex.Log(c0));
//Console.WriteLine(Complex.Exp(c0));
//Complex complex1 = Complex.ImaginaryOne;
//Complex complex2 = Complex.ImaginaryOne + complex1;
//Complex c3 = complex1 / complex2;//complex1 * complex2;
//Console.WriteLine();
Console.ReadLine();


