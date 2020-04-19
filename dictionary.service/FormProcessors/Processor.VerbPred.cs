using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class VerbPred : Verb
    {

        public VerbPred(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase)
        {

        }



        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            //brak
        }

        protected override void AddRelated(Entry entry)
        {
            //rzeczownik
            AddRelatedNouns(entry);

            //czasownik właściwy
            var verbs = Forms.Where(x => x.Categories.Contains("inf"));
            if (verbs != null)
            {
                entry.Relateds.Add(
                    new Entry.Related
                    {
                        Id = entry.Relateds.Count(),
                        Categories = new[] { LabelPrototypes.VerbForms.BaseVerb },
                        Word = verbs.First().Word,
                        Url = Path.Combine(FormQueryUrlBase, verbs.First().Word)
                    }
                );
            }

        }

        protected override void AddTables(Entry entry)
        {
            addAdditionalForms();

            //tryb ozn., czas ter.
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Present },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Ind().Pres()))
                }
            });

            //tryb ozn., czas przesz.
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Past },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Ind().Past()))
                }
            });

            //tryb ozn., czas przysz.
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Future },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Ind().Fut()))
                }
            });

            //tryb przyp., czas nieprzesz.
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.NonPast },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.CondNonPast()))
                }
            });

            //tryb przyp., czas przesz.
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.Past },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.CondPast()))
                }
            });

            //tryb rozkaz.
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Imperative },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Imperat()))
                }
            });

            //bezokolicznik
            entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.VerbForms.Infinitive },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Inf()))
                }
            });

        }

        // protected override IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms)
        // {
        //     throw new NotImplementedException();
        // }




        private void addAdditionalForms()
        {
            var aspect = GetAspect();
            string baseWord = Forms.Where(x => x.Categories.Contains("winien")).First().Word;

            //tryb ozn., czas ter. (brak [jest])
            SupplementForms(JoinAnalyticalForms(baseWord, "(jest)"), aspect.Add("fin"));
            SupplementForms(JoinAnalyticalForms(baseWord, "(jest)", false), aspect.Add("fin"));

            //tryb ozn., czas przesz. (brak było)
            SupplementForms(JoinAnalyticalForms(baseWord, "było"), aspect.Add("praet"));
            SupplementForms(JoinAnalyticalForms(baseWord, "było", false), aspect.Add("praet"));

            //tryb ozn., czas przysz. (brak będzie)
            SupplementForms(JoinAnalyticalForms(baseWord, "będzie"), aspect.Add("fut"));
            SupplementForms(JoinAnalyticalForms(baseWord, "będzie", false), aspect.Add("fut"));

            //tryb przyp., czas nieprzesz. (brak by)
            SupplementForms(JoinAnalyticalForms(baseWord, "by"), aspect.Add("cond"));
            SupplementForms(JoinAnalyticalForms(baseWord, "by", false), aspect.Add("cond"));

            //tryb przyp., czas przesz. (byłoby brak)
            SupplementForms(JoinAnalyticalForms(baseWord, "byłoby"), aspect.Add("cond").Add("praet"));
            SupplementForms(JoinAnalyticalForms(baseWord, "byłoby", false), aspect.Add("cond").Add("praet"));
            SupplementForms(JoinAnalyticalForms(baseWord, "by było", false), aspect.Add("cond").Add("praet"));

            //tryb rozkaz. (niech będzie brak)
            SupplementForms(JoinAnalyticalForms(baseWord, "niech będzie", false), aspect.Add("impt"));

            //bezokolicznik (być brak)
            SupplementForms(JoinAnalyticalForms(baseWord, "być", false), aspect.Add("inf"));


        }




    }


}