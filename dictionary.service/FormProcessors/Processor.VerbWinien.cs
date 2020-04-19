using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class VerbWinien : Verb
    {
        public VerbWinien(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase)
        {

        }


        protected override void AddRelated(Entry entry)
        {
            //brak;
        }

        protected override void AddTables(Entry entry)
        {
            addAdditionalForms();

            //tryb ozn., czas ter.
            entry.Tables.Append(new Entry.Table
            {
                Id = 0,
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Present },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.Pres().Pri().Sg()), GetTableCellForms(Forms.Pres().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.Pres().Sec().Sg()), GetTableCellForms(Forms.Pres().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.Pres().Ter().Sg()), GetTableCellForms(Forms.Pres().Ter().Pl()) )
                }
            });

            //tryb ozn., czas przyszły
            entry.Tables.Append(new Entry.Table
            {
                Id = 1,
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Future },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.Fut().Pri().Sg()), GetTableCellForms(Forms.Fut().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.Fut().Sec().Sg()), GetTableCellForms(Forms.Fut().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.Fut().Ter().Sg()), GetTableCellForms(Forms.Fut().Ter().Pl()) )
                }
            });

            //tryb ozn., czas przeszły
            entry.Tables.Append(new Entry.Table
            {
                Id = 2,
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Past },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.Past().Pri().Sg()), GetTableCellForms(Forms.Past().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.Past().Sec().Sg()), GetTableCellForms(Forms.Past().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.Past().Ter().Sg()), GetTableCellForms(Forms.Past().Ter().Pl()) )
                }
            });

            //tryb przypuszcz. w czasie nie przeszłym
            entry.Tables.Append(new Entry.Table
            {
                Id = 3,
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.NonPast },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.CondNonPast().Pri().Sg()), GetTableCellForms(Forms.CondNonPast().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.CondNonPast().Sec().Sg()), GetTableCellForms(Forms.CondNonPast().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.CondNonPast().Ter().Sg()), GetTableCellForms(Forms.CondNonPast().Ter().Pl()) )
                }
            });

            //tryb przypuszcz. w czasie nie przeszłym
            entry.Tables.Append(new Entry.Table
            {
                Id = 4,
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.Past },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.CondPast().Pri().Sg()), GetTableCellForms(Forms.CondPast().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.CondPast().Sec().Sg()), GetTableCellForms(Forms.CondPast().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.CondPast().Ter().Sg()), GetTableCellForms(Forms.CondPast().Ter().Pl()) )
                }
            });

            //tryb rozkaz.
            entry.Tables.Append(new Entry.Table
            {
                Id = 5,
                Titles = new[] { LabelPrototypes.Mood.Imperative },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.Imperat().Pri().Sg()), GetTableCellForms(Forms.Imperat().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.Imperat().Sec().Sg()), GetTableCellForms(Forms.Imperat().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.Imperat().Ter().Sg()), GetTableCellForms(Forms.Imperat().Ter().Pl()) )
                }
            });

            //bezokolicznik
            entry.Tables.Append(new Entry.Table
            {
                Id = 6,
                Titles = new[] { LabelPrototypes.VerbForms.Infinitive },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Inf()))
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

                //wokaliczność
                newForm.AddVocalityLabels(forms.ToList()[i]);

                //budowa formy
                newForm.AddFormStructureLabels(forms.ToList()[i]);

                //styl
                newForm.AddStyleLabels(forms.ToList()[i]);

                yield return newForm;

            }

        }


        protected override void AddAnalyticalForms(string[] auxiliaryVerb, string oldCat, string newCat, bool auxVerbInPreposition = true)
        {
            //if oldCat = ""...
            var sgForms = Forms.Sg();

            foreach (var form in sgForms)
            {
                SupplementForms(JoinAnalyticalForms(auxiliaryVerb[0], form.Word, auxVerbInPreposition), oldCat == "" ? form.Categories.Add(newCat) : form.Categories.Replace(oldCat, newCat));
            }

            var plForms = Forms.Pl();
            foreach (var form in plForms)
            {
                SupplementForms(JoinAnalyticalForms(auxiliaryVerb[0], form.Word, auxVerbInPreposition), oldCat == "" ? form.Categories.Add(newCat) : form.Categories.Replace(oldCat, newCat));
            }

        }



        private void addAdditionalForms()
        {
            //formy analityczne czasu teraźniejszego (gowówem [jest]) --do uzupełnienia
            AddAnalyticalForms(new[] { "(jest)", "(są)" }, "", "fin", false);

            //formy analityczne czasu przeszłego (gotówem był)
            AddAnalyticalForms(_auxiliaryVerbs, GetFullParadigm(Forms).ToArray(), "", "praet", false);

            //formy analityczne czasu przeszłęgo 2 (gotów byłem)
            var _auxiliaryVerbs2 = new[] {
                "byłem", "byłem", "byłem", "byłam", "byłom",
                "byliśmy", "byłyśmy", "byłyśmy", "byłyśmy", "byłyśmy"
            };
            AddAnalyticalForms(_auxiliaryVerbs2, GetPseudoParticiples().ToArray(), "", "praet", false);

            //formy analityczne trybu przypuszczającego (gotów byłbym)
            var _auxiliaryVerbs3 = new[] {
                "byłbym", "byłbym", "byłbym", "byłabym", "byłobym",
                "bylibyśmy", "byłybyśmy", "byłybyśmy", "byłybyśmy", "byłybyśmy"
            };
            AddAnalyticalForms(_auxiliaryVerbs3, GetPseudoParticiples().ToArray(), "", "cond", false);

            //formy czasu przyszłego (będę gotów)
            var _auxiliaryVerbs4 = new[] {
                "będę", "będziesz", "będzie",
                "będziemy", "będziecie", "będą"
            };
            var pseudoParticiplesSg = GetPseudoParticiples().Sg();
            var pseudoParticiplesPl = GetPseudoParticiples().Pl();
            foreach (var form in pseudoParticiplesSg)
            {
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[0], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[1], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[2], form.Word), form.Categories.Add("fut"));
            }

            foreach (var form in pseudoParticiplesPl)
            {
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[3], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[4], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[5], form.Word), form.Categories.Add("fut"));
            }

            //formy trybu rozkazującego (niech będę gotów, bądź gotów)
            var _auxiliaryVerbs5 = new[] {
                "niech będę", "bądź", "niech będzie",
                "bądźmy", "bądźcie", "niech będą"
            };
            foreach (var form in pseudoParticiplesSg)
            {
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs5[0], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs5[1], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs5[2], form.Word), form.Categories.Add("fut"));
            }

            foreach (var form in pseudoParticiplesPl)
            {
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[3], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[4], form.Word), form.Categories.Add("fut"));
                SupplementForms(JoinAnalyticalForms(_auxiliaryVerbs4[5], form.Word), form.Categories.Add("fut"));
            }

            //bezokolicznik
            foreach (var item in GetPseudoParticiples())
            {
                SupplementForms(JoinAnalyticalForms("być", item.Word), item.Categories.Add("inf"));
            }


        }

        protected IEnumerable<Form> GetPseudoParticiples()
        {
            yield return Forms.Sg().Ter().M1().First();
            yield return Forms.Sg().Ter().M2().First();
            yield return Forms.Sg().Ter().M3().First();
            yield return Forms.Sg().Ter().F().First();
            yield return Forms.Sg().Ter().N().First();

            yield return Forms.Pl().Ter().M1().First();
            yield return Forms.Pl().Ter().M2().First();
            yield return Forms.Pl().Ter().M3().First();
            yield return Forms.Pl().Ter().F().First();
            yield return Forms.Pl().Ter().N().First();
        }



    }
}