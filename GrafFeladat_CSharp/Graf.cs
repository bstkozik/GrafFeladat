﻿using System;
using System.Collections.Generic;

namespace GrafFeladat_CSharp
{
    /// <summary>
    /// Irányítatlan, egyszeres gráf.
    /// </summary>
    class Graf
    {
        int csucsokSzama;
        /// <summary>
        /// A gráf élei.
        /// Ha a lista tartalmaz egy(A, B) élt, akkor tartalmaznia kell
        /// a(B, A) vissza irányú élt is.
        /// </summary>
        readonly List<El> elek = new List<El>();
        /// <summary>
        /// A gráf csúcsai.
        /// A gráf létrehozása után új csúcsot nem lehet felvenni.
        /// </summary>
        readonly List<Csucs> csucsok = new List<Csucs>();

        /// <summary>
        /// Létehoz egy úgy, N pontú gráfot, élek nélkül.
        /// </summary>
        /// <param name="csucsok">A gráf csúcsainak száma</param>
        public Graf(int csucsok)
        {
            this.csucsokSzama = csucsok;

            // Minden csúcsnak hozzunk létre egy új objektumot
            for (int i = 0; i < csucsok; i++)
            {
                this.csucsok.Add(new Csucs(i));
            }
        }

        /// <summary>
        /// Hozzáad egy új élt a gráfhoz.
        /// Mindkét csúcsnak érvényesnek kell lennie:
        /// 0 &lt;= cs &lt; csúcsok száma.
        /// </summary>
        /// <param name="cs1">Az él egyik pontja</param>
        /// <param name="cs2">Az él másik pontja</param>
        public void Hozzaad(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha már szerepel az él, akkor nem kell felvenni
            foreach (var el in elek)
            {
                if (el.Csucs1 == cs1 && el.Csucs2 == cs2)
                {
                    return;
                }
            }

            elek.Add(new El(cs1, cs2));
            elek.Add(new El(cs2, cs1));
        }
        public void Torol(int cs1, int cs2)
        {
            if (cs1 < 0 || cs1 >= csucsokSzama ||
                cs2 < 0 || cs2 >= csucsokSzama)
            {
                throw new ArgumentOutOfRangeException("Hibas csucs index");
            }

            // Ha találunk ilyen élt. akkor töröljük
            for (int i = 0; i < elek.Count; i++)
            {
                if ((elek[i].Csucs1==cs1 && elek[i].Csucs2==cs2))
                {
                    elek.RemoveAt(i);
                }
            }
            for (int i = 0; i < elek.Count; i++)
            {
                if ((elek[i].Csucs1 == cs2 && elek[i].Csucs2 == cs1))
                {
                    elek.RemoveAt(i);
                }
            }
        }

        public void SzellesegiBejar()
        {
            Random rand = new Random();
            int kezdopont = rand.Next(0, this.csucsok.Count);

            Console.WriteLine("Szélességi bejárás:");
            Console.WriteLine("Kezdőpont: {0}",kezdopont);

            List<int> bejart = new List<int>();
            List<int> kovetkezok = new List<int>();
            bejart.Add(kezdopont);
            kovetkezok.Add(kezdopont);

            int k = 0;
            while (kovetkezok.Count!=0)
            {
                k = kovetkezok[0];
                kovetkezok.RemoveAt(0);
                Console.WriteLine(this.csucsok[k].ToString());

                for (int j = 0; j < this.elek.Count; j++)
                {
                    if (this.elek[j].Csucs1 == k && !(bejart.Contains(this.elek[j].Csucs2)))
                    {
                        kovetkezok.Add(this.elek[j].Csucs2);
                        bejart.Add(this.elek[j].Csucs2);
                    }
                }
            }


        }

        public override string ToString()
        {
            string str = "Csucsok:\n";
            foreach (var cs in csucsok)
            {
                str += cs + "\n";
            }
            str += "Elek:\n";
            foreach (var el in elek)
            {
                str += el + "\n";
            }
            return str;
        }
    }
}