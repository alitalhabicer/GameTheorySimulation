// 10 adet karakterimiz var 
//birbirleri ile 10 tane oyun oynasın her biri 

/*
  // önce karakterler tanıtılır 
  // sonra karakterler kodlanır belirsiz sayı oyun oynanacak şekilde boolean ile true false 
  // sonra kullanıcı karakterleri seçer ve ikisin savaştırır 
  // sonuc önce genel kazananı sonra her maçın sonucunu gösterir 
  // strategy a ve strategy b olarak 2 tane bool listesi oluştururuz  
  // çünkü sadece ver yada verme yapabiliriiz ama ++ +- -- -+ şeklinde 4 tane kombinasyonumuz var
  // bu kombinasyon sonucuna göre +2+2  +3-1 -1+3 yada 00 şeklinde 4 tane sonuc gireceğiz her heronun bakiyesine 
  // bakiye de değiştirilmesin diye private olup her mac sonuna göre pakiye a ve bakiye b ye eklenip çıkarılsın ama 
  // oyun sayısı belirlemesi lazım kullanıcının bu yüzden bool listesi büyüklüğü tanımsız olmalı 
  // oyun sonunda hepsi sıfırlanmalı 
 
 */
using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
namespace OyunTeorisi
{
    public class program
    {


        public static void Main()
        {
            
            


            // Konsolu temizle ve başlık yaz
            Console.Clear();
            Console.Title = "Oyun Teorisi Simülasyonu: Tekrarlı Mahkum İkilemi";

            OyunAmaciniYazdir();
            Console.WriteLine("\n" + new string('-', 80) + "\n");
            PuanTablosunuGoster();
            Console.WriteLine("\n" + new string('-', 80) + "\n");
            KarakterAciklamalariniGoster();

            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();


            CharSelect();
        }

        // Oyunun amacını yazdıran metot
        public static void OyunAmaciniYazdir()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- OYUN TEORİSİ SİMÜLASYONU: TEKRARLI MAHKUM İKİLEMİ ---\n");
            Console.ResetColor();

            Console.WriteLine("Oyunun Amacı:");
            Console.WriteLine("\tBu oyun, iki rasyonel oyuncunun (karakterin) belirli bir sayıda tur boyunca stratejik olarak etkileşimini inceler.");
            Console.WriteLine("\tHer turda, her oyuncu aynı anda iki karardan birini verir:");
            Console.WriteLine("\t\t1. İşbirliği (True / C)");
            Console.WriteLine("\t\t2. İhanet (False / D)");
            Console.WriteLine("\n\tKazanan, oyun sonunda **en yüksek toplam puanı (kazancı)** elde eden karakterdir.");
            Console.WriteLine("\tAmacınız, rakibinizin hamlelerini tahmin ederek kendi skorunuzu maksimize etmektir.");
        }

        // Puan tablosunu (Kazanç Matrisi) yazdıran metot
        public static void PuanTablosunuGoster()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- KAZANÇ MATRİSİ (PUAN TABLOSU) ---\n");
            Console.ResetColor();

            // Puan Tablosu Başlığı
            Console.WriteLine("\t\t\t| Oyuncu B: İşbirliği (True)\t| Oyuncu B: İhanet (False)");
            Console.WriteLine(new string('-', 100));

            // Durum 1: Oyuncu A: İşbirliği (True)
            Console.Write("Oyuncu A: İşbirliği (True)");
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Orta Kazanç: A: +2, B: +2 ");
            Console.ResetColor();
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Sömürü: A: -1, B: +3");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));

