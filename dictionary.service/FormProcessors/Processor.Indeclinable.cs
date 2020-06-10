using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Indeclinable : ProcessorBase
    {
        public Indeclinable(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }

        protected override void CorrectEntry(Entry entry)
        {

        }


        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            
        }

        protected override void AddRelateds(Entry entry)
        {
            //skrót
            var categories = new[] { LabelPrototypes.Other.AbbreviationExplanation };
            WordSelector = () => SearchedForm.Lemma.Form;
            AddRelated(entry, "brev", categories, WordSelector);

            //imiesłów przysłówkowy uprzedni
            categories = new[] { LabelPrototypes.VerbForms.BaseVerb };
            WordSelector = () => LexemeForms.Inf().Word();
            AddRelated(entry, "pant", categories, WordSelector);
            AddRelated(entry, "pcon", categories, WordSelector);
            AddRelated(entry, "pacta", categories, WordSelector);
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables = entry.Tables.Add(new Entry.Table
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
            var formList = forms.ToList();

            for (int i = 0; i < formList.Count(); i++)
            {
                var entryForm = new Entry.Form
                {
                    Id = i,
                    Word = formList[i].Word,
                    Categories = new List<Entry.Label>()
                };

                entryForm.AddStyleLabels(formList[i]);

                yield return entryForm;
            }
        }
    }
}