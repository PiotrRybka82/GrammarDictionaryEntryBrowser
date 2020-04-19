using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class Verb : ProcessorBase
    {
        protected string[] _auxiliaryVerbs = new[] {
                    "był", "był", "był", "była", "było",
                    "był", "był", "był", "była", "było",
                    "był", "był", "był", "była", "było",
                    "byli", "były", "były", "były", "były",
                    "byli", "były", "były", "były", "były",
                    "byli", "były", "były", "były", "były" };


        public Verb(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase)
        {


        }


        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            entry.AddAspectGeneralLabels(Forms);
        }

        protected override void AddRelated(Entry entry)
        {
            //imiesłowy 
            AddRelatedParticiples(entry);

            //odsłowniki
            AddRelatedNouns(entry);

        }


        protected override void AddTables(Entry entry)
        {
            addAdditionalForms();

            //tryb ozn., czas ter.
            entry.Tables.Append(new Entry.Table
            {
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
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

            //tryb ozn., czas zaprzeszły
            entry.Tables.Append(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Plusquamperf },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(Forms.Plusquamperf().Pri().Sg()), GetTableCellForms(Forms.Plusquamperf().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(Forms.Plusquamperf().Sec().Sg()), GetTableCellForms(Forms.Plusquamperf().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(Forms.Plusquamperf().Ter().Sg()), GetTableCellForms(Forms.Plusquamperf().Ter().Pl()) )
                }
            });

            //tryb przypuszcz. w czasie nie przeszłym
            entry.Tables.Append(new Entry.Table
            {
                Id = entry.Tables.Count(),
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

            //tryb przypuszcz. w czasie przeszłym
            entry.Tables.Append(new Entry.Table
            {
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.VerbForms.Infinitive },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(Forms.Inf()))
                }
            });

            //bezosobnik
            entry.Tables.Append(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.VerbForms.Impersonal },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.VerbForms.Impersonal, GetTableCellForms(Forms.Impers()))
                }
            });

        }


        protected override IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms)
        {
            int x;

            if (forms == null)
            {
                yield return new Entry.Form
                {
                    Id = 0,
                    Word = "",
                    Categories = null
                };

                goto end;
            }

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

        end:

            x = 0;

        }

        protected IEnumerable<string> GetAspect()
        {
            IEnumerable<string> aspect = new List<string>();
            if (SearchedForm.Categories.Contains("imperf")) aspect.Add("impref");
            if (SearchedForm.Categories.Contains("perf")) aspect.Add("pref");

            return aspect;
        }

        protected virtual void AddAnalyticalForms(string[] auxiliaryVerbs, string newCategory, string additionalCategory = "", bool auxVerbInPreposition = true)
        {
            foreach (var item in Forms.Past().Sg().Ter())
            {
                var newCategories = item.Categories.Replace("praet", newCategory);
                if (additionalCategory != "") newCategories.Add(additionalCategory);
                if (auxiliaryVerbs[0] != "") SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[0], item.Word, auxVerbInPreposition), newCategories.Replace("ter", "pri"));
                if (auxiliaryVerbs[1] != "") SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[1], item.Word, auxVerbInPreposition), newCategories.Replace("pri", "sec"));
                if (auxiliaryVerbs[2] != "") SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[2], item.Word, auxVerbInPreposition), newCategories.Replace("sec", "ter"));
            }

            foreach (var item in Forms.Past().Pl().Ter())
            {
                var newCategories = item.Categories.Replace("praet", newCategory);
                if (additionalCategory != "") newCategories.Add(additionalCategory);
                if (auxiliaryVerbs[3] != "") SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[3], item.Word, auxVerbInPreposition), newCategories.Replace("ter", "pri"));
                if (auxiliaryVerbs[4] != "") SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[4], item.Word, auxVerbInPreposition), newCategories.Replace("pri", "sec"));
                if (auxiliaryVerbs[5] != "") SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[5], item.Word, auxVerbInPreposition), newCategories.Replace("sec", "ter"));
            }
        }

        protected virtual void AddAnalyticalForms(string[] auxiliaryVerbs, Form mainVerb, string oldCat, string newCat, bool auxVerbInPreposition = true)
        {
            var newCategories = mainVerb.Categories;
            string[] persons = new[] { "pri", "sec", "ter", "pri", "sec", "ter" };
            string[] numbers = new[] { "sg", "sg", "sg", "pl", "pl", "pl" };
            for (int i = 0; i < auxiliaryVerbs.Count(); i++)
            {
                SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[i], mainVerb.Word, auxVerbInPreposition), newCategories.Replace(oldCat, newCat).Add(persons[i]).Add(numbers[i]));
            }
        }

        protected virtual void AddAnalyticalForms(string[] auxiliaryVerbs, Form[] mainVerbs, string oldCat, string newCat, bool auxVerbInPreposition = true)
        {
            for (int i = 0; i < auxiliaryVerbs.Count(); i++)
            {
                var newCategories = mainVerbs[i].Categories;
                if (oldCat == "") newCategories.Add(newCat);
                else newCategories.Replace(oldCat, newCat);
                SupplementForms(JoinAnalyticalForms(auxiliaryVerbs[i], mainVerbs[i].Word, auxVerbInPreposition), newCategories);
            }
        }


        protected string JoinAnalyticalForms(string form1, string form2, bool form12order = true)
        {
            if (form12order) return form1 + " " + form2;
            else return form2 + " " + form1;
        }

        protected IEnumerable<Form> GetFullParadigm(IEnumerable<Form> forms)
        {
            yield return forms.Sg().Pri().M1().First();
            yield return forms.Sg().Pri().M2().First();
            yield return forms.Sg().Pri().M3().First();
            yield return forms.Sg().Pri().F().First();
            yield return forms.Sg().Pri().N().First();

            yield return forms.Sg().Sec().M1().First();
            yield return forms.Sg().Sec().M2().First();
            yield return forms.Sg().Sec().M3().First();
            yield return forms.Sg().Sec().F().First();
            yield return forms.Sg().Sec().N().First();

            yield return forms.Sg().Ter().M1().First();
            yield return forms.Sg().Ter().M2().First();
            yield return forms.Sg().Ter().M3().First();
            yield return forms.Sg().Ter().F().First();
            yield return forms.Sg().Ter().N().First();

            yield return forms.Pl().Pri().M1().First();
            yield return forms.Pl().Pri().M2().First();
            yield return forms.Pl().Pri().M3().First();
            yield return forms.Pl().Pri().F().First();
            yield return forms.Pl().Pri().N().First();

            yield return forms.Pl().Sec().M1().First();
            yield return forms.Pl().Sec().M2().First();
            yield return forms.Pl().Sec().M3().First();
            yield return forms.Pl().Sec().F().First();
            yield return forms.Pl().Sec().N().First();

            yield return forms.Pl().Ter().M1().First();
            yield return forms.Pl().Ter().M2().First();
            yield return forms.Pl().Ter().M3().First();
            yield return forms.Pl().Ter().F().First();
            yield return forms.Pl().Ter().N().First();
        }

        protected void AddRelatedNouns(Entry entry)
        {
            Func<IEnumerable<Form>, string> getSgNom = (x) => x.Sg().Nom().Word();

            var nouns = Forms.Where(x => x.Categories.Contains("subst"));
            addRelated(entry, getSgNom(nouns), new[] { LabelPrototypes.Pos.Noun });

            var gerunds = Forms.Where(x => x.Categories.Contains("ger"));
            addRelated(entry, getSgNom(gerunds.NotNeg()), new[] { LabelPrototypes.VerbForms.Gerund });
            addRelated(entry, getSgNom(gerunds.Neg()), new[] { LabelPrototypes.VerbForms.Gerund, LabelPrototypes.Other.Neg });

        }

        protected void AddRelatedParticiples(Entry entry)
        {
            var actPartLab = LabelPrototypes.VerbForms.Participle.Active;
            var pasPartLab = LabelPrototypes.VerbForms.Participle.Passive;
            var conPartLab = LabelPrototypes.VerbForms.Participle.Concurrent;
            var antPartLab = LabelPrototypes.VerbForms.Participle.Anterior;
            var negLab = LabelPrototypes.Other.Neg;

            var actPartic = (Forms.ParticAct().NotNeg(), new[] { actPartLab });
            var negActPartic = (Forms.ParticAct().Neg(), new[] { actPartLab, negLab });

            var pasPartic = (Forms.ParticPas().NotNeg(), new[] { pasPartLab });
            var negPasPartic = (Forms.ParticPas().Neg(), new[] { pasPartLab, negLab });

            var antPartic = (Forms.ParticAnt().NotNeg(), new[] { antPartLab });
            var negAntPartic = (Forms.ParticAnt().Neg(), new[] { antPartLab, negLab });

            var conPartic = (Forms.ParticCon().NotNeg(), new[] { conPartLab });
            var negConPartic = (Forms.ParticCon().Neg(), new[] { conPartLab, negLab });

            Func<IEnumerable<Form>, string> getSgNomM1 = (x) => x.Sg().Nom().M1().Word();

            addRelated(entry, getSgNomM1(actPartic.Item1), actPartic.Item2);
            addRelated(entry, getSgNomM1(negActPartic.Item1), negActPartic.Item2);
            addRelated(entry, getSgNomM1(pasPartic.Item1), pasPartic.Item2);
            addRelated(entry, getSgNomM1(negPasPartic.Item1), negPasPartic.Item2);
            addRelated(entry, getSgNomM1(antPartic.Item1), antPartic.Item2);
            addRelated(entry, getSgNomM1(negAntPartic.Item1), negAntPartic.Item2);
            addRelated(entry, getSgNomM1(conPartic.Item1), conPartic.Item2);
            addRelated(entry, getSgNomM1(negConPartic.Item1), negConPartic.Item2);

        }



        private void addAdditionalForms()
        {

            //czas przyszły (będę pisać, pisał...)
            if (!SearchedForm.Lemma.Form.Equals("być")) //wyjątek: formy "być" (mają osobne formy czasu przysz.)
            {
                //będę pisał|pisała|pisało...
                AddAnalyticalForms(new[] { "będę", "będziesz", "będzie", "będziemy", "będziecie", "będą" }, "fut");

                //będę pisać
                var mainVerb = Forms.Inf().First();
                AddAnalyticalForms(new[] { "będę", "będziesz", "będzie", "będziemy", "będziecie", "będą" }, mainVerb, "inf", "fut");
            }

            //em pisał, m pisał
            AddAnalyticalForms(new[] { "em", "eś", "", "eśmy", "eście", "" }, "praet", "wok");
            AddAnalyticalForms(new[] { "m", "ś", "", "śmy", "ście", "" }, "praet", "nwok");

            //pisałem był...
            AddAnalyticalForms(_auxiliaryVerbs, GetFullParadigm(Forms.Past()).ToArray(), "praet", "plus", false);

            //bym pisał --do uzupełnienia

            //pisałbym był
            AddAnalyticalForms(_auxiliaryVerbs, GetFullParadigm(Forms.CondNonPast()).ToArray(), "", "praet", false);


            //byłbym pisał --do uzupełnienia

            //bym był pisał --do uzupełnienia

















        }


        private void addRelated(Entry entry, string word, Entry.Label[] categories)
        {
            if (word != "")
            {
                entry.Relateds.Add(
                    new Entry.Related
                    {
                        Id = entry.Relateds.Count(),
                        Categories = categories,
                        Word = word,
                        Url = Path.Combine(FormQueryUrlBase, word)
                    }
                );
            }
        }



    }
}