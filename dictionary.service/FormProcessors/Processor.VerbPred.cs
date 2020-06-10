using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class VerbPred : Verb
    {
        public VerbPred(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {

        }

        protected override void AddRelateds(Entry entry)
        {
            //leksem homonimiczny: czasownik
            RelatedAddingCondition = () => HomonymousForms.SelectMany(x => x.Categories).Contains("inf"); //czasownik
            var categories = new[] { LabelPrototypes.Pos.Verb };
            WordSelector = () => HomonymousForms.Inf().Word();
            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //leksem homonimiczny: rzeczownik
            RelatedAddingCondition = () => HomonymousForms.SelectMany(x => x.Categories).Contains("subst"); //rzeczownik
            categories = new[] { LabelPrototypes.Pos.Noun };
            WordSelector = () => HomonymousForms.Where(x => x.Categories.Contains("subst")).Word();
            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            //leksem homonimiczny: odsłownik
            RelatedAddingCondition = () => HomonymousForms.SelectMany(x => x.Categories).Contains("ger"); //odsłownik
            categories = new[] { LabelPrototypes.VerbForms.Gerund };
            WordSelector = () => HomonymousForms.Where(x => x.Categories.Contains("ger")).Word();
            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);
        }

        protected override void AddTables(Entry entry)
        {
            addAdditionalForms();

            //tryb ozn., czas ter.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Present },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Pres()))
                }
            });

            //tryb ozn., czas przesz.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Past },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Past()))
                }
            });

            //tryb ozn., czas przysz.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Future },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Fut()))
                }
            });

            //tryb przyp., czas nieprzesz.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.NonPast },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.CondNonPast()))
                }
            });

            //tryb przyp., czas przesz.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.Past },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.CondPast()))
                }
            });

            //tryb rozkaz.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Imperative },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Imperat()))
                }
            });

            //bezokolicznik
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.VerbForms.Infinitive },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Inf()))
                }
            });

        }

        private void addAdditionalForms()
        {
            var aspect = GetAspect();
            string baseWord = LexemeForms.Where(x => x.Categories.Contains("pred")).Word();

            //tryb ozn., czas ter. (brak [jest])
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "(jest)"), aspect.Add("fin"));
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "(jest)", false), aspect.Add("fin"));

            //tryb ozn., czas przesz. (brak było)
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "było"), aspect.Append("praet"));
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "było", false), aspect.Append("praet"));

            //tryb ozn., czas przysz. (brak będzie)
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "będzie"), aspect.Append("fut"));
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "będzie", false), aspect.Append("fut"));

            //tryb przyp., czas nieprzesz. (brak by)
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "by"), aspect.Append("cond"));
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "by", false), aspect.Append("cond"));

            //tryb przyp., czas przesz. (byłoby brak)
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "byłoby"), aspect.Append("praet cond"));
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "byłoby", false), aspect.Append("praet cond"));
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "by było", false), aspect.Append("praet cond"));

            //tryb rozkaz. (niech będzie brak)
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "niech będzie", false), aspect.Append("impt"));

            //bezokolicznik (być brak)
            SupplementLexemeForms(JoinAnalyticalForms(baseWord, "być", false), aspect.Append("inf"));
        }
    }
}