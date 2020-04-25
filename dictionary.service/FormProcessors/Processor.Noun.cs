using Dictionary.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary.Service.FormProcessors
{
    internal class Noun : ProcessorBase
    {
        public Noun(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }



        protected override void CorrectEntry(Entry entry)
        {
            //korekta lematu dla odsłowników
            if (SearchedForm.Categories.Contains("ger"))
            {
                entry.Lemma = LexemeForms.Where(x => x.Categories.Contains("ger")).Sg().Nom().Word();
            }


        }


        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            

        }

        protected override void AddRelateds(Entry entry)
        {
            // skrót
            var categories = new[] { LabelPrototypes.Other.AbbreviatedForm };
            WordSelector = () => LexemeForms.Where(x => x.Categories.Contains("brev")).Word();

            AddRelated(entry, "brev", categories, WordSelector);


            //czasownik bazowy (dla odsłowników)
            categories = new[] { LabelPrototypes.VerbForms.BaseVerb };
            WordSelector = () => LexemeForms.Inf().Word();

            AddRelated(entry, "ger", categories, WordSelector);

            

        }

        protected override void AddTables(Entry entry)
        {
            //jeśli odsłownik, wybierz tylko formy z kategorią 'ger'
            var forms = SearchedForm.Categories.Contains("ger") ? LexemeForms.Gerund() : LexemeForms;

            entry.Tables = entry.Tables.Add(new Entry.Table
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

                //negacja
                newForm.AddNegationLabel(forms.ToList()[i]);

                //indywidualne kwalifikatory stylistyczne
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;
            }
        }






    }
}