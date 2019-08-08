using System.Collections.Generic;
using System.Linq;

namespace IntegrationTests.Rps.Products.Features.NautilusArtisan
{
    public static class OperationsQuestions
    {
        public static List<OperationsQuestion> GetQuestions(int craftId, int[] additionalQuestionIds = null)
        {
            var questions = new List<OperationsQuestion>();
            
            questions.AddRange(operationsQuestions.Where(q => q.Id >= 25 && q.Id <= 44));

            questions.Add(operationsQuestions.Single(q => q.Id == 72));

            if (additionalQuestionIds != null && additionalQuestionIds.Length > 0)
            {
                foreach (var questionId in additionalQuestionIds)
                {
                    questions.Add(operationsQuestions.Single(q => q.Id == questionId));
                }
            }            

            return questions;
        }

        private static readonly IEnumerable<OperationsQuestion> operationsQuestions = new List<OperationsQuestion>
        {
            new OperationsQuestion { Id = 25, Description = "Asbestos work / remediation" },
            new OperationsQuestion { Id = 26, Description = "Blasting operations" },
            new OperationsQuestion { Id = 27, Description = "Caisson work" },
            new OperationsQuestion { Id = 28, Description = "Condo or Townhouse New Residential Construction Projects" },
            new OperationsQuestion { Id = 29, Description = "Earthquake or hurricane retrofitting" },
            new OperationsQuestion { Id = 30, Description = "Exterior or structural work over (3) three stories" },
            new OperationsQuestion { Id = 31, Description = "Fireproofing risks" },
            new OperationsQuestion { Id = 32, Description = "Lead abatement" },
            new OperationsQuestion { Id = 33, Description = "List track home developers as additional insureds" },
            new OperationsQuestion { Id = 34, Description = "List multiple named insured entities on the policy" },
            new OperationsQuestion { Id = 35, Description = "Soil Testing" },
            new OperationsQuestion { Id = 36, Description = "Mold remediation" },
            new OperationsQuestion { Id = 37, Description = "Synthetic stucco or Exterior Insulating Finishing Systems (EIFS)" },
            new OperationsQuestion { Id = 38, Description = "Truss or beam manufacturing" },
            new OperationsQuestion { Id = 39, Description = "Water, smoke or fire restoration" },
            new OperationsQuestion { Id = 40, Description = "Work in Colorado or New York" },
            new OperationsQuestion { Id = 41, Description = "Work on assisted living facilities, retirement homes, military housing" },
            new OperationsQuestion { Id = 42, Description = "Work on grain bins, elevators or silos" },
            new OperationsQuestion { Id = 43, Description = "Work on railroads" },
            new OperationsQuestion { Id = 44, Description = "Work on underground tanks" },
            new OperationsQuestion { Id = 45, Description = "Freon reclaiming" },
            new OperationsQuestion { Id = 46, Description = "TV or radio receiving installation, service or repair" },
            new OperationsQuestion { Id = 47, Description = "Gas hook ups" },
            new OperationsQuestion { Id = 48, Description = "Cleaning or washing of aircraft or ship hulls" },
            new OperationsQuestion { Id = 49, Description = "Interior Tank Cleaning" },
            new OperationsQuestion { Id = 50, Description = "Snow or ice removal" },
            new OperationsQuestion { Id = 51, Description = "Use toxic or caustic supplies" },
            new OperationsQuestion { Id = 52, Description = "Use equipment in excess of 3,500 psi" },
            new OperationsQuestion { Id = 53, Description = "Post-tension concrete construction" },
            new OperationsQuestion { Id = 54, Description = "Hazardous waste removal" },
            new OperationsQuestion { Id = 55, Description = "Fracking" },
            new OperationsQuestion { Id = 56, Description = "Aerospace work" },
            new OperationsQuestion { Id = 57, Description = "Serve transformers containing PCB\"s (Polychlorinated biphenyls)" },
            new OperationsQuestion { Id = 58, Description = "Work on traffic barriers or guard rails" },
            new OperationsQuestion { Id = 59, Description = "Work on retaining walls" },
            new OperationsQuestion { Id = 60, Description = "Boiler work" },
            new OperationsQuestion { Id = 61, Description = "Crane work" },
            new OperationsQuestion { Id = 62, Description = "Ergonomics" },
            new OperationsQuestion { Id = 63, Description = "Commercial interior design" },
            new OperationsQuestion { Id = 64, Description = "Dusting, spraying or fumigating or crops" },
            new OperationsQuestion { Id = 65, Description = "Highway mowing operations, including freeways and right of ways" },
            new OperationsQuestion { Id = 66, Description = "Foundation work or repair" },
            new OperationsQuestion { Id = 67, Description = "Installation, service, or repair for commercial applications" },
            new OperationsQuestion { Id = 68, Description = "Use barges" },
            new OperationsQuestion { Id = 69, Description = "Work performed is NOT in compliance with federal, state, municipal or local statutes or regulations" },
            new OperationsQuestion { Id = 70, Description = "Lifeguard services" },
            new OperationsQuestion { Id = 71, Description = "Work on balconies, bleachers or staircases" },
            new OperationsQuestion { Id = 72, Description = "Work on airports" },
            new OperationsQuestion { Id = 73, Description = "Application of coatings to improve structural integrity" },
            new OperationsQuestion { Id = 74, Description = "LPG Hookups" },
            new OperationsQuestion { Id = 75, Description = "Industrial Work" },
            new OperationsQuestion { Id = 76, Description = "Renting Equipment" },
            new OperationsQuestion { Id = 77, Description = "Structural Work" },
            new OperationsQuestion { Id = 78, Description = "Mobile equipment operated on streets or roads" },
            new OperationsQuestion { Id = 79, Description = "Roofing" },
            new OperationsQuestion { Id = 80, Description = "Medical Institutions" },
            new OperationsQuestion { Id = 81, Description = "Power plant work" },
            new OperationsQuestion { Id = 82, Description = "Work on outdoor theatrical lighting" },
            new OperationsQuestion { Id = 83, Description = "Work with the elderly" },
            new OperationsQuestion { Id = 84, Description = "Environmental or green design services" },
            new OperationsQuestion { Id = 85, Description = "Pool repair" },
            new OperationsQuestion { Id = 86, Description = "Work on bridges or elevated highways" },
            new OperationsQuestion { Id = 87, Description = "Paint tanks used for chemicals of fuel" },
            new OperationsQuestion { Id = 88, Description = "Use open flames to remove paint" },
            new OperationsQuestion { Id = 89, Description = "Work on dams" },
            new OperationsQuestion { Id = 90, Description = "Industrial and chemical waste collection or sedimentation pond" },
            new OperationsQuestion { Id = 91, Description = "Subway or tunnel construction" },
            new OperationsQuestion { Id = 92, Description = "Past or present projects on landfills" },
            new OperationsQuestion { Id = 93, Description = "Work on gas mains" },
            new OperationsQuestion { Id = 94, Description = "Steam pipe work" },
            new OperationsQuestion { Id = 95, Description = "Work on fire escapes, pipelines, security bars, towers or antennas" },
            new OperationsQuestion { Id = 96, Description = "Work on systems intended for chemical or petroleum products" },
            new OperationsQuestion { Id = 97, Description = "Levee and breakwater construction" }
        };
    }

    public class OperationsQuestion
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
