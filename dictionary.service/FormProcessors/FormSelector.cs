using System.Collections.Generic;
using Dictionary.Core.Models;
using System.Linq;

namespace Dictionary.Service.FormProcessors
{
    internal static class FormSelector
    {
        //wybranie ostatecznej formy
        public static string Word(this IEnumerable<Form> forms) => forms.FirstOrDefault() != null ? forms.First().Word : "";

        //rodzaj
        public static IEnumerable<Form> M1(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("m1"));
        public static IEnumerable<Form> M2(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("m2"));
        public static IEnumerable<Form> M3(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("m3"));
        public static IEnumerable<Form> N(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("n"));
        public static IEnumerable<Form> F(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("f"));


        //osoba
        public static IEnumerable<Form> Pri(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pri"));
        public static IEnumerable<Form> Sec(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("sec"));
        public static IEnumerable<Form> Ter(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("ter"));


        //liczba
        public static IEnumerable<Form> Sg(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("sg"));
        public static IEnumerable<Form> Pl(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pl"));


        //przypadek
        public static IEnumerable<Form> Nom(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("nom"));
        public static IEnumerable<Form> Gen(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("gen"));
        public static IEnumerable<Form> Dat(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("dat"));
        public static IEnumerable<Form> Acc(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("acc"));
        public static IEnumerable<Form> Ins(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("inst"));
        public static IEnumerable<Form> Loc(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("loc"));
        public static IEnumerable<Form> Voc(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("voc"));


        //tryb
        public static IEnumerable<Form> Ind(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("fin") || x.Categories.Contains("praet"));
        public static IEnumerable<Form> CondNonPast(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("cond") && !x.Categories.Contains("praet"));
        public static IEnumerable<Form> CondPast(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("cond") && x.Categories.Contains("praet"));
        public static IEnumerable<Form> Imperat(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("impt"));


        //czas
        public static IEnumerable<Form> Pres(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("fin"));
        public static IEnumerable<Form> Past(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("praet"));

        public static IEnumerable<Form> Plusquamperf(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("plus"));
        public static IEnumerable<Form> Fut(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("bedzie") || x.Categories.Contains("fut"));


        //stopień
        public static IEnumerable<Form> Posit(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pos"));
        public static IEnumerable<Form> Compar(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("com"));
        public static IEnumerable<Form> Super(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("sup"));



        //formy czasownikowe
        public static IEnumerable<Form> Gerund(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("ger")); //odsłownik
        public static IEnumerable<Form> Inf(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("inf")); //bezokolicznik
        public static IEnumerable<Form> Impers(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("imps")); //bezosobnik
        public static IEnumerable<Form> ParticAct(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pact")); //imiesłów czynny
        public static IEnumerable<Form> ParticPas(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("ppas")); //imiesłów bierny
        public static IEnumerable<Form> ParticCon(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pcon")); //imiesłów współczesny
        public static IEnumerable<Form> ParticAnt(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pant")); //imiesłów uprzedni


        //derywaty i formy szczególne
        public static IEnumerable<Form> Depr(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("depr"));
        public static IEnumerable<Form> AdjBAdv(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("adja")); //przysłówek odprzymiotnikowy
        public static IEnumerable<Form> Pewien(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("adjc")); //przymiotniki typu pewien, rówien
        public static IEnumerable<Form> Polsku(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("adjp")); //formy typu polsku, staremu
        public static IEnumerable<Form> Accented(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("akc")); //forma akcentowana
        public static IEnumerable<Form> Unaccented(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("nakc")); //forma nieakcentowana
        public static IEnumerable<Form> NotNeg(this IEnumerable<Form> forms) => forms.Where(x => !x.Categories.Contains("neg")); //forma niezanegowana
        public static IEnumerable<Form> Neg(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("neg")); //forma zanegowana
        public static IEnumerable<Form> Vocal(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("wok")); //forma wokaliczna
        public static IEnumerable<Form> NVocal(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("nwok")); //forma niewokaliczna
        public static IEnumerable<Form> Prep(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("praep")); //forma poprzyimkowa
        public static IEnumerable<Form> NPrep(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("npraep")); //forma niepoprzyimkowa
        public static IEnumerable<Form> PartBADv(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("pacta")); //przysłówek odimiesłowowy
        public static IEnumerable<Form> Agl(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("agl")); //forma aglutynacyjna
        public static IEnumerable<Form> NAgl(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("nagl")); //forma nieaglutynacyjna

        public static IEnumerable<Form> Agglutinate(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("aglt")); //aglutynat (końcówka ruchoma)

        public static IEnumerable<Form> Col(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("col")); //forma kolektywna
        public static IEnumerable<Form> NCol(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("ncol")); //forma niekolektywna
        public static IEnumerable<Form> Congr(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("congr")); //forma kongruentna
        public static IEnumerable<Form> Rec(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("rec")); //forma niekongruentna
        public static IEnumerable<Form> Aff(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("aff")); //forma z opcjonalnym afiksem (postfiksem)
        public static IEnumerable<Form> Numcomp(this IEnumerable<Form> forms) => forms.Where(x => x.Categories.Contains("numcomp")); //cząstka liczebnikó złożonych





    }
}