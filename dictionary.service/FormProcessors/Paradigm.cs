
namespace Dictionary.Service.FormProcessors
{
    internal enum Paradigm
    {
        indeclinable, //spójniki, przyimki, partykuły, wykrzykniki, burkinostki, imiesłowy przysłówkowe, skróty

        noun, //rzeczowniki

        adj, //przymiotniki (wysoki), imiesłowy przymiotnikowe (stojący)
        adv, //przysłówki

        v, //czasowniki właściwe (pisać, mówić)
        v_tobe, //czasownik być
        v_pred, //predykatywy (czasowniki nie mające form osobowych, np. trzeba, można, warto)
        v_winien, //czasowniki niewłaściwe (mające formy rodzaju w czasie teraźniejszym, np. winien, gotów)

        num, //liczebniki główne i cząstki liczebników złożonych

        pron12, //zaimki osobowe 1. i 2. osoby
        pron3, //zaimek osobowy 3. osoby
    }
}