using System;
using System.Collections.Generic;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class VerbWinien : Verb
    {
        public VerbWinien(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }


        protected override void AddRelateds(Entry entry)
        {
            //przymiotnik homonimiczny

        }

        protected override void AddTables(Entry entry)
        {
            addAdditionalForms();

            //tryb ozn., czas ter.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 0,
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Present },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.Pres().Pri().Sg()), GetTableCellForms(LexemeForms.Pres().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.Pres().Sec().Sg()), GetTableCellForms(LexemeForms.Pres().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.Pres().Ter().Sg()), GetTableCellForms(LexemeForms.Pres().Ter().Pl()) )
                }
            });

            //tryb ozn., czas przyszły
            entry.Tables =  entry.Tables.Add(new Entry.Table
            {
                Id = 1,
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Future },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.Fut().Pri().Sg()), GetTableCellForms(LexemeForms.Fut().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.Fut().Sec().Sg()), GetTableCellForms(LexemeForms.Fut().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.Fut().Ter().Sg()), GetTableCellForms(LexemeForms.Fut().Ter().Pl()) )
                }
            });

            //tryb ozn., czas przeszły
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 2,
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Past },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.Past().Pri().Sg()), GetTableCellForms(LexemeForms.Past().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.Past().Sec().Sg()), GetTableCellForms(LexemeForms.Past().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.Past().Ter().Sg()), GetTableCellForms(LexemeForms.Past().Ter().Pl()) )
                }
            });

            //tryb przypuszcz. w czasie nie przeszłym
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 3,
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.NonPast },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.CondNonPast().Pri().Sg()), GetTableCellForms(LexemeForms.CondNonPast().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.CondNonPast().Sec().Sg()), GetTableCellForms(LexemeForms.CondNonPast().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.CondNonPast().Ter().Sg()), GetTableCellForms(LexemeForms.CondNonPast().Ter().Pl()) )
                }
            });

            //tryb przypuszcz. w czasie nie przeszłym
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 4,
                Titles = new[] { LabelPrototypes.Mood.Conditional, LabelPrototypes.Tense.Past },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.CondPast().Pri().Sg()), GetTableCellForms(LexemeForms.CondPast().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.CondPast().Sec().Sg()), GetTableCellForms(LexemeForms.CondPast().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.CondPast().Ter().Sg()), GetTableCellForms(LexemeForms.CondPast().Ter().Pl()) )
                }
            });

            //tryb rozkaz.
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 5,
                Titles = new[] { LabelPrototypes.Mood.Imperative },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.Imperat().Pri().Sg()), GetTableCellForms(LexemeForms.Imperat().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.Imperat().Sec().Sg()), GetTableCellForms(LexemeForms.Imperat().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.Imperat().Ter().Sg()), GetTableCellForms(LexemeForms.Imperat().Ter().Pl()) )
                }
            });

            //bezokolicznik
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = 6,
                Titles = new[] { LabelPrototypes.VerbForms.Infinitive },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Inf()))
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
            var sgForms = LexemeForms.Sg();

            foreach (var form in sgForms)
            {
                SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerb[0], form.Word, auxVerbInPreposition), oldCat == "" ? form.Categories.Append(newCat) : form.Categories.Replace(oldCat, newCat));
            }

            var plForms = LexemeForms.Pl();
            foreach (var form in plForms)
            {
                SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerb[0], form.Word, auxVerbInPreposition), oldCat == "" ? form.Categories.Append(newCat) : form.Categories.Replace(oldCat, newCat));
            }

        }



        private void addAdditionalForms()
        {
            //formy analityczne czasu teraźniejszego (gowówem [jest]) --do uzupełnienia
            AddAnalyticalForms(new[] { "(jest)", "(są)" }, "", "fin", false);

            //formy analityczne czasu przeszłego (gotówem był)
            AddAnalyticalForms(_auxiliaryVerbs, GetFullParadigm(LexemeForms).ToArray(), "", "praet", false);

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
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[0], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[1], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[2], form.Word), form.Categories.Append("fut"));
            }

            foreach (var form in pseudoParticiplesPl)
            {
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[3], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[4], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[5], form.Word), form.Categories.Append("fut"));
            }

            //formy trybu rozkazującego (niech będę gotów, bądź gotów)
            var _auxiliaryVerbs5 = new[] {
                "niech będę", "bądź", "niech będzie",
                "bądźmy", "bądźcie", "niech będą"
            };
            foreach (var form in pseudoParticiplesSg)
            {
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs5[0], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs5[1], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs5[2], form.Word), form.Categories.Append("fut"));
            }

            foreach (var form in pseudoParticiplesPl)
            {
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[3], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[4], form.Word), form.Categories.Append("fut"));
                SupplementLexemeForms(JoinAnalyticalForms(_auxiliaryVerbs4[5], form.Word), form.Categories.Append("fut"));
            }

            //bezokolicznik
            foreach (var item in GetPseudoParticiples())
            {
                SupplementLexemeForms(JoinAnalyticalForms("być", item.Word), item.Categories.Append("inf"));
            }


        }

        protected IEnumerable<Form> GetPseudoParticiples()
        {
            yield return LexemeForms.Sg().M1().First();
            yield return LexemeForms.Sg().M2().First();
            yield return LexemeForms.Sg().M3().First();
            yield return LexemeForms.Sg().F().First();
            yield return LexemeForms.Sg().N().First();

            yield return LexemeForms.Pl().M1().First();
            yield return LexemeForms.Pl().M2().First();
            yield return LexemeForms.Pl().M3().First();
            yield return LexemeForms.Pl().F().First();
            yield return LexemeForms.Pl().N().First();
        }


        protected override IEnumerable<Form> GetFullParadigm(IEnumerable<Form> forms)
        {
            yield return forms.Sg().M1().First();
            yield return forms.Sg().M2().First();
            yield return forms.Sg().M3().First();
            yield return forms.Sg().F().First();
            yield return forms.Sg().N().First();

            yield return forms.Sg().M1().First();
            yield return forms.Sg().M2().First();
            yield return forms.Sg().M3().First();
            yield return forms.Sg().F().First();
            yield return forms.Sg().N().First();

            yield return forms.Sg().M1().First();
            yield return forms.Sg().M2().First();
            yield return forms.Sg().M3().First();
            yield return forms.Sg().F().First();
            yield return forms.Sg().N().First();

            yield return forms.Pl().M1().First();
            yield return forms.Pl().M2().First();
            yield return forms.Pl().M3().First();
            yield return forms.Pl().F().First();
            yield return forms.Pl().N().First();

            yield return forms.Pl().M1().First();
            yield return forms.Pl().M2().First();
            yield return forms.Pl().M3().First();
            yield return forms.Pl().F().First();
            yield return forms.Pl().N().First();

            yield return forms.Pl().M1().First();
            yield return forms.Pl().M2().First();
            yield return forms.Pl().M3().First();
            yield return forms.Pl().F().First();
            yield return forms.Pl().N().First();
        }







    }
}