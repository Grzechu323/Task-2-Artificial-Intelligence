using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaneZPliku
{
    public class Regula2
    {
        public Dictionary<int, string> deskryptory = new Dictionary<int, string>();
        public string decyzja;
        public int support;

        public Regula2(string[] ob, int[] kombinacje)
        {

            this.decyzja = ob.Last();
            for (int i = 0; i < kombinacje.Length; i++)
            {
                int nrAtrybutu = kombinacje[i];
                string wartoscAtrybutu = ob[nrAtrybutu];
                this.deskryptory.Add(nrAtrybutu, wartoscAtrybutu);
            }

        }
        public override string ToString()
        {
            string wynik = string.Empty;
            string r = "";
            if (deskryptory.Count != 1)
            {
                for (int i = 0; i < deskryptory.Count; i++)
                {
                    int nrAtr = deskryptory.Keys.ElementAt(i) + 1;
                    string wartAtr = deskryptory.Values.ElementAt(i);

                    r += "(a" + nrAtr + "=" + wartAtr + ")";
                    if (!(i == deskryptory.Count - 1))
                    {
                        r += "/" + @"\";
                    }
                    if (i == deskryptory.Count - 1)
                    {
                        r += "=>(d=" + decyzja + ")";
                    }
                }
                if (support > 1)
                    wynik += r + "[" + support + "]" + "\r\n";
                else
                    wynik += r + "\r\n";
                r = "";
            }
            else
            {
                foreach (var desk in deskryptory)
                {
                    int nrAtr = desk.Key + 1;
                    string wartAtr = desk.Value;
                    if (support > 1)
                        wynik += "(a" + nrAtr + "=" + wartAtr + ")" + "=>(d=" + decyzja + ")" + "[" + support + "]" + "\r\n";
                    else
                        wynik += "(a" + nrAtr + "=" + wartAtr + ")" + "=>(d=" + decyzja + ")" + "\r\n";
                }
            }

            return wynik;
        }

        public bool czyRegulaZawieraRegule(Regula2 r)
        {
            foreach (var desk in r.deskryptory)
            {
                if (!this.deskryptory.ContainsKey(desk.Key) || this.deskryptory[desk.Key] != desk.Value)
                    return false;
            }
            return true;
        }

        public bool czyRegulaZawieraReguleZListy(List<Regula2> reguly)
        {
            foreach (var reg in reguly)
            {
                if (czyRegulaZawieraRegule(reg))
                    return true;
            }
            return false;
        }



        public bool czyObiektSpelniaRegule(string[] obiekt)
        {
            foreach (var deskryptor in this.deskryptory)
            {
                if (obiekt[deskryptor.Key] != deskryptor.Value)
                    return false;
            }
            return true;
        }
        public int SupportReguly(string[][] obiekty)
        {
            support = 0;
            foreach (var ob in obiekty)
            {
                if (czyObiektSpelniaRegule(ob))
                    support++;
            }
            return support;
        }

    }
}
