using System;
using Xunit;
using Dictionary.Core.Models;
using Dictionary.Data;
using Dictionary.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionary.Data.Tests
{
    public class RepositoryLemma
    {
        private static DatabaseSettings _databaseSetting = new DatabaseSettings
        {
            ConnectionString = "",
            DatabaseName = "dictionary",
            FormsCollectionName = "forms",
            LemmasCollectionName = "lemmas"
        };
        UnitOfWork _unitOfWork = new UnitOfWork(_databaseSetting);

        [Fact]
        public void FindPred_numberOfItemsEquals1()
        {
            var res = _unitOfWork.Lemmas.Find(x => x.Form.Equals("być"));

            var actual = res.Count();
            var expected = 1;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindBrak_firstTagEqualsS()
        {
            var res = _unitOfWork.Lemmas.Find(x => x.Form.Equals("brak"));

            var actual = res.First().Tag.Equals("s");

            Assert.True(actual);
        }

        [Fact]
        public async Task SingleOrDefaultAsyncAbak_tagIsEmpty()
        {
            var res = await _unitOfWork.Lemmas
                .FirstOrDefaultAsync(x => x.Form.Equals("abak"));


            var actual = res.Tag;
            var expected = "";

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task GetAllAsync_countForms()
        {
            var res = await _unitOfWork.Lemmas
                .GetAllAsync();


            var actual = res.Count();
            var expected = 385_000;

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }



        [Fact]
        public async Task GetByIdAsync_lemmaEqualsByc()
        {
            var id = @"5de40d7131bbd63bc3f75b5e";

            var res = await _unitOfWork.Lemmas
                .GetByIdAsync(id);


            var actual = res.Form;
            var expected = "abak";

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }



        [Fact]
        public async Task FindAsyncLemmaByc_resultsEqual2()
        {
            var form = "brak";

            var res = await _unitOfWork.Lemmas
                .FindAsync(x => x.Form.Equals(form));


            var actual = res.Count();
            var expected = 2;

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task FindAsyncLemmaByc_lemmaEqualsByc()
        {
            var form = "być";

            var res = await _unitOfWork.Lemmas
                .FindAsync(x => x.Form.Equals(form));

            var actual = res.First().Form;
            var expected = "być";

            Assert.Equal(expected, actual);

        }


        [Fact]
        public async Task FindAsyncLemmaByc_numberOfLemmasEquals1()
        {
            var form = "być";

            var res = await _unitOfWork.Lemmas
                .FindAsync(x => x.Form.Equals(form));

            var actual = res.Count();
            var expected = 1;

            Assert.Equal(expected, actual);

        }








    }
}
