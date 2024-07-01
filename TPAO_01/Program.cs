using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TPAO_01;

namespace tpao_project_01
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Pc\OneDrive\Masaüstü\tpao_list\Random_kuyu_adlari.csv";

            string outputDir = @"C:\Users\Pc\OneDrive\Masaüstü\tpao_list\";



            List<Saha> SahaList = [];
            List<Kuyu_Grubu> Kuyu_GrubuList = [];
            List<Kuyu> KuyuList = [];
            List<Wellbore> WellboreList = [];

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
                    string kuyuGrubuEntry = sahaAdi + '-' + kuyuGrubuAdi;

                    // SahaList içinde sahaAdi'nin unique olup olmadığını kontrol et
                    bool sahaExists = SahaList.Any(saha => saha.SahaAdi == sahaAdi);

                    // Eğer SahaList içinde yoksa yeni Sahalar nesnesi oluşturup ekleyin
                    if (!sahaExists)
                    {
                        Saha saha = new Saha(sahaAdi);
                        SahaList.Add(saha);
                    }

                    // Kuyu_GruplariList içinde kuyuGrubuAdi'nin unique olup olmadığını kontrol et
                    bool kuyuGrubuExists = Kuyu_GrubuList.Any(kuyuGrubu => kuyuGrubu.KuyuGrubuAdi == kuyuGrubuEntry);

                    // Eğer Kuyu_GruplariList içinde yoksa yeni Kuyu_Gruplari nesnesi oluşturup ekleyin
                    if (!kuyuGrubuExists)
                    {
                        Kuyu_Grubu kuyuGrubu = new Kuyu_Grubu(kuyuGrubuEntry);
                        Kuyu_GrubuList.Add(kuyuGrubu);
                    }

                    bool kuyuExists = KuyuList.Any(kuyu => kuyu.KuyuAdi == kuyuGrubuEntry);

                    // Eğer KuyuList içinde yoksa yeni Kuyular nesnesi oluşturup ekleyin
                    if (!kuyuExists)
                    {
                        Kuyu kuyu = new Kuyu(kuyuGrubuEntry);
                        KuyuList.Add(kuyu);
                    }

                    // KuyuList'e kuyu bilgilerini ekleyin (burada kuyuDict kullanımı yerine KuyuList'e doğrudan ekleyeceğiz)
                    for (int i = 1; i < wellboreComponents.Length; i++)
                    {
                        if (wellboreComponents[i].StartsWith("K"))
                        {
                            // Önceki /K değerlerini oluştur
                            int kValue;
                            if (int.TryParse(wellboreComponents[i].Substring(1), out kValue))
                            {
                                for (int j = kValue; j >= 1; j--)
                                {
                                    string previousK = $"{kuyuGrubuEntry}/K{j}";
                                    AddIfNotExists(KuyuList, new Kuyu(previousK));

                                }
                                string previousKK = $"{kuyuGrubuEntry}/K";
                                AddIfNotExists(KuyuList, new Kuyu(previousKK));

                            }

                        }
                    }

                    // Wellborelar wellbore = new Wellborelar(line);

                    // WellBorelarList içinde wellbore varlığını kontrol et ve ekleyin
                    if (!WellboreList.Any(w => w.WellboreAdi == line))
                    {
                        Wellbore wellbore = new Wellbore(line);
                        WellboreList.Add(wellbore);

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
                                        AddIfNotExists(WellboreList, new Wellbore(wellboreName));

                                    }
                                    string wellboreNameWithoutNumber = previous + "/" + wellType; // ADANA-36/K1/S
                                    AddIfNotExists(WellboreList, new Wellbore(wellboreNameWithoutNumber));

                                }
                            }
                            else
                            {
                                string previous = string.Join("/", wellborePartsSlashSeparated, 0, j); //ADANA-36/K1/R
                                string wellboreNameWithoutNumber = previous + "/" + wellType;
                                AddIfNotExists(WellboreList, new Wellbore(wellboreNameWithoutNumber));

                            }
                        }
                        wellbore.WellboreAdi = wellborePartsSlashSeparated[0];
                        AddIfNotExists(WellboreList, wellbore);

                    }
                }

                //foreach (var kuyu in KuyuList)
                //{
                //    Console.WriteLine(kuyu.KuyuAdi);
                //}
                //foreach (var saha in SahaList)
                //{
                //    Console.WriteLine(saha.SahaAdi);
                //}
                //foreach (var kuyugrubu in Kuyu_GruplariList)
                //{
                //    Console.WriteLine(kuyugrubu.KuyuGrubuAdi);
                //}
                //foreach (var wellbore in WellboreList)
                //{
                //    Console.WriteLine(wellbore.WellboreAdi);
                //}


                WriteListToCsv(SahaList, Path.Combine(outputDir, "Sahalar.csv"), saha => saha.SahaAdi);
                WriteListToCsv(Kuyu_GrubuList, Path.Combine(outputDir, "Kuyu_Gruplari.csv"), kuyuGrubu => kuyuGrubu.KuyuGrubuAdi);
                WriteListToCsv(KuyuList, Path.Combine(outputDir, "Kuyular.csv"), kuyu => kuyu.KuyuAdi);
                WriteListToCsv(WellboreList, Path.Combine(outputDir, "Wellborelar.csv"), wellbore => wellbore.WellboreAdi);



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

        static void WriteListToCsv<T>(List<T> list, string filePath, Func<T, string> formatFunc)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Klasörü oluşturur (varsa tekrar oluşturmaz)

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach (var item in list)
                {
                    sw.WriteLine(formatFunc(item));
                }
            }
        }




    }   
}
