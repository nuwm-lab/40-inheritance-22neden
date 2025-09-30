using System;

class Fraction
{
    protected double a1;

    public virtual void SetCoefficients()
    {
        Console.Write("Введіть коефіцієнт a1: ");
        a1 = Convert.ToDouble(Console.ReadLine());
    }

    public virtual void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнт: a1 = {a1}");
    }

    public virtual double Evaluate(double x)
    {
        if (a1 == 0 || x == 0)
            throw new DivideByZeroException("Ділення на нуль!");
        return 1 / (a1 * x);
    }
}

// Похідний клас
class ThreeDimFraction : Fraction
{
    private double a2, a3;

    public override void SetCoefficients()
    {
        Console.Write("Введіть коефіцієнт a1: ");
        a1 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коефіцієнт a2: ");
        a2 = Convert.ToDouble(Console.ReadLine());
        Console.Write("Введіть коефіцієнт a3 (≠0): ");
        a3 = Convert.ToDouble(Console.ReadLine());
        if (a3 == 0) throw new ArgumentException("a3 не може бути 0!");
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти: a1 = {a1}, a2 = {a2}, a3 = {a3}");
    }

    public override double Evaluate(double x)
    {
        if (x == 0) throw new DivideByZeroException("Ділення на нуль!");
        return 1 / (a1 * x + 1 / (a2 * x + 1 / (a3 * x)));
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Простий дріб ===");
        Fraction f = new Fraction();
        f.SetCoefficients();
        f.PrintCoefficients();
        Console.Write("Введіть значення x: ");
        double x1 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine($"Значення дробу: {f.Evaluate(x1)}");

        Console.WriteLine("\n=== Тривимірний дріб ===");
        ThreeDimFraction tf = new ThreeDimFraction();
        tf.SetCoefficients();
        tf.PrintCoefficients();
        Console.Write("Введіть значення x: ");
        double x2 = Convert.ToDouble(Console.ReadLine());
        Console.WriteLine($"Значення дробу: {tf.Evaluate(x2)}");
    }
}
