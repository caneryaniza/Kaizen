// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using ReadJSON;

string jsonFilePath = "C:\\Users\\caner\\Desktop\\Kaizen\\ReadJSON\\response.json"; //local için değiştirilmesi gerekiyor.
string responseFilePath = "C:\\Users\\caner\\Desktop\\Kaizen\\ReadJSON\\response.txt"; //local için değiştirilmesi gerekiyor.
Console.WriteLine("İşlem devam ediyor lütfen bekleyin.");
List<JSONClass> items = new List<JSONClass>();
readJSON();
if (File.Exists(responseFilePath))
{
    File.Delete(responseFilePath);
}
makeReceipt();
void makeReceipt()
{
    bool isStarted = false;
    using (StreamWriter w = File.AppendText(responseFilePath))
    {
        for (int y = 0; y < 1100; y++)
        {
            for (int x = 0; x < 1000; x++)
            {

                
                foreach (var hede in items.ToList())
                { // benzer x-y düzeleminde olabilecek değerleri alıp yukarıdan aşağıya - soldan sağa yazdırma işlemi gerçekleştiriliyor.
                    // bir değer için verilen dikdörtgensel alan baz alınmadı. Sadece başlangıç noktaları esas alınarak işlem gerçekleştirildi.
                    if (hede?.boundingPoly?.vertices?.Where(q => (q.y == y || (q.y > y - 10 && q.y < y + 10)) && (q.x == x || (q.x > x - 400 && q.x < x + 400))).ToList().Count > 0)
                    {
                        if (hede != null)
                        {
                            isStarted = true;
                            w.Write(hede.description);
                            items.Remove(hede);
                        }
                        if (x % 3 == 0 && isStarted)
                        {
                            w.Write(" ");  // yazdırma işlemi başladıysa kareler resize edilerek x düzleminde her üç atlamada bir boşluk konuldu.
                        }
                    }
                }


            }
            if (isStarted && y%27 == 0)
            {
                w.WriteLine(Environment.NewLine); // her yirmi yedi y ekseni oynamasında bir satır aşağı inildi.
            }

        }
    }
}

void readJSON() // json text içeriğini okuma işlemi
{
    using (StreamReader r = new StreamReader(jsonFilePath))
    {
        string json = r.ReadToEnd();
        items = JsonConvert.DeserializeObject<List<JSONClass>>(json);
    }
}