            // Durum 2: Oyuncu A: İhanet (False)
            Console.Write("Oyuncu A: İhanet (False)");
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Sömürülme: A: +3, B: -1");
            Console.ResetColor();
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" Karşılıklı Kayıp: A: 0, B: 0");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));
        }

        // Karakter açıklamalarını yazdıran metot
        public static void KarakterAciklamalariniGoster()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- KARAKTER LİSTESİ VE STRATEJİLERİ ---\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NO\tKARAKTER ADI\t\tSTRATEJİ ÖZETİ");
            Console.WriteLine(new string('-', 60));
            Console.ResetColor();

            // Karakterler ve açıklamalar
            Console.WriteLine("1.\tKopyacı\t\t\tÖnce True, sonra rakibin bir önceki hamlesini kopyalar (Tit-for-Tat).");
            Console.WriteLine("2.\tPonçik\t\t\tRakip ne yaparsa yapsın **HEP TRUE** hamlesini yapar (Always Cooperate).");
            Console.WriteLine("3.\tSinsi\t\t\tHer durumda sadece kendi çıkarını düşünür ve false hamlesini yapar (Always Defect)");
            Console.WriteLine("4.\tHain Hafıza\t\tBaşlangıçta True. Rakip **bir kere bile** False yaparsa, oyun bitene kadar hep False.");
            Console.WriteLine("5.\tŞanslı Cimbom\t\tGeçmişi umursamaz. Her turda kararlarını rastgele (random True/False) verir.");
            Console.WriteLine("6.\tAffetmez Ayna\t\tKopyacı gibidir, ancak nadiren (küçük bir olasılıkla) rakibin False hamlesini affeder.");
            Console.WriteLine("7.\tSömürücü\t\tBaşlangıçta False. Rakip tepki vermezse False devam eder, tepki verirse True'ya geçer.");
            Console.WriteLine("8.\tFırsatçı\t\tKopyacı stratejisini uygular ama arada bir sürpriz False hamlesi yapar.");
            Console.WriteLine("9.\tGrupçu\t\t\tBaşlangıç True. Sonraki hamleleri, oyun boyunca yapılmış tüm hamlelerin göre belirler.");
            Console.WriteLine("10.\tİntikamcı\t\tSon turda kazandıysa aynı eylemi tekrarlar, kaybettiyse hamlesini değiştirir (Pavlov).");
        }



        public static void bilgiAl()
        {
            string[] CharacterOzellik = { "(1)Kopyacı\t\tTit-for-Tat (Kısasa Kısas)\t\nBaşlangıçta nazik davranır (işbirliği yapar). Daha sonra, rakibin hemen önceki hamlesini kopyalar. (İyiliğe iyilik, kötülüğe kötülük)\n\n", "(2)Ponçik\t\tAlways Cooperate (Her Zaman İşbirliği)\t\nRakip ne yaparsa yapsın (sömürse bile) her turda istisnasız işbirliği (C) yapmaya devam eder.\n\n", "(3)Sinsi\t\tAlways Defect (Her Zaman İhanet)\t\nDiğer oyuncunun eylemlerini umursamaz ve her turda sadece kendi çıkarı için ihanet (D) eder.\n\n", "(4)Hain Hafıza\t\tGrim Trigger (Tetik Stratejisi)\t\nBaşlangıçta işbirliği yapar. Rakip bir kere bile ihanet ederse, bu olayı kaydeder ve oyun bitene kadar sürekli ihanet (D) eder.\n\n", "(5)Şanslı Cimbom\tRandom (Rastgele)\t\nGeçmişi dikkate almaz. Her turda kararlarını yarı yarıya şansa (yazı tura) bırakır.\n\n", "(6)Affetmez Ayna\tTit-for-Tat with Forgiveness\t\nKopyacı gibidir, ancak nadiren (küçük bir olasılıkla) rakibin ihanetini göz ardı edip işbirliğine geri döner (affeder).\n\n", "(7)Sömürücü\t\tTester / Explorer\t\nOyunun başında (ilk birkaç tur) bilerek ihanet (D) ederek rakibi test eder. Rakip tepki vermezse sömürmeye devam eder.\n\n", "(8)Fırsatçı\t\tJoss\t\nKopyacı stratejisini uygularken, arada bir (az bir olasılıkla) rakibi şaşırtmak için fazladan ihanet (D) hamlesi yapar.\n\n", "(9)Grupçu\t\tSimple Majority\t\nOyun boyunca en çok yapılan hamle neyse (işbirliği ya da ihanet), bir sonraki turda o çoğunluğa uyar.\n\n", "(10)İntikamcı\t\tPavlov\t\nKazanmayı ödüllendirir, kaybetmeyi cezalandırır. Son turda kazandıysa aynı eylemi tekrar eder, kaybettiyse değiştirir.\n\n" };
            Console.WriteLine("Bilgi almak için karakterinizi seçin \n (1)  Kopyacı \n (2)  Ponçik\n (3)  Sinsi\n (4)  Hain Hafıza \n (5)  Şanslı Cimbom\n (6)  Affetmez Ayna \n (7)  Sömürücü \n (8)  Fırsatçı \n (9)  Grupçu \n (10) İntikamcı \n hepsini görmek için hepsi yaz ");
            string CharSelect1 = Console.ReadLine().ToString();

            switch (CharSelect1)
            {
                case "1":
                    Console.WriteLine(CharacterOzellik[0] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision1 = Console.ReadLine().ToString();
                    switch (decision1)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "2":
                    Console.WriteLine(CharacterOzellik[1] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision2 = Console.ReadLine().ToString();
                    switch (decision2)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "3":
                    Console.WriteLine(CharacterOzellik[2] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision3 = Console.ReadLine().ToString();
                    switch (decision3)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "4":
                    Console.WriteLine(CharacterOzellik[3] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision4 = Console.ReadLine().ToString();
                    switch (decision4)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "5":
                    Console.WriteLine(CharacterOzellik[4] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision5 = Console.ReadLine().ToString();
                    switch (decision5)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "6":
                    Console.WriteLine(CharacterOzellik[5] + "\n\nbaska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision6 = Console.ReadLine().ToString();
                    switch (decision6)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "7":
                    Console.WriteLine(CharacterOzellik[6] + "\n\nbaska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision7 = Console.ReadLine().ToString();
                    switch (decision7)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "8":
                    Console.WriteLine(CharacterOzellik[7] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision8 = Console.ReadLine().ToString();
                    switch (decision8)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "9":
                    Console.WriteLine(CharacterOzellik[8] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision9 = Console.ReadLine().ToString();
                    switch (decision9)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "10":
                    Console.WriteLine(CharacterOzellik[9] + "\n\n baska karakterler görmek için 1 e bas devam etmek herhangi bir tuşa bas");
                    string decision10 = Console.ReadLine().ToString();
                    switch (decision10)
                    {
                        case "1": bilgiAl(); break;
                        default: Console.WriteLine("karakterinizi seçin"); break;
                    }
                    break;
                case "hepsi":
                    Console.WriteLine("\n\n------------------------------------------------------------------------------------------------------------------------");
                    foreach (string karakterler in CharacterOzellik)
                        Console.WriteLine($"{karakterler}");
                    break;
                default:
                    Console.WriteLine("yanlıs karakter seçimi tekrar karakter girin");
                    bilgiAl();
                    break;

            }

            CharSelect(); 



        }


        public static void CharSelect()
        {
            Console.WriteLine("(1)  Kopyacı \n (2)  Ponçik\n (3)  Sinsi\n (4)  Hain Hafıza \n (5)  Şanslı Cimbom\n (6)  Affetmez Ayna \n (7)  Sömürücü \n (8)  Fırsatçı \n (9)  Grupçu \n (10) İntikamcı ");
            Console.WriteLine("Karakter A ve ardınadan B yi seçmek için 10a kadar olan numaralardan ikisini seçin");
            string[] heroInfo = { "(1)  Kopyacı \n", "(2)  Ponçik\n ", "(3)  Sinsi\n ", "(4)  Hain Hafıza \n ", "(5)  Şanslı Cimbom\n ", "(6)  Affetmez Ayna \n ", "(7)  Sömürücü \n ", "(8)  Fırsatçı \n", " (9)  Grupçu \n", " (10) İntikamcı \n" };
            string select1, select2 = null;
            select1 = Console.ReadLine();
            if (select1 == "0") { bilgiAl(); }
            Console.WriteLine($"seçtiğiniz 1. heronun numarası ve ismi{heroInfo[Convert.ToInt16(select1) - 1]}");
            
            
            select2 = Console.ReadLine();
            if (select2 == "0") { bilgiAl(); }
            Console.WriteLine($"seçtiğiniz 2. heronun numarası ve ismi{heroInfo[Convert.ToInt16(select2) - 1]}");


            Console.WriteLine("eğer yanlış karakter seçtiyseniz 0 a basın");
            string Iptal = Console.ReadLine();
            if (Iptal == "0") { CharSelect(); }

            Game(select1, select2);



        }



        public static void Game(string select1, string select2)
        {
            Console.WriteLine("oyun sayısını seçin");
            string oyunSayisi = Console.ReadLine();
            bool[] strategyA = new bool[Convert.ToInt16(oyunSayisi)];
            bool[] strategyB = new bool[Convert.ToInt16(oyunSayisi)];
            if (int.TryParse(oyunSayisi, out int sayisalDeger))
            {
                // 2. Çevrim başarılıysa, kontrolü yapın
                //sayisal değer 0dan küçükse kontrolünü yapar
                if (sayisalDeger <= 0)
                {
                    Game(select1, select2);
                    Console.WriteLine("lütfen oyun sayısını 0dan büyük bir sayı girin ");
                }


            }
            // bu sayı değilse okunacak
            else
            {
                Game(select1, select2);
                Console.WriteLine("lütfen oyun sayısını 0dan büyük bir sayı girin ");
            }


            // buraya kadar karakterleri seçtik oyun sayısını seçtik 
            //geriye hangi karakter nasıl hareket eder onu kodlamalıyız peki bunu nasıl yapacağız 
            // önce selec1 herosu sonra select2 herosu hareket edecek sonra kim kazanmıs onu kontrol edicez 
            //tabi bu sırada strateji a ve strateji b nin hareketleri bool dizisinde saklanıyor kimin kazandığını if yapısı ile buradan belirleyeceğiz
            //// bunu for döngüsü ile yapabiliriz 
            //sıra diğer oyunlarda bu sefer hepsi karşıdakinin 1 önceki hamlesine göre hareket edeccek
            //önce select1 sonra select2 sonra kazananı kontrol edicez
            //i == oyunSayisi olduğunda kazanan listesinden kontrol edicez kim kaç kez kazanmış onu öğrencez 

            Vs(select1, select2, strategyA, strategyB);

        }

        //ÖNCE KENDİ STRATEJİN SONRA RAKİBİN STRATEJİSİ SONRA HANGİ TURDA OLDUĞUN YAZILACAK 

        //önce iyilik sonra karşıdakinin bi önceki hamlesi 
        public static void kopyaci(bool[] strategyA, bool[] strategyB, int i)
        {
            if (i == 0)
            {
                strategyA[0] = true;

            }

            else if (i > 0)
            {
                if (strategyB[i - 1] == true) { strategyA[i] = true; }
                else if (strategyB[i - 1] == false) { strategyA[i] = false; }
            }

        }

        //hep true
        public static void poncik(bool[] strategyA, bool[] strategyB, int i)
        {
            if (i == 0)
            { strategyA[0] = true; }
            else if (i > 0)
            { strategyA[i] = true; }
        }

        //hep false
        public static void sinsi(bool[] strategyA, bool[] strategyB, int i)
        {
            if (i == 0)
            { strategyA[0] = false; }
            else if (i > 0)
            { strategyA[i] = false; }
        }

        //başlangıçta iyi sonra rakip bir kere false yaparsa hep false 
        public static void hainHafiza(bool[] strategyA, bool[] strategyB, int i)
        {
            if (i == 0)
            { strategyA[0] = true; }

            else if (i > 0)
            {
                if (strategyB[i - 1] == true) { strategyA[i] = true; }
                else if (strategyB[i - 1] == false) { strategyA[i] = false; }

                if (strategyA[i - 1] == false) { strategyA[i] = false; }
            }

        }

        //random 
        public static void sansliCimbom(bool[] strategyA, bool[] strategyB, int i)
        {
            Random rnd = new Random();
            int sonuc = rnd.Next(0, 2);

            if (sonuc == 0) { strategyA[i] = true; }
            else if (sonuc == 1) { strategyA[i] = false; }

        }

        //başlangıç iyi sonra karşıdakinin bir önceki hamlesi ama nadiren affeder
        public static void afetmezAyna(bool[] strategyA, bool[] strategyB, int i)
        {
            Random rnd = new Random();
            int SONUC = rnd.Next(1, 6);

            if (i == 0) { strategyA[i] = true; }
            else if (i > 0)
            {
                if (strategyB[i - 1] == true) { strategyA[i] = true; }
                else if (strategyB[i - 1] == false)
                {
                    if (SONUC < 5) { strategyA[i] = false; }
                    else if (SONUC == 5) { strategyA[i] = true; }

                }
            }
        }

        //başlangıç false sonra rakip tepki vermezse false devam  tepki verirse kopyacı olur 
        public static void somurucu(bool[] strategyA, bool[] strategyB, int i)
        {
            if (i == 0) { strategyA[i] = false; }
            else if (i > 0)
            {
                if (strategyB[i - 1] == true) { strategyA[i] = false; }
                else if (strategyB[i - 1] == false) { strategyA[i] = strategyB[i - 1]; }
            }
        }

        //başlangıç true (kopyacı) ama arada bir ihanet (düşük olasılık)  %20 
        public static void firsatci(bool[] strategyA, bool[] strategyB, int i)
        {
            Random rnd = new Random();
            int sonuc = rnd.Next(1, 6);
            if (i == 0) { strategyA[i] = true; }
            else if (i > 0)
            {
                if (strategyB[i] == true)
                {
                    if (sonuc < 5) { strategyA[i] = true; }
                    else if (sonuc > 5) { strategyA[i] = false; }
                }

                else if (strategyB[i] == false)
                {
                    strategyA[i] = strategyB[i - 1];
                }
            }

        }

        //başlangıç true sonra oyun boyunca en çok yapılan hamle neyse o 
        public static void grupcu(bool[] strategyA, bool[] strategyB, int i)
        {
            List<bool> result = new List<bool>();


            if (i == 0) { strategyA[i] = true; result.Add(strategyA[i]); }


            else if (i > 0)
            {
                result.Add(strategyA[i]);
                result.Add(strategyB[i]);

                int trueSayisi = result.Count(b => b == true);
                int falseSayisi = result.Count - trueSayisi;

                if (trueSayisi > falseSayisi) { strategyA[i] = true; }
                else if (trueSayisi < falseSayisi) { strategyA[i] = false; }
            }


        }


        //baslangıç true son turda kazandıysa aynı eylemi tekrar eder kaybettiyse değiştirir
        public static void intikamci(bool[] strategyA, bool[] strategyB, int i)
        {
            if (i == 0)
            { strategyA[i] = true; }
            else if (i > 0)
            {
                if (strategyB[i] == true) { strategyA[i] = strategyA[i - 1]; }
                else if (strategyB[i] == false) { strategyA[i] = !strategyA[i - 1]; }
            }
        }



        public static void Vs(string select1, string select2, bool[] strategyA, bool[] strategyB)
        {
            for (int i = 0; i < strategyA.Length; i++)
            {
                /*hainHafiza(strategyA, strategyB, i);
                sansliCimbom(strategyB, strategyA, i);
                if (strategyA[i] == true && strategyB[i] == true) { Console.WriteLine("true , true => +2 , +2 "); }
                else if (strategyA[i] == true && strategyB[i] == false) { Console.WriteLine("true , false => -1 , +3 "); }
                else if (strategyA[i] == false && strategyB[i] == true) { Console.WriteLine("false , true => +3 , -1 "); }
                else if (strategyA[i] == false && strategyB[i] == false) { Console.WriteLine("false , false => 0 , 0 "); }

                Console.WriteLine("\n");

                fonksiyonlarımız 
                //intikamci grupcu firsatci somurucu affetmezAyna sansliCimbom hainHafiza sinsi poncik kopyacı
                */

                //şimdi select1 ve select2 miz var bunlara göre 1 ise şu fonksiyon 2 ise şu fonksiyon çalışsın diye bir switchCase yapısı yapmalıyız
                switch (select1)
                {
                    case "1":
                        kopyaci(strategyA, strategyB, i);
                        break;
                    case "2":
                        poncik(strategyA, strategyB, i);
                        break;
                    case "3":
                        sinsi(strategyA, strategyB, i);
                        break;
                    case "4":
                        hainHafiza(strategyA, strategyB, i);
                        break;
                    case "5":
                        sansliCimbom(strategyA, strategyB, i);
                        break;
                    case "6":
                        afetmezAyna(strategyA, strategyB, i);
                        break;
                    case "7":
                        somurucu(strategyA, strategyB, i);
                        break;
                    case "8":
                        firsatci(strategyA, strategyB, i);
                        break;
                    case "9":
                        grupcu(strategyA, strategyB, i);
                        break;
                    case "10":
                        intikamci(strategyA, strategyB, i);
                        break;
                    case "0":
                        bilgiAl();
                        break;
                }

                switch (select2)
                {
                    case "1":
                        kopyaci(strategyB, strategyA, i);
                        break;
                    case "2":
                        poncik(strategyB, strategyA, i);
                        break;
                    case "3":
                        sinsi(strategyB, strategyA, i);
                        break;
                    case "4":
                        hainHafiza(strategyB, strategyA, i);
                        break;
                    case "5":
                        sansliCimbom(strategyB, strategyA, i);
                        break;
                    case "6":
                        afetmezAyna(strategyB, strategyA, i);
                        break;
                    case "7":
                        somurucu(strategyB, strategyA, i);
                        break;
                    case "8":
                        firsatci(strategyB, strategyA, i);
                        break;
                    case "9":
                        grupcu(strategyB, strategyA, i);
                        break;
                    case "10":
                        intikamci(strategyB, strategyA, i);
                        break;
                    case "0":
                        bilgiAl();
                        break; 
                }

                if (strategyA[i] == true && strategyB[i] == true) { Console.WriteLine("true , true => +2 , +2 "); }
                else if (strategyA[i] == true && strategyB[i] == false) { Console.WriteLine("true , false => -1 , +3 "); }
                else if (strategyA[i] == false && strategyB[i] == true) { Console.WriteLine("false , true => +3 , -1 "); }
                else if (strategyA[i] == false && strategyB[i] == false) { Console.WriteLine("false , false => 0 , 0 "); }


            }

            
                CharSelect();


            


        }

    }
}


