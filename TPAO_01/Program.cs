using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using TPAO_01;

namespace tpao_project_01
{
    class Program
    {
        static void Main(string[] args)
        {
            //string filePath = @"C:\Users\Pc\OneDrive\Masaüstü\tpao_list\Random_kuyu_adlari.csv";
            //string outputDir = @"C:\Users\Pc\OneDrive\Masaüstü\tpao_list\";

            string filePath = @"C:\Users\Asus\Desktop\TPAO\Parsing_Project\TPAO_01\Random_kuyu_adlari.csv";
            string outputDir = @"C:\Users\Asus\Desktop\TPAO\Parsing_Project\TPAO_01\output\";

           

            Dictionary<string, Saha> sahalar = new Dictionary<string, Saha>();
            Dictionary<string, Kuyu_Grubu> kuyuGruplari = new Dictionary<string, Kuyu_Grubu>();
            Dictionary<string, Kuyu> kuyular = new Dictionary<string, Kuyu>();
            Dictionary<string, Wellbore> wellborelar = new Dictionary<string, Wellbore>();


            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string sahaAdi = SahaOlustur(line);  // ADANA

                    Saha saha;
                    if (!sahalar.TryGetValue(sahaAdi, out saha))
                    {
                        saha = new Saha(sahaAdi);
                        sahalar[saha.SahaAdi] = saha;
                    }


                    string kuyuGrubuAdi = KuyuGrubuOlustur(line);  // ADANA-36
                        
                    Kuyu_Grubu kuyuGrubu;
                    if (!kuyuGruplari.TryGetValue(kuyuGrubuAdi, out kuyuGrubu))
                    {
                        kuyuGrubu = new Kuyu_Grubu(kuyuGrubuAdi);
                        kuyuGruplari[kuyuGrubu.KuyuGrubuAdi] = kuyuGrubu;
                    }

                    foreach (KuyuOlustur(line))
                    {
                        AddKuyu(kuyular, kuyu)
                    } //liste 

                    static void AddKuyu(Dictionary<string, Kuyu> kuyular, string kuyuEntry)
                    {
                        if (!kuyular.ContainsKey(kuyuEntry))
                        {
                            kuyular[kuyuEntry] = new Kuyu(kuyuEntry);
                        }
                    }

                    static void AddSaha(Dictionary<string, Saha> sahalar, string sahaAdi)
                    {
                        if (!sahalar.ContainsKey(sahaAdi))
                        {
                            sahalar[sahaAdi] = new Saha(sahaAdi);
                        }
                    }

                    Kuyu kuyu;
                    if (!kuyular.TryGetValue(kuyuGrubuEntry, out kuyu))
                    {
                        kuyu = new Kuyu(kuyuGrubuEntry);
                        kuyular[kuyu.KuyuAdi] = kuyu;
                    }

                    // kuyular dictionary'ine kuyu bilgileri eklemme
                    for (int i = 1; i < wellboreComponents.Length; i++)
                    {
                        if (wellboreComponents[i].StartsWith("K"))
                        {
                            // Önceki /K değerlerini oluştur
                            int kValue;
                            if (wellboreComponents[i].Length == 1)
                            {
                                string previous = $"{kuyuGrubuEntry}/K";
                                kuyular[previous] = new Kuyu(previous);
                            }
                            if (int.TryParse(wellboreComponents[i].Substring(1), out kValue))
                            {
                                for (int j = kValue; j >= 1; j--)
                                {
                                    string previousK = $"{kuyuGrubuEntry}/K{j}";
                                    if (!kuyular.ContainsKey(previousK))
                                    {
                                        kuyular[previousK] = new Kuyu(previousK);
                                    }
                                }
                                string previousKK = $"{kuyuGrubuEntry}/K";
                                if (!kuyular.ContainsKey(previousKK))
                                {
                                    kuyular[previousKK] = new Kuyu(previousKK);
                                }
                            }
                        }
                    }

                    // Wellborelar wellbore = new Wellborelar(line);

                    // WellBorelarList içinde wellbore varlığını kontrol et ve ekleyin
                    if (!wellborelar.ContainsKey(line))
                    {
                        Wellbore wellbore = new Wellbore(line);
                        wellborelar[line] = wellbore;

                        string[] wellborePartsSlashSeparated = line.Split('/'); // slashlara göre parçaladı

                        int k = wellborePartsSlashSeparated.Length - 1;
                        for (int j = k; j > 0; j--) // son elemandan ilk elemana kadar
                        {
                            char wellType = wellborePartsSlashSeparated[j][0]; // S gibi
                            if (wellborePartsSlashSeparated[j].Length > 1) // S1 gibi
                            {
                                int sValue;
                                if (int.TryParse(wellborePartsSlashSeparated[j].Substring(1), out sValue)) // S'in yanındaki 1 i aldı //1
                                {
                                    string previous = string.Join("/", wellborePartsSlashSeparated, 0, j); //ADANA-36/K1/R//Birleştirme
                                    for (int i = sValue; i > 0; i--)
                                    {
                                        string wellboreName = previous + "/" + wellType + i; //öncesi + /S2 ,  öncesi + /S1  
                                        if (!wellborelar.ContainsKey(wellboreName))
                                        {
                                            wellborelar[wellboreName] = new Wellbore(wellboreName);
                                        }
                                    }
                                    string wellboreNameWithoutNumber = previous + "/" + wellType; // ADANA-36/K1/S
                                    if (!wellborelar.ContainsKey(wellboreNameWithoutNumber))
                                    {
                                        wellborelar[wellboreNameWithoutNumber] = new Wellbore(wellboreNameWithoutNumber);
                                    }
                                }
                            }
                            else
                            {
                                string previous = string.Join("/", wellborePartsSlashSeparated, 0, j); //ADANA-36/K1/R
                                string wellboreNameWithoutNumber = previous + "/" + wellType;
                                if (!wellborelar.ContainsKey(wellboreNameWithoutNumber))
                                {
                                    wellborelar[wellboreNameWithoutNumber] = new Wellbore(wellboreNameWithoutNumber);
                                }
                            }
                        }
                        wellbore.WellboreAdi = wellborePartsSlashSeparated[0];
                        if (!wellborelar.ContainsKey(wellbore.WellboreAdi))
                        {
                            wellborelar[wellbore.WellboreAdi] = wellbore; 
                        }
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
            string sahaAdi = parts[0];
            return sahaAdi;
        }

        public static string KuyuGrubuOlustur(string line)
        {
            string[] parts = line.Split('/');
            string KuyuGrubu = parts[0];
            return KuyuGrubu;
        }

        public static List<string> KuyuOlustur(string line)
        {
            //KuyuAdlarını bir diziye ye da listeye ekleyip return et ve ekleme fonksiyonuna ver
            string[] parts = line.Split('/');
            string KuyuAdi;

            List<string> OlusturulanKuyular = new List<string>();

            // Eğer parts en az iki eleman içeriyorsa, ikinci elemanda "K" olup olmadığını kontrol edelim
            if (parts.Length > 1 && parts[1].Contains("K"))
            {
                KuyuAdi = parts[0];  //ADANA-36
                OlusturulanKuyular.Add(KuyuAdi);

                KuyuAdi = parts[0] + "/" + parts[1];  //ADANA-36/K1
                OlusturulanKuyular.Add(KuyuAdi);

                int kValue;
                if (int.TryParse(parts[1].Substring(1), out kValue))         //k nın yanındaki sayıyı alıyor
                {
                    for (int i = kValue - 1; i > 0; i--)
                    {
                        KuyuAdi = parts[0] + "/K" + i;
                        OlusturulanKuyular.Add(KuyuAdi);
                    }
                }

                KuyuAdi = parts[0] + "/K";  //ADANA-36/K
                OlusturulanKuyular.Add(KuyuAdi);
            }
            else  // ADANA-36  veya ADANA-36/R/S  falan
            {
                KuyuAdi = parts[0];
                OlusturulanKuyular.Add(KuyuAdi);
            }
            return OlusturulanKuyular;
        }

        public static List<string> WellborelarıOlustur(string line)
        {
            string WellBoreAdi;

            List<string> OlusturulanWellborelar = new List<string>();

            WellBoreAdi = line;  //önce tamamını ekledi ADANA-36/K1/R/S2
            OlusturulanWellborelar.Add(line);

            string[] parts = line.Split('/'); //slashlara göre parçaladı

            int k = parts.Length - 1;
            for (int j = k; j > 0; j--)  //son elemandan ilk elemana kadar   // [ADANA-36] [K1] [R] [S2] 
            {
                char wellType = parts[j][0];  //S
                if (parts[j].Length > 1) //S1
                {
                    int kValue;
                    if (int.TryParse(parts[j].Substring(1), out kValue))  //1
                    {
                        string öncesi = string.Join("/", parts, 0, j); //ADANA-36/K1/R
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
                    string öncesi = string.Join("/", parts, 0, j); //ADANA-36/K1/R
                    WellBoreAdi = öncesi + "/" + wellType;
                    OlusturulanWellborelar.Add(WellBoreAdi);
                }
            }
            WellBoreAdi = parts[0];
            OlusturulanWellborelar.Add(WellBoreAdi);

            return OlusturulanWellborelar;
        }

        static void AddIfNotExists<T>(List<T> list, T item)
        {
            if (!list.Any(x => x.Equals(item)))
            {
                list.Add(item);
            }
        }

       
        static void WriteToCsv(IEnumerable<string> dataList, string outputPath)
        {
            List<string> lines = new List<string> {  };
            lines.AddRange(dataList);
            File.WriteAllLines(outputPath, lines);
        }
    }




    }   


//sayısını kontrol et 
// kuyuları kontrol et
// verinin formatına göre kontrol et
//static nedir öğren
