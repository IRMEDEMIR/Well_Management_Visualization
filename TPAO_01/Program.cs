using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TPAO_01;
// ADDİFNOTEXİST eklenecek
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
                    string[] parts = line.Split('-'); // ADANA-36/K1/S/R
                    string sahaAdi = parts[0]; // ADANA
                    string kuyuGrubuveKuyu = parts[1]; // 36/K1/S/R

                    string[] wellboreComponents = kuyuGrubuveKuyu.Split('/'); // [36][K1][S][R]

                    string kuyuGrubuAdi = wellboreComponents[0]; // 36
                    string kuyuGrubuEntry = sahaAdi + '-' + kuyuGrubuAdi; // ADANA-26

                  

                    Saha saha;
                    if (!sahalar.TryGetValue(sahaAdi, out saha))
                    {
                        saha = new Saha(sahaAdi);
                        sahalar[saha.SahaAdi] = saha;
                    }

                    Kuyu_Grubu kuyuGrubu;
                    if (!kuyuGruplari.TryGetValue(kuyuGrubuEntry, out kuyuGrubu))
                    {
                        kuyuGrubu = new Kuyu_Grubu(kuyuGrubuEntry);
                        kuyuGruplari[kuyuGrubu.KuyuGrubuAdi] = kuyuGrubu;
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

