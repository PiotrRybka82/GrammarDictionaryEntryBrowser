using System;
using System.Linq;
using System.Collections.Generic;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    public static class LabelAdder
    {


        private static void addStyleLabel(this Entry.Form entryForm, Form formFromDb, string ifContains, string valueFull, string valueAbbr = "", string description = "")
        {

            if (formFromDb.Categories.Contains(ifContains) && formFromDb.Labels.Contains(ifContains))
            {
                entryForm.Categories.Append(new Entry.Label
                {
                    Id = 0,
                    Name = "styl",
                    Description = description,
                    ValueAbbr = string.IsNullOrWhiteSpace(valueAbbr) ? ifContains : valueAbbr,
                    ValueFull = valueFull
                });
            }
        }



        public static void AddAspectGeneralLabels(this Entry entry, IEnumerable<Form> formsFromDb)
        {
            if (formsFromDb.SelectMany(x => x.Categories).Contains("imperf"))
            {
                entry.Labels.Append(LabelPrototypes.Aspect.Imperf);
            }
            else if (formsFromDb.SelectMany(x => x.Categories).Contains("perf"))
            {
                entry.Labels.Append(LabelPrototypes.Aspect.Perf);
            }
        }


        public static void AddAdjcLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("adjc"))
            {
                entryForm.Categories.Append(LabelPrototypes.Other.Pewien);
            }
        }

        public static void AddAccentedLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("nakc"))
            {
                entryForm.Categories.Append(LabelPrototypes.Accentedness.NAcc);
            }
            else if (formFromDb.Categories.Contains("akc"))
            {
                entryForm.Categories.Append(LabelPrototypes.Accentedness.Acc);
            }
        }

        public static void AddPostprepositionLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("npraep"))
            {
                entryForm.Categories.Append(LabelPrototypes.Postprepositionness.Npraep);
            }
            else if (formFromDb.Categories.Contains("praep"))
            {
                entryForm.Categories.Append(LabelPrototypes.Postprepositionness.Praep);
            }
        }

        public static void AddGenderLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("m1"))
            {
                entryForm.Categories.Append(LabelPrototypes.Gender.Masculine1);
            }
            else if (formFromDb.Categories.Contains("m2"))
            {
                entryForm.Categories.Append(LabelPrototypes.Gender.Masculine2);
            }
            else if (formFromDb.Categories.Contains("m3"))
            {
                entryForm.Categories.Append(LabelPrototypes.Gender.Masculine3);
            }
            else if (formFromDb.Categories.Contains("f"))
            {
                entryForm.Categories.Append(LabelPrototypes.Gender.Feminine);
            }
            else if (formFromDb.Categories.Contains("n"))
            {
                entryForm.Categories.Append(LabelPrototypes.Gender.Neutral);
            }
        }

        public static void AddUniformityLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Labels.Contains("hom.") || formFromDb.Categories.Contains("hom."))
            {
                entryForm.Categories.Append(LabelPrototypes.Uniformity.Uniform);
            }
            else if (formFromDb.Labels.Contains("char.") || formFromDb.Categories.Contains("char."))
            {
                entryForm.Categories.Append(LabelPrototypes.Uniformity.NotUniform);
            }
        }

        public static void AddDeprecativeLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("depr"))
            {
                entryForm.Categories.Append(LabelPrototypes.Other.Deprecating);
            }
        }

        public static void AddStyleLabels(this Entry.Form entryForm, Form formFromDb)
        {
            entryForm.addStyleLabel(formFromDb, "daw.", "dawne");
            entryForm.addStyleLabel(formFromDb, "rzad.", "rzadkie");
            entryForm.addStyleLabel(formFromDb, "przest.", "przestarzałe");
            entryForm.addStyleLabel(formFromDb, "pot.", "potoczne");
            entryForm.addStyleLabel(formFromDb, "arch.", "archaiczne");
            entryForm.addStyleLabel(formFromDb, "hist.", "historyczne");
            entryForm.addStyleLabel(formFromDb, "przen.", "przenośne");
            entryForm.addStyleLabel(formFromDb, "żart.", "żartobliwe");
            entryForm.addStyleLabel(formFromDb, "daw._dziś_gwar.", "dawne, obecnie gwarowe", "daw., dziś gwar.");
            entryForm.addStyleLabel(formFromDb, "arch._(tylko_po_\"ku\")", "archaiczne, tylko po przyimku »ku«", "arch., tylko po »ku«");
            entryForm.addStyleLabel(formFromDb, "przest._dziś_książk.", "przestarzałe, obecnie książkowe", "przest., dziś książk.");
            entryForm.addStyleLabel(formFromDb, "gwar.", "gwarowe");
            entryForm.addStyleLabel(formFromDb, "pogard.", "pogardliwe");
            entryForm.addStyleLabel(formFromDb, "książk.", "książkowe");
            entryForm.addStyleLabel(formFromDb, "erud.", "erudycyjne");
            entryForm.addStyleLabel(formFromDb, "reg.", "regionalne");
            entryForm.addStyleLabel(formFromDb, "indyw.", "indywidualne");
            entryForm.addStyleLabel(formFromDb, "lekcew.", "lekceważące");
            entryForm.addStyleLabel(formFromDb, "fraz.", "frazeologizm");
            entryForm.addStyleLabel(formFromDb, "środ.", "środowiskowe");
            entryForm.addStyleLabel(formFromDb, "wulg.", "wulgarne");
            entryForm.addStyleLabel(formFromDb, "niepopr.", "niepoprawne");
            entryForm.addStyleLabel(formFromDb, "iron.", "ironiczne");
            entryForm.addStyleLabel(formFromDb, "slang", "slangowe");
            entryForm.addStyleLabel(formFromDb, "rub.", "rubaszne");
            entryForm.addStyleLabel(formFromDb, "euf.", "eufemistyczne");
            entryForm.addStyleLabel(formFromDb, "daw._dziś_środ.", "dawne, obecnie środowiskowe", "daw., dziś środ.");
            entryForm.addStyleLabel(formFromDb, "poet.", "poetyckie");
            entryForm.addStyleLabel(formFromDb, "form.", "formalne");
            entryForm.addStyleLabel(formFromDb, "daw._dziś_rzad.", "dawne, obecnie rzadkie", "daw., dziś rzad.");
            entryForm.addStyleLabel(formFromDb, "pisane_łącznie_z_przyimkiem", "pisane łącznie z przyimkiem", "pis. łącz. z przyim.");
            entryForm.addStyleLabel(formFromDb, "daw._dziś_fraz.", "dawne, obecnie we frazeologizmie", "daw., dziś fraz.");
            entryForm.addStyleLabel(formFromDb, "przest._dziś_gwar.", "przestarzałe, obecnie gwarowe", "przest., dziś gwar.");
            entryForm.addStyleLabel(formFromDb, "po_liczebniku", "po liczebniku", "po liczeb.");
            entryForm.addStyleLabel(formFromDb, "podniosłe", "podniosłe", "podn.");


        }

        public static void AddDegreeLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("pos"))
            {
                entryForm.Categories.Append(LabelPrototypes.Degree.Positive);
            }
            else if (formFromDb.Categories.Contains("com"))
            {
                entryForm.Categories.Append(LabelPrototypes.Degree.Comparative);
            }
            else if (formFromDb.Categories.Contains("sup"))
            {
                entryForm.Categories.Append(LabelPrototypes.Degree.Superlative);
            }
        }

        public static void AddCongrencyLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("congr"))
            {
                entryForm.Categories.Append(LabelPrototypes.Congruence.Congruent);
            }
            else if (formFromDb.Categories.Contains("rec"))
            {
                entryForm.Categories.Append(LabelPrototypes.Congruence.Rection);
            }
        }

        public static void AddVocalityLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Categories.Contains("wok"))
            {
                entryForm.Categories.Append(LabelPrototypes.Vocality.Vocalic);
            }
            else if (formFromDb.Categories.Contains("nwok"))
            {
                entryForm.Categories.Append(LabelPrototypes.Vocality.NonVocalic);
            }
        }

        public static void AddFormStructureLabels(this Entry.Form entryForm, Form formFromDb)
        {
            if (formFromDb.Word.Contains(" "))
            {
                entryForm.Categories.Append(LabelPrototypes.FormStructure.Analytic);
            }
            else
            {
                entryForm.Categories.Append(LabelPrototypes.FormStructure.Synthetic);
            }
        }



    }
}