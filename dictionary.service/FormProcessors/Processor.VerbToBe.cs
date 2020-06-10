using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class VerbToBe : Verb
    {
        public VerbToBe(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase)
        {
            //uzupełnienie o formy rozkaźnika analitycznego (niech będę)
            SupplementLexemeForms("niech będę", new[] { "impt", "sg", "pri", "imperf" });
            SupplementLexemeForms("niech będzie", new[] { "impt", "sg", "ter", "imperf" });
            SupplementLexemeForms("niech jest", new[] { "impt", "sg", "ter", "imperf" });
            SupplementLexemeForms("niech będą", new[] { "impt", "pl", "ter", "imperf" });
            SupplementLexemeForms("niech są", new[] { "impt", "pl", "ter", "imperf" });

            //analityczne formy czasu teraźniejszego (-m jest)
            var aglutynaty = LexemeForms.Agglutinate();
            foreach (var form in aglutynaty)
            {
                var categories = form.Categories.ToList();
                categories.Remove("aglt");
                categories.Prepend("fin");
                SupplementLexemeForms(form.Word + " " + (form.Categories.Contains("sg") ? "jest" : "są"), categories);
            }                                                  
        }
    }
}