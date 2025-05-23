//double[] X = { 0.4, 0.8, 1.2, 1.6, 2 };
double[] X = { 2, 4, 6, 8, 10 };
//double[] Y = { -0.916291, -0.223144, 0.182322, 0.47004, 0.693147 };
double[] Y = { 0.693147, 1.38629, 1.79176, 2.07944, 2.30259 };

Y = fillY(X);

double x = 3;
//double x = 0.5;

double result = NewtonBackwardInterpolation(X, Y, x);
Console.WriteLine($"The value of a function at a point x = {x}: {result}");

double absoluteError = Math.Abs(Math.Log(x) - result);
Console.WriteLine($"Absolute error: {absoluteError}");
Console.WriteLine($"Relative error: {absoluteError / Math.Abs(Math.Log(x)) * 100}%");

double[] fillY(double[] X)
{
    double[] tmp = new double[X.Length];
    for (int i = 0; i < X.Length; i++)
    {
        tmp[i] = Math.Log(X[i]);
    }
    return tmp;
}

double[,] CalculateBackwardDifferences(double[] Y)
{
    int n = Y.Length;
    double[,] differences = new double[n, n];

    for (int i = 0; i < n; i++)
        differences[i, 0] = Y[i];

    for (int j = 1; j < n; j++)
        for (int i = n - 1; i >= j; i--)
            differences[i, j] = differences[i, j - 1] - differences[i - 1, j - 1];

    return differences;
}

double Factorial(int n)
{
    double result = 1;
    for (int i = 2; i <= n; i++)
        result *= i;
    return result;
}

double NewtonBackwardInterpolation(double[] X, double[] Y, double x)
{
    int n = X.Length;
    double[,] differences = CalculateBackwardDifferences(Y);

    double h = X[1] - X[0];
    double result = Y[n - 1];

    double termMultiplier = 1.0;

    for (int i = 1; i <= 4; i++)
    {
        termMultiplier *= (x - X[n - i]);
        double term = (termMultiplier / (Factorial(i) * Math.Pow(h, i))) * differences[n - 1, i];
        result += term;
    }

    return result;
}