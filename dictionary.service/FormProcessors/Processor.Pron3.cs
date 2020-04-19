using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Pron3 : ProcessorBase
    {
        public Pron3(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase) { }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            throw new NotImplementedException();
        }

        protected override void AddRelated(Entry entry)
        {
            throw new NotImplementedException();
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables.Append(new Entry.Table
            {
                Id = 0,
                Titles = new[] { LabelPrototypes.EmptyLabel },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                            {
                    //mianownik
                    GenerateEntryTableRow(0, LabelPrototypes.Case.Nominative, GetTableCellForms(Forms.Nom()) ),

                    //dopełniacz
                    GenerateEntryTableRow(1, LabelPrototypes.Case.Genitive, GetTableCellForms(Forms.Gen()) ),

                    //celownik
                    GenerateEntryTableRow(2, LabelPrototypes.Case.Dative, GetTableCellForms(Forms.Dat()) ),

                    //biernik
                    GenerateEntryTableRow(3, LabelPrototypes.Case.Accusative, GetTableCellForms(Forms.Acc()) ),

                    //narzędnik
                    GenerateEntryTableRow(4, LabelPrototypes.Case.Instrumental, GetTableCellForms(Forms.Ins()) ),

                    //miejscownik
                    GenerateEntryTableRow(5, LabelPrototypes.Case.Locative, GetTableCellForms(Forms.Loc()) )

                }
            });
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

                //rodzaj
                newForm.AddGenderLabels(forms.ToList()[i]);

                //forma akcentowana|nieakcentowana
                newForm.AddAccentedLabels(forms.ToList()[i]);

                //forma poprzyimkowa|niepoprzyimkowa
                newForm.AddPostprepositionLabels(forms.ToList()[i]);

                //forma deprecjatywna
                newForm.AddDeprecativeLabels(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;
            }
        }
    }
}