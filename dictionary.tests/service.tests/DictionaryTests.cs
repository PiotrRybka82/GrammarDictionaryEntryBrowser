using System;
using Xunit;
using Dictionary.Service.FormProcessors;
using Dictionary.Core.Models;
using Dictionary.Data;
using Dictionary.Service.Services;
using System.Linq;
using System.Collections.Generic;

namespace Dictionary.Service.Tests
{
    public class DictionaryServiceForTests : DictionaryService
    {
        public DictionaryServiceForTests(UnitOfWork unitOfWork) : base(unitOfWork) { }
        public DictionaryServiceForTests(UnitOfWork unitOfWork, string formQueryUrlBase) : base(unitOfWork, formQueryUrlBase) { }

        public IEnumerable<Form> GroupFormsForTests(IEnumerable<Form> forms)
        {
            return GroupForms(forms);
        }

    }

    public class DictionaryTests
    {
        private const string _data_tableCategories 
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\tableCategoriesTestData.csv";
        private const string _data_lemma
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\lemmaTestData.csv";
        private const string _data_pos
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\partOfSpeechTestData.csv";
        private const string _data_formCategories
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\formCategoriesTestData.csv";
        private const string _data_numberOfEntries
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\numberOfEntriesTestData.csv";
        private const string _data_related
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\relatedTestData.csv";
        private const string _data_numberOfTablesColumnsRows
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\numberOfTablesColumnsRowTestData.csv";
        private const string _data_generatedForms
            = @"C:\Users\piotr\OneDrive\Desktop\praca inzynierska\_backend\dictionary.tests\data\generatedFormsTest.csv";


        private static DatabaseSettings _databaseSetting = new DatabaseSettings
        {
            ConnectionString = "",
            DatabaseName = "dictionary",
            FormsCollectionName = "forms",
            LemmasCollectionName = "lemmas"
        };

        private static UnitOfWork _unitOfWork = new UnitOfWork(_databaseSetting);


        private DictionaryServiceForTests _dictionary = new DictionaryServiceForTests(_unitOfWork, "");


        #region DictionaryService.GroupForms() tests


        [Fact]
        public void GroupForms_fakeForms_numberOfEquals4()
        {
            var lemma1 = new Lemma { Form = "a", Tag = "a" };
            var lemma2 = new Lemma { Form = "b", Tag = "a" };
            var lemma3 = new Lemma { Form = "c", Tag = "a" };

            var form1 = new Form { Categories = new[] { "cat1" }, Lemma = lemma1 };
            var form2 = new Form { Categories = new[] { "cat1" }, Lemma = lemma1 };
            var form3 = new Form { Categories = new[] { "cat2" }, Lemma = lemma2 };
            var form4 = new Form { Categories = new[] { "cat3" }, Lemma = lemma3 };
            var form5 = new Form { Categories = new[] { "cat4" }, Lemma = lemma3 };

            var forms = new[] { form1, form2, form3, form4, form5 };

            var groups = _dictionary.GroupFormsForTests(forms);

            var actual = groups.Count();
            var expected = 4;

            Assert.Equal(expected, actual);

        }


        [Fact]
        public void GroupForms_fakeForms_numberOfEquals3()
        {
            var lemma1 = new Lemma { Form = "a", Tag = "a" };

            var form1 = new Form { Categories = new[] { "cat1" }, Lemma = lemma1 };
            var form2 = new Form { Categories = new[] { "cat2" }, Lemma = lemma1 };
            var form3 = new Form { Categories = new[] { "cat3" }, Lemma = lemma1 };
            var form4 = new Form { Categories = new[] { "cat3" }, Lemma = lemma1 };
            var form5 = new Form { Categories = new[] { "cat3" }, Lemma = lemma1 };

            var forms = new[] { form1, form2, form3, form4, form5 };

            var groups = _dictionary.GroupFormsForTests(forms);

            var actual = groups.Count();
            var expected = 3;

            Assert.Equal(expected, actual);

        }


        [Fact]
        public void GroupForms_fakeForms_numberOfEquals4_2()
        {
            var lemma1 = new Lemma { Form = "a", Tag = "a" };
            var lemma2 = new Lemma { Form = "a", Tag = "b" };
            var lemma3 = new Lemma { Form = "a", Tag = "c" };
            var lemma4 = new Lemma { Form = "b", Tag = "" };

            var form1 = new Form { Categories = new[] { "cat1" }, Lemma = lemma1 };
            var form2 = new Form { Categories = new[] { "cat1" }, Lemma = lemma2 };
            var form3 = new Form { Categories = new[] { "cat1" }, Lemma = lemma3 };
            var form4 = new Form { Categories = new[] { "cat2" }, Lemma = lemma4 };

            var forms = new[] { form1, form2, form3, form4 };

            var groups = _dictionary.GroupFormsForTests(forms);

            var actual = groups.Count();
            var expected = 4;

            Assert.Equal(expected, actual);

        }





        #endregion



        #region Table categories tests

        [Theory]
        [CsvData(_data_tableCategories)]
        public void TableTitlesContainTableAbbr(string form, int tableNo, string tableAbbr, string columnAbbr, string rowAbbr)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);

