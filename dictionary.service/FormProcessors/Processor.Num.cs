using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Num : ProcessorBase
    {
        public Num(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase) { }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak
        }

        protected override void AddRelated(Entry entry)
        {
            //jeśli przymiotnik "jeden"
            if (SearchedForm.Lemma.Form.Equals("jeden"))
            {
                //rzeczownik "jeden"
                entry.Relateds.Append(new Entry.Related { Id = 0, Word = "jeden", Categories = new[] { LabelPrototypes.Pos.Noun }, Url = Path.Combine(FormQueryUrlBase, "jeden") });
                //przymiotnik "jeden"
                entry.Relateds.Append(new Entry.Related { Id = 1, Word = "jeden", Categories = new[] { LabelPrototypes.Pos.Adjective }, Url = Path.Combine(FormQueryUrlBase, "jeden") });
            }
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables.Append
            (
                new Entry.Table
                {
                    Id = 0,
                    Titles = new[] { LabelPrototypes.EmptyLabel },
                    ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                    Rows = new[]
                    {
                        //mianownik
                        GenerateEntryTableRow(0, LabelPrototypes.Case.Nominative, GetTableCellForms(Forms.Nom())),
                        //dopełniacz
                        GenerateEntryTableRow(1, LabelPrototypes.Case.Genitive, GetTableCellForms(Forms.Gen())),
                        //celownik
                        GenerateEntryTableRow(2, LabelPrototypes.Case.Dative, GetTableCellForms(Forms.Dat())),
                        //biernik
                        GenerateEntryTableRow(3, LabelPrototypes.Case.Accusative, GetTableCellForms(Forms.Acc())),
                        //narzędnik
                        GenerateEntryTableRow(4, LabelPrototypes.Case.Instrumental, GetTableCellForms(Forms.Ins())),
                        //miejscownik
                        GenerateEntryTableRow(5, LabelPrototypes.Case.Locative, GetTableCellForms(Forms.Loc()))

                    }
                }
            );

            entry.Tables.Append
            (
                new Entry.Table
                {
                    Id = 0,
                    Titles = new[] { LabelPrototypes.Other.Numcomp },
                    ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                    Rows = new[] { GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Numcomp())) }
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

                //rodzaj
                newForm.AddGenderLabels(forms.ToList()[i]);

                //kongruentność
                newForm.AddCongrencyLabels(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                //dodanie łącznika, jeśli forma jest cząstką liczebnika złożonego
                if (forms.SelectMany(x => x.Categories).Contains("numcomp"))
                {
                    newForm.Word = newForm.Word + "-";
                }

                yield return newForm;

            }
        }


    }
}