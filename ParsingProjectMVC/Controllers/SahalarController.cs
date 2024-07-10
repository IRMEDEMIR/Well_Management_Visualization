﻿using Microsoft.AspNetCore.Mvc;
using ParsingProjectMVC.Models;
using CsvHelper;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace ParsingProjectMVC.Controllers
{
    public class SahalarController : Controller
    {
        //private readonly string _filePath = "C:\\Users\\WİN10\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Sahalar.csv";
        //private readonly string _filePath = "C:\\Users\\demir\\OneDrive\\Desktop\\Parsing_Project\\TPAO_01\\output\\Sahalar.csv";
        //private readonly string _filePath = "C:\Users\Pc\OneDrive\Masaüstü\tpao_list\Parsing_Project\TPAO_01\output\Sahalar.csv";
        private readonly string _filePath = "C:\\Users\\Asus\\Desktop\\TPAO\\Parsing_Project\\TPAO_01\\output\\Sahalar.csv";

        private List<SahaModel> sahalar = new List<SahaModel>();

        public SahalarController()
        {
            LoadSahalarFromCsv();
        }

        private void LoadSahalarFromCsv()
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<dynamic>().ToList();
                    int idCounter = 1;
                    foreach (var record in records)
                    {
                        sahalar.Add(new SahaModel
                        {
                            Id = idCounter++,
                            SahaAdi = record.SahaAdi
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyası okunurken bir hata oluştu: {ex.Message}");
            }
        }

        private void SaveSahalarToCsv()
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(sahalar.Select(s => new { SahaAdi = s.SahaAdi }));
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                Console.WriteLine($"CSV dosyasına yazılırken bir hata oluştu: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 50)
        {
            var pagedSahalar = sahalar
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalItems = sahalar.Count;

            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;

            return View(pagedSahalar);
        }

        [HttpPost]
        public IActionResult Create(string sahaAdi)
        {
            if (!string.IsNullOrEmpty(sahaAdi))
            {
                var newSaha = new SahaModel
                {
                    Id = sahalar.Count > 0 ? sahalar.Max(s => s.Id) + 1 : 1,
                    SahaAdi = sahaAdi
                };
                sahalar.Add(newSaha);
                SaveSahalarToCsv();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var saha = sahalar.FirstOrDefault(s => s.Id == id);
            if (saha != null)
            {
                sahalar.Remove(saha);
                SaveSahalarToCsv();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var saha = sahalar.FirstOrDefault(s => s.Id == id);
            if (saha == null)
            {
                return NotFound();
            }
            return View(saha);
        }

        [HttpPost]
        public IActionResult Update(int id, SahaModel updatedSaha)
        {
            var saha = sahalar.FirstOrDefault(s => s.Id == id);
            if (saha == null)
            {
                return NotFound();
            }

            saha.SahaAdi = updatedSaha.SahaAdi;
            SaveSahalarToCsv();
            return RedirectToAction("Index");
        }
    }
}
