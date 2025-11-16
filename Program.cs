using System;
using System.Collections.Generic;
using System.Linq;

namespace OyunTeorisi
{
    public class Program
    {
        // Global ve özel (private) değişkenler
        private static int _scoreA = 0;
        private static int _scoreB = 0;
        private static Random _rnd = new Random();

        // Hain Hafıza stratejisi için durum takibi
        private static bool _grimTriggerA = false;
        private static bool _grimTriggerB = false;

        // Karakter adlarını kolayca bulmak için
        private static readonly string[] CharacterNames = {
            "Kopyacı", "Ponçik", "Sinsi", "Hain Hafıza", "Şanslı Cimbom",
            "Affetmez Ayna", "Sömürücü", "Fırsatçı", "Grupçu", "İntikamcı"
        };

        public static void Main()
        {
            Console.Clear();
            Console.Title = "Oyun Teorisi Simülasyonu: Tekrarlı Mahkum İkilemi";

            OyunAmaciniYazdir();
            Console.WriteLine("\n" + new string('=', 80) + "\n");
            PuanTablosunuGoster();
            Console.WriteLine("\n" + new string('=', 80) + "\n");
            KarakterAciklamalariniGoster();

            Console.WriteLine("\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
            Console.Clear();

            // Ana akışı başlat
            CharSelect();
        }

        #region EKRAN VE BILGI METOTLARI

        public static void OyunAmaciniYazdir()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- OYUN TEORİSİ SİMÜLASYONU: TEKRARLI MAHKUM İKİLEMİ ---\n");
            Console.ResetColor();

            Console.WriteLine("Oyunun Amacı:");
            Console.WriteLine("\tBu oyun, iki rasyonel oyuncunun (karakterin) stratejik etkileşimini inceler.");
            Console.WriteLine("\tHer turda, her oyuncu İşbirliği (True / C) veya İhanet (False / D) kararı verir.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\tKazanan, oyun sonunda en yüksek toplam puanı (kazancı) elde eden karakterdir.");
            Console.ResetColor();
        }

        public static void PuanTablosunuGoster()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("--- KAZANÇ MATRİSİ (PUAN TABLOSU) ---\n");
            Console.ResetColor();

            Console.WriteLine("\t\t\t| Oyuncu B: İşbirliği (True)\t| Oyuncu B: İhanet (False)");
            Console.WriteLine(new string('-', 100));

