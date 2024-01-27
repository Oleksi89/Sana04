using System;
using System.Linq;

class Program
{
    static void Main()
    {
        int N = 5, M = 5;
        int[,] matrix = new int[N, M];
        Random rand = new Random();

        
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < M; j++)
            {
                matrix[i, j] = rand.Next(-10, 10);
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.WriteLine("Кількість додатних елементів: " + CountPositiveElements(matrix));
        Console.WriteLine("Максимальне число, що зустрічається більше одного разу: " + MaxRepeatedElement(matrix));
        Console.WriteLine("Кількість рядків, що не містять жодного нульового елемента: " + CountRowsWithoutZero(matrix));
        Console.WriteLine("Кількість стовпців, що містять хоча б один нульовий елемент: " + CountColumnsWithZero(matrix));
        Console.WriteLine("Номер рядка, в якому знаходиться найдовша серія однакових елементів: " + RowWithLongestSeries(matrix));
        Console.WriteLine("Добуток елементів в рядках, що не містять від'ємних елементів: " + ProductOfRowsWithoutNegative(matrix));
        Console.WriteLine("Максимум серед сум елементів діагоналей, паралельних головній діагоналі матриці: " + MaxSumOfDiagonals(matrix));
        Console.WriteLine("Сума елементів в стовпцях, що не містять від'ємних елементів: " + SumOfColumnsWithoutNegative(matrix));
        Console.WriteLine("Мінімум серед сум модулів елементів діагоналей, паралельних побічній діагоналі матриці: " + MinSumOfSecondaryDiagonals(matrix));
        Console.WriteLine("Сума елементів в стовпцях, які  містять хоча б один від'ємний елемент: " + SumOfColumnsWithNegative(matrix));

        Console.WriteLine("Транспонована матриця:");
        int[,] transposedMatrix = TransposeMatrix(matrix);
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                Console.Write(transposedMatrix[i, j] + "\t");
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }

    static int CountPositiveElements(int[,] matrix)
    {
        int count = 0;
        foreach (int element in matrix)
        {
            if (element > 0)
            {
                count++;
            }
        }
        return count;
    }

    static int MaxRepeatedElement(int[,] matrix)
    {
        int max = int.MinValue;
        int[] flatMatrix = matrix.Cast<int>().ToArray();
        foreach (int element in flatMatrix.Distinct())
        {
            if (flatMatrix.Count(n => n == element) > 1 && element > max)
            {
                max = element;
            }
        }
        return max;
    }

    static int CountRowsWithoutZero(int[,] matrix)
    {
        int count = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (!Enumerable.Range(0, matrix.GetLength(1)).Any(j => matrix[i, j] == 0))
            {
                count++;
            }
        }
        return count;
    }

    static int CountColumnsWithZero(int[,] matrix)
    {
        int count = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (Enumerable.Range(0, matrix.GetLength(0)).Any(i => matrix[i, j] == 0))
            {
                count++;
            }
        }
        return count;
    }

    static int RowWithLongestSeries(int[,] matrix)
    {
        int maxSeriesRow = 0;
        int maxSeriesLength = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            int currentSeriesLength = 1;
            for (int j = 1; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == matrix[i, j - 1])
                {
                    currentSeriesLength++;
                    if (currentSeriesLength > maxSeriesLength)
                    {
                        maxSeriesLength = currentSeriesLength;
                        maxSeriesRow = i;
                    }
                }
                else
                {
                    currentSeriesLength = 1;
                }
            }
        }
        return maxSeriesRow;
    }

    static int ProductOfRowsWithoutNegative(int[,] matrix)
    {
        int product = 1;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (!Enumerable.Range(0, matrix.GetLength(1)).Any(j => matrix[i, j] < 0))
            {
                product *= Enumerable.Range(0, matrix.GetLength(1)).Aggregate(1, (p, j) => p * matrix[i, j]);
            }
        }
        return product;
    }

    static int MaxSumOfDiagonals(int[,] matrix)
    {
        int maxSum = int.MinValue;
        for (int k = -matrix.GetLength(0) + 1; k < matrix.GetLength(1); k++)
        {
            int sum = 0;
            for (int j = Math.Max(0, k); j < Math.Min(matrix.GetLength(1), matrix.GetLength(0) + k); j++)
            {
                sum += matrix[j - k, j];
            }
            if (sum > maxSum)
            {
                maxSum = sum;
            }
        }
        return maxSum;
    }

    static int SumOfColumnsWithoutNegative(int[,] matrix)
    {
        int sum = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (!Enumerable.Range(0, matrix.GetLength(0)).Any(i => matrix[i, j] < 0))
            {
                sum += Enumerable.Range(0, matrix.GetLength(0)).Sum(i => matrix[i, j]);
            }
        }
        return sum;
    }

    static int MinSumOfSecondaryDiagonals(int[,] matrix)
    {
        int minSum = int.MaxValue;
        for (int k = 0; k < matrix.GetLength(0) + matrix.GetLength(1) - 1; k++)
        {
            int sum = 0;
            for (int j = Math.Max(0, k - matrix.GetLength(0) + 1); j < Math.Min(matrix.GetLength(1), k + 1); j++)
            {
                sum += Math.Abs(matrix[k - j, j]);
            }
            if (sum < minSum)
            {
                minSum = sum;
            }
        }
        return minSum;
    }

    static int SumOfColumnsWithNegative(int[,] matrix)
    {
        int sum = 0;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            if (Enumerable.Range(0, matrix.GetLength(0)).Any(i => matrix[i, j] < 0))
            {
                sum += Enumerable.Range(0, matrix.GetLength(0)).Sum(i => matrix[i, j]);
            }
        }
        return sum;
    }

    static int[,] TransposeMatrix(int[,] matrix)
    {
        int[,] transposedMatrix = new int[matrix.GetLength(1), matrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                transposedMatrix[j, i] = matrix[i, j];
            }
        }
        return transposedMatrix;
    }
}
