using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Adj : ProcessorBase
    {
        public Adj(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase) { }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak
        }

        protected override void AddRelated(Entry entry)
        {
            //jeśli imiesłów
            if (SearchedForm.Categories.Contains("pact") || SearchedForm.Categories.Contains("ppas"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Id = entry.Relateds.Count(),
                    Word = SearchedForm.Lemma.Form,
                    Categories = new[] { LabelPrototypes.VerbForms.BaseVerb },
                    Url = Path.Combine(FormQueryUrlBase, SearchedForm.Lemma.Form)
                });
            }

            //forma na -u
            if (SearchedForm.Categories.Contains("adjp"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Other.Polsku },
                    Id = entry.Relateds.Count(),
                    Word = Forms.Polsku().First().Word,
                    Url = Path.Combine(FormQueryUrlBase, Forms.Polsku().First().Word)
                });
            }

            //przysłówek derywowany
            if (SearchedForm.Categories.Contains("adja"))
            {
                entry.Relateds.Append(new Entry.Related
                {
                    Categories = new[] { LabelPrototypes.Pos.Adverb },
                    Id = entry.Relateds.Count(),
                    Word = Forms.AdjBAdv().First().Word,
                    Url = Path.Combine(FormQueryUrlBase, Forms.AdjBAdv().First().Word)
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
                    ColumnHeaders = LabelSets.SgPl,
                    Rows = new[]
                    {
                        //mianownik
                        GenerateEntryTableRow(0, LabelPrototypes.Case.Nominative, GetTableCellForms(Forms.Sg().Nom()), GetTableCellForms(Forms.Pl().Nom())),
                        //dopełniacz
                        GenerateEntryTableRow(1, LabelPrototypes.Case.Genitive, GetTableCellForms(Forms.Sg().Gen()), GetTableCellForms(Forms.Pl().Gen())),
                        //celownik
                        GenerateEntryTableRow(2, LabelPrototypes.Case.Dative, GetTableCellForms(Forms.Sg().Dat()), GetTableCellForms(Forms.Pl().Dat())),
                        //biernik
                        GenerateEntryTableRow(3, LabelPrototypes.Case.Accusative, GetTableCellForms(Forms.Sg().Acc()), GetTableCellForms(Forms.Pl().Acc())),
                        //narzędnik
                        GenerateEntryTableRow(4, LabelPrototypes.Case.Instrumental, GetTableCellForms(Forms.Sg().Ins()), GetTableCellForms(Forms.Pl().Ins())),
                        //miejscownik
                        GenerateEntryTableRow(5, LabelPrototypes.Case.Locative, GetTableCellForms(Forms.Sg().Loc()), GetTableCellForms(Forms.Pl().Loc())),
                        //wołacz
                        GenerateEntryTableRow(6, LabelPrototypes.Case.Vocative, GetTableCellForms(Forms.Sg().Voc()), GetTableCellForms(Forms.Pl().Voc()))
                    }
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

                //forma dawnej odmiany rzeczownikowej
                newForm.AddAdjcLabels(forms.ToList()[i]);

                //rodzaj
                newForm.AddGenderLabels(forms.ToList()[i]);

                //stopień
                newForm.AddDegreeLabels(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;

            }
        }
    }
}