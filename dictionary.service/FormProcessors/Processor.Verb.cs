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


        public Verb(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }


        protected override void CorrectEntry(Entry entry)
        {
            //brak 
        }


        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            entry.AddAspectGeneralLabels(LexemeForms);
        }


        protected override void AddRelateds(Entry entry)
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
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
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
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
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

            //tryb ozn., czas zaprzeszły
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.Mood.Indicative, LabelPrototypes.Tense.Plusquamperf },
                ColumnHeaders = LabelSets.SgPl,
                Rows = new[]
                {
                    //1 osoba
                    GenerateEntryTableRow(0, LabelPrototypes.Person.First, GetTableCellForms(LexemeForms.Plusquamperf().Pri().Sg()), GetTableCellForms(LexemeForms.Plusquamperf().Pri().Pl()) ),
                    //2 osoba
                    GenerateEntryTableRow(1, LabelPrototypes.Person.Second, GetTableCellForms(LexemeForms.Plusquamperf().Sec().Sg()), GetTableCellForms(LexemeForms.Plusquamperf().Sec().Pl()) ),
                    //3 osoba
                    GenerateEntryTableRow(2, LabelPrototypes.Person.Third, GetTableCellForms(LexemeForms.Plusquamperf().Ter().Sg()), GetTableCellForms(LexemeForms.Plusquamperf().Ter().Pl()) )
                }
            });

            //tryb przypuszcz. w czasie nie przeszłym
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
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

            //tryb przypuszcz. w czasie przeszłym
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
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
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.VerbForms.Infinitive },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Inf()))
                }
            });

            //bezosobnik
            entry.Tables = entry.Tables.Add(new Entry.Table
            {
                Id = entry.Tables.Count(),
                Titles = new[] { LabelPrototypes.VerbForms.Impersonal },
                ColumnHeaders = new[] { LabelPrototypes.EmptyLabel },
                Rows = new[]
                {
                    GenerateEntryTableRow(0, LabelPrototypes.EmptyLabel, GetTableCellForms(LexemeForms.Impers()))
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

                //negacja
                newForm.AddNegationLabel(forms.ToList()[i]);

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
            if (SearchedForm.Categories.Contains("imperf")) aspect.Append("impref");
            if (SearchedForm.Categories.Contains("perf")) aspect.Append("pref");

            return aspect;
        }

        protected virtual void AddAnalyticalForms(string[] auxiliaryVerbs, string newCategory, string additionalCategory = "", bool auxVerbInPreposition = true)
        {
            foreach (var item in LexemeForms.Past().Sg().Ter())
            {
                var newCategories = item.Categories.Replace("praet", newCategory);
                if (additionalCategory != "") newCategories.Append(additionalCategory);
                if (auxiliaryVerbs[0] != "") SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[0], item.Word, auxVerbInPreposition), newCategories.Replace("ter", "pri"));
                if (auxiliaryVerbs[1] != "") SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[1], item.Word, auxVerbInPreposition), newCategories.Replace("pri", "sec"));
                if (auxiliaryVerbs[2] != "") SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[2], item.Word, auxVerbInPreposition), newCategories.Replace("sec", "ter"));
            }

            foreach (var item in LexemeForms.Past().Pl().Ter())
            {
                var newCategories = item.Categories.Replace("praet", newCategory);
                if (additionalCategory != "") newCategories.Append(additionalCategory);
                if (auxiliaryVerbs[3] != "") SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[3], item.Word, auxVerbInPreposition), newCategories.Replace("ter", "pri"));
                if (auxiliaryVerbs[4] != "") SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[4], item.Word, auxVerbInPreposition), newCategories.Replace("pri", "sec"));
                if (auxiliaryVerbs[5] != "") SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[5], item.Word, auxVerbInPreposition), newCategories.Replace("sec", "ter"));
            }
        }

        protected virtual void AddAnalyticalForms(string[] auxiliaryVerbs, Form mainVerb, string oldCat, string newCat, bool auxVerbInPreposition = true)
        {
            var newCategories = mainVerb.Categories;
            string[] persons = new[] { "pri", "sec", "ter", "pri", "sec", "ter" };
            string[] numbers = new[] { "sg", "sg", "sg", "pl", "pl", "pl" };
            for (int i = 0; i < auxiliaryVerbs.Count(); i++)
            {
                SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[i], mainVerb.Word, auxVerbInPreposition), newCategories.Replace(oldCat, newCat).Append(persons[i]).Append(numbers[i]));
            }
        }

        protected virtual void AddAnalyticalForms(string[] auxiliaryVerbs, Form[] mainVerbs, string oldCat, string newCat, bool auxVerbInPreposition = true)
        {
            for (int i = 0; i < auxiliaryVerbs.Count(); i++)
            {
                var newCategories = mainVerbs[i].Categories;
                
                if (oldCat == "") newCategories = newCategories.Add(newCat);
                else newCategories = newCategories.Replace(oldCat, newCat);

                SupplementLexemeForms(JoinAnalyticalForms(auxiliaryVerbs[i], mainVerbs[i].Word, auxVerbInPreposition), newCategories);
            }
        }


        protected string JoinAnalyticalForms(string form1, string form2, bool form12order = true)
        {
            if (form12order) return form1 + " " + form2;
            else return form2 + " " + form1;
        }

        protected virtual IEnumerable<Form> GetFullParadigm(IEnumerable<Form> forms)
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

            var nouns = AdditionalLexemeEqualForms.Where(x => x.Categories.Contains("subst"));
            addRelated(entry, getSgNom(nouns), new[] { LabelPrototypes.Pos.Noun });

            var gerunds = AdditionalLexemeEqualForms.Where(x => x.Categories.Contains("ger"));
            addRelated(entry, getSgNom(gerunds.NotNeg()), new[] { LabelPrototypes.VerbForms.Gerund });
            addRelated(entry, getSgNom(gerunds.Neg()), new[] { LabelPrototypes.VerbForms.Gerund, LabelPrototypes.Derivatives.Neg });


            RelatedAddingCondition = () => SearchedForm.Categories.Contains("pred");
            var categories = new[] { LabelPrototypes.Pos.Noun };
            WordSelector = () => HomonymousForms.Where(x => x.Categories.Contains("subst")).Word();

            AddRelated(entry, RelatedAddingCondition, categories, WordSelector);

            
        }

        protected void AddRelatedParticiples(Entry entry)
        {
            var actPartLab = LabelPrototypes.VerbForms.Participle.Active;
            var pasPartLab = LabelPrototypes.VerbForms.Participle.Passive;
            var conPartLab = LabelPrototypes.VerbForms.Participle.Concurrent;
            var antPartLab = LabelPrototypes.VerbForms.Participle.Anterior;
            var negLab = LabelPrototypes.Derivatives.Neg;

            var actPartic = (LexemeForms.ParticAct().NotNeg(), new[] { actPartLab });
            var negActPartic = (LexemeForms.ParticAct().Neg(), new[] { actPartLab, negLab });

            var pasPartic = (LexemeForms.ParticPas().NotNeg(), new[] { pasPartLab });
            var negPasPartic = (LexemeForms.ParticPas().Neg(), new[] { pasPartLab, negLab });

            var antPartic = (LexemeForms.ParticAnt().NotNeg(), new[] { antPartLab });
            var negAntPartic = (LexemeForms.ParticAnt().Neg(), new[] { antPartLab, negLab });

            var conPartic = (LexemeForms.ParticCon().NotNeg(), new[] { conPartLab });
            var negConPartic = (LexemeForms.ParticCon().Neg(), new[] { conPartLab, negLab });

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
                var mainVerb = LexemeForms.Inf().First();
                AddAnalyticalForms(new[] { "będę", "będziesz", "będzie", "będziemy", "będziecie", "będą" }, mainVerb, "inf", "fut");
            }

            //em pisał, m pisał
            AddAnalyticalForms(new[] { "em", "eś", "", "eśmy", "eście", "" }, "praet", "wok");
            AddAnalyticalForms(new[] { "m", "ś", "", "śmy", "ście", "" }, "praet", "nwok");

            //pisałem był...
            AddAnalyticalForms(_auxiliaryVerbs, GetFullParadigm(LexemeForms.Past()).ToArray(), "praet", "plus", false);

            //bym pisał --do uzupełnienia

            //pisałbym był
            AddAnalyticalForms(_auxiliaryVerbs, GetFullParadigm(LexemeForms.CondNonPast()).ToArray(), "", "praet", false);


            //byłbym pisał --do uzupełnienia

            //bym był pisał --do uzupełnienia

            //niech piszę, niesz pisze, niech piszą            
            var presentSgPri = LexemeForms.Pres().Sg().Pri().Word(); //piszę, mówię, siedzę
            var presentSgTer = LexemeForms.Pres().Sg().Ter().Word(); //pisze, mówi, siedzi
            var presentPlTer = LexemeForms.Pres().Pl().Ter().Word(); //piszą, mówią, siedzą

            SupplementLexemeForms(JoinAnalyticalForms("niech", presentSgPri), new[] { "impt", "sg", "pri" });
            SupplementLexemeForms(JoinAnalyticalForms("niech", presentSgTer), new[] { "impt", "sg", "ter" });
            SupplementLexemeForms(JoinAnalyticalForms("niech", presentPlTer), new[] { "impt", "pl", "ter" });


        }


        private void addRelated(Entry entry, string word, Entry.Label[] categories)
        {
            if (word != "")
            {
                entry.Relateds = entry.Relateds.Add(
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