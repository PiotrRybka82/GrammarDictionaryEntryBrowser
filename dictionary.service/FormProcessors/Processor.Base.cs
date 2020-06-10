using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Dictionary.Core.Models;

[assembly: InternalsVisibleTo("dictionary.tests")]
namespace Dictionary.Service.FormProcessors
{
    internal abstract class ProcessorBase
    {
        #region private fields

        private readonly Form _searchedForm;
        private IEnumerable<Form> _lexemeForms;
        private IEnumerable<Form> _homonymousForms;
        private IEnumerable<Form> _additionalLexemeEqualForms;

        private readonly string _formQueryUrlBase;

        #endregion

        #region protected properties

        protected Func<string> WordSelector;
        protected Func<bool> RelatedAddingCondition;
        protected string FormQueryUrlBase => _formQueryUrlBase;
        protected Form SearchedForm => _searchedForm;
        protected IEnumerable<Form> LexemeForms
        {
            get { return _lexemeForms; }
            set { _lexemeForms = value; }
        }
        protected IEnumerable<Form> HomonymousForms => _homonymousForms;
        protected IEnumerable<Form> AdditionalLexemeEqualForms
        {
            get { return _additionalLexemeEqualForms; }
            set { _additionalLexemeEqualForms = value; }
        }

        protected string SearchedFormFirstCategory => SearchedForm.Categories.FirstOrDefault();
        protected IEnumerable<string> FormsFirstCategory => LexemeForms.Select(x => x.Categories.FirstOrDefault()).Distinct();

        #endregion

        #region constructor

        internal ProcessorBase(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
        {
            _searchedForm = searchedForm;
            _lexemeForms = lexemeForms;
            _homonymousForms = homonymousForms;
            _formQueryUrlBase = formQueryUrlBase;
            _additionalLexemeEqualForms = lexemeForms;
        }

        #endregion

        #region abstract methods

        protected abstract void AddParadigmSpecificGeneralLabels(Entry entry);
        protected abstract void AddTables(Entry entry);
        protected abstract void AddRelateds(Entry entry);
        protected abstract IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms);
        protected abstract void CorrectEntry(Entry entry);

        #endregion

        #region internal methods

        internal Entry GetEntry(int index)
        {
            var entry = InitializeEntry(index);
            AddGeneralLabels(entry);
            AddParadigmSpecificGeneralLabels(entry);
            AddTables(entry);
            AddRelateds(entry);
            CorrectEntry(entry);
            return entry;
        }

        internal Paradigm? GetParadigm()
        {
            if (isIndeclinable()) return Paradigm.indeclinable;
            else if (isNoun()) return Paradigm.noun;
            else if (isAdj()) return Paradigm.adj;
            else if (isAdv()) return Paradigm.adv;
            else if (isNum()) return Paradigm.num;
            else if (isPron12()) return Paradigm.pron12;
            else if (isPron3()) return Paradigm.pron3;

            //czasowniki po przymiotnikach i przyslówkach (żeby przechwycic odslowniki, imieslowy i formy odimieslowowe)
            //na poczatku winien i pred jako bardziej specyficzne kategorie
            else if (isV_winien()) return Paradigm.v_winien;
            else if (isV_pred()) return Paradigm.v_pred;
            //byc przed innymi czaasownikami
            else if (isV_ToBe()) return Paradigm.v_tobe;
            else if (isV()) return Paradigm.v;

            else return null;
        }

        #endregion

        #region protected auxiliary methods

        protected void SupplementLexemeForms(string newWord, IEnumerable<string> categories)
        {
            LexemeForms = LexemeForms.Add(new Form
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
                newRow.Columns = newRow.Columns.Add(new Entry.Column
                {
                    Id = i,
                    Forms = forms.ToList()[i]
                });
            }

            return newRow;
        }

        protected Entry InitializeEntry(int index, string lemma = "")
        {
            return new Entry
            {
                Lemma = lemma == "" ? SearchedForm.Lemma.Form : lemma,
                Meanings = LexemeForms.SelectMany(x => x.Meanings).Distinct(),
                Pos = GetPos(),
                Labels = null,
                Id = index,
                Paradigm = GetParadigm()?.ToString(),
                Relateds = new List<Entry.Related>(),
                Tables = new List<Entry.Table>()
            };
        }

        protected Entry.Label GetPos()
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
                case "depr": abbr = "subst"; value = "rzeczownik"; break;
                case "ger": value = "odsłownik"; descr = "rzeczownik odczasownikowy"; break;
                case "adj":
                case "adjc": abbr = "adj"; value = "przymiotnik"; break;
                case "adjp":
                case "adja":
                case "adv":
                case "pacta": abbr = "adv"; value = "przysłówek"; break;
                case "inf":
                case "praet":
                case "cond":
                case "imps":
                case "fin":
                case "impt":
                case "bedzie":
                case "aglt":
                case "winien":
                case "pred": abbr = "verb"; value = "czasownik"; break;
                case "pact": value = "imiesłów przymiotnikowy czynny"; break;
                case "ppas": value = "imiesłów przymiotnikowy bierny"; break;
                case "pant": value = "imiesłów przysłówkowy uprzedni"; break;
                case "pcon": value = "imiesłów przysłówkowy współczesny"; break;
                case "conj":
                case "comp": abbr = "conj"; value = "spójnik"; break;
                case "part": value = "partykuła"; break;
                case "brev": value = "skrót"; break;
                case "frag": value = "burkinostka"; descr = "cząstka nazwy wielowyrazowej niewystępujaca samodzielnie"; break;
                case "prep": value = "przyimek"; break;
                case "numcomp":
                case "num": abbr = "num"; value = "liczebnik"; break;
                case "ppron12":
                case "ppron3": abbr = "pron"; value = "zaimek osobowy"; break;
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

