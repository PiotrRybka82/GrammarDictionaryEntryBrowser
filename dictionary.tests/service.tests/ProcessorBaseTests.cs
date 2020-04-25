using System;
using Xunit;
using Dictionary.Service.FormProcessors;
using Dictionary.Core.Models;
using Dictionary.Data;
using Dictionary.Service.Services;
using System.Linq;
using System.Collections.Generic;
using Dictionary.Core;

namespace Dictionary.Service.Tests
{
    internal class VerbForTests : Verb
    {
        public VerbForTests(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase) 
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase) { }

        public Entry InitializeEntry(int index) => base.InitializeEntry(index);

        public new void AddGeneralLabels(Entry entry) { base.AddGeneralLabels(entry); }

        public new void AddParadigmSpecificGeneralLabels(Entry entry) { base.AddParadigmSpecificGeneralLabels(entry); }

    }

    internal class DictionaryServiceForTesting : DictionaryService
    {
        public DictionaryServiceForTesting(IUnitOfWork unitOfWork, string formQueryUrlBase): base(unitOfWork, formQueryUrlBase) { }

        public IEnumerable<Form> GroupFormsByLemmas(IEnumerable<Form> forms) => GroupForms(forms);

    }

    internal class ProcessorBaseForTesting : ProcessorBase
    {
        public new void AddGeneralLabels(Entry entry)
        {
            base.AddGeneralLabels(entry);
        }

        

        public new Entry.Label GetPos()
        {
            return base.GetPos();
        }

        public new Entry InitializeEntry(int index, string lemma = "")
        {
            return base.InitializeEntry(index, lemma);
        }

        public ProcessorBaseForTesting(Form searchedForm, IEnumerable<Form> lexemeForms, IEnumerable<Form> homonymousForms, string formQueryUrlBase) 
            : base(searchedForm, lexemeForms, homonymousForms, formQueryUrlBase)
        { }


        protected override void AddTables(Entry entry)
        {
            throw new NotImplementedException();
        }

        protected override void AddRelateds(Entry entry)
        {
            throw new NotImplementedException();
        }

        protected override IEnumerable<Entry.Form> GetTableCellForms(IEnumerable<Form> forms)
        {
            throw new NotImplementedException();
        }

        protected override void AddParadigmSpecificGeneralLabels(Entry entry)
        {
            
        }

        protected override void CorrectEntry(Entry entry)
        {
            throw new NotImplementedException();
        }
    }


    public class ProcessorBaseTests
    {
        private static DatabaseSettings _databaseSetting = new DatabaseSettings
        {
            ConnectionString = "",
            DatabaseName = "dictionary",
            FormsCollectionName = "forms",
            LemmasCollectionName = "lemmas"
        };
        UnitOfWork _unitOfWork = new UnitOfWork(_databaseSetting);
        ProcessorBaseForTesting _processorBase = new ProcessorBaseForTesting(null, null, null, "");

        #region GetParadigm


        [Fact]
        public void GetParadigm_indeclinable()
        {
            var word = "a";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var paradigm = _processorBase.GetParadigm();

            var actual = paradigm;
            var expected = Paradigm.indeclinable;

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetParadigm_noun()
        {
            var word = "pies";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var paradigm = _processorBase.GetParadigm();

            var actual = paradigm;
            var expected = Paradigm.noun;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetParadigm_adjective()
        {
            var word = "duży";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var paradigm = _processorBase.GetParadigm();

            var actual = paradigm;
            var expected = Paradigm.adj;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetParadigm_verbToBe()
        {
            var word = "jesteś";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var paradigm = _processorBase.GetParadigm();

            var actual = paradigm;
            var expected = Paradigm.v_tobe;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetParadigm_verb()
        {
            var word = "pisać";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var paradigm = _processorBase.GetParadigm();

            var actual = paradigm;
            var expected = Paradigm.v;

            Assert.Equal(expected, actual);
        }

        #endregion


        #region GroupForms

        [Fact]
        public void GroupForms_byc()
        {
            var word = "być";

            var forms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));

            var groups = new DictionaryServiceForTesting(_unitOfWork, "").GroupFormsByLemmas(forms);
            
            var count = groups.Count();

            var actual = count >= 2;

            Assert.True(actual);
        }


        [Fact]
        public void GroupForms_a()
        {
            var word = "aa";

            var forms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));

            var groups = new DictionaryServiceForTesting(_unitOfWork, "").GroupFormsByLemmas(forms);

            var actual = groups.Count() == 1;

            Assert.True(actual);
        }





        #endregion


        #region InitializeEntry

        [Fact]
        public void InitializeEntryAbakan_lemmaEqualsAbakan()
        {
            var word = "abakan";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var entry = _processorBase.InitializeEntry(0);

            var actual = entry.Lemma;
            var expected = word;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeEntryAbakan_posEqualsNoun()
        {
            var word = "abakan";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var entry = _processorBase.InitializeEntry(0);

            var actual = entry.Pos.ValueFull;
            var expected = LabelPrototypes.Pos.Noun.ValueFull;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeEntryAbakan_paradigmEqualsNoun()
        {
            var word = "abakan";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var entry = _processorBase.InitializeEntry(0);

            var actual = entry.Paradigm;
            var expected = Paradigm.noun.ToString(); ;

            Assert.Equal(expected, actual);
        }



        #endregion


        #region GetPos

        [Fact]
        public void GetPosPies_posEqualsNoun()
        {
            var word = "pies";

            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var pos = _processorBase.GetPos();

            var actual = pos.ValueFull;
            var expected = LabelPrototypes.Pos.Noun.ValueFull;

            Assert.Equal(expected, actual);
        }

        #endregion


        #region AddGeneralLabels

        [Fact]
        public void AddGeneralLabelsPies_meaningsEqualNazwaPospolita()
        {
            var word = "pies";
            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            _processorBase = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var entry = _processorBase.InitializeEntry(0);
            _processorBase.AddGeneralLabels(entry);

            var actual = entry.Meanings.First();
            var expected = "nazwa_pospolita";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AddGeneralLabelsAbbami_labelsEqualsDaw()
        {
            var word = "abbami";
            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            var processor = new ProcessorBaseForTesting(searchedForm, lexemeForms, homonymousForms, "");

            var entry = processor.InitializeEntry(0);
            processor.AddGeneralLabels(entry);
            //processor.AddParadigmSpecificGeneralLabels(entry);

            var actual = entry.Labels.First().ValueAbbr;
            var expected = "daw.";

            Assert.Equal(expected, actual);
        }


        #endregion


        #region AddParadigmSpecificGeneralLabels

        [Fact]
        public void AddParadigmSpecificGeneralLabels_()
        {
            var word = "pisać";
            var homonymousForms = _unitOfWork.Forms.Find(x => x.Word.Equals(word));
            var searchedForm = homonymousForms.First();
            var lexemeForms = _unitOfWork.Forms.Find(x => x.Lemma.Form.Equals(searchedForm.Lemma.Form));

            var processor = new VerbForTests(searchedForm, lexemeForms, homonymousForms, "");

            var entry = processor.InitializeEntry(0);
            processor.AddGeneralLabels(entry);
            processor.AddParadigmSpecificGeneralLabels(entry);

            var actual = entry.Labels.First().ValueAbbr;
            var expected = LabelPrototypes.Aspect.Imperf.ValueAbbr;

            Assert.Equal(expected, actual);
        }

        #endregion




    }
}
