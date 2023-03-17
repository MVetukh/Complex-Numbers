//using System.Numerics;
using ComplexLib;


Random random = new Random();
double[] a;
Equation equation;
Test(2);
Test(3);
Test(4);


/// <summary>
/// метод который проверяет уравнения n-ого порядка
/// </summary>
void Test(int order)
{
    do
    {
        a = new double[order];
        Console.WriteLine($"Проверка уравнения {order}-го порядка ");
        for (int i = 0; i < a.Length; i++)
        {
            a[i] = 6 * random.NextDouble() - 3;
        }
        switch (order)
        {
            case 2: equation = new QuadEqv(a); break;
            case 3: equation = new CubeEqv(a); break;
            case 4: equation = new BiQuadEqv(a); break;

            default: return;
        }
        equation.Solve();
        for (int i = 0; i < order; i++)
        {
            Console.WriteLine($"x[{i}] = {equation.X[i]:g5}; zero{i} = {equation.LeftHandSide(i):f10}");
        }
    } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
}

/// <summary>
/// класс предок, методы-предки наследуют его переменные и методы
/// </summary>
internal abstract class Equation
{
    /// <summary>
    /// массив который хранит коэф-ты уравнений 
    /// </summary>
    protected double[] a;
    /// <summary>
    /// порядок уравнений
    /// </summary>
    public int Order { get; }
    /// <summary>
    /// массив содержащий в себе решения, в отличие от массива  с коэф-ами имеет еще свойство protected set те мы можем не только получать заначение , но устанавливать его 
    /// </summary>
    public Complex[] X { get; protected set; }
    protected Equation(double[] a)
    {
        this.a = a;
        Order = a.Length;
        X = new Complex[Order];

    }

    internal abstract void Solve();
    /// <summary>
    /// метод, который подставляет корни уравнений в плином
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    internal Complex LeftHandSide(int i)
    {
        return Polynom(X[i], Order);
    }
    /// <summary>
    /// метод, который считает уравнения по формуле полинома n-ого порядка
    /// </summary>
    /// <param name="x"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    private Complex Polynom(Complex x, int n) => n != 0 ? Polynom(x, n - 1) * x + a[Order - n] : 1;
}

/// <summary>
/// класс квадратных уравнений
/// </summary>
internal class QuadEqv : Equation
{

    public QuadEqv(double[] a) : base(a) { }
    /// <summary>
    /// метод решающий уравнение
    /// </summary>
    internal override void Solve()
    {

        Complex det = Complex.Sqrt(a[1] * a[1] - 4 * a[0]);
        X[0] = .5 * (-a[1] + det);
        X[1] = .5 * (-a[1] - det);
    }

}

/// <summary>
/// класс кубических уравнений
/// </summary>
internal class CubeEqv : Equation
{
    public CubeEqv(double[] a) : base(a) { }

    /// <summary>
    /// метод решающий кубичекие уравнения по формулам Кардано
    /// </summary>
    internal override void Solve()
    {
        double p = -a[2] * a[2] / 3.0 + a[1];
        double q = 2 * (a[2] * a[2] * a[2] / 27.0) - a[2] * a[1] / 3.0 + a[0];
        Complex Q = Complex.Sqrt(p * p * p / 27.0 + q * q / 4.0);
        Complex A = Complex.Pow((-q / 2.0) + Q, 1 / 3.0);
        Complex B = -p / 3.0 / A;
        {
            X[0] = A + B - a[2] / 3.0;
            X[1] = -.5 * (A + B) + .5 * Complex.ImaginaryOne * (A - B) * Math.Sqrt(3) - a[2] / 3.0;
            X[2] = -.5 * (A + B) - .5 * Complex.ImaginaryOne * (A - B) * Math.Sqrt(3) - a[2] / 3.0;
        }
    }

}

/// <summary>
/// класс уравнений 4 степени
/// </summary>
internal class BiQuadEqv : Equation
{

    public BiQuadEqv(double[] a) : base(a) { }

    /// <summary>
    /// метод, решающий уравнения 4 степени, методом Декарта-Эйлера
    /// </summary>
    internal override void Solve()
    {
        double p = a[2] - 3 * a[3] * a[3] / 8.0;
        double q = a[1] - .5 * a[3] * a[2] + Math.Pow(a[3], 3) / 8.0;
        double r = a[0] - a[3] * a[1] / 4.0 + a[3] * a[3] * a[2] / 16.0 - 3 * Math.Pow(a[3], 4) / 256.0;


        double[] al = new double[3];
        al[0] = -q * q / 64.0;
        al[1] = (p * p - 4 * r) / 16.0;
        al[2] = .5 * p;
        double p1 = -al[2] * al[2] / 3.0 + al[1];
        double q1 = 2 * (al[2] * al[2] * al[2] / 27.0) - al[2] * al[1] / 3.0 + al[0];



        Complex Q = Complex.Sqrt(p1 * p1 * p1 / 27.0 + q1 * q1 / 4.0);
        Complex A = Complex.Pow((-q1 / 2.0) + Q, 1 / 3.0);
        Complex B = -p1 / 3 / A;

        Complex[] Z = new Complex[3];


        Z[0] = Complex.Sqrt(A + B - al[2] / 3.0);
        Z[1] = Complex.Sqrt(-.5 * (A + B) + .5 * Complex.ImaginaryOne * (A - B) * Math.Sqrt(3) - al[2] / 3.0);
        Z[2] = -q / 8.0 / Z[0] / Z[1];


        for (int i = 0; i < 4; i++)
        {
            int znak1 = (i & 1) * 2 - 1;
            int znak2 = (i / 2) * 2 - 1;
            Z[0] = Z[0] * znak1;
            Z[1] = Z[1] * znak2;
            Z[2] = -q / 8.0 / Z[0] / Z[1];
            X[i] = Z[0] + Z[1] + Z[2] - a[3] / 4.0;
        }

        //X[0] = -Z[0] - Z[1] + Z[2] - a[3] / 4.0;

        //X[1] = Z[0] - Z[1] - Z[2] - a[3] / 4.0;

        //X[2] = -Z[0] +Z[1] - Z[2] - a[3] / 4.0;

        //X[3] = Z[0] + Z[1] + Z[2] - a[3] / 4.0;


    }

}

