using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Adv : Adj
    {
        public Adv(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase) 
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }

        protected override void CorrectEntry(Entry entry)
        {
            base.CorrectEntry(entry);

            //korekta lematu dla przysłówków odimiesłowowych
            if (SearchedForm.Categories.Contains("pacta"))
            {
                entry.Lemma = LexemeForms.Where(x => x.Categories.Contains("pacta")).First().Word;
            }

        }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak
        }

        protected override void AddRelateds(Entry entry)
        {
            //stopień równy
            RelatedAddingCondition = () =>
                AdditionalLexemeEqualForms.SelectMany(x => x.Categories).Contains("com") &&
                !SearchedForm.Categories.Contains("pos");
            var categories = new[] { LabelPrototypes.Degree.Positive };
            WordSelector = () => AdditionalLexemeEqualForms.Where(x => x.Categories.Contains("adv")).Posit().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //stopień wyższy
            RelatedAddingCondition = () =>
                AdditionalLexemeEqualForms.SelectMany(x => x.Categories).Contains("com") &&
                !SearchedForm.Categories.Contains("com");
            categories = new[] { LabelPrototypes.Degree.Comparative };
            WordSelector = () => AdditionalLexemeEqualForms.Where(x => x.Categories.Contains("adv")).Compar().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //stopień najwyższy
            RelatedAddingCondition = () =>
                AdditionalLexemeEqualForms.SelectMany(x => x.Categories).Contains("sup") &&
                !SearchedForm.Categories.Contains("sup");
            categories = new[] { LabelPrototypes.Degree.Superlative };
            WordSelector = () => AdditionalLexemeEqualForms.Where(x => x.Categories.Contains("adv")).Super().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //przymiotnik podstawowy
            categories = new[] { LabelPrototypes.Pos.Adjective };
            WordSelector = () => LexemeForms.Where(x => x.Categories.Contains("adj")).Posit().Sg().Nom().M1().Word();

            AddRelated(entry, "adja", categories, WordSelector);

            //imiesłów podstawowy (jeśli przysłówek odimiesłowowy)
            categories = new[] { LabelPrototypes.VerbForms.Participle.Active };
            WordSelector = () => LexemeForms.ParticAct().Sg().Nom().M1().Word();

            AddRelated(entry, "pacta", categories, WordSelector);

            //czasownik podstawowy (jeśli przysłówek odimiesłowowy)
            categories = new[] { LabelPrototypes.VerbForms.BaseVerb };
            WordSelector = () => LexemeForms.Inf().Word();

            AddRelated(entry, "pacta", categories, WordSelector);
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables = entry.Tables.Add(
                new Entry.Table
                {
                    Id = 0,
                    Titles = new[] { LabelPrototypes.EmptyLabel },
                    ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                    Rows = new[] { GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(new[] { SearchedForm })) }
                }
            );
        }

        protected override IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms)
        {
            for (int i = 0; i < forms.Count(); i++)
            {
                var newForm = new Entry.Form
                {
                    Id = i,
                    Word = forms.ToList()[i].Word
                };

                //stopień
                newForm.AddDegreeLabels(forms.ToList()[i]);

                //negacja
                newForm.AddNegationLabel(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;
            }
        }
    }
}