            var tables = entries.SelectMany(x => x.Tables).ToList();

            var tableAbbrs = tables[tableNo].Titles.Select(x => x.ValueAbbr);

            var condition = tableAbbrs.Contains(tableAbbr);

            Assert.True(condition, 
                $"form: {form}\n" +
                $"axtual table abbrs: {string.Join(", ", tableAbbrs)}\n" +
                $"expected table abbr: {tableAbbr}");
        }

        [Theory]
        [CsvData(_data_tableCategories)]
        public void FirstTableColumnsContainColumnAbbr(string form, int tableNo, string tableAbbr, string columnAbbr, string rowAbbr)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);

            var tables = entries.SelectMany(x => x.Tables);

            var columnAbbrs = tables.ToList()[tableNo].ColumnHeaders.Select(x => x.ValueAbbr);

            var condition = columnAbbrs.Contains(columnAbbr);

            Assert.True(condition, 
                $"form: {form}\n" +
                $"columnAbbrs: {string.Join(", ", columnAbbrs)}\n" +
                $"columnAbbr: {columnAbbr}");
        }

        [Theory]
        [CsvData(_data_tableCategories)]
        public void FirstTableFirstColumnRowsContainRowAbbr(string form, int tableNo, string tableAbbr, string columnAbbr, string rowAbbr)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
                        
            var tables = entries.SelectMany(x => x.Tables);

            var rowAbbrs = tables.ToList()[tableNo].Rows.Select(x => x.RowCategory.ValueAbbr);

            var condition = rowAbbrs.Contains(rowAbbr);

            Assert.True(condition,
                $"form: {form}\n" +
                $"rowAbbrs: {string.Join(", ", rowAbbrs)}\n" +
                $"rowAbbr: {rowAbbr}");
        }


        #endregion



        #region Lemma tests

        [Theory]
        [CsvData(_data_lemma)]
        public void EntriesContainsLemma(string form, string lemma)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);

            var lemmas = entries.Select(x => x.Lemma);

            var condition = lemmas.Contains(lemma);

            Assert.True(condition, 
                $"form: {form}" +
                $"\nlemmas: {string.Join(", ", lemmas)}" +
                $"\nlemma: {lemma}" +
                $"\npos: {string.Join(", ", entries.Select(x => x.Pos.ValueFull))}");
        }


        #endregion



        #region Part of speech tests

        [Theory]
        [CsvData(_data_pos)]
        public void EntryGeneralLabelAbbrContainsCategoryAbbr(string form, string categoryAbbr, string categoryFull)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var labels = entries.Select(x => x.Pos.ValueAbbr);

            var condition = labels.Contains(categoryAbbr);

            Assert.True(condition, $"labels: {string.Join(", ", labels)}, categoryAbbr: {categoryAbbr}");
        }

        [Theory]
        [CsvData(_data_pos)]
        public void EntryGeneralLabelFullContainsCategoryFull(string form, string categoryAbbr, string categoryFull)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var labels = entries.Select(x => x.Pos.ValueFull);

            var condition = labels.Contains(categoryFull);

            Assert.True(condition, $"labels: {string.Join(", ", labels)}, categoryFull: {categoryFull}");
        }


        #endregion



        #region Form categories tests

        [Theory]
        [CsvData(_data_formCategories)]
        public void FormCategoriesContainsCategoryAbbr(string form, string categoryAbbr, string categoryFull, string categoryName)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var forms = entries.SelectMany(x => x.Tables.SelectMany(y => y.Rows.SelectMany(z => z.Columns.SelectMany(t => t.Forms))));

            var categoriesAbbr = forms.SelectMany(x => x.Categories.Select(y => y.ValueAbbr));

            var condition = categoriesAbbr.Contains(categoryAbbr);

            Assert.True(condition, 
                $"form: {form}\n" +
                $"categoriesAbbr: {string.Join(", ", categoriesAbbr)}\n" +
                $"categoryAbbr: {categoryAbbr}");
        }

        [Theory]
        [CsvData(_data_formCategories)]
        public void FormCategoriesContainsCategoryFull(string form, string categoryAbbr, string categoryFull, string categoryName)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var forms = entries.SelectMany(x => x.Tables.SelectMany(y => y.Rows.SelectMany(z => z.Columns.SelectMany(t => t.Forms))));

            var categoriesFull = forms.SelectMany(x => x.Categories.Select(y => y.ValueFull));

            var condition = categoriesFull.Contains(categoryFull);

            Assert.True(condition, $"categoriesFull: {string.Join(", ", categoriesFull)}, categoryFull: {categoryFull}");
        }

        [Theory]
        [CsvData(_data_formCategories)]
        public void FormCategoriesContainsCategoryName(string form, string categoryAbbr, string categoryFull, string categoryName)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var forms = entries.SelectMany(x => x.Tables.SelectMany(y => y.Rows.SelectMany(z => z.Columns.SelectMany(t => t.Forms))));

            var categoriesName = forms.SelectMany(x => x.Categories.Select(y => y.Name));

            var condition = categoriesName.Contains(categoryName);

            Assert.True(condition, 
                $"form: {form}\n" +
                $"categoriesName: {string.Join(", ", categoriesName)}\n" +
                $"categoryName: {categoryName}");
        }


        #endregion



        #region Number of entries

        [Theory]
        [CsvData(_data_numberOfEntries)]
        public void NumberOfEntriesTests(string form, int number, string greater)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);

            int actual = entries.Count();
            int expected = number;
            bool ifGreater = bool.Parse(greater);

            var condition = ifGreater ? actual >= expected : actual == expected;

            Assert.True(condition, $"actual: {actual} " + (ifGreater ? ">=" : "==") + $" expected: {expected}");
        }


        #endregion



        #region Related tests

        [Theory]
        [CsvData(_data_related)]
        public void RelatedsContainRelatedWord(string form, string relatedWord, string relatedCategoryFull)
        {
            var word = form;

            var entries = _dictionary.GetEntries(form);
            var relateds = entries.SelectMany(x => x.Relateds).Select(x => x.Word);
            var relatedsCategories = entries.SelectMany(x => x.Relateds).SelectMany(x => x.Categories).Select(x => x.ValueFull);

            var condition = relateds.Contains(relatedWord);

            Assert.True(condition, 
                $"form: {form}" +
                $"\nrelateds: {string.Join(", ", relateds)}" +
                $"\nrelCats: {string.Join(", ", relatedsCategories)}" +
                $"\nrelatedWord: {relatedWord}"
                );

        }

        [Theory]
        [CsvData(_data_related)]
        public void RelatedsContainRelatedCategoryFull(string forma, string relatedWord, string relatedCategoryFull)
        {
            var word = forma;

            var entries = _dictionary.GetEntries(forma);
            var poses = entries.Select(x => x.Pos.ValueFull);
            var paradigms = entries.Select(x => x.Paradigm);
            var categoriesFull = entries.SelectMany(x => x.Relateds).SelectMany(x => x.Categories.Select(y => y.ValueFull));

            var condition = categoriesFull.Contains(relatedCategoryFull);

            Assert.True(condition, 
                $"form: {forma}\n" +
                $"entries poses: {string.Join(", ", poses)}\n" +
                $"paradigms: {string.Join(", ", paradigms)}\n" +
                $"actual categories:\n\t{string.Join(", ", categoriesFull)}\n" +
                $"expected category:\n\t{relatedCategoryFull}");

        }

        #endregion



        #region Number of tables, columns, rows

        [Theory]
        [CsvData(_data_numberOfTablesColumnsRows)]
        public void NumberOfTablesInFirstEntry(string form, int tables, int columns, int rows)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var actualTables = entries.First().Tables;

            var actual = actualTables.Count();
            var expected = tables;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [CsvData(_data_numberOfTablesColumnsRows)]
        public void NumberOfRowsInfirstTableOfFirstEntry(string form, int tables, int columns, int rows)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var actualTables = entries.First().Tables;
            var actualRows = actualTables.First().Rows;

            var actual = actualRows.Count();
            var expected = rows;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [CsvData(_data_numberOfTablesColumnsRows)]
        public void NumberOfColumnsInfirstRowOfFirstTableOfFirstEntry(string form, int tables, int columns, int rows)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var actualTables = entries.First().Tables;
            var actualRows = actualTables.First().Rows;
            var actualColumns = actualRows.First().Columns;

            var actual = actualColumns.Count();
            var expected = columns;

            Assert.Equal(expected, actual);
        }

        #endregion



        #region Generated forms tests

        [Theory]
        [CsvData(_data_generatedForms)]
        public void GeneratedFormInTableColumnRowEqualsResult(string form, int entryNo, int tableNo, int columnNo, int rowNo, string result)
        {
            var word = form;

            var entries = _dictionary.GetEntries(word);
            var entry = _dictionary.GetEntries(word).ToList()[entryNo];

            var forms = entry.Tables.ToList()[tableNo].Rows.ToList()[rowNo].Columns.ToList()[columnNo].Forms.Select(x => x.Word);

            var condition = forms.Contains(result);

            Assert.True(condition, 
                $"entries: {entries.Count()}\n" +
                $"form: {form}\n" +
                $"actual forms: {string.Join(", ", forms)}\n" +
                $"expected form: {result}");

        }

        #endregion


        
        [Fact]
        public void GetEntriesPsa_secondRowCategoryFullEqualsDopelniacz()
        {
            var word = "psa";

            var entry = _dictionary.GetEntries(word).First();

            var actual = entry.Tables.First().Rows.ToList()[1].RowCategory.ValueFull;
            var expected = "dopełniacz";

            Xunit.Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetEntriesPsa_pluralGenitiveFormEqualsPsow()
        {
            var word = "psa";

            var entry = _dictionary.GetEntries(word).First();

            var actual = entry.Tables.First().Rows.ToList()[1].Columns.ToList()[1].Forms.First().Word;
            var expected = "psów";

            Xunit.Assert.Equal(expected, actual);
        }



    }
}
