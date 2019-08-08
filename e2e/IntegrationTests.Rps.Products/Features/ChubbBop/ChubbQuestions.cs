using System.Collections.Generic;

namespace IntegrationTests.Rps.Products.Features.ChubbBop
{
    public static class ChubbSpecialtyQuestions
    {
        public static List<ChubbQuestion> GetQuestions(ChubbSpecialty specialty)
        {
            var questions = new List<ChubbQuestion>
            {
                ChubbQuestions[1],
                ChubbQuestions[18],
                ChubbQuestions[19],
                ChubbQuestions[23],
                ChubbQuestions[24]
            };

            switch (specialty)
            {
                case ChubbSpecialty.ArtStudioIncludingArtLessons:
                case ChubbSpecialty.DirectMarketing:
                    questions.Add(ChubbQuestions[7]);
                    questions.Add(ChubbQuestions[8]);
                    break;
                case ChubbSpecialty.BicycleStores:
                    questions.Add(ChubbQuestions[11]);
                    questions.Add(ChubbQuestions[17]);
                    break;
                case ChubbSpecialty.BridalStores:
                    questions.Add(ChubbQuestions[17]);
                    break;
                case ChubbSpecialty.BakeryStoreNoBakingOnPremises:
                case ChubbSpecialty.ButcherShops:
                case ChubbSpecialty.CandyOrConfectionaryStoresNoManufacturing:
                case ChubbSpecialty.CheeseShops:
                case ChubbSpecialty.CoffeeShops:
                case ChubbSpecialty.CupcakeStores:
                case ChubbSpecialty.DessertShops:
                case ChubbSpecialty.DonutAndBagelShops:
                case ChubbSpecialty.FrozenYogurtStores:
                case ChubbSpecialty.FruitsAndVegetablesStores:
                case ChubbSpecialty.IceCreamStores:
                case ChubbSpecialty.JuiceBar:
                case ChubbSpecialty.SmoothieBar:
                case ChubbSpecialty.SnackBar:
                    questions.Add(ChubbQuestions[10]);
                    break;
                case ChubbSpecialty.DelicatessensNoCooking:
                case ChubbSpecialty.SandwichShopNoCooking:
                    questions.Add(ChubbQuestions[10]);
                    questions.Add(ChubbQuestions[12]);
                    break;
                case ChubbSpecialty.Housekeeping:
                    questions.Add(ChubbQuestions[15]);
                    questions.Add(ChubbQuestions[16]);
                    break;
                case ChubbSpecialty.BillingServiceMedical:
                case ChubbSpecialty.Acupuncturists:
                case ChubbSpecialty.Allergists:
                case ChubbSpecialty.AudioogistsTestingOnly:
                case ChubbSpecialty.Cardiologists:
                case ChubbSpecialty.Chiropractors:
                case ChubbSpecialty.Dermatologists:
                case ChubbSpecialty.DiagnosticCenters:
                case ChubbSpecialty.DialysisCenters:
                case ChubbSpecialty.Dieticians:
                case ChubbSpecialty.EarNoseandThroat:
                case ChubbSpecialty.Endocrinologists:
                case ChubbSpecialty.Gastroenterologists:
                case ChubbSpecialty.GeneralHealthPractitionersPrimaryCarePhysicians:
                case ChubbSpecialty.GeriatricSpecialists:
                case ChubbSpecialty.Gynecologists:
                case ChubbSpecialty.Hemotologists:
                case ChubbSpecialty.Internists:
                case ChubbSpecialty.Neurologists:
                case ChubbSpecialty.Obstetricians:
                case ChubbSpecialty.OccupationalTherapists:
                case ChubbSpecialty.Oncologists:
                case ChubbSpecialty.Ophthalmologists:
                case ChubbSpecialty.Optometrists:
                case ChubbSpecialty.OrthopedicSurgeons:
                case ChubbSpecialty.Osteopaths:
                case ChubbSpecialty.Pediatricians:
                case ChubbSpecialty.Podiatrists:
                case ChubbSpecialty.Psychiatrists:
                case ChubbSpecialty.Psychologists:
                case ChubbSpecialty.Pulmonologists:
                case ChubbSpecialty.SpeechTherapists:
                case ChubbSpecialty.Urologists:
                    questions.Add(ChubbQuestions[2]);
                    questions.Add(ChubbQuestions[3]);
                    break;
                case ChubbSpecialty.BarberShops:
                    questions.Add(ChubbQuestions[9]);
                    questions.Add(ChubbQuestions[22]);
                    break;
                case ChubbSpecialty.PersonalTrainingHealthAndFitness:
                    questions.Add(ChubbQuestions[20]);
                    questions.Add(ChubbQuestions[21]);
                    break;
                case ChubbSpecialty.Photographers:
                case ChubbSpecialty.Videographers:
                    questions.Add(ChubbQuestions[13]);
                    questions.Add(ChubbQuestions[14]);
                    break;
                case ChubbSpecialty.RealEstateSales:
                    questions.Add(ChubbQuestions[4]);
                    break;
                case ChubbSpecialty.TravelAgencies:
                    questions.Add(ChubbQuestions[5]);
                    questions.Add(ChubbQuestions[6]);
                    break;
            }

            return questions;
        }

