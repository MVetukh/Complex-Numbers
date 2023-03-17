namespace ComplexLib
{
    public struct Complex : IEquatable<Complex>, IFormattable
    {
        /// <summary>
        /// определение постоянных значений(т.е. их нельзя будет изменить вне структуры )
        /// </summary>
        #region Fields 
        /// <summary>
        /// определение мнимая еденица
        /// </summary>
        public static readonly Complex ImaginaryOne = new(0, 1);
        /// <summary>
        /// действительняая еденица
        /// </summary>
        public static readonly Complex One = new(1, 0);
        /// <summary>
        /// ноль
        /// </summary>
        public static readonly Complex Zero = new(0, 0);
        #endregion

        /// <summary>
        ///  свойства, они обеспечивают удобный доступ к числам
        /// </summary>
        #region Properties
        /// <summary>
        /// возвращает действительную часть комплексного числа
        /// </summary>
        public double Real { get; }
        /// <summary>
        /// возвращает мнимую часть комплексного числа
        /// </summary>
        public double Imaginary { get; }
        /// <summary>
        /// возвращает модуль комплексного число
        /// </summary>
        public double Magnitude { get { return Math.Sqrt(Real * Real + Imaginary * Imaginary); } }
        /// <summary>
        /// возвращает угол комлексного числа
        /// </summary>
        public double Phase { get { return Math.Atan2(Imaginary, Real); } }
        #endregion

        #region Constructors
        /// <summary>
        /// записывает комплексное число, как число состаящее из действетильной и мнимой части 
        
        /// </summary>
        /// <param name="real"></param>
        /// <param name="imaginary"></param>
        public Complex(double real, double imaginary) 
        {
            Real = real; Imaginary = imaginary;
        }
        /// <summary>
        /// записывает ковплексное число в полярных координатах
        /// </summary>
        /// <param name="magnitude"></param>
        /// <param name="phase"></param>
        /// <returns></returns>
        public static Complex FromPolarCoordinates(Double magnitude, double phase)
        {
            return new Complex(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
        }

        #endregion

        #region Operators
        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }
        public static Complex operator *(Complex c1, Complex c2)
        {
            return new Complex(c1.Real * c2.Real - c1.Imaginary * c2.Imaginary, c1.Real * c2.Imaginary + c1.Imaginary * c2.Real);
        }
        public static implicit operator Complex(double d)
        {
            return new(d, 0);
        }
        public static Complex operator -(Complex c)
        {
            return new Complex(-c.Real, -c.Imaginary);
        }
        public static Complex operator -(Complex c1, Complex c2)
        {
            return c1 + (-c2);
        }
        public static Complex operator ~(Complex c)
        {
            return new Complex(c.Real, -c.Imaginary);
        }
        public static Complex operator /(Complex c1, Complex c2)
        {
            return c1 * ~c2 * (1 / c2.Magnitude / c2.Magnitude);
        }
        public static bool operator ==(Complex c1, Complex c2)
        {
            if (((object)c1) == null || ((object)c2) == null)
                return Object.Equals(c1, c2);

            return c1.Equals(c2);
        }
        public static bool operator !=(Complex c1, Complex c2)
        {
            return c1.Real != c2.Real || c1.Imaginary != c2.Imaginary;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Считает экспонету от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Exp(Complex c)
        {
            return FromPolarCoordinates(Math.Exp(c.Real), c.Imaginary);//new Complex(Math.Exp(c.Real) * Math.Cos(c.Imaginary), Math.Exp(c.Real) * Math.Sin(c.Imaginary));
        }
        /// <summary>
        /// Возводит комплексное число в действительную степень
        /// </summary>
        /// <param name="c"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Complex Pow(Complex c, Double d)
        {
            Complex z = Complex.Exp(d * Complex.Log(c));
            // Complex z = Math.Pow(c.Magnitude, d) * Complex.Exp(ImaginaryOne * d * c.Phase);
            return new Complex(z.Real, z.Imaginary);
            //return new Complex(Math.Pow(c.Magnitude, d) * (Math.Cos(d * c.Phase)), Math.Pow(c.Magnitude, d) * Math.Sin(d * c.Phase));
        }
        /// <summary>
        /// Возводит комплексное число в степень коплекного числа
        /// </summary>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <returns></returns>
        public static Complex Pow(Complex c1, Complex c2)
        {
            Complex z = Complex.Exp((c2) * (Complex.Log(c1.Magnitude) + Complex.ImaginaryOne * (Complex.Atan(c1.Imaginary / c1.Real))));
            return new Complex(z.Real, z.Imaginary);
        }
        /// <summary>
        /// Берет корень из комплексноого числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Sqrt(Complex c)
        {
            return FromPolarCoordinates(Math.Sqrt(c.Magnitude), c.Phase * .5);
            //Complex z = Complex.Pow(c, 0.5);
            //return new Complex(z.Real, z.Imaginary); //Complex(Math.Sqrt(c.Magnitude) * Math.Cos(0.5 * c.Phase), Math.Sqrt(c.Magnitude) * Math.Sin(c.Phase * 0.5));
        }
        /// <summary>
  /// Считает косинус от комлекного числа 
  /// </summary>
  /// <param name="c"></param>
  /// <returns></returns>
        public static Complex Cos(Complex c)
        {
            return (Complex.Exp(c * ImaginaryOne) + Complex.Exp(-c * ImaginaryOne)) / 2;
        }
        /// <summary>
        /// Считает синус от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Sin(Complex c)
        {
            return (Complex.Exp(c * ImaginaryOne) - Complex.Exp(-c * ImaginaryOne)) / (2 * ImaginaryOne);
        }
        /// <summary>
        /// Считает тангенс от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Tan(Complex c)
        {
            return Complex.Sin(c) / Complex.Cos(c);
        }
        /// <summary>
        /// Считает котангенс от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Cotan(Complex c)
        {
            return Complex.Cos(c) / Complex.Sin(c);
        }
        /// <summary>
        /// Считает арксинус от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Asin(Complex c)
        {
            return -ImaginaryOne * Complex.Log(c * ImaginaryOne + Complex.Sqrt(1 - Complex.Pow(c, 2)));
        }
        /// <summary>
        /// Считает арккосинус от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Acos(Complex c)
        {
            return ImaginaryOne * Complex.Log(c + Complex.Sqrt(Complex.Pow(c, 2) - 1));
        }
        /// <summary>
        /// Считает арктангенс от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Atan(Complex c)
        {
            return -0.5 * ImaginaryOne * Complex.Log((1 + ImaginaryOne * c) / (1 - ImaginaryOne * c));
        }
        /// <summary>
        /// Считает арккотангенс от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Acotan(Complex c)
        {
            return -0.5 * ImaginaryOne * Complex.Log((ImaginaryOne * c - 1) / (ImaginaryOne * c + 1));
        }
        /// <summary>
        /// Считает натуральный логарифм от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Log(Complex c)
        {
            return new Complex(Math.Log(c.Magnitude), c.Phase);
        }
        /// <summary>
        /// Считает гиперболический синус от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Sinh(Complex c)
        {
            return (Complex.Exp(c) - Complex.Exp(-c)) / 2;
        }
        /// <summary>
        /// Считает гиперболический косинус от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Cosh(Complex c)
        {
            return (Complex.Exp(c) + Complex.Exp(-c)) / 2;
        }
        /// <summary>
        /// Считает гиперболический тангенс от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Tanh(Complex c)
        {
            return Complex.Sinh(c) / Complex.Cosh(c);
        }
        /// <summary>
        /// Считает гиперболический котангенс от комплексного числа
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Complex Cotanh(Complex c)
        {
            return Complex.Cosh(c) / Complex.Sinh(c);
        }
        #endregion
        /// <summary>
        /// Определяет, равны ли два экземпляра объекта
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(Complex obj)
        {
            return Equals(obj as Object);
            //Complex other = obj;

            //if (other == null)
            //    return false;

            //return other.Real == Real && other.Imaginary == Imaginary;
            //return base.Equals(obj);
        }
        /// <summary>
        /// Возвращает хэш-код для текущего объекта Complex
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Real.GetHashCode() ^ Imaginary.GetHashCode();
        }


        /// <summary>
        /// Проверяет знаковую запись комплексного числа и преобразует ее в комплекс>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="FormatException"></exception>
        public static Complex Parse(string? s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }
            if (!s.StartsWith("<") || !s.Contains(';') || !s.EndsWith(">"))
            {
                throw new FormatException();
            }
            int index = s.IndexOf(';');
            return new Complex(
                double.Parse(s.Substring(1, index - 1)),
                double.Parse(s.Substring(index + 1, s.Length - 2 - index)));
        }
        /// <summary>
        /// Делает аноаогичные действия, что и Parse только еще и показывает было ли выполнено действие
        /// </summary>
        /// <param name="s"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static Boolean TryParse(string s, out Complex c)
        {
            c = double.NaN;
            try
            {
                c = Parse(s);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }//

        /// <summary>
        /// преобразует числа типа Complex в строкове значение(String)
        /// </summary>
        /// <param name="format"></param>
        /// <param name="formatProvider"></param>
        /// <returns></returns>
        public string ToString(string? format, IFormatProvider formatProvider)
        {
            return "<" + Real.ToString(format, formatProvider) + ";" + Imaginary.ToString(format, formatProvider) + ">";
        }


        //public string GetFormat(string format, IFormatProvider formatProvider)
        //{
        //    return "<" + Real.ToString(format, formatProvider) + ";" + Imaginary.ToString(format, formatProvider) + ">";
        //}


    }
}













