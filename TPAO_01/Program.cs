using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TPAO_01;

namespace tpao_project_01
{
    public static class Program
    {

        static void Main(string[] args)
        {
            //string filePath = @"C:\Users\Pc\OneDrive\Masaüstü\tpao_list\Random_kuyu_adlari.csv";
            //string outputDir = @"C:\Users\Pc\OneDrive\Masaüstü\tpao_list\";

            string filePath = @"C:\Users\Asus\Desktop\TPAO\Parsing_Project\TPAO_01\Random_kuyu_adlari.csv";
            string outputDir = @"C:\Users\Asus\Desktop\TPAO\Parsing_Project\TPAO_01\output\";

            //string filePath = @"C:\Users\demir\OneDrive\Desktop\Parsing_Project\TPAO_01\Random_kuyu_adlari.csv";
            //string outputDir = @"C:\Users\demir\OneDrive\Desktop\Parsing_Project\TPAO_01\output\";

           // string filePath = @"C:\Users\WİN10\Desktop\TPAO\Parsing_Project\TPAO_01\Random_kuyu_adlari.csv";
           // string outputDir = @"C:\Users\WİN10\Desktop\TPAO\Parsing_Project\TPAO_01\output\";

            Dictionary<string, Saha> sahalar = new Dictionary<string, Saha>();
            Dictionary<string, Kuyu_Grubu> kuyuGruplari = new Dictionary<string, Kuyu_Grubu>();
            Dictionary<string, Kuyu> kuyular = new Dictionary<string, Kuyu>();
            Dictionary<string, Wellbore> wellborelar = new Dictionary<string, Wellbore>();

            try
            {
                //satır satır oku
                string[] lines = File.ReadAllLines(filePath);

                //her satır için işlem yap
                foreach (string line in lines)
                {

                    //saha oluturma fonksiyonunu çağır
                    string sahaAdi = SahaOlustur(line);  // ADANA

                    //sahaları dictionary ye ekle
                    AddSaha(sahalar, sahaAdi);

                    //kuyu grubu oluşturma fonksiyonunu çağır
                    string kuyuGrubuAdi = KuyuGrubuOlustur(line);  // ADANA-36

                    //kuyugruplarını dictionary ye ekle
                    AddKuyuGrubu(kuyuGruplari, kuyuGrubuAdi);

                    //kuyuOluştur fonksiyonunu çağır ve liste olarak al
                    List<string> KuyuListesi = KuyuOlustur(line);

                    //kuyuları dictionary ye ekle
                    foreach (string kuyu in KuyuListesi)
                    {
                        AddKuyu(kuyular, kuyu);
                    }

                    //wellboreOluştur fonksiyonunu çağır ve liste olarak al
                    List<string> wellboreListesi = WellboreOlustur(line);

                    //wellboreları dictionary ye ekle 
                    foreach (string wellbore in wellboreListesi)
                    {
                        AddWellbore(wellborelar, wellbore);
                    }

                }

                WriteToCsv(sahalar.Keys, Path.Combine(outputDir, "Sahalar.csv"));
                WriteToCsv(kuyuGruplari.Keys, Path.Combine(outputDir, "Kuyu Grupları.csv"));
                WriteToCsv(kuyular.Keys, Path.Combine(outputDir, "Kuyular.csv"));
                WriteToCsv(wellborelar.Keys, Path.Combine(outputDir, "Wellborelar.csv"));

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }
        }
        public static string SahaOlustur(string line)
        {
            string[] parts = line.Split('-');

            if (parts.Length > 2)
            {
                Console.WriteLine("Error");
                string err = "Error";
                return err;
            }

            else if (Regex.IsMatch(parts[0], @"^[A-Z ]+$"))
            {
                return parts[0];
            }
            else
            {
                Console.WriteLine("Error");
                string err = "Error";
                return err;
            }
        }