        private static readonly Dictionary<int, ChubbQuestion> ChubbQuestions = new Dictionary<int, ChubbQuestion>()
        {
            { 1, new ChubbQuestion { Id = 1, Description = "Do you sell any products under your name or label?" } },
            { 2, new ChubbQuestion { Id = 2, Description = "Do you provide any physical rehabilitation services?" } },
            { 3, new ChubbQuestion { Id = 3, Description = "Do you perform any procedures under anesthesia on premises?" } },
            { 4, new ChubbQuestion { Id = 4, Description = "Do you provide any property management services?" } },
            { 5, new ChubbQuestion { Id = 5, Description = "Do you organize or host guided tours?" } },
            { 6, new ChubbQuestion { Id = 6, Description = "Do you primarily specialize in international travel?" } },
            { 7, new ChubbQuestion { Id = 7, Description = "Do you manufacture any industrial artwork or metalwork?" } },
            { 8, new ChubbQuestion { Id = 8, Description = "Do you sell any products besides artwork for display?" } },
            { 9, new ChubbQuestion { Id = 9, Description = "Do you provide massage or tanning services?" } },
            { 10, new ChubbQuestion { Id = 10, Description = "Do you use any commercial cooking equipment (e.g. deep fat fryer)?" } },
            { 11, new ChubbQuestion { Id = 11, Description = "Do you rent or loan bicycles?" } },
            { 12, new ChubbQuestion { Id = 12, Description = "Do you sell any alcohol or tobacco?" } },
            { 13, new ChubbQuestion { Id = 13, Description = "Do you perform any film shoots or production services?" } },
            { 14, new ChubbQuestion { Id = 14, Description = "If you currently use, or at any point in the future plan to use aerial drones in the scope of your business, do you/will you always obtain all necessary legal permits and meet all other legal requirements related to operating your drone(s)?" } },
            { 15, new ChubbQuestion { Id = 15, Description = "Do you provide any handyperson services?" } },
            { 16, new ChubbQuestion { Id = 16, Description = "Are your cleaning services limited to residential locations only?" } },
            { 17, new ChubbQuestion { Id = 17, Description = "Do you sell any used, refurbished, or pre-owned items?" } },
            { 18, new ChubbQuestion { Id = 18, Description = "Do you operate the business out of your home?" } },
            { 19, new ChubbQuestion { Id = 19, Description = "Do you act as a franchisor?" } },
            { 20, new ChubbQuestion { Id = 20, Description = "Do you hold a current U.S. certificate in your area of expertise and require participants to sign waivers?" } },
            { 21, new ChubbQuestion { Id = 21, Description = "Do you provide any instruction or services specific to the following: acupuncture, boxing, bungee jumping, cheerleading, equestrian yoga, esthetician, extreme sports, health care provider, martial arts or weapons training, massage therapy, naturopath, nutritionists, osteopaths, parkour instruction, physical therapy, pole dancing, SCUBA, trapeze, wilderness hiking, or wrestling?" } },
            { 22, new ChubbQuestion { Id = 22, Description = "Do you and all employees or independent contractors hold valid licenses?" } },
            { 23, new ChubbQuestion { Id = 23, Description = "Are there functioning and operational smoke/heat detectors in all units and/or occupancies?" } },
            { 24, new ChubbQuestion { Id = 24, Description = "Has your insurance coverage been cancelled or non-renewed in the past three years for reasons other than nonpayment of premium?" } }
        };
    }

    public class ChubbQuestion
    {
        public int Id { get; set; }
        public string Description { get; set; }        
    }
}
