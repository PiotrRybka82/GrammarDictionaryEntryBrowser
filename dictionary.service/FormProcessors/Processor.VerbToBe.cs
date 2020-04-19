using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Dictionary.Core.Models;

namespace Dictionary.Service.FormProcessors
{
    internal class VerbToBe : Verb
    {
        public VerbToBe(IEnumerable<Form> forms, Form searchedForm, string formQueryUrlBase) : base(forms, searchedForm, formQueryUrlBase)
        {
            //uzupełnienie o formy rozkaźnika analitycznego (niech będę)
            SupplementForms("niech będę", new[] { "impt", "sg", "pri", "imperf" });
            SupplementForms("niech będzie", new[] { "impt", "sg", "ter", "imperf" });
            SupplementForms("niech jest", new[] { "impt", "sg", "ter", "imperf" });
            SupplementForms("niech będą", new[] { "impt", "pl", "ter", "imperf" });
            SupplementForms("niech są", new[] { "impt", "pl", "ter", "imperf" });

            //analityczne formy czasu teraźniejszego (-m jest)
            var aglutynaty = Forms.Agglutinate();
            foreach (var form in aglutynaty)
            {
                var categories = form.Categories.ToList();
                categories.Remove("aglt");
                categories.Prepend("fin");
                SupplementForms(form.Word + " " + (form.Categories.Contains("sg") ? "jest" : "są"), categories);
            }


            //analityczne formy czasu przeszłego (-m był)
            // var aglt_sgs = Forms.Agglutinate().Sg();
            // var praet_ter_sgs = Forms.Agl() == null ? Forms.Past().Ter().Sg() : Forms.Agl().Sg();

            // foreach (var aglt_sg in aglt_sgs)
            // {
            //     foreach (var praet_ter_sg in praet_ter_sgs)
            //     {
            //         var categories = aglt_sg.Categories.ToList(); //aglt+sg+pri+imperf+wok
            //         categories.AddRange(praet_ter_sg.Categories); //aglt+sg+pri+imperf+wok+praet+sg+m1+pri|imperf+imperf|agl
            //         categories.Remove("aglt"); //sg+pri+imperf+wok+praet+sg+m1+pri|imperf+imperf|agl
            //         categories = categories.Distinct().ToList(); //sg+pri+imperf+wok+praet+m1+agl

            //         SupplementForms(aglt_sg.Word + " " + praet_ter_sg.Word, categories);
            //     }
            // }

            // var aglt_pls = Forms.Agglutinate().Pl();
            // var praet_ter_pls = Forms.Agl() == null ? Forms.Past().Ter().Pl() : Forms.Agl().Pl();

            // foreach (var aglt_pl in aglt_pls)
            // {
            //     foreach (var praet_ter_pl in praet_ter_pls)
            //     {
            //         var categories = aglt_pl.Categories.ToList();
            //         categories.AddRange(praet_ter_pl.Categories);
            //         categories.Remove("aglt");
            //         categories = categories.Distinct().ToList();

            //         SupplementForms(aglt_pl.Word + " " + praet_ter_pl.Word, categories);
            //     }
            // }


            //analityczne formy trybu przypuszczającego w czasie nieprzeszłym (bym był)
            //analityczne formy trybu przypuszczającego w czasie przeszłym (byłbym był)

            // aglt_sgs = Forms.Agglutinate().Sg().NVocal();

            // foreach (var praet_ter_sg in praet_ter_sgs)
            // {
            //     foreach (var aglt_sg in aglt_sgs)
            //     {
            //         var categories = aglt_sg.Categories.ToList(); //aglt+sg+pri+imperf+wok
            //         categories.AddRange(praet_ter_sg.Categories); //aglt+sg+pri+imperf+wok+praet+sg+m1+pri|imperf+imperf|agl
            //         categories.Remove("aglt"); //sg+pri+imperf+wok+praet+sg+m1+pri|imperf+imperf|agl
            //         categories.Remove("nwok"); //sg+pri+imperf+praet+sg+m1+pri|imperf+imperf|agl
            //         categories.Remove("praet"); //sg+pri+imperf+sg+m1+pri|imperf+imperf|agl
            //         categories.Add("cond"); //sg+pri+imperf+sg+m1+pri|imperf+imperf|agl+cond
            //         categories = categories.Distinct().ToList(); //sg+pri+imperf+m1+agl+cond

            //         //tryb przyp. w czasie nieprzeszłym (bym był)
            //         SupplementForms("by" + aglt_sg.Word + " " + praet_ter_sg.Word, categories);
            //         //tryb przyp. w czasie przeszłym (byłbym był)
            //         categories.Add("praet");
            //         SupplementForms(praet_ter_sg.Word + "by" + aglt_sg.Word + " " + praet_ter_sg.Word, categories);
            //     }

            //     {
            //         var categories = praet_ter_sg.Categories.ToList();
            //         categories.Remove("praet");
            //         categories.Add("cond");

            //         //tryb przyp. w czasie nieprzeszłym (by był)
            //         SupplementForms("by" + " " + praet_ter_sg.Word, categories); //praet+sg+m1+pri|imperf+imperf|agl

            //         //tryb przyp. w czasie przeszłym (byłby był)
            //         categories.Add("praet");
            //         SupplementForms(praet_ter_sg.Word + "by" + " " + praet_ter_sg.Word, categories); //praet+sg+m1+pri|imperf+imperf|agl
            //     }
            // }

            // aglt_pls = Forms.Agglutinate().Pl().NVocal();

            // foreach (var praet_ter_pl in praet_ter_pls)
            // {
            //     foreach (var aglt_pl in aglt_pls)
            //     {
            //         var categories = aglt_pl.Categories.ToList();
            //         categories.AddRange(praet_ter_pl.Categories);
            //         categories.Remove("aglt");
            //         categories.Remove("nwok");
            //         categories.Remove("praet");
            //         categories.Add("cond");
            //         categories = categories.Distinct().ToList();

            //         SupplementForms("by" + aglt_pl.Word + " " + praet_ter_pl.Word, categories);

            //         categories.Add("praet");
            //         SupplementForms(praet_ter_pl + "by" + aglt_pl.Word + " " + praet_ter_pl.Word, categories);
            //     }

            //     {
            //         var categories = praet_ter_pl.Categories.ToList();
            //         categories.Remove("praet");
            //         categories.Add("cond");
            //         SupplementForms("by" + " " + praet_ter_pl.Word, categories);

            //         categories.Add("praet");
            //         SupplementForms(praet_ter_pl + "by" + " " + praet_ter_pl.Word, categories);
            //     }

            // }





        }



    }
}