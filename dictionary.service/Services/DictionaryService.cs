using System.Collections.Generic;
using System.Threading.Tasks;
using Dictionary.Core;
using Dictionary.Core.Models;
using Dictionary.Core.Services;
using Dictionary.Service.FormProcessors;
using System.Linq;

namespace Dictionary.Service.Services
{
    public class DictionaryService : IDictionary
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly string _formQueryUrlBase = "";

        

        public DictionaryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DictionaryService(IUnitOfWork unitOfWork, string formQueryUrlBase)
        {
            _unitOfWork = unitOfWork;
            _formQueryUrlBase = formQueryUrlBase;
        }




        public IEnumerable<Entry> GetEntries(string form)
        {
            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(form));
            
            var entrySeedForms = GroupForms(homonymousForms).ToList();
            
            var entries = new List<Entry>();

            for (int i = 0; i < entrySeedForms.Count(); i++)
            {
                var lexemeForms = findAllFormsOfEqualLemma(entrySeedForms[i]);

                var processor = getFormProcessor(entrySeedForms[i], lexemeForms, homonymousForms, _formQueryUrlBase);
                
                yield return processor.GetEntry(i);
            }

            
        }
        
        public IEnumerable<Entry> GetEntries(string form, IEnumerable<string> categories = null, bool useRegEx = false)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Entry>> GetEntriesAsync(string form)
        {
            return new Task<IEnumerable<Entry>>(() => GetEntries(form));
        }

        public Task<IEnumerable<Entry>> GetEntriesAsync(string form, IEnumerable<string> categories = null, bool useRegEx = false)
        {
            throw new System.NotImplementedException();
        }



        protected IEnumerable<Form> GroupForms(IEnumerable<Form> foundForms)
        {
            var groups = foundForms.GroupBy(form => form.Lemma, form => form, new LemmaEqualityComparer());

            foreach (var group in groups)
            {
                var firstCategories = group.Select(x => x.Categories.First()).Distinct();

                if (firstCategories.Count() == 1) //wszystkie formy o tym samym lemacie maj¹ tê sam¹ kategoriê g³ówn¹
                {
                    yield return group.First(); //wystarczy zwróciæ pierwsz¹ formê
                }
                else //formy o tym samym lemacie maj¹ inne kategorie g³ówne - zgrupuj je wg tej kategorii
                {
                    //wyj¹tki

                    //formy adv i adja -- osobno notowany przys³ówek i przys³ówek odprzymiotnikowy
                    if (foundForms.SelectMany(x => x.Categories).Contains("adv") && foundForms.SelectMany(x => x.Categories).Contains("adja"))
                    {
                        yield return group.First();
                        yield break;
                    }

                    var subgroups = group.GroupBy(form => form.Categories.First());

                    foreach (var subgroup in subgroups) yield return subgroup.First();
                }

            }

        }




        #region private auxiliary methods

        private IEnumerable<Form> findAllFormsOfEqualLemma(Form formSeed)
        {
            return _unitOfWork
                .Forms
                .Find(x => x.Lemma.Form.Equals(formSeed.Lemma.Form) && x.Lemma.Tag.Equals(formSeed.Lemma.Tag));
        }

        

        private ProcessorBase getFormProcessor(Form entrySeed, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase)
        {
            ProcessorBase processor = new Indeclinable(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase);

            var paradigm = processor.GetParadigm();

            switch (paradigm)
            {
                case Paradigm.adj:          return new Adj(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.adv:          return new Adv(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.noun:         return new Noun(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.num:          return new Num(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.pron12:       return new Pron12(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.pron3:        return new Pron3(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.v_pred:       return new VerbPred(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.v_tobe:       return new VerbToBe(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.v_winien:     return new VerbWinien(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                case Paradigm.v:            return new Verb(entrySeed, lexemeForms, homonymousForms, formQueryUrlBase); 
                default: break;
            }

            return processor;
        }

        #endregion


    }
}