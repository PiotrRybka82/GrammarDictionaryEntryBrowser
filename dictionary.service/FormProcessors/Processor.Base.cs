using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal abstract class ProcessorBase
    {
        private readonly Form _searchedForm;
        private IEnumerable<Form> _forms;

        private readonly string _formQueryUrlBase;


        protected string FormQueryUrlBase => _formQueryUrlBase;

        protected Form SearchedForm => _searchedForm;
        protected IEnumerable<Form> Forms => _forms;

        protected string SearchedFormFirstCategory => SearchedForm.Categories.FirstOrDefault();
        protected IEnumerable<string> FormsFirstCategory => Forms.Select(x => x.Categories.FirstOrDefault()).Distinct();




        internal ProcessorBase(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase)
        {
            _searchedForm = searchedForm;
            _forms = forms;
            _formQueryUrlBase = formQueryUrlBase;
        }



        protected abstract void AddParadigmSpecificGeneralLabels(Entry entry);
        protected abstract void AddTables(Entry entry);
        protected abstract void AddRelated(Entry entry);
        protected abstract IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms);



        protected void SupplementForms(string newWord, IEnumerable<string> categories)
        {
            Forms.Append(new Form
            {
                Categories = categories,
                Labels = SearchedForm.Labels,
                Lemma = SearchedForm.Lemma,
                Meanings = SearchedForm.Meanings,
                Word = newWord
            });
        }

        protected Entry.Row GenerateEntryTableRow(int id, Entry.Label rowCategory, params IEnumerable<Entry.Form>[] forms)
        {
            var newRow = new Entry.Row
            {
                Id = id,
                RowCategory = rowCategory
            };

            for (int i = 0; i < forms.Count(); i++)
            {
                newRow.Columns.Append(new Entry.Column
                {
                    Id = i,
                    Forms = forms.ToList()[i]
                });
            }

            return newRow;
        }

        internal IEnumerable<Entry> GetEntries()
        {
            var formGroups = GroupFormsByLemmas(Forms).ToList();

            for (int i = 0; i < formGroups.Count(); i++)
            {
                var entry = initializeEntry(i);
                addGeneralLabels(entry);
                AddParadigmSpecificGeneralLabels(entry);
                AddTables(entry);
                AddRelated(entry);

                yield return entry;
            }
        }


        protected Paradigm? GetParadigm()
        {
            if (isIndeclinable()) return Paradigm.indeclinable;
            else if (isNoun()) return Paradigm.noun;
            else if (isAdj()) return Paradigm.adj;
            else if (isAdv()) return Paradigm.adv;
            else if (isNum()) return Paradigm.num;
            else if (isPron12()) return Paradigm.pron12;
            else if (isPron3()) return Paradigm.pron3;

            //czasowniki po przymiotnikach i przysłówkach (żeby przechwycić odsłowniki, imiesłowy i formy odimiesłowowe)
            //na początku winien i pred jako bardziej specyficzne kategorie
            else if (isV_winien()) return Paradigm.v_winien;
            else if (isV_pred()) return Paradigm.v_pred;
            //być przed innymi czaasownikami
            else if (isV_ToBe()) return Paradigm.v_tobe;
            else if (isV()) return Paradigm.v;

            else return null;
        }


        private Entry initializeEntry(int index, string lemma = "")
        {
            return new Entry
            {
                Lemma = lemma == "" ? SearchedForm.Lemma.Form : lemma,
                Meanings = Forms.SelectMany(x => x.Meanings).Distinct(),
                Pos = getPos(),
                Labels = null,
                Id = index,
                Paradigm = GetParadigm()?.ToString()
            };
        }

        private IEnumerable<IEnumerable<Form>> GroupFormsByLemmas(IEnumerable<Form> forms)
        {
            var lemmaTags = forms.Select(x => x.Lemma.Tag).Distinct();

            foreach (var lemmaTag in lemmaTags)
            {
                yield return forms.Where(x => x.Lemma.Tag.Equals(lemmaTag));
            }
        }

        private Entry.Label getPos()
        {
            string abbr = SearchedFormFirstCategory;
            int id = 0;
            string name = "";
            string value = "";
            string descr = "";

            switch (abbr)
            {
                case "interj": value = "wykrzyknik"; break;

                case "subst":
                case "depr": value = "rzeczownik"; break;

                case "adj":
                case "adjc": value = "przymiotnik"; break;

                case "adjp":
                case "adja":
                case "adv":
                case "pacta": value = "przysłówek"; break;

                case "inf":
                case "praet":
                case "cond":
                case "imps":
                case "fin":
                case "impt":
                case "bedzie":
                case "aglt":
                case "winien":
                case "pred": value = "czasownik"; break;

                case "ger": value = "odsłownik"; descr = "rzeczownik odczasownikowy"; break;

                case "pact":
                case "ppas": value = "imiesłów przymiotnikowy"; break;

                case "pant":
                case "pcon": value = "imiesłów przysłówkowy"; break;

                case "conj":
                case "comp": value = "spójnik"; break;

                case "part": value = "partykuła"; break;

                case "brev": value = "skrót"; break;

                case "frag": value = "burkinostka"; descr = "cząstka nazwy wielowyrazowej, niewystępująca samodzielnie"; break;

                case "prep": value = "przyimek"; break;

                case "numcomp":
                case "num": value = "liczebnik"; break;

                case "ppron12":
                case "ppron3": value = "zaimek osobowy"; break;

                default: value = ""; break;
            }

            return new Entry.Label
            {
                Description = descr,
                ValueAbbr = abbr,
                Id = id,
                Name = name,
                ValueFull = value
            };
        }

        private void addGeneralLabels(Entry entry)
        {
            //dodanie etykiet pojawiających się we wszystkich formach

            //znaczenia
            entry.Meanings = Forms.SelectMany(x => x.Meanings).Distinct();

            //globalne kwalifikatory stylistyczne
            var globalLabels = findAndRemoveGlobalLabels(Forms);
            globalLabels.ToList().ForEach(x => entry.Labels.Append(x));
        }

        private IEnumerable<Entry.Label> findAndRemoveGlobalLabels(IEnumerable<Form> forms)
        {
            int numberOfForms = forms.Count();

            var allLabels = forms.SelectMany(x => x.Labels);
            var distinctLabels = allLabels.Distinct();

            foreach (string label in distinctLabels)
            {
                int numberOfLabels = allLabels.Count(x => x.Equals(label));

                if (numberOfLabels == numberOfForms)
                {
                    removeGlobalLabel(forms, label);

                    var newForm = new Form { Categories = new[] { label } };
                    var newEntryForm = new Entry.Form();
                    newEntryForm.AddStyleLabels(newForm);

                    yield return newEntryForm.Categories.First();
                }
            }

        }

        private void removeGlobalLabel(IEnumerable<Form> forms, string label)
        {
            for (int i = 0; i < forms.Count(); i++)
            {
                if (forms.ToList()[i].Categories.Contains(label))
                {
                    List<string> newCategories = forms.ToList()[i].Categories.ToList();
                    newCategories.Remove(label);
                    forms.ToList()[i].Categories = newCategories;
                }

            }
        }

        private bool searchedFormFirstCategoryContainsTags(string tags, string separator)
        {
            var separatedTags = tags.Split(separator);

            foreach (var tag in separatedTags)
            {
                if (SearchedFormFirstCategory.Equals(tag))
                {
                    return true;
                }
            }

            return false;
        }


        private bool isIndeclinable()
        {
            return searchedFormFirstCategoryContainsTags("interj|conj|comp|part|brev|frag|prep|pant|pcon|pacta", "|");
        }

        private bool isNoun()
        {
            return searchedFormFirstCategoryContainsTags("subst|depr|ger", "|");
        }

        private bool isAdj()
        {
            return searchedFormFirstCategoryContainsTags("adj|adjc|pact|ppas", "|");
        }

        private bool isAdv()
        {
            return searchedFormFirstCategoryContainsTags("adv|adjp|adja", "|");
        }

        private bool isV()
        {
            return searchedFormFirstCategoryContainsTags("inf|preat|fin|cond|impt|imps", "|");
        }

        private bool isV_ToBe()
        {
            return SearchedForm.Lemma.Form.Equals("być");
        }

        private bool isV_pred()
        {
            return searchedFormFirstCategoryContainsTags("pred", "|");
        }

        private bool isV_winien()
        {
            return searchedFormFirstCategoryContainsTags("winien", "|");
        }

        private bool isNum()
        {
            return searchedFormFirstCategoryContainsTags("num|numcomp", "|");
        }

        private bool isPron12()
        {
            return searchedFormFirstCategoryContainsTags("ppron12", "|");
        }

        private bool isPron3()
        {
            return searchedFormFirstCategoryContainsTags("ppron3", "|");
        }



    }

}