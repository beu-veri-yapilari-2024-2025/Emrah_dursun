using System;

class MatrixMultiplication
{
    static void Main()
    {
        int[,] A = {
            {1, 2},
            {3, 4}
        };

        int[,] B = {
            {5, 6},
            {7, 8}
        };

        int[,] result = new int[2, 2];

        for (int i = 0; i < 2; i++) // Satır
        {
            for (int j = 0; j < 2; j++) // Sütun
            {
                result[i, j] = 0;

                for (int k = 0; k < 2; k++)
                {
                    result[i, j] += A[i, k] * B[k, j];
                }
            }
        }

        Console.WriteLine("Matris çarpımı sonucu:");
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                Console.Write(result[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
}
