using FluentAssertions;
using NUnit.Framework;
using System.Configuration;
using System.Threading.Tasks;

namespace IntegrationTests.Rps.Products.Features.NautilusArtisan
{
    [TestFixture]
    public class CraftQuestionQueryTextFixture
    {
        private HttpTestHelpers _httpHelpers;
        private NautilusTestHelpers _nautilusHelpers;
        private readonly string _productId = ConfigurationManager.AppSettings["ArtisansProductId"];

        [SetUp]
        public void Setup()
        {
            _httpHelpers = new HttpTestHelpers();
            _nautilusHelpers = new NautilusTestHelpers();
        }

        [TestCase(112)]
        [TestCase(113, new int[] { 45 })]
        [TestCase(114, new int[] { 46, 47 })]
        [TestCase(115, new int[] { 46, 47 })]
        [TestCase(116)]
        [TestCase(117)]
        [TestCase(118)]
        [TestCase(119)]
        [TestCase(120)]
        [TestCase(121, new int[] { 53, 66, 86, 89, 90, 91 })]
        [TestCase(122, new int[] { 54 })]
        [TestCase(123)]
        [TestCase(124, new int[] { 55 })]
        [TestCase(125)]
        [TestCase(126)]
        [TestCase(127, new int[] { 56, 80, 81, 82, 57 })]
        [TestCase(128)]
        [TestCase(129, new int[] { 92, 50, 89, 66, 93 })]
        [TestCase(130, new int[] { 48, 51, 52 })]
        [TestCase(131, new int[] { 58, 59 })]
        [TestCase(132)]
        [TestCase(133)]
        [TestCase(134, new int[] { 50, 89, 92 })]
        [TestCase(135)]
        [TestCase(136)]
        [TestCase(137)]
        [TestCase(138, new int[] { 60, 61 })]
        [TestCase(139, new int[] { 60, 61 })]
        [TestCase(140)]
        [TestCase(141)]
        [TestCase(142)]
        [TestCase(143, new int[] { 62, 83, 84, 63 })]
        [TestCase(144, new int[] { 64, 65, 50 })]
        [TestCase(145, new int[] { 64, 65, 50 })]
        [TestCase(146, new int[] { 66, 61, 94 })]
        [TestCase(147, new int[] { 71, 86, 95 })]
        [TestCase(148, new int[] { 71, 86, 95 })]
        [TestCase(149, new int[] { 71, 86, 95 })]
        [TestCase(150, new int[] { })]
        [TestCase(151, new int[] { })]
        [TestCase(152, new int[] { 87, 88, 73, 86 })]
        [TestCase(153, new int[] { 87, 88, 73, 86 })]
        [TestCase(154, new int[] { 87, 88, 73, 86 })]
        [TestCase(155, new int[] { })]
        [TestCase(156, new int[] { 66 })]
        [TestCase(157, new int[] { 74, 75, 60, 94, 96 })]
        [TestCase(158, new int[] { 74, 75 })]
        [TestCase(159, new int[] { 60, 94 })]
        [TestCase(160, new int[] { 48, 49, 50, 51, 52 })]
        [TestCase(161, new int[] { 45, 76 })]
        [TestCase(162, new int[] { 77, 62, 83, 84 })]
        [TestCase(163, new int[] { })]
        [TestCase(164, new int[] { 51 })]
        [TestCase(165, new int[] { 78 })]
        [TestCase(166, new int[] { 78 })]
        [TestCase(167, new int[] { })]
        [TestCase(168, new int[] { })]
        [TestCase(169, new int[] { 79 })]
        [TestCase(170, new int[] { 66 })]
        [TestCase(171, new int[] { })]
        [TestCase(172, new int[] { })]
        [TestCase(173, new int[] { })]
        [TestCase(174, new int[] { 67, 61 })]
        [TestCase(175, new int[] { 68, 86, 89, 97, 91 })]
        [TestCase(176, new int[] { 58 })]
        [TestCase(177, new int[] { 70, 69, 85 })]
        [TestCase(178, new int[] { 69, 70 })]
        [TestCase(179, new int[] { 69, 70 })]
        [TestCase(180, new int[] { })]
        [TestCase(181, new int[] { })]
        [TestCase(182, new int[] { 61, 64, 65, 78 })]
        [TestCase(183, new int[] { })]
        [TestCase(184, new int[] { })]
        [TestCase(185, new int[] { })]
        [TestCase(186, new int[] { })]
        [TestCase(187, new int[] { })]
        [TestCase(188, new int[] { })]
        public async Task Artisans_CraftQuestionQuery_ShouldReturnCorrectQuestions(int craftId, int[] additionalQuestionIds = null)
        {
            var expectedQuestions = OperationsQuestions.GetQuestions(craftId, additionalQuestionIds);

            var actualQuestions = await _httpHelpers.GetQuestionsForClassification(_productId, "craft", craftId);

            actualQuestions.Length.Should().Be(expectedQuestions.Count);

            foreach (var question in expectedQuestions)
            {
                actualQuestions.Should().Contain(q => q.Id == question.Id);
            }
        }
    }
}
