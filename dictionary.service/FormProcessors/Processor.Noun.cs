using Dictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary.Service.FormProcessors
{
    internal class Noun : ProcessorBase
    {
        public Noun(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase) { }


        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak
        }

        protected override void AddRelated(Entry entry)
        {
            // skrót
            var skrot = Forms.FirstOrDefault(x => x.Categories.Contains("brev"));

            if (skrot != null)
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Other.AbbreviatedForm },
                    Id = entry.Relateds.Count(),
                    Word = skrot.Word,
                    Url = Path.Combine(FormQueryUrlBase, skrot.Word)
                });
            }

            //czasownik bazowy (dla odsłowników)
            var inf = Forms.Inf().FirstOrDefault();
            if (SearchedForm.Categories.Contains("ger") && inf != null)
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.VerbForms.BaseVerb },
                    Id = entry.Relateds.Count(),
                    Word = inf.Word,
                    Url = Path.Combine(FormQueryUrlBase, inf.Word)
                });
            }
        }

        protected override void AddTables(Entry entry)
        {
            //jeśli odsłownik, wybierz tylko formy z kategorią 'ger'
            var forms = SearchedForm.Categories.Contains("ger") ? Forms.Gerund() : Forms;

            entry.Tables.Append(new Entry.Table
            {
                ColumnHeaders = LabelSets.SgPl,
                Id = 0,
                Titles = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    //mianownik
                    GenerateEntryTableRow(0, LabelPrototypes.Case.Nominative, GetTableCellForms(forms.Sg().Nom()), GetTableCellForms(forms.Pl().Nom()) ),

                    //dopełniacz
                    GenerateEntryTableRow(1, LabelPrototypes.Case.Genitive, GetTableCellForms(forms.Sg().Gen()), GetTableCellForms(forms.Pl().Gen()) ),

                    //celownik
                    GenerateEntryTableRow(2, LabelPrototypes.Case.Dative, GetTableCellForms(forms.Sg().Dat()), GetTableCellForms(forms.Pl().Dat()) ),

                    //biernik
                    GenerateEntryTableRow(3, LabelPrototypes.Case.Accusative, GetTableCellForms(forms.Sg().Acc()), GetTableCellForms(forms.Pl().Acc()) ),

                    //narzędnik
                    GenerateEntryTableRow(4, LabelPrototypes.Case.Instrumental, GetTableCellForms(forms.Sg().Ins()), GetTableCellForms(forms.Pl().Ins()) ),

                    //miejscownik
                    GenerateEntryTableRow(5, LabelPrototypes.Case.Locative, GetTableCellForms(forms.Sg().Loc()), GetTableCellForms(forms.Pl().Loc()) ),

                    //wołacz
                    GenerateEntryTableRow(6, LabelPrototypes.Case.Vocative, GetTableCellForms(forms.Sg().Voc()), GetTableCellForms(forms.Pl().Voc()) )
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

                //rodzaj gramatyczny
                newForm.AddGenderLabels(forms.ToList()[i]);

                //uniformizm
                newForm.AddUniformityLabels(forms.ToList()[i]);

                //deprecjatywność
                newForm.AddDeprecativeLabels(forms.ToList()[i]);

                //indywidualne kwalifikatory stylistyczne
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;
            }
        }

    }
}