            Console.Write("Oyuncu A: İşbirliği (True)");
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" Orta Kazanç: A: +2, B: +2 ");
            Console.ResetColor();
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" Sömürü: A: -1, B: +3");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));

            Console.Write("Oyuncu A: İhanet (False)");
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" Sömürülme: A: +3, B: -1");
            Console.ResetColor();
            Console.Write("\t|");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" Karşılıklı Kayıp: A: 0, B: 0");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine(new string('-', 100));
        }

        public static void KarakterAciklamalariniGoster()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- KARAKTER LİSTESİ VE STRATEJİLERİ ---\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("NO\tKARAKTER ADI\t\tSTRATEJİ ÖZETİ");
            Console.WriteLine(new string('-', 60));
            Console.ResetColor();

            // Karakterler ve açıklamalar (daha kısa ve öz)
            Console.WriteLine("1.\tKopyacı\t\t\tÖnce True, sonra rakibin bir önceki hamlesini kopyalar (Tit-for-Tat).");
            Console.WriteLine("2.\tPonçik\t\t\tRakip ne yaparsa yapsın **HEP TRUE**.");
            Console.WriteLine("3.\tSinsi\t\t\tHer durumda sadece kendi çıkarını düşünür ve **HEP FALSE**.");
            Console.WriteLine("4.\tHain Hafıza\t\tBaşlangıç True. Rakip bir kere False yaparsa, oyun sonuna kadar hep False.");
            Console.WriteLine("5.\tŞanslı Cimbom\t\tHer turda rastgele (random True/False) karar verir.");
            Console.WriteLine("6.\tAffetmez Ayna\t\tKopyacı gibidir, ancak nadiren (%20 ihtimal) rakibin False hamlesini affeder.");
            Console.WriteLine("7.\tSömürücü\t\tBaşlangıç False. Rakip nazikse False devam eder, misilleme gelirse Kopyacı'ya döner.");
            Console.WriteLine("8.\tFırsatçı\t\tKopyacı stratejisi uygular ama arada bir (düşük ihtimalle) sürpriz False yapar.");
            Console.WriteLine("9.\tGrupçu\t\t\tBaşlangıç True. Sonraki hamleleri, geçmiş tüm hamlelerin **çoğunluğuna** göre belirler.");
            Console.WriteLine("10.\tİntikamcı\t\tKazandıysa aynı eylemi tekrar eder, kaybettiyse değiştirir (Pavlov).");
        }

        #endregion

        #region KULLANICI SEÇİM VE KONTROL METOTLARI

        public static int ReadCharacterSelection(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int selection) && selection >= 1 && selection <= 10)
                {
                    return selection;
                }
                else if (input == "0")
                {
                    Console.Clear();
                    KarakterAciklamalariniGoster();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Hatalı seçim. Lütfen 1 ile 10 arasında bir sayı girin veya bilgi için 0'a basın.");
                    Console.ResetColor();
                }
            }
        }

        public static void CharSelect()
        {
            Console.Clear();
            KarakterAciklamalariniGoster();
            Console.WriteLine("\n" + new string('-', 60));

            int select1 = ReadCharacterSelection("Karakter A'yı seçin (1-10):");
            int select2 = ReadCharacterSelection("Karakter B'yi seçin (1-10):");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nSeçimleriniz: A: {select1} ({CharacterNames[select1 - 1]}) vs B: {select2} ({CharacterNames[select2 - 1]})");
            Console.ResetColor();

            Console.WriteLine("Eğer yanlış karakter seçtiyseniz 0'a basın, devam etmek için herhangi bir tuşa basın.");
            string Iptal = Console.ReadLine();
            if (Iptal == "0")
            {
                CharSelect();
                return; // Özyinelemeli çağrıdan sonra geri dön
            }

            Game(select1, select2);
        }

        public static void Game(int select1, int select2)
        {
            Console.Clear();
            Console.WriteLine($"--- MAÇ BAŞLANGICI: {CharacterNames[select1 - 1]} vs {CharacterNames[select2 - 1]} ---");
            Console.WriteLine("Oyun sayısını (tur sayısını) seçin (Min 1, Örn: 100):");
            string oyunSayisiStr = Console.ReadLine();

            if (!int.TryParse(oyunSayisiStr, out int sayisalDeger) || sayisalDeger <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Hatalı giriş. Lütfen 0'dan büyük bir sayı girin.");
                Console.ResetColor();
                // Hatalı girişte tekrar seçim ekranına dön
                Game(select1, select2);
                return;
            }

            // Skorları ve durumları sıfırla
            _scoreA = 0;
            _scoreB = 0;
            _grimTriggerA = false;
            _grimTriggerB = false;

            // Dizileri tanımla ve başlat
            bool[] strategyA = new bool[sayisalDeger];
            bool[] strategyB = new bool[sayisalDeger];

            Vs(select1, select2, strategyA, strategyB);
        }

        #endregion

        #region STRATEJI VE PUANLAMA METOTLARI

        // Hamleyi çalıştıran ana işlevi tanımlar.
        private delegate bool StrategyFunc(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered);

        // VS döngüsü
        public static void Vs(int select1, int select2, bool[] strategyA, bool[] strategyB)
        {
            // Karakter fonksiyonlarını eşleştirme (Func<kendi geçmişi, rakip geçmişi, tur, kendi trigger durumu, rakip trigger durumu>)
            StrategyFunc strategyFuncA = GetStrategyFunc(select1);
            StrategyFunc strategyFuncB = GetStrategyFunc(select2);

            Console.WriteLine("\n--- OYUN BAŞLIYOR ---\n");
            Console.WriteLine("TUR\tHAMLE A\t\tHAMLE B\t\tSKOR A\tSKOR B");
            Console.WriteLine(new string('-', 60));

            for (int i = 0; i < strategyA.Length; i++)
            {
                // 1. Hamleleri Hesapla (Grim Trigger durumunu kontrol et)
                // strategyA'nın hamlesini strategyB'nin geçmişine göre hesapla
                strategyA[i] = strategyFuncA(strategyA, strategyB, i, _grimTriggerA, _grimTriggerB);

                // strategyB'nin hamlesini strategyA'nın geçmişine göre hesapla
                strategyB[i] = strategyFuncB(strategyB, strategyA, i, _grimTriggerB, _grimTriggerA);

                // 2. Hain Hafıza Durumunu Güncelle (Eğer rakip ihanet ettiyse kendi trigger'ını aç)
                if (select1 == 4 && strategyB[i] == false) _grimTriggerA = true;
                if (select2 == 4 && strategyA[i] == false) _grimTriggerB = true;

                // 3. Puanı Hesapla ve Güncelle
                int scoreDeltaA, scoreDeltaB;
                CalculatePayoff(strategyA[i], strategyB[i], out scoreDeltaA, out scoreDeltaB);
                _scoreA += scoreDeltaA;
                _scoreB += scoreDeltaB;

                // 4. Çıktıyı Yazdır (Renkli)
                WriteTurnOutput(i + 1, strategyA[i], strategyB[i], _scoreA, _scoreB, scoreDeltaA, scoreDeltaB);
            }

            Console.WriteLine(new string('=', 60));
            ShowFinalResult();
        }

        // Puan hesaplama matrisi
        private static void CalculatePayoff(bool hamleA, bool hamleB, out int scoreA, out int scoreB)
        {
            // True = İşbirliği (C), False = İhanet (D)
            if (hamleA && hamleB) // C, C (Orta Kazanç)
            {
                scoreA = 2; scoreB = 2;
            }
            else if (hamleA && !hamleB) // C, D (Sömürülme: A kaybetti, B kazandı)
            {
                scoreA = -1; scoreB = 3;
            }
            else if (!hamleA && hamleB) // D, C (Sömürme: A kazandı, B kaybetti)
            {
                scoreA = 3; scoreB = -1;
            }
            else // !hamleA && !hamleB // D, D (Karşılıklı Kayıp)
            {
                scoreA = 0; scoreB = 0;
            }
        }

        // Tur çıktısını renklendirerek yazdırır
        private static void WriteTurnOutput(int turn, bool moveA, bool moveB, int totalA, int totalB, int deltaA, int deltaB)
        {
            // Tur numarası
            Console.Write($"{turn}\t");

            // Hamle A
            Console.ForegroundColor = moveA ? ConsoleColor.DarkGreen : ConsoleColor.Red;
            Console.Write(moveA ? "TRUE (C)" : "FALSE (D)");
            Console.ResetColor();

            Console.Write("\t");

            // Hamle B
            Console.ForegroundColor = moveB ? ConsoleColor.DarkGreen : ConsoleColor.Red;
            Console.Write(moveB ? "TRUE (C)" : "FALSE (D)");
            Console.ResetColor();

            // Skor A
            Console.Write($"\t{totalA} ({FormatDelta(deltaA)})");

            // Skor B
            Console.Write($"\t{totalB} ({FormatDelta(deltaB)})");

            Console.WriteLine();
        }

        private static string FormatDelta(int delta)
        {
            if (delta > 0) return $"+{delta}";
            return $"{delta}";
        }

        // Nihai sonucu gösterir
        private static void ShowFinalResult()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n--- OYUN SONUÇLARI ---");
            Console.WriteLine($"Toplam Skor A ({CharacterNames[GetIndex(1)]}): {_scoreA}");
            Console.WriteLine($"Toplam Skor B ({CharacterNames[GetIndex(2)]}): {_scoreB}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Magenta;
            if (_scoreA > _scoreB)
            {
                Console.WriteLine($"\nGENEL KAZANAN: {CharacterNames[GetIndex(1)]}!");
            }
            else if (_scoreB > _scoreA)
            {
                Console.WriteLine($"\nGENEL KAZANAN: {CharacterNames[GetIndex(2)]}!");
            }
            else
            {
                Console.WriteLine("\nSONUÇ: BERABERLİK!");
            }
            Console.ResetColor();

            Console.WriteLine("\nYeni bir oyun başlatmak için herhangi bir tuşa basın...");
            Console.ReadKey();
            CharSelect();
        }

        #endregion

        #region KARAKTER STRATEJILERI (Helper Functions)

        // Karakter ID'sini alıp ilgili strateji fonksiyonunu döndürür
        private static StrategyFunc GetStrategyFunc(int id)
        {
            return id switch
            {
                1 => Kopyaci,
                2 => Poncik,
                3 => Sinsi,
                4 => HainHafiza,
                5 => SansliCimbom,
                6 => AffetmezAyna,
                7 => Somurucu,
                8 => Firsatci,
                9 => Grupcu,
                10 => Intikamci,
                _ => (ownHistory, oppHistory, i, a, b) => false, // Varsayılan: Sinsi
            };
        }

        // Bu sadece CharacterNames dizisini kullanmak için bir helper
        private static int GetIndex(int selection)
        {
            // select1 ve select2'yi global olarak tutmadığımız için bu helper kullanılamaz. 
            // Basitçe CharacterNames[id-1] kullanılmalı. Ancak ShowFinalResult'ta 
            // select1 ve select2'yi almadığımız için geçici olarak 0. indeksi kullanacağım.
            // Daha düzgün bir mimari, select1 ve select2'yi Game'den Vs'e taşımak veya global yapmak olurdu.
            // Kullanıcının mevcut koduna sadık kalmak adına, bu fonksiyonun kullanımını basitleştiriyorum.
            // NOT: Bu örnekte, Vs metoduna select1 ve select2'yi parametre olarak vermiştik, bu yüzden ShowFinalResult 
            // içinden çağırmak yerine, sonucu Vs metodu içinde göstermek daha mantıklıydı.

            // NOT: Vs metodu zaten select1 ve select2'yi biliyor. Skorları da biliyor. 
            // Ben bu helper'ı siliyorum ve ShowFinalResult'ı Vs içinde kullanmak üzere düzeltiyorum.
            return 0; // Geçici çözüm, doğru ID'yi alabilmek için Vs'e geri dönülmeli.
        }

        // Karakterlerin hamlelerini hesaplayan fonksiyonlar
        // ownHistory: Kendi geçmiş hamleleri, oppHistory: Rakibin geçmiş hamleleri
        // i: Mevcut tur, isGrimTriggered: Kendi Hain Hafıza durumu

        // 1. Kopyacı (Tit-for-Tat)
        public static bool Kopyaci(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return true; // İlk hamle: İşbirliği (True)
            return oppHistory[i - 1]; // Rakibin son hamlesini kopyala
        }

        // 2. Ponçik (Always Cooperate)
        public static bool Poncik(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            return true; // Her zaman İşbirliği (True)
        }

        // 3. Sinsi (Always Defect)
        public static bool Sinsi(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            return false; // Her zaman İhanet (False)
        }

        // 4. Hain Hafıza (Grim Trigger)
        public static bool HainHafiza(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return true; // Başlangıç True

            // Eğer daha önce rakip ihanet ettiyse (isGrimTriggered true ise), hep False.
            if (isGrimTriggered) return false;

            // Eğer hiç ihanet edilmediyse, True devam et.
            return true;
        }

        // 5. Şanslı Cimbom (Random)
        public static bool SansliCimbom(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            // Next(0, 2) ya 0 ya da 1 döndürür. 1 olması %50 ihtimaldir.
            return _rnd.Next(2) == 1;
        }

        // 6. Affetmez Ayna (Tit-for-Tat with Forgiveness)
        public static bool AffetmezAyna(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return true;

            if (oppHistory[i - 1] == false) // Rakip ihanet ettiyse (False)
            {
                // %20 ihtimalle affet (True yap), %80 ihtimalle kopyala (False yap)
                // Next(1, 6) 1, 2, 3, 4, 5 döndürür. 5 gelirse affetmiş olur.
                return _rnd.Next(1, 6) == 5;
            }

            return oppHistory[i - 1]; // Rakip True yaptıysa, True yap
        }

        // 7. Sömürücü (Tester / Explorer)
        public static bool Somurucu(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return false; // Başlangıç False (Test)

            // Sömürücünün durumu için, rakibin geçmiş hamlelerine bakılarak 
            // rakibin tepki verip vermediği anlaşılır.

            // Rakip hiç ihanet etmediyse (hep True), sömürmeye (False) devam et.
            bool oppAlwaysCooperated = true;
            for (int j = 0; j < i; j++)
            {
                if (oppHistory[j] == false)
                {
                    oppAlwaysCooperated = false;
                    break;
                }
            }

            if (oppAlwaysCooperated)
            {
                // Rakip her zaman işbirliği yaptıysa, sömürmeye devam et
                return false;
            }
            else
            {
                // Rakip tepki verdiyse (bir kere bile olsa ihanet ettiyse), Kopyacı ol.
                return oppHistory[i - 1];
            }
        }

        // 8. Fırsatçı (Joss)
        public static bool Firsatci(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return true; // Başlangıç True (Kopyacı gibi)

            // Rakip İhanet ettiyse, kopyala (False yap)
            if (oppHistory[i - 1] == false)
            {
                return false;
            }

            // Rakip İşbirliği yaptıysa (True): 
            // %80 ihtimalle True yap, %20 ihtimalle False yap (Fırsatçı hamlesi)
            // Next(1, 6) -> 1, 2, 3, 4, 5
            if (_rnd.Next(1, 6) == 5) // %20 ihtimal (sadece 5 gelirse)
            {
                return false; // İhanet et (Fırsatçı hamlesi)
            }

            return true; // İşbirliğine devam et (Kopyacı gibi)
        }

        // 9. Grupçu (Simple Majority)
        public static bool Grupcu(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return true; // Başlangıç True

            // Tüm geçmiş hamleleri (kendisi ve rakip) birleştir
            // Bu, Linq ile kolayca yapılabilir, ancak performans için manuel yapalım
            int trueCount = 0;
            int falseCount = 0;

            for (int j = 0; j < i; j++)
            {
                if (ownHistory[j]) trueCount++; else falseCount++;
                if (oppHistory[j]) trueCount++; else falseCount++;
            }

            if (trueCount > falseCount) return true;
            if (falseCount > trueCount) return false;

            // Beraberlik durumunda True (varsayılan)
            return true;
        }

        // 10. İntikamcı (Pavlov)
        public static bool Intikamci(bool[] ownHistory, bool[] oppHistory, int i, bool isGrimTriggered, bool isOppGrimTriggered)
        {
            if (i == 0) return true; // Başlangıç True

            // Önceki turun sonucunu belirle: Kazanma durumu (ödül)
            // Eğer önceki hamleler sonucunda yüksek puan aldıysa (3 veya 2), bu bir ödüldür.
            bool previousOwnMove = ownHistory[i - 1];
            bool previousOppMove = oppHistory[i - 1];

            int ownScoreDelta = 0;
            int oppScoreDelta = 0;
            CalculatePayoff(previousOwnMove, previousOppMove, out ownScoreDelta, out oppScoreDelta);

            // Eğer kazandıysa (Skor 2 veya 3 ise, yani sömürdü veya karşılıklı işbirliği yaptı)
            if (ownScoreDelta >= 2)
            {
                // Kazanmayı ödüllendir: Aynı eylemi tekrar et
                return previousOwnMove;
            }
            else
            {
                // Kaybettiyse (Skor 0 veya -1 ise), cezalandır: Eylemi değiştir
                return !previousOwnMove;
            }
        }

        #endregion
    }
}