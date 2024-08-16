# Well Management Visualization
## Açıklama
Bu proje, Türkiye Petrolleri Anonim Ortaklığı stajında geliştirilmiş bir web uygulamasıdır. Proje, çeşitli kuyu gruplarının ve kuyu lokasyonlarının yönetimini ve görselleştirilmesini sağlar. Yer altı formasyonlarını sismik veri dosyalarından okuyarak modeller. MVC (Model-View-Controller) yapısını kullanarak, verilerin düzenli bir şekilde sunulmasını ve etkileşimli bir kullanıcı arayüzü sağlanmasını hedefler.
## Özellikler
- Saha, Kuyu Grupları, Kuyular ve Wellboreların Yönetimi: Ekleme, silme, güncelleme, okuma işlemlerinin gerçekleştirilmesi.
- Kuyu Lokasyonları Görselleştirme: Kuyu lokasyonlarını harita üzerinde görselleştirme ve detaylı veri görüntüleme.
- 3D Veri Görselleştirme: Verileri 3D modelleme ile görselleştirme ve analiz etme.
- Pagination (Sayfalama): Uzun veri listelerini yönetmek için sayfalama özelliği.
- Hakımızda sayfası: Web sayfası hakkında bilgilendirme sayfası.
## Teknolojiler
- ASP.NET MVC: Model-View-Controller yapısını kullanarak uygulama geliştirme.
- Bootstrap: Responsive ve modern tasarım için CSS framework.
- Leaflet: Harita görselleştirmesi için JavaScript kütüphanesi.
- Plotly: 3D veri görselleştirme için JavaScript kütüphanesi.
- jQuery: Dinamik ve etkileşimli kullanıcı arayüzleri için JavaScript kütüphanesi.

## Kurulum ve Başlangıç
- Proje Dosyalarını İndirin: Projeyi GitHub'dan veya diğer kaynaklardan indirerek yerel makinenizde bir klasöre çıkarın.

- Gerekli Paketleri Kurun: ASP.NET MVC ve diğer bağımlılıkları kurmak için proje dizininde "dotnet restore" komutunu çalıştırın.
  `dotnet restore`

- Proje Başlatma: Projeyi başlatmak için aşağıdaki komutu kullanın:
  `dotnet run`

- Kullanım
Kuyu Grupları Sayfası: Kuyu gruplarını listeleyin, ekleyin, güncelleyin ve silin.
Kuyu Lokasyonları: Kuyu lokasyonlarını harita üzerinde görüntüleyin ve detaylı verileri inceleyin.
3D Veri Görselleştirme: Kuyu ve yüzey verilerini görselleştirin. Verilerinizi harita üzerinde veya 3D grafikte analiz edin.

![image](https://github.com/user-attachments/assets/a6acc303-b639-4750-aa82-6bd3d34f150f)

- 3D görselleştirme sayfasında wwwroot/data sekmesinde bulunan "Bireno.json", "Serdj.json", "Borelis.json", "WellboreGeometrisi.csv" dosyalarını ayrı ayrı veya birlikte yükleyerek görselleştirin.
- Formasyonlar sayfasında sol taraftaki bölüme wwwroot/data sekmesindeki "Formasyonlar.csv" dosyasını yükleyerek formasyonların kalınlıklarına göre görselleştirin. Sağ taraftaki bölüme "WellboreGometrisi.csv" dosyasını ekleyerek kuyu geometrsisine göre güncellenen formasyon katmanlarını görüntüleyin.

## Katkıda Bulunanlar
- İrem DEMİR - Proje geliştiricisi
- Hilal Nurevşan KARAPINAR - Proje geliştiricisi
- Ayşe KAVAK - Proje geliştiricisi
- Ali Nadir ERDİL - Proje geliştiricisi
- Aziz Barış ÜZÜMCÜ - Proje geliştiricisi
- Halil Hazar TEMİZYÜREK - Proje geliştiricisi

## İletişim
Herhangi bir sorunuz veya öneriniz varsa, 
- demirirem.0202@gmail.com
- halilhazarrr@gmail.com 
- baris.uzumcu@gmail.com 
- akvk511@gmail.com
- hilalnurevsan25@gmail.com   
e-mail adreslerinden bizimle iletişime geçebilirsiniz.









