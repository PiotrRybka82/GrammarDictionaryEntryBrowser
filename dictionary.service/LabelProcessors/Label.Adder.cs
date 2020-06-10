using System;
using System.Linq;
using System.Collections.Generic;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    public static class LabelAdder
    {
        private static Entry.Form addStyleLabel(Entry.Form entryForm, Form formFromDb, string ifContains, string valueFull, string valueAbbr = "", string description = "")
        {
            if (formFromDb.Labels == null) return entryForm;

            if (formFromDb.Labels.Contains(ifContains))
            {
                if (entryForm.Categories == null) entryForm.Categories = new List<Entry.Label>();

                var newEntryLabel = new Entry.Label
                {
                    Id = entryForm.Categories.Count(),
                    Name = "styl",
                    Description = description,
                    ValueAbbr = string.IsNullOrEmpty(valueAbbr) ? ifContains : valueAbbr,
                    ValueFull = valueFull
                };

                entryForm.Categories = entryForm.Categories.Add(newEntryLabel);
            }

            return entryForm;

        }

        public static void AddAspectGeneralLabels(this Entry entry, IEnumerable<Form> formsFromDb)
        {
            if (formsFromDb.SelectMany(x => x.Categories).Contains("imperf"))
            {
                entry.Labels = entry.Labels.Add(LabelPrototypes.Aspect.Imperf);
            }
            else if (formsFromDb.SelectMany(x => x.Categories).Contains("perf"))
            {
                entry.Labels = entry.Labels.Add(LabelPrototypes.Aspect.Perf);
            }
        }

        public static void AddAdjcLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("adjc"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Other.Pewien);
            }
        }

        public static void AddAccentedLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("nakc"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Accentedness.NAcc);
            }
            else if (formFromDb.Categories.Contains("akc"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Accentedness.Acc);
            }
        }

        public static void AddPostprepositionLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("npraep"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Postprepositionness.Npraep);
            }
            else if (formFromDb.Categories.Contains("praep"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Postprepositionness.Praep);
            }
        }

        public static void AddGenderLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("m1"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Gender.Masculine1);
            }
            else if (formFromDb.Categories.Contains("m2"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Gender.Masculine2);
            }
            else if (formFromDb.Categories.Contains("m3"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Gender.Masculine3);
            }
            else if (formFromDb.Categories.Contains("f"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Gender.Feminine);
            }
            else if (formFromDb.Categories.Contains("n"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Gender.Neutral);
            }
        }

        public static void AddUniformityLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Labels.Contains("hom.") || formFromDb.Categories.Contains("hom."))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Uniformity.Uniform);
            }
            else if (formFromDb.Labels.Contains("char.") || formFromDb.Categories.Contains("char."))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Uniformity.NotUniform);
            }
        }

        public static void AddDeprecativeLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("depr"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Other.Deprecating);
            }
        }

        public static void AddStyleLabels(this Entry.Form entryForm, Form formFromDb)
        {
            entryForm = addStyleLabel(entryForm, formFromDb, "daw.", "dawne");
            entryForm = addStyleLabel(entryForm, formFromDb, "rzad.", "rzadkie");
            entryForm = addStyleLabel(entryForm, formFromDb, "przest.", "przestarzałe");
            entryForm = addStyleLabel(entryForm, formFromDb, "pot.", "potoczne");
            entryForm = addStyleLabel(entryForm, formFromDb, "arch.", "archaiczne");
            entryForm = addStyleLabel(entryForm, formFromDb, "hist.", "historyczne");
            entryForm = addStyleLabel(entryForm, formFromDb, "przen.", "przenośne");
            entryForm = addStyleLabel(entryForm, formFromDb, "żart.", "żartobliwe");
            entryForm = addStyleLabel(entryForm, formFromDb, "daw._dziś_gwar.", "dawne, obecnie gwarowe", "daw., dziś gwar.");
            entryForm = addStyleLabel(entryForm, formFromDb, "arch._(tylko_po_\"ku\")", "archaiczne, tylko po przyimku »ku«", "arch., tylko po »ku«");
            entryForm = addStyleLabel(entryForm, formFromDb, "przest._dziś_książk.", "przestarzałe, obecnie książkowe", "przest., dziś książk.");
            entryForm = addStyleLabel(entryForm, formFromDb, "gwar.", "gwarowe");
            entryForm = addStyleLabel(entryForm, formFromDb, "pogard.", "pogardliwe");
            entryForm = addStyleLabel(entryForm, formFromDb, "książk.", "książkowe");
            entryForm = addStyleLabel(entryForm, formFromDb, "erud.", "erudycyjne");
            entryForm = addStyleLabel(entryForm, formFromDb, "reg.", "regionalne");
            entryForm = addStyleLabel(entryForm, formFromDb, "indyw.", "indywidualne");
            entryForm = addStyleLabel(entryForm, formFromDb, "lekcew.", "lekceważące");
            entryForm = addStyleLabel(entryForm, formFromDb, "fraz.", "frazeologizm");
            entryForm = addStyleLabel(entryForm, formFromDb, "środ.", "środowiskowe");
            entryForm = addStyleLabel(entryForm, formFromDb, "wulg.", "wulgarne");
            entryForm = addStyleLabel(entryForm, formFromDb, "niepopr.", "niepoprawne");
            entryForm = addStyleLabel(entryForm, formFromDb, "iron.", "ironiczne");
            entryForm = addStyleLabel(entryForm, formFromDb, "slang", "slangowe");
            entryForm = addStyleLabel(entryForm, formFromDb, "rub.", "rubaszne");
            entryForm = addStyleLabel(entryForm, formFromDb, "euf.", "eufemistyczne");
            entryForm = addStyleLabel(entryForm, formFromDb, "daw._dziś_środ.", "dawne, obecnie środowiskowe", "daw., dziś środ.");
            entryForm = addStyleLabel(entryForm, formFromDb, "poet.", "poetyckie");
            entryForm = addStyleLabel(entryForm, formFromDb, "form.", "formalne");
            entryForm = addStyleLabel(entryForm, formFromDb, "daw._dziś_rzad.", "dawne, obecnie rzadkie", "daw., dziś rzad.");
            entryForm = addStyleLabel(entryForm, formFromDb, "pisane_łącznie_z_przyimkiem", "pisane łącznie z przyimkiem", "pis. łącz. z przyim.");
            entryForm = addStyleLabel(entryForm, formFromDb, "daw._dziś_fraz.", "dawne, obecnie we frazeologizmie", "daw., dziś fraz.");
            entryForm = addStyleLabel(entryForm, formFromDb, "przest._dziś_gwar.", "przestarzałe, obecnie gwarowe", "przest., dziś gwar.");
            entryForm = addStyleLabel(entryForm, formFromDb, "po_liczebniku", "po liczebniku", "po liczeb.");
            entryForm = addStyleLabel(entryForm, formFromDb, "podniosłe", "podniosłe", "podn.");


        }

        public static void AddDegreeLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("pos"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Degree.Positive);
            }
            else if (formFromDb.Categories.Contains("com"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Degree.Comparative);
            }
            else if (formFromDb.Categories.Contains("sup"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Degree.Superlative);
            }
        }

        public static void AddCongrencyLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("congr"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Congruence.Congruent);
            }
            else if (formFromDb.Categories.Contains("rec"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Congruence.Rection);
            }
        }

        public static void AddVocalityLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("wok"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Vocality.Vocalic);
            }
            else if (formFromDb.Categories.Contains("nwok"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Vocality.NonVocalic);
            }
        }

        public static void AddFormStructureLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Word.Contains(" "))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.FormStructure.Analytic);
            }
            else
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.FormStructure.Synthetic);
            }
        }

        public static void AddNegationLabel(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Word.Contains("neg"))
            {
                entryForm.Categories = entryForm.Categories.Add(LabelPrototypes.Derivatives.Neg);
            }
        }
    }
}