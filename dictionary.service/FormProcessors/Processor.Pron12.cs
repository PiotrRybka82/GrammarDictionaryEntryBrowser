using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Pron12 : ProcessorBase
    {
        public Pron12(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }

        protected override void CorrectEntry(Entry entry)
        {
 
        }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            entry.Labels = entry.Labels.Add(LabelPrototypes.Pos.Ppron12);
        }

        protected override void AddRelateds(Entry entry)
        {

        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 0,
                Titles = new[] { LabelPrototypes.EmptyLabel },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[] 
                {
                    //mianownik
                    GenerateEntryTableRow(0, LabelPrototypes.Case.Nominative, GetTableCellForms(LexemeForms.Nom()) ),

                    //dopełniacz
                    GenerateEntryTableRow(1, LabelPrototypes.Case.Genitive, GetTableCellForms(LexemeForms.Gen()) ),

                    //celownik
                    GenerateEntryTableRow(2, LabelPrototypes.Case.Dative, GetTableCellForms(LexemeForms.Dat()) ),

                    //biernik
                    GenerateEntryTableRow(3, LabelPrototypes.Case.Accusative, GetTableCellForms(LexemeForms.Acc()) ),

                    //narzędnik
                    GenerateEntryTableRow(4, LabelPrototypes.Case.Instrumental, GetTableCellForms(LexemeForms.Ins()) ),

                    //miejscownik
                    GenerateEntryTableRow(5, LabelPrototypes.Case.Locative, GetTableCellForms(LexemeForms.Loc()) ),

                    //wołacz
                    GenerateEntryTableRow(6, LabelPrototypes.Case.Vocative, GetTableCellForms(LexemeForms.Voc()) )
                }
            });
        }

        protected override IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms)
        {
            //jeśli brak wołacza
            if (forms == null) yield return new Entry.Form { Id = 0, Word = "" };

            for (int i = 0; i < forms.Count(); i++)
            {
                var newForm = new Entry.Form
                {
                    Id = i,
                    Word = forms.ToList()[i].Word
                };

                //forma akcentowana|nieakcentowana
                newForm.AddAccentedLabels(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;
            }
        }
    }
}