using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Indeclinable : ProcessorBase
    {
        public Indeclinable(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase) { }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak
        }

        protected override void AddRelated(Entry entry)
        {
            if (SearchedFormFirstCategory.Equals("brev"))
            {
                //skrót
                var nomSg = Forms.Nom().Sg().M1().FirstOrDefault();

                if (nomSg != null)
                {
                    entry.Relateds.Append(new Entry.Related
                    {
                        Categories = new[] { LabelPrototypes.Other.AbbreviationExplanation },
                        Word = nomSg.Lemma.Form,
                        Id = entry.Relateds.Count(),
                        Url = Path.Combine(FormQueryUrlBase, nomSg.Lemma.Form)
                    });
                }
            }
            else if (SearchedFormFirstCategory.Equals("pant") || SearchedFormFirstCategory.Equals("pcon") || SearchedFormFirstCategory.Equals("pacta"))
            {
                //imiesłów przysłówkowy uprzedni || imiesłów przysłówkowy współczesny || przysłówek odimiesłowowy
                var inf = Forms.Inf().FirstOrDefault();

                if (inf != null)
                {
                    entry.Relateds.Append(new Entry.Related
                    {
                        Categories = new[] { LabelPrototypes.VerbForms.BaseVerb },
                        Id = entry.Relateds.Count(),
                        Word = inf.Lemma.Form,
                        Url = Path.Combine(FormQueryUrlBase, inf.Lemma.Form)
                    });
                }
            }
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables.Append(new Entry.Table
            {
                Titles = new[] { LabelPrototypes.EmptyLabel },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Id = 0,
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(new[] {SearchedForm}))
                }
            });
        }

        protected override IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms)
        {
            for (int i = 0; i < forms.Count(); i++)
            {
                var newEntry = new Entry.Form
                {
                    Id = i,
                    Word = forms.ToList()[i].Word,
                    Categories = new[] { LabelPrototypes.EmptyLabel }
                };

                newEntry.AddStyleLabels(forms.ToList()[i]);

                yield return newEntry;
            }
        }
    }
}