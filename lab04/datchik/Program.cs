using System;

class Program
{
    static int N = 100000;

    // Мультипликативный конгруэнтный генератор
    public static double[] MKG()
    {
        double a = 1664525;        // множитель
        double m = Math.Pow(2, 32); // модуль
        double seed = 12345;        // зерно (начальное значение)

        double[] x_calculated = new double[N];
        double[] x = new double[N];

        x_calculated[0] = seed;

        for (int i = 0; i < N; i++)
        {
            x[i] = x_calculated[i] / m;
            if (i < N - 1)
            {
                x_calculated[i + 1] = (x_calculated[i] * a) % m;
            }
        }

        return x;
    }

    // Встроенный генератор C#
    public static double[] RealRandom()
    {
        Random rnd = new Random();
        double[] x = new double[N];
        for (int i = 0; i < N; i++)
        {
            x[i] = rnd.NextDouble();
        }
        return x;
    }

    public static double CalculateMean(double[] data)
    {
        double sum = 0.0;
        foreach (double value in data)
        {
            sum += value;
        }
        return sum / data.Length;
    }

    public static double CalculateVariance(double[] data)
    {
        double mean = CalculateMean(data);
        double sumOfSquares = 0.0;
        foreach (double value in data)
        {
            sumOfSquares += Math.Pow(value - mean, 2);
        }
        return sumOfSquares / (data.Length - 1);
    }

    static void Main(string[] args)
    {
        double[] resultMKG = MKG();
        double[] resultRealRandom = RealRandom();

        double teor_mean = 0.5;
        double teor_var = 1.0 / 12.0;

        double mkg_mean = CalculateMean(resultMKG);
        double mkg_var = CalculateVariance(resultMKG);

        double rand_mean = CalculateMean(resultRealRandom);
        double rand_var = CalculateVariance(resultRealRandom);

        Console.WriteLine("Теоретические значения для равномерного распределения на [0, 1]:");
        Console.WriteLine($"- Среднее: {teor_mean:F6}");
        Console.WriteLine($"- Дисперсия: {teor_var:F6}\n");

        Console.WriteLine("Выборочные значения для метода MKG:");
        Console.WriteLine($"- Среднее: {mkg_mean:F6} (отклонение от теоретического: {mkg_mean - teor_mean:F6})");
        Console.WriteLine($"- Дисперсия: {mkg_var:F6} (отклонение от теоретического: {mkg_var - teor_var:F6})\n");

        Console.WriteLine("Выборочные значения для встроенного генератора:");
        Console.WriteLine($"- Среднее: {rand_mean:F6} (отклонение от теоретического: {rand_mean - teor_mean:F6})");
        Console.WriteLine($"- Дисперсия: {rand_var:F6} (отклонение от теоретического: {rand_var - teor_var:F6})");
    }
}