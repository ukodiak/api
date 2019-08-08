using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Features.ChubbBop
{
    [TestFixture]
    public class SpecialtyQuestionQueryTestFixture
    {
        private HttpTestHelpers _httpHelpers;
        private ChubbBopTestHelpers _chubbHelpers;
        private readonly string _productId = ConfigurationManager.AppSettings["ChubbProductId"];

        [SetUp]
        public void Setup()
        {
            _httpHelpers = new HttpTestHelpers();
            _chubbHelpers = new ChubbBopTestHelpers();
        }

        [TestCase(ChubbSpecialty.AccountingServices)]
        [TestCase(ChubbSpecialty.Chiropractors)]
        [TestCase(ChubbSpecialty.RealEstateSales)]
        [TestCase(ChubbSpecialty.TravelAgencies)]
        [TestCase(ChubbSpecialty.DirectMarketing)]
        [TestCase(ChubbSpecialty.BarberShops)]
        [TestCase(ChubbSpecialty.SandwichShopNoCooking)]
        [TestCase(ChubbSpecialty.BicycleStores)]
        [TestCase(ChubbSpecialty.Housekeeping)]
        [TestCase(ChubbSpecialty.PersonalTrainingHealthAndFitness)]
        [TestCase(ChubbSpecialty.Photographers)]
        public async Task Chubb_SpecialtyQuestionQuery_ShouldReturnCorrectQuestions(ChubbSpecialty specialty)
        {
            List<ChubbQuestion> expectedQuestions = ChubbSpecialtyQuestions.GetQuestions(specialty);

            var actualQuestions = await _httpHelpers.GetQuestionsForClassification(_productId, "specialty", (int)specialty);

            actualQuestions.Length.Should().Be(expectedQuestions.Count);

            foreach (var question in expectedQuestions)
            {
                actualQuestions.Should().Contain(q => q.Description == question.Description);
            }
        }
    }
}
