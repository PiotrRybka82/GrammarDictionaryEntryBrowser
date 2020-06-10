namespace Dictionary.Service.FormProcessors
{
    internal enum Paradigm
    {
        indeclinable, //spojniki, przyimki, partykuly, wykrzykniki, burkinostki, imieslowy przyslowkowe, skroty
        noun, //rzeczowniki
        adj, //przymiotniki (wysoki), imieslowy przymiotnikowe (stojacy)
        adv, //przyslowki
        v, //czasowniki wlasciwe (pisac, mowic)
        v_tobe, //czasownik byc
        v_pred, //predykatywy (czasowniki nie majace form osobowych, np. trzeba, mozna, warto)
        v_winien, //czasowniki niewlasciwe (majace formy rodzaju w czasie terazniejszym, np. winien, gotow)
        num, //liczebniki glowne i czastki liczebnikow zlozonych
        pron12, //zaimki osobowe 1. i 2. osoby
        pron3, //zaimek osobowy 3. osoby
    }
}