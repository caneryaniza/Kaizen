// See https://aka.ms/new-console-template for more information
List<string> secretCodes = new List<string>();
generateCode(); // business
Console.WriteLine("Devam etmek için bir tuşa basınız...");
Console.ReadKey();

void generateCode()
{
    Console.WriteLine("-------------Generated Codes-------------");
    Random rnd = new Random();
    for (int i = 0; i < 100000; i++)
    {
        string generatedCode = "";
        for (int j = 0; j < 8; j++)
        {
            int rndLetter, rndNumber;
            int numb = rnd.Next();
            if (numb % 2 == 0) // çift ise random bir harf oluştur.
            {
                rndLetter = rnd.Next(65, 91);
                // IJOQSVUW harfleri hariç bir harf üretir.
                if (rndLetter == 73 || rndLetter == 74 || rndLetter == 79 || rndLetter == 81 || rndLetter == 83 || rndLetter == 85 || rndLetter == 86 || rndLetter == 87) 
                {
                    j = j - 1;
                    continue;
                }
                char character = (char)rndLetter;
                generatedCode += character;

            }
            else // değil ise random bir sayı oluştur.
            {
                rndNumber = rnd.Next(0, 10);
                // 0168 sayıları hariç bir sayı üretir.
                if (rndNumber == 0 || rndNumber == 1 || rndNumber == 6 || rndNumber == 8)
                {
                    j = j - 1;
                    continue;
                }
                generatedCode += rndNumber.ToString();
            }
        }
        bool isValid = false; 
        checkCode(generatedCode, out isValid); // gerekli checkCode fonksiyonu. Kod üretilirken kontrol işlemi sağlıyor
        if (isValid && !secretCodes.Contains(generatedCode)) // üretilen kod daha önce üretildiyse check edilip geçerli datalar içerisinde basılmıyor.
        {
            secretCodes.Add(generatedCode);
            Console.WriteLine(i + 1 + ": " + generatedCode + "  ---  isValid data: " + isValid);
        }
        else
        {
            i = i - 1;
        }
    }
}

void checkCode (string code, out bool isValid) // kod kontrolü için gerekli fonksiyon
{
    isValid = true;
    //IJOQSVUW, 0168
    if (code.Length != 8 &&  
        (code.Contains('I') || code.Contains('J') || code.Contains('O') || code.Contains('Q') || code.Contains('S') || code.Contains('V') || code.Contains('W') || code.Contains('U') || code.Contains('0') || code.Contains('1') || code.Contains('6') || code.Contains('8'))
       )
    {
        isValid = false;
    }


    
}