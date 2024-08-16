using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;
using WebApp1.ViewModels;
namespace WebApp1.Controllers
{
    public class SurveyController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SurveyController(ApplicationDbContext db) { _db = db; }
        [HttpGet]
        public IActionResult Index()
        {
            List<SurveyHeader> objSurveyHeaderList = _db.SurveyHeaders.ToList();
            return View(objSurveyHeaderList);
        }
        public IActionResult Create()
        {
            var model = new SurveyHeaderViewModel()
            {
                SurveySections = new List<SurveySectionViewModel>()
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult GetSurveySectionPartial(int index)
        {
            ViewBag.SectionIndex = index;
            var model = new SurveySectionViewModel()
            {
                SurveyQuestions = new List<SurveyQuestionViewModel>()
            };
            return PartialView("SurveySectionPartial", model);
        }
        [HttpGet]
        public IActionResult GetSurveyQuestionPartial(int sectionIndex, int questionIndex)
        {
            ViewBag.SectionIndex = sectionIndex;
            ViewBag.QuestionIndex = questionIndex;
            var model = new SurveyQuestionViewModel();
            return PartialView("SurveyQuestionPartial", model);
        }
        [HttpGet]
        public IActionResult GetEditSurveySectionPartial(int index)
        {
            ViewBag.SectionIndex = index;
            var model = new SurveySectionViewModel()
            {
                SurveyQuestions = new List<SurveyQuestionViewModel>()
            };
            return PartialView("EditSurveySectionPartial", model);
        }
        [HttpGet]
        public IActionResult GetEditSurveyQuestionPartial(int sectionIndex, int questionIndex)
        {
            ViewBag.SectionIndex = sectionIndex;
            ViewBag.QuestionIndex = questionIndex;
            var model = new SurveyQuestionViewModel();
            return PartialView("EditSurveyQuestionPartial", model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(SurveyHeaderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var surveyHeader = new SurveyHeader
                {
                    SurveyName = model.SurveyName,
                    SurveyDescription = model.SurveyDescription,
                    SurveySections = model.SurveySections.Select(s => new SurveySection
                    {
                        // Assuming SurveySectionViewModel has the same properties as SurveySection entity
                        SectionName = s.SectionName,
                        SectionDescription = s.SectionDescription,
                        Questions = s.SurveyQuestions.Select(q => new Question
                        {
                            QuestionContent = q.QuestionContent
                        }).ToList()
                    }).ToList()
                };

                // Save to database
                _db.SurveyHeaders.Add(surveyHeader);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            SurveyHeader? surveyHeaderFromDb = await _db.SurveyHeaders
                .Include(sh => sh.SurveySections)
                    .ThenInclude(ss => ss.Questions)
                .FirstOrDefaultAsync(sh => sh.SurveyId == id);
            if (surveyHeaderFromDb == null)
            {
                return NotFound();
            }
            var model = new SurveyHeaderViewModel
            {
                SurveyName = surveyHeaderFromDb.SurveyName,
                SurveyDescription = surveyHeaderFromDb.SurveyDescription,
                SurveySections = surveyHeaderFromDb.SurveySections.Select(ss => new SurveySectionViewModel
                {
                    SectionName = ss.SectionName,
                    SectionDescription = ss.SectionDescription,
                    SurveyQuestions = ss.Questions.Select(q => new SurveyQuestionViewModel
                    {
                        QuestionContent = q.QuestionContent
                    }).ToList()
                }).ToList()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(SurveyHeaderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the existing SurveyHeader from the database using the SurveyId
                var surveyHeader = await _db.SurveyHeaders
                    .Include(sh => sh.SurveySections)
                    .ThenInclude(ss => ss.Questions)
                    .FirstOrDefaultAsync(sh => sh.SurveyName == model.SurveyName);

                if (surveyHeader == null)
                {
                    return NotFound(); // If the SurveyHeader is not found
                }

                // Update the properties of the existing SurveyHeader
                surveyHeader.SurveyName = model.SurveyName;
                surveyHeader.SurveyDescription = model.SurveyDescription;

                // Clear existing sections and questions to replace them with the updated list
                surveyHeader.SurveySections.Clear();

                // Update SurveySections
                foreach (var sectionViewModel in model.SurveySections)
                {
                    var surveySection = new SurveySection
                    {
                        SectionName = sectionViewModel.SectionName,
                        SectionDescription = sectionViewModel.SectionDescription,
                        Questions = sectionViewModel.SurveyQuestions.Select(q => new Question
                        {
                            QuestionContent = q.QuestionContent
                        }).ToList()
                    };

                    surveyHeader.SurveySections.Add(surveySection);
                }

                // Save changes to the database
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
