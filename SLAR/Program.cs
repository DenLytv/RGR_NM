double[,] A = {
    { -11, 1, -6, -3 },
    { -6, 15, -4, 3 },
    { 1, 1, 11, 1 },
    { -4, 1, 1, -11 }
};
double[] B = { -9, 4, -2, 4 };

double[] solutionKhaletskyScheme = ModifiedKhaletskyScheme(A, B);
double[] solutionGaussSeidel = GaussSeidel(A, B, 3);

Console.WriteLine("Solution by the modified Gaussian method according to the Khaletskyi scheme:");
for (int i = 0; i < solutionKhaletskyScheme.Length; i++)
{
    Console.WriteLine($"x[{i + 1}] = {solutionKhaletskyScheme[i]}");
}

Console.WriteLine("\nSolution by the Gauss-Seidel method (3 iterations):");
for (int i = 0; i < solutionGaussSeidel.Length; i++)
{
    Console.WriteLine($"x[{i + 1}] = {solutionGaussSeidel[i]}");
}

CalculateErrors(solutionKhaletskyScheme, solutionGaussSeidel);

double[] ModifiedKhaletskyScheme(double[,] A, double[] B)
{
    int n = A.GetLength(0);
    double[,] C = new double[n, n];
    double[,] D = new double[n, n];

    for (int i = 0; i < n; i++)
        D[i, i] = 1;

    for (int i = 0; i < n; i++)
    {
        for (int j = i; j < n; j++)
        {
            C[j, i] = A[j, i];
            for (int k = 0; k < i; k++)
            {
                C[j, i] -= C[j, k] * D[k, i];
            }
        }

        for (int j = i + 1; j < n; j++)
        {
            D[i, j] = A[i, j];
            for (int k = 0; k < i; k++)
            {
                D[i, j] -= C[i, k] * D[k, j];
            }
            D[i, j] /= C[i, i];
        }
    }

    double[] y = new double[n];
    for (int i = 0; i < n; i++)
    {
        y[i] = B[i];
        for (int k = 0; k < i; k++)
        {
            y[i] -= C[i, k] * y[k];
        }
        y[i] /= C[i, i];
    }

    double[] x = new double[n];
    x[n - 1] = y[n - 1];
    for (int i = n - 2; i >= 0; i--)
    {
        x[i] = y[i];
        for (int k = i + 1; k < n; k++)
        {
            x[i] -= D[i, k] * x[k];
        }
    }
    return x;
}

double[] GaussSeidel(double[,] A, double[] B, int iterations)
{
    int n = A.GetLength(0);
    double[] x = new double[n];

    for (int iteration = 0; iteration < iterations; iteration++)
    {
        for (int i = 0; i < n; i++)
        {
            double sum = B[i];
            for (int j = 0; j < n; j++)
            {
                if (j != i)
                {
                    sum -= A[i, j] * x[j];
                }
            }
            x[i] = sum / A[i, i];
        }
    }
    return x;
}

void CalculateErrors(double[] exactSolution, double[] approxSolution)
{
    Console.WriteLine("Absolute errors:");
    for (int i = 0; i < exactSolution.Length; i++)
    {
        double absoluteError = Math.Abs(exactSolution[i] - approxSolution[i]);
        Console.WriteLine($"Absolute error for x[{i + 1}] = {absoluteError}");
    }

    Console.WriteLine("Relative errors:");
    for (int i = 0; i < exactSolution.Length; i++)
    {
        double relativeError = Math.Abs((exactSolution[i] - approxSolution[i]) / exactSolution[i]);
        Console.WriteLine($"Relative error for x[{i + 1}] = {relativeError * 100}%");
    }
}