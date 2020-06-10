using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Adj : ProcessorBase
    {
        public Adj(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) 
        {            
            FilterOutDegreeForms(); //filtrowanie form stopnia            
        }

        protected override void CorrectEntry(Entry entry)
        {
            //korekta lematu dla form stopnia
            CorrectEntryLemmaOfComparables(entry);
        }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak

        }
        
        protected override void AddRelateds(Entry entry)
        {
            //jeśli imiesłów niezanegowany -> dodaj czasownik podstawowy
            RelatedAddingCondition = () => SearchedForm.Categories.Contains("pact") || SearchedForm.Categories.Contains("ppas");
            var categories = new[] { LabelPrototypes.VerbForms.BaseVerb };
            WordSelector = () => LexemeForms.Inf().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //jeśli forma zanegowana -> dodaj formę niezanegowaną
            RelatedAddingCondition = () => SearchedForm.Categories.Contains("neg");
            categories = new[] { LabelPrototypes.Derivatives.NotNeg };
            if (SearchedForm.Categories.Contains("pact"))
            {
                WordSelector = () => LexemeForms.NotNeg().ParticAct().Sg().Nom().M1().Word();
            }
            else if (SearchedForm.Categories.Contains("ppas"))
            {
                WordSelector = () => LexemeForms.NotNeg().ParticPas().Sg().Nom().M1().Word();
            }
            else
            {
                WordSelector = () => LexemeForms.NotNeg().Sg().Nom().M1().Word();
            }

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //forma na -u
            categories = new[] { LabelPrototypes.Other.Polsku };
            WordSelector = () => LexemeForms.Polsku().Word();

            AddRelated(entry, "adjp", categories, WordSelector);

            //przysłówek derywowany
            categories = new[] { LabelPrototypes.Pos.Adverb };
            WordSelector = () => LexemeForms.AdjBAdv().Word();

            AddRelated(entry, "adja", categories, WordSelector);

            //stopień równy
            RelatedAddingCondition = () =>
                AdditionalLexemeEqualForms.SelectMany(x => x.Categories).Contains("com") &&
                !SearchedForm.Categories.Contains("pos");
            categories = new[] { LabelPrototypes.Degree.Positive };
            WordSelector = () => AdditionalLexemeEqualForms.Posit().Sg().Nom().M1().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //stopień wyższy
            RelatedAddingCondition = () =>
                AdditionalLexemeEqualForms.SelectMany(x => x.Categories).Contains("com") &&
                !SearchedForm.Categories.Contains("com");
            categories = new[] { LabelPrototypes.Degree.Comparative };
            WordSelector = () => AdditionalLexemeEqualForms.Compar().Sg().Nom().M1().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //stopień najwyższy
            RelatedAddingCondition = () =>
                AdditionalLexemeEqualForms.SelectMany(x => x.Categories).Contains("sup") &&
                !SearchedForm.Categories.Contains("sup");
            categories = new[] { LabelPrototypes.Degree.Superlative };
            WordSelector = () => AdditionalLexemeEqualForms.Super().Sg().Nom().M1().Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);
        }

        protected override void AddTables(Entry entry)
        {
            entry.Tables = entry.Tables.Add(
                new Entry.Table
                {
                    Id = 0,
                    Titles = new[] { LabelPrototypes.EmptyLabel },
                    ColumnHeaders = LabelSets.SgPl,
                    Rows = new[]
                    {
                        //mianownik
                        GenerateEntryTableRow(0, LabelPrototypes.Case.Nominative, GetTableCellForms(LexemeForms.Sg().Nom()), GetTableCellForms(LexemeForms.Pl().Nom())),
                        //dopełniacz
                        GenerateEntryTableRow(1, LabelPrototypes.Case.Genitive, GetTableCellForms(LexemeForms.Sg().Gen()), GetTableCellForms(LexemeForms.Pl().Gen())),
                        //celownik
                        GenerateEntryTableRow(2, LabelPrototypes.Case.Dative, GetTableCellForms(LexemeForms.Sg().Dat()), GetTableCellForms(LexemeForms.Pl().Dat())),
                        //biernik
                        GenerateEntryTableRow(3, LabelPrototypes.Case.Accusative, GetTableCellForms(LexemeForms.Sg().Acc()), GetTableCellForms(LexemeForms.Pl().Acc())),
                        //narzędnik
                        GenerateEntryTableRow(4, LabelPrototypes.Case.Instrumental, GetTableCellForms(LexemeForms.Sg().Ins()), GetTableCellForms(LexemeForms.Pl().Ins())),
                        //miejscownik
                        GenerateEntryTableRow(5, LabelPrototypes.Case.Locative, GetTableCellForms(LexemeForms.Sg().Loc()), GetTableCellForms(LexemeForms.Pl().Loc())),
                        //wołacz
                        GenerateEntryTableRow(6, LabelPrototypes.Case.Vocative, GetTableCellForms(LexemeForms.Sg().Voc()), GetTableCellForms(LexemeForms.Pl().Voc()))
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

                //negacja
                newForm.AddNegationLabel(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;
            }
        }
    }
}