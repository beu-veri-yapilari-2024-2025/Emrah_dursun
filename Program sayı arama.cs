using System;

class SearchInArray
{
    static void Main()
    {
        int[] numbers = { 3, 7, 2, 9, 4 };
        Console.Write("Aranacak sayıyı girin: ");
        int search = int.Parse(Console.ReadLine());

        bool found = false;

        foreach (int number in numbers)
        {
            if (number == search)
            {
                found = true;
                break;
            }
        }

        if (found)
            Console.WriteLine("Sayı dizide bulundu.");
        else
            Console.WriteLine("Sayı dizide bulunamadı.");
    }
}