        public static string KuyuGrubuOlustur(string line)
        {
            string[] parts = line.Split('-');
            string kuyuGrubuveKuyu = parts[1]; // 36/K1/S
            string[] wellboreComponents = kuyuGrubuveKuyu.Split('/'); // 36/K1/S
            string kuyuGrubuEntry = parts[0] + "-" + wellboreComponents[0];

            if (parts.Length > 2)
            {
                string err = "Error";
                Console.WriteLine(err);
                return err;
            }
            else if (wellboreComponents.Length < 1 || !Regex.IsMatch(wellboreComponents[0], @"^\d+$") || !Regex.IsMatch(parts[0], @"^[A-Z ]+$"))
            {
                string err = "Error";
                Console.WriteLine(err);
                return err;
            }
            else
                return kuyuGrubuEntry;
        }

        public static List<string> KuyuOlustur(string line)
        {

            string[] parts = line.Split('/');
            string KuyuAdi;

            List<string> OlusturulanKuyular = new List<string>();

            if (parts.Length > 1 && parts[1].Contains("K"))
            {
                KuyuAdi = parts[0];  //ADANA-36
                OlusturulanKuyular.Add(KuyuAdi);

                if (parts[1].Length == 1)
                {
                    KuyuAdi = parts[0] + "/K";  // ADANA-36/K
                    OlusturulanKuyular.Add(KuyuAdi);
                }
                else
                {
                    // "K" harfinden sonraki sayıyı al ve geriye doğru kuyu adlarını oluştur
                    int kValue;
                    if (int.TryParse(parts[1].Substring(1), out kValue))
                    {
                        KuyuAdi = parts[0] + "/" + parts[1];  // ADANA-36/K1
                        OlusturulanKuyular.Add(KuyuAdi);

                        for (int i = kValue - 1; i > 0; i--)
                        {
                            KuyuAdi = parts[0] + "/K" + i;
                            OlusturulanKuyular.Add(KuyuAdi);
                        }
                        KuyuAdi = parts[0] + "/K";  // ADANA-36/K
                        OlusturulanKuyular.Add(KuyuAdi);
                    }
                    else
                    {

                        return new List<string>() { "Error" };
                    }
                }
            }
            else  // ADANA-36  veya ADANA-36/R/S  falan
            {
                KuyuAdi = parts[0];
                OlusturulanKuyular.Add(KuyuAdi);
            }
            return OlusturulanKuyular;
        }

        public static List<string> WellboreOlustur(string line)
        {
            string WellBoreAdi;

            List<string> OlusturulanWellborelar = new List<string>();

            WellBoreAdi = line;  //önce tamamını ekledi ADANA-36/K1/R/S2
            OlusturulanWellborelar.Add(line);

            string[] parts = line.Split('-');
            string kuyuGrubuveKuyu = parts[1]; // 36/K1/S
            string[] wellboreComponents = kuyuGrubuveKuyu.Split('/'); // 36/K1/S
            string kuyuGrubuEntry = parts[0] + "-" + wellboreComponents[0];

            if (parts.Length > 2)// '-' Kontorlü
            {
                OlusturulanWellborelar.Clear();
                OlusturulanWellborelar.Add("Error");
                return OlusturulanWellborelar;
            }
            else if (!Regex.IsMatch(wellboreComponents[0], @"^\d+$") || !Regex.IsMatch(parts[0], @"^[A-Z ]+$"))
            {
                OlusturulanWellborelar.Clear();
                OlusturulanWellborelar.Add("Error");
                return OlusturulanWellborelar;
            }
            else
            {
                OlusturulanWellborelar.Clear();


                string[] slashParts = line.Split('/'); //slashlara göre parçaladı //ADANA -36      K1/R1
                if (slashParts.Length > 1 && !Regex.IsMatch(slashParts[1], @"^[KSRM]\d*$"))
                {
                    Console.WriteLine("Error");
                    OlusturulanWellborelar.Clear();
                    OlusturulanWellborelar.Add("Error");
                    return OlusturulanWellborelar;
                }



                int k = slashParts.Length - 1;
                for (int j = k; j > 0; j--)  //son elemandan ilk elemana kadar   // [ADANA-36] [K1] [R] [S2] 
                {
                    char wellType = slashParts[j][0];  //S
                    if (slashParts[j].Length > 1) //S1
                    {
                        int kValue;
                        if (int.TryParse(slashParts[j].Substring(1), out kValue))  //1
                        {
                            string öncesi = string.Join("/", slashParts, 0, j); //ADANA-36/K1/R
                            for (int i = kValue; i > 0; i--)
                            {
                                WellBoreAdi = öncesi + "/" + wellType + i;  //öncesi + /S2 ,  öncesi + /S1  
                                OlusturulanWellborelar.Add(WellBoreAdi);
                            }
                            WellBoreAdi = öncesi + "/" + wellType; //öncesi + S
                            OlusturulanWellborelar.Add(WellBoreAdi);
                        }
                    }
                    else
                    {
                        string öncesi = string.Join("/", slashParts, 0, j); //ADANA-36/K1/R
                        WellBoreAdi = öncesi + "/" + wellType;
                        OlusturulanWellborelar.Add(WellBoreAdi);
                    }
                }
                WellBoreAdi = slashParts[0];
                OlusturulanWellborelar.Add(WellBoreAdi);

                return OlusturulanWellborelar;
            }
        }


