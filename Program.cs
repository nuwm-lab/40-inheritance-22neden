using System;

class Fraction
{
    protected double a1;

    // Безпечне зчитування числа з консолі
    protected double ReadDouble(string message)
    {
        double value;
        Console.Write(message);
        while (!double.TryParse(Console.ReadLine(), out value))
        {
            Console.Write("❌ Неправильне значення! Спробуйте ще раз: ");
        }
        return value;
    }

    public virtual void SetCoefficients()
    {
        a1 = ReadDouble("Введіть коефіцієнт a1: ");
    }

    public virtual void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнт: a1 = {a1:F3}");
    }

    public virtual double Evaluate(double x)
    {
        if (a1 == 0 || x == 0)
            throw new DivideByZeroException("❌ Ділення на нуль неможливе!");

        return 1 / (a1 * x);
    }
}

// === Похідний клас для тривимірного дробу ===
class ThreeDimFraction : Fraction
{
    private double a2, a3;

    public override void SetCoefficients()
    {
        a1 = ReadDouble("Введіть коефіцієнт a1: ");
        a2 = ReadDouble("Введіть коефіцієнт a2: ");

        do
        {
            a3 = ReadDouble("Введіть коефіцієнт a3 (≠0): ");
            if (a3 == 0)
                Console.WriteLine("❌ a3 не може бути 0! Повторіть введення.");
        } while (a3 == 0);
    }

    public override void PrintCoefficients()
    {
        Console.WriteLine($"Коефіцієнти: a1 = {a1:F3}, a2 = {a2:F3}, a3 = {a3:F3}");
    }

    public override double Evaluate(double x)
    {
        if (x == 0)
            throw new DivideByZeroException("❌ Ділення на нуль неможливе!");

        return 1 / (a1 * x + 1 / (a2 * x + 1 / (a3 * x)));
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
            Console.WriteLine($"➡ Результат: {f.Evaluate(x1):F4}");

            Console.WriteLine("\n=== Тривимірний дріб ===");
            ThreeDimFraction tf = new ThreeDimFraction();
            tf.SetCoefficients();
            tf.PrintCoefficients();

            double x2 = ReadInput("Введіть значення x: ");
            Console.WriteLine($"➡ Результат: {tf.Evaluate(x2):F4}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nПрограма завершена ✅");
    }

    // Статичний метод для зчитування числа у головному класі
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


