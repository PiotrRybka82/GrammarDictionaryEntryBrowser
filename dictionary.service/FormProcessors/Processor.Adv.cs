using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Adv : ProcessorBase
    {
        public Adv(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase) { }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //styl

        }

        protected override void AddRelated(Entry entry)
        {
            //przymiotnik podstawowy
            if (SearchedForm.Categories.Contains("adja"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Pos.Adjective },
                    Id = entry.Relateds.Count(),
                    Word = SearchedForm.Lemma.Form,
                    Url = Path.Combine(FormQueryUrlBase, SearchedForm.Lemma.Form)
                });
            }

            //imiesłów podstawowy
            if (SearchedForm.Categories.Contains("pacta"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.VerbForms.Participle.Active },
                    Id = entry.Relateds.Count(),
                    Word = SearchedForm.Lemma.Form,
                    Url = Path.Combine(FormQueryUrlBase, SearchedForm.Lemma.Form)
                });
            }

            //stopień równy
            if (Forms.SelectMany(x => x.Categories).Contains("com") && !SearchedForm.Categories.Contains("pos"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Degree.Positive },
                    Id = entry.Relateds.Count(),
                    Word = Forms.Compar().First().Word,
                    Url = Path.Combine(FormQueryUrlBase, Forms.Posit().First().Word)
                });
            }

            //stopień wyższy
            if (Forms.SelectMany(x => x.Categories).Contains("com") && !SearchedForm.Categories.Contains("com"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Degree.Comparative },
                    Id = entry.Relateds.Count(),
                    Word = Forms.Compar().First().Word,
                    Url = Path.Combine(FormQueryUrlBase, Forms.Compar().First().Word)
                });
            }

            //stopień najwyższy
            if (Forms.SelectMany(x => x.Categories).Contains("sup") && !SearchedForm.Categories.Contains("sup"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Degree.Superlative },
                    Id = entry.Relateds.Count(),
                    Word = Forms.Compar().First().Word,
                    Url = Path.Combine(FormQueryUrlBase, Forms.Super().First().Word)
                });
            }
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables.Append(
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

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;

            }

        }
    }
}