        static void AddSaha(Dictionary<string, Saha> sahalar, string sahaAdi)
        {

            if (!sahalar.ContainsKey(sahaAdi))
            {
                sahalar[sahaAdi] = new Saha(sahaAdi);
            }
        }

        static void AddKuyuGrubu(Dictionary<string, Kuyu_Grubu> kuyuGruplari, string kuyuGrubuEntry)
        {
            if (!kuyuGruplari.ContainsKey(kuyuGrubuEntry))
            {
                kuyuGruplari[kuyuGrubuEntry] = new Kuyu_Grubu(kuyuGrubuEntry);
            }
        }

        static void AddKuyu(Dictionary<string, Kuyu> kuyular, string kuyuEntry)
        {
            if (!kuyular.ContainsKey(kuyuEntry))
            {
                kuyular[kuyuEntry] = new Kuyu(kuyuEntry);
            }
        }

        static void AddWellbore(Dictionary<string, Wellbore> wellborelar, string line)
        {
            if (!wellborelar.ContainsKey(line))
            {
                wellborelar[line] = new Wellbore(line);
            }
        }


        static void WriteToCsv(IEnumerable<string> dataList, string outputPath)
        {
            List<string> lines = new List<string> { };
            lines.AddRange(dataList);
            File.WriteAllLines(outputPath, lines);
        }
    }
}




//public static string ParseSahaAdi(string line)
//{
//    string[] parts = line.Split('-');

//    if (parts.Length > 2)
//    {
//        Console.WriteLine("Error");
//        string err = "Error";
//        return err;
//    }

//    else if (Regex.IsMatch(parts[0], @"^[a-zA-Z]+$"))
//    {
//        return parts[0];
//    }

//    else
//    {
//        Console.WriteLine("Error");
//        string err = "Error";
//        return err;
//    }
//}

//public static string ParseKuyuGrubuAdi(string line)
//{
//    string[] parts = line.Split('-');
//    string kuyuGrubuveKuyu = parts[1]; // 36/K1/S
//    string[] wellboreComponents = kuyuGrubuveKuyu.Split('/'); // 36/K1/S

//    if (parts.Length > 2)
//    {
//        string err = "Error";
//        Console.WriteLine(err);
//        return err;
//    }
//    else if (wellboreComponents.Length < 1 || !Regex.IsMatch(wellboreComponents[0], @"^\d+$"))
//    {
//        string err = "Error";
//        Console.WriteLine(err);
//        return err;
//    }
//    else
//        return wellboreComponents[0];
//}