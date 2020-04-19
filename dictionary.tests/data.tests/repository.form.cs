using System;
using Xunit;
using Dictionary.Core.Models;
using Dictionary.Data;
using Dictionary.Data.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Dictionary.Data.Tests
{
    public class RepositoryForm
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
        public void FindPred_numberOfItemsEquals30()
        {
            var res = _unitOfWork.Forms.Find(x => x.Categories.Contains("pred"));

            var actual = res.Count();
            var expected = 30;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FindBrak_firstCategoryEqualsPred()
        {
            var res = _unitOfWork.Forms.Find(x => x.Word.Equals("brak"));

            var actual = res.SelectMany(x => x.Categories).Contains("pred");

            Assert.True(actual);
        }

        [Fact]
        public async Task SingleOrDefaultAsyncByc_firstCategoryEqualsImpt()
        {
            var res = await _unitOfWork.Forms
                .FirstOrDefaultAsync(x =>
                    x.Lemma.Form.Equals("być"));


            var actual = res.Categories.ToList()[0];
            var expected = "impt";

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task SingleOrDefaultAsyncCategoriesContainsPred_wordEqualsBrak()
        {
            var res = await _unitOfWork.Forms
                .FirstOrDefaultAsync(x =>
                    x.Categories.Contains("pred"));


            var actual = res.Word;
            var expected = "brak";

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task GetByIdAsync_lemmaEqualsByc()
        {
            var id = @"5de40e7031bbd63bc3025904";

            var res = await _unitOfWork.Forms
                .GetByIdAsync(id);


            var actual = res.Lemma.Form;
            var expected = "być";

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }



        [Fact]
        public async Task FindAsyncLemmaByc_resultsEqual260()
        {
            var form = "być";

            var res = await _unitOfWork.Forms
                .FindAsync(x => x.Lemma.Form.Equals(form));


            var actual = res.Count();
            var expected = 260;

            System.Console.WriteLine($"Actual: {actual}");
            System.Console.WriteLine($"Expected: {expected}");

            Assert.Equal(expected, actual);
        }


        [Fact]
        public async Task FindAsyncFormJestem_lemmaEqualsByc()
        {
            var form = "jestem";

            var res = await _unitOfWork.Forms
                .FindAsync(x => x.Word.Equals(form));

            var actual = res.First().Lemma.Form;
            var expected = "być";

            Assert.Equal(expected, actual);

        }








    }
}