        protected void AddGeneralLabels(Entry entry)
        {
            //znaczenia
            entry.Meanings = LexemeForms.SelectMany(x => x.Meanings).Distinct();
            var temp = entry.Meanings.ToList();
            var newMeanings = new List<string>();
            temp.ForEach(x => newMeanings.Add(x.Replace("_", " ")));
            entry.Meanings = newMeanings;

            //globalne kwalifikatory stylistyczne
            var globalLabels = FindGlobalLabels(LexemeForms).ToList();
            globalLabels.ForEach(x => RemoveGlobalLabel(LexemeForms, x));

            if (entry.Labels == null) entry.Labels = new List<Entry.Label>();

            foreach (string globalLabel in globalLabels)
            {
                var tempForm = new Form { Labels = new[] { globalLabel } };

                var tempEntryForm = new Entry.Form();

                tempEntryForm.AddStyleLabels(tempForm);

                if (tempEntryForm.Categories != null)
                {
                    if (tempEntryForm.Categories.Count() > 0) entry.Labels = entry.Labels.Add(tempEntryForm.Categories.First());
                }
            }
        }

        protected void AddRelated(
            Entry entry,
            Func<bool> conditionOfAddingRelated,
            IEnumerable<Entry.Label> categoriesOfRelated,
            Func<string> relatedWordSelector)
        {
            string word = relatedWordSelector();

            if (conditionOfAddingRelated() && !word.Equals(""))
            {
                entry.Relateds = entry.Relateds.Add(new Entry.Related
                {
                    Id = entry.Relateds.Count(),
                    Word = word,
                    Url = Path.Combine(FormQueryUrlBase, word),
                    Categories = categoriesOfRelated
                });
            }
        }

        protected void AddRelated(
            Entry entry,
            string conditionalCategoryToBeFoundInForms,
            IEnumerable<Entry.Label> categoriesOfRelated,
            Func<string> relatedWordSelector)
        {
            Func<bool> condition = () => LexemeForms.SelectMany(x => x.Categories).Contains(conditionalCategoryToBeFoundInForms);

            AddRelated(entry, condition, categoriesOfRelated, relatedWordSelector);
        }

        protected void FilterOutDegreeForms()
        {
            if (SearchedForm.Categories.Contains("pos")) //na we stopien równy -> usun stopien wyższy i najwyższy
            {
                LexemeForms = LexemeForms.Where(x => !x.Categories.Contains("com") && !x.Categories.Contains("sup"));
            }
            else if (SearchedForm.Categories.Contains("com")) //na we stopien wyższy -> usun stopien równy i najwyższy
            {
                LexemeForms = LexemeForms.Where(x => !x.Categories.Contains("pos") && !x.Categories.Contains("sup"));
            }
            else if (SearchedForm.Categories.Contains("sup")) //na we stopien najwyższy -> usun stopien równy i wyższy
            {
                LexemeForms = LexemeForms.Where(x => !x.Categories.Contains("pos") && !x.Categories.Contains("com"));
            }
        }

        protected void CorrectEntryLemmaOfComparables(Entry entry)
        {
            if (SearchedForm.Categories.Contains("com") || SearchedForm.Categories.Contains("sup"))
            {
                entry.Lemma = SearchedForm.Word;
            }
        }

        protected IEnumerable<string> FindGlobalLabels(IEnumerable<Form> forms)
        {
            int numberOfForms = forms.Count();
            var allLabels = forms.SelectMany(x => x.Labels);
            var distinctLabels = allLabels.Distinct();

            foreach (string label in distinctLabels)
            {
                if (label == null) continue;
                if (label == "") continue;

                int numberOfLabels = allLabels.Count(x => x.Equals(label));

                if (numberOfLabels == numberOfForms)
                {
                    yield return label;
                }
            }
        }

        protected void RemoveGlobalLabel(IEnumerable<Form> forms, string label)
        {
            foreach (var form in forms)
            {
                if (form.Labels != null)
                {
                    var tempList = form.Labels.ToList();
                    tempList.Remove(label);
                    form.Labels = tempList;
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

        #endregion

        #region isPos auxiliary methods

        private bool isIndeclinable()
        {
            return searchedFormFirstCategoryContainsTags("interj|conj|comp|part|brev|frag|prep|pant|pcon", "|");
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
            return searchedFormFirstCategoryContainsTags("adv|adjp|adja|pacta", "|");
        }

        private bool isV()
        {
            return searchedFormFirstCategoryContainsTags("inf|praet|fin|cond|impt|imps", "|");
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

        #endregion
    }
}