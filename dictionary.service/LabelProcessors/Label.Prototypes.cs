using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal static class LabelPrototypes
    {

        public static Entry.Label EmptyLabel => new Entry.Label
        {
            Id = 0,
            Description = "",
            Name = "",
            ValueAbbr = "",
            ValueFull = ""
        };



        public static Entry.Label ReIndex(this Entry.Label category, int newIndex)
        {
            return new Entry.Label
            {
                ValueAbbr = category.ValueAbbr,
                Description = category.Description,
                Id = newIndex,
                Name = category.Name,
                ValueFull = category.ValueFull
            };
        }



        internal static class Mood //tryb
        {
            public static Entry.Label Indicative => new Entry.Label { ValueAbbr = "ind", Id = 0, Name = "tryb", ValueFull = "oznajmujący" };
            public static Entry.Label Conditional => new Entry.Label { ValueAbbr = "cond", Id = 0, Name = "tryb", ValueFull = "przypuszczający" };
            public static Entry.Label Imperative => new Entry.Label { ValueAbbr = "impt", Id = 0, Name = "tryb", ValueFull = "rozkazujący" };
        }

        internal static class Tense //czas
        {
            //czasy
            public static Entry.Label Present => new Entry.Label { ValueAbbr = "fin", Id = 0, Name = "czas", ValueFull = "teraźniejszy" };
            public static Entry.Label Past => new Entry.Label { ValueAbbr = "praet", Id = 0, Name = "czas", ValueFull = "przeszły" };
            public static Entry.Label Future => new Entry.Label { ValueAbbr = "fut", Id = 0, Name = "czas", ValueFull = "przyszły" };
            public static Entry.Label NonPast => new Entry.Label { ValueAbbr = "nprzysz", Id = 0, Name = "czas", ValueFull = "nieprzyszły" };
            public static Entry.Label Plusquamperf => new Entry.Label { ValueAbbr = "plus", Id = 0, Name = "czas", ValueFull = "zaprzeszły" };
        }

        internal static class Person //osoba
        {
            //osoby
            public static Entry.Label First => new Entry.Label { ValueAbbr = "pri", Id = 0, Name = "osoba", ValueFull = "pierwsza" };
            public static Entry.Label Second => new Entry.Label { ValueAbbr = "sec", Id = 0, Name = "osoba", ValueFull = "druga" };
            public static Entry.Label Third => new Entry.Label { ValueAbbr = "ter", Id = 0, Name = "osoba", ValueFull = "trzecia" };
        }

        internal static class Number //liczba
        {
            //liczby
            public static Entry.Label Singular => new Entry.Label { ValueAbbr = "sg", Id = 0, Name = "liczba", ValueFull = "pojedyncza" };
            public static Entry.Label Plural => new Entry.Label { ValueAbbr = "pl", Id = 0, Name = "liczba", ValueFull = "mnoga" };
        }

        internal static class Case //przypadek
        {
            //przypadki
            public static Entry.Label Nominative => new Entry.Label { ValueAbbr = "nom", Id = 0, Name = "przypadek", ValueFull = "mianownik" };
            public static Entry.Label Genitive => new Entry.Label { ValueAbbr = "gen", Id = 0, Name = "przypadek", ValueFull = "dopełniacz" };
            public static Entry.Label Dative => new Entry.Label { ValueAbbr = "dat", Id = 0, Name = "przypadek", ValueFull = "celownik" };
            public static Entry.Label Accusative => new Entry.Label { ValueAbbr = "acc", Id = 0, Name = "przypadek", ValueFull = "biernik" };
            public static Entry.Label Instrumental => new Entry.Label { ValueAbbr = "inst", Id = 0, Name = "przypadek", ValueFull = "narzędnik" };
            public static Entry.Label Locative => new Entry.Label { ValueAbbr = "loc", Id = 0, Name = "przypadek", ValueFull = "miejscownik" };
            public static Entry.Label Vocative => new Entry.Label { ValueAbbr = "voc", Id = 0, Name = "przypadek", ValueFull = "wołacz" };
        }

        internal static class Gender //rodzaj
        {
            //rodzaje
            public static Entry.Label Masculine1 => new Entry.Label { ValueAbbr = "m1", Id = 0, Name = "rodzaj", ValueFull = "męskoosobowy" };
            public static Entry.Label Masculine2 => new Entry.Label { ValueAbbr = "m2", Id = 0, Name = "rodzaj", ValueFull = "męskozwierzęcy" };
            public static Entry.Label Masculine3 => new Entry.Label { ValueAbbr = "m3", Id = 0, Name = "rodzaj", ValueFull = "męskorzeczowy" };
            public static Entry.Label Feminine => new Entry.Label { ValueAbbr = "f", Id = 0, Name = "rodzaj", ValueFull = "żeński" };
            public static Entry.Label Neutral => new Entry.Label { ValueAbbr = "n", Id = 0, Name = "rodzaj", ValueFull = "nijaki" };
        }

        internal static class Degree //stopień
        {
            public static Entry.Label Positive => new Entry.Label { ValueAbbr = "pos", Id = 0, Name = "stopień", ValueFull = "równy" };
            public static Entry.Label Comparative => new Entry.Label { ValueAbbr = "comp", Id = 0, Name = "stopień", ValueFull = "wyższy" };
            public static Entry.Label Superlative => new Entry.Label { ValueAbbr = "sup", Id = 0, Name = "stopień", ValueFull = "najwyższy" };
        }

        internal static class Aspect //aspekt
        {
            public static Entry.Label Imperf => new Entry.Label { ValueAbbr = "imperf", Id = 0, Name = "aspekt", ValueFull = "niedokonany" };
            public static Entry.Label Perf => new Entry.Label { ValueAbbr = "perf", Id = 0, Name = "aspekt", ValueFull = "dokonany" };
        }

        internal static class Postprepositionness //poprzyimkowość
        {
            public static Entry.Label Praep => new Entry.Label { ValueAbbr = "praep", Id = 0, Name = "", ValueFull = "forma poprzyimkowa", Description = "forma stojąca zawsze po przyimku" };
            public static Entry.Label Npraep => new Entry.Label { ValueAbbr = "npraep", Id = 0, Name = "", ValueFull = "forma niepoprzyimkowa", Description = "forma nie stawiana po przyimkach" };
        }

        internal static class Accentedness //akcentowanie
        {
            public static Entry.Label Acc => new Entry.Label { Description = "forma używana pod akcentem logicznym", Id = 0, Name = "akcentowość", ValueAbbr = "akc", ValueFull = "forma akcentowana" };
            public static Entry.Label NAcc => new Entry.Label { Description = "forma nie używana pod akcentem logicznym", Id = 0, Name = "akcentowość", ValueAbbr = "nakc", ValueFull = "forma nieakcentowana" };
        }

        internal static class Agglutinativeness //aglutynacyjność
        {
            public static Entry.Label Agl => new Entry.Label { ValueAbbr = "agl", Id = 0, Name = "", ValueFull = "forma aglutynacyjna", Description = "forma łącząca się tylko z końcówką ruchomą w postpozycji" };
            public static Entry.Label Nagl => new Entry.Label { ValueAbbr = "nagl", Id = 0, Name = "", ValueFull = "forma nieaglutynacyjna", Description = "forma samodzielna (nie łączy się z innymi)" };
        }

        internal static class Punctuated //"kropkowanie"
        {
            public static Entry.Label Pun => new Entry.Label { ValueAbbr = "pun", Id = 0, Name = "", ValueFull = "skrót pisany z kropką" };
            public static Entry.Label Npun => new Entry.Label { ValueAbbr = "npun", Id = 0, Name = "", ValueFull = "skrót pisany bez kropki" };
        }

        internal static class Collectiveness //kolektywność
        {
            public static Entry.Label Collective => new Entry.Label { ValueAbbr = "col", Id = 0, Name = "kolektywność", ValueFull = "forma kolektywna", Description = "liczebnik zbiorowy lub rzeczownik w rodzaju nijakim łączący się tylko z liczebnikami zbiorowymi innymi niż pluralia tantum, np. »kocię«, »dziecko«" };
            public static Entry.Label NonCollective => new Entry.Label { ValueAbbr = "ncol", Id = 0, Name = "kolektywność", ValueFull = "forma niekolektywna", Description = "liczebnik główny lub rzeczownik w rodzaju nijakim niełączący się z liczebnikami zbiorowymi innymi niż pluralia tantum, np. »ptactwo«" };
        }

        internal static class Uniformity //uniformizm
        {
            public static Entry.Label Uniform => new Entry.Label { Description = "forma równa dopełniaczowi liczby pojedynczej", Id = 0, ValueAbbr = "hom.", ValueFull = "forma uniforemna (homonimiczna)" };
            public static Entry.Label NotUniform => new Entry.Label { Description = "forma różna od dopełniacza liczby pojedynczej", Id = 0, ValueAbbr = "char.", ValueFull = "forma nieuniforemna (charakterystyczna)" };
        }

        internal static class VerbForms //formy czasownika i odczasownikowe
        {
            public static Entry.Label BaseVerb => new Entry.Label { ValueFull = "czasownik bazowy", Id = 0 };
            public static Entry.Label Impersonal => new Entry.Label { ValueAbbr = "imps", Id = 0, Name = "", ValueFull = "bezosobnik", Description = "forma zakończona na »-no« lub »-to«" };
            public static Entry.Label Infinitive => new Entry.Label { ValueAbbr = "inf", Id = 0, Name = "", ValueFull = "bezokolicznik" };
            public static Entry.Label Gerund => new Entry.Label { ValueAbbr = "ger", Id = 0, Name = "część mowy", ValueFull = "odsłownik", Description = "rzeczownik odczasownikowy" };

            public static Entry.Label Winien => new Entry.Label { ValueAbbr = "winien", Id = 0, Name = "część mowy", ValueFull = "czasownik niewłaściwy", Description = "czasownik mający formy osobowe, odmieniany ze słowem posiłkowym »być«" };
            public static Entry.Label Predicative => new Entry.Label { ValueAbbr = "pred", Id = 0, Name = "część mowy", ValueFull = "predykatyw", Description = "czasownik bez form osobowych, odmieniany ze słowem posiłkowym »być«" };
            public static Entry.Label Agglutinative => new Entry.Label { ValueAbbr = "aglt", Id = 0, Name = "część mowy", ValueFull = "aglutynat", Description = "końcówka ruchoma czasownika »być«" };

            internal static class Participle //imiesłowy
            {
                public static Entry.Label Active => new Entry.Label { ValueAbbr = "pact", Id = 0, Name = "forma czasownika", ValueFull = "imiesłów przymiotnikowy czynny" };
                public static Entry.Label Passive => new Entry.Label { ValueAbbr = "ppas", Id = 0, Name = "forma czasownika", ValueFull = "imiesłów przymiotnikowy bierny" };
                public static Entry.Label Anterior => new Entry.Label { ValueAbbr = "pant", Id = 0, Name = "forma czasownika", ValueFull = "imiesłów przysłówkowy uprzedni" };
                public static Entry.Label Concurrent => new Entry.Label { ValueAbbr = "pcon", Id = 0, Name = "forma czasownika", ValueFull = "imiesłów przysłówkowy współczesny" };
            }

        }

        internal static class Congruence //kongruentność
        {
            public static Entry.Label Congruent => new Entry.Label { ValueAbbr = "congr", Id = 0, Name = "kongruentność", ValueFull = "forma kongruentna", Description = "forma liczebnika łącząca się z rzeczownikiem w związku zgody (kongruencja), np. »dwaj chłopcy«" };
            public static Entry.Label Rection => new Entry.Label { ValueAbbr = "rec", Id = 0, Name = "kongruentność", ValueFull = "forma niekongruentna", Description = "forma liczebnika łącząca się z rzeczownikiem w związku rządu (rekcja), np. »dwóch chłopców«" };
        }

        internal static class Derivatives //formy pochodne (derywaty)
        {
            public static Entry.Label AdjectiveBasedAdverb => new Entry.Label { ValueAbbr = "adja", Id = 0, Name = "", ValueFull = "przysłówek odprzymiotnikowy" };
            public static Entry.Label ParticipleBasedAdverb => new Entry.Label { ValueAbbr = "pacta", Id = 0, Name = "", ValueFull = "przysłówek odimiesłowowy, np. »drwiąco«" };

        }

        internal static class Vocality //wokaliczność
        {
            public static Entry.Label Vocalic => new Entry.Label { ValueAbbr = "wok", Id = 0, Name = "wokaliczność", ValueFull = "forma wokaliczna", Description = "obecność »e« wstawnego" };
            public static Entry.Label NonVocalic => new Entry.Label { ValueAbbr = "nwok", Id = 0, Name = "wokaliczność", ValueFull = "forma niewokaliczna", Description = "brak »e« wstawnego" };
        }

        internal static class FormStructure //budowa formy
        {
            public static Entry.Label Analytic => new Entry.Label { ValueAbbr = "analit", Id = 0, Name = "budowa formy", ValueFull = "forma analityczna", Description = "forma składająca się z kilku przestawialnych elementów (osobnych wyrazów)" };
            public static Entry.Label Synthetic => new Entry.Label { ValueAbbr = "syntet", Id = 0, Name = "budowa formy", ValueFull = "forma syntetyczna", Description = "forma składająca się z pojedynczego wyrazu" };
        }

        internal static class Other
        {
            public static Entry.Label Aff => new Entry.Label { ValueAbbr = "aff", Id = 0, Name = "", ValueFull = "forma z opcjonalnym sufiksem »się«", Description = "formie może towarzyszyć sufiks »się«" };
            public static Entry.Label Neg => new Entry.Label { ValueAbbr = "neg", Id = 0, Name = "", ValueFull = "forma zanegowana" };
            public static Entry.Label Numcomp => new Entry.Label { ValueAbbr = "numcomp", Id = 0, Name = "", ValueFull = "cząstka liczebników złożonych" };

            public static Entry.Label Abbreviation => new Entry.Label { ValueAbbr = "brev", Id = 0, Name = "część mowy", ValueFull = "skrót lub skrótowiec" };
            public static Entry.Label AbbreviatedForm => new Entry.Label { ValueAbbr = "skr.", Id = 0, ValueFull = "skrót" };
            public static Entry.Label AbbreviationExplanation => new Entry.Label { ValueFull = "rozwinięcie skrótu", Id = 0 };



            public static Entry.Label Deprecating => new Entry.Label { ValueAbbr = "depr", Id = 0, Name = "", ValueFull = "forma deprecjatywna", Description = "forma wyrażająca lekceważnie, rzadziej forma wzniosła" };
            public static Entry.Label Fragment => new Entry.Label { ValueAbbr = "frag", Id = 0, Name = "", ValueFull = "burkinostka", Description = "wyraz występujący zawsze w połączeniu z innym" };
            public static Entry.Label Pewien => new Entry.Label { ValueAbbr = "adjc", Id = 0, Name = "", ValueFull = "forma dawnej, rzeczownikowej odmiany przymiotników", Description = "" };
            public static Entry.Label Polsku => new Entry.Label { ValueAbbr = "adjp", Id = 0, Name = "", ValueFull = "forma odprzymiotnikowa zakończona na »-u«", Description = "" };
            public static Entry.Label PluraliaTantum => new Entry.Label { ValueAbbr = "pt", Id = 0, Name = "", ValueFull = "rzeczownik plurale tantum", Description = "rzeczownik posiadający tylko formy liczby mnogiej, np. »spodnie«, »nożyczki«" };
        }





        internal static class Pos //część mowy i derywaty
        {
            public static Entry.Label Noun => new Entry.Label { ValueAbbr = "subst", Id = 0, Name = "część mowy", ValueFull = "rzeczownik" };
            public static Entry.Label Numeral => new Entry.Label { ValueAbbr = "num", Id = 0, Name = "część mowy", ValueFull = "liczebnik" };
            public static Entry.Label Interjection => new Entry.Label { ValueAbbr = "interj", Id = 0, Name = "część mowy", ValueFull = "wykrzyknik" };
            public static Entry.Label Praeposition => new Entry.Label { ValueAbbr = "prep", Id = 0, Name = "część mowy", ValueFull = "przyimek" };
            public static Entry.Label Adjective => new Entry.Label { ValueAbbr = "adj", Id = 0, Name = "część mowy", ValueFull = "przymiotnik" };
            public static Entry.Label Adverb => new Entry.Label { ValueAbbr = "adv", Id = 0, Name = "część mowy", ValueFull = "przysłówek" };
            public static Entry.Label Particle => new Entry.Label { ValueAbbr = "part", Id = 0, Name = "część mowy", ValueFull = "partykuła" };

            public static Entry.Label Ppron12 => new Entry.Label { ValueAbbr = "ppron12", Id = 0, Name = "część mowy", ValueFull = "zaimek 1. lub 2. osoby" };
            public static Entry.Label Ppron3 => new Entry.Label { ValueAbbr = "ppron3", Id = 0, Name = "część mowy", ValueFull = "zaimek 3. osoby" };



        }







    }
}