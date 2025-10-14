using System;

class Fraction
{
    protected double[] coefficients = new double[1];

    // ✅ Єдиний метод для безпечного зчитування чисел
    protected double ReadDouble(string message, bool mustBeNonZero = false)
    {
        double value;
        do
        {
            Console.Write(message);
            while (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("❌ Неправильне значення! Спробуйте ще раз: ");
            }

            if (mustBeNonZero && value == 0)
                Console.WriteLine("⚠ Коефіцієнт не може бути 0!");
        } 
        while (mustBeNonZero && value == 0);

        return value;
    }

    public virtual void SetCoefficients()
    {
        coefficients[0] = ReadDouble("Введіть коефіцієнт a1 (≠0): ", mustBeNonZero: true);
    }

    public virtual void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнт: a1 = {coefficients[0]:F3}");
    }

    public virtual double Evaluate(double x)
    {
        if (x == 0)
            throw new DivideByZeroException("❌ Ділення на нуль неможливе!");

        return 1 / (coefficients[0] * x);
    }
}

// === Похідний клас ===
class ThreeDimFraction : Fraction
{
    public ThreeDimFraction()
    {
        coefficients = new double[3];
    }

    public override void SetCoefficients()
    {
        for (int i = 0; i < coefficients.Length; i++)
        {
            coefficients[i] = ReadDouble($"Введіть коефіцієнт a{i + 1} (≠0): ", mustBeNonZero: true);
        }
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти: a1 = {coefficients[0]:F3}, a2 = {coefficients[1]:F3}, a3 = {coefficients[2]:F3}");
    }

    public override double Evaluate(double x)
    {
        if (x == 0)
            throw new DivideByZeroException("❌ Ділення на нуль неможливе!");

        // ✅ Обчислення зсередини назовні (від останнього коефіцієнта)
        double result = 0;

        for (int i = coefficients.Length - 1; i >= 0; i--)
        {
            double denominator = coefficients[i] * x + result;

            if (denominator == 0)
                throw new DivideByZeroException($"❌ Внутрішній знаменник дорівнює нулю при a{i + 1}!");

            result = 1 / denominator;
        }

        return result;
    }
}

// === Головна програма ===
class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        try
        {
            Console.WriteLine("=== Простий дріб ===");
            Fraction f = new Fraction();
            f.SetCoefficients();
            f.PrintCoefficients();

            double x1 = ReadInput("Введіть значення x: ");
            Console.WriteLine($"➡ Результат: {f.Evaluate(x1):F5}");

            Console.WriteLine("\n=== Тривимірний дріб ===");
            ThreeDimFraction tf = new ThreeDimFraction();
            tf.SetCoefficients();
            tf.PrintCoefficients();

            double x2 = ReadInput("Введіть значення x: ");
            Console.WriteLine($"➡ Результат: {tf.Evaluate(x2):F5}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nПрограма завершена ✅");
    }

    // ✅ Спільний метод для зчитування x
    static double ReadInput(string message)
    {
        double value;
        Console.Write(message);
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("❌ Неправильне значення! Спробуйте ще раз: ");
        }
        return value;
    }
}

