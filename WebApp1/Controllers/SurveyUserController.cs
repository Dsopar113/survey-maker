using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp1.Data;
using WebApp1.Models;
using WebApp1.ViewModels;

namespace WebApp1.Controllers
{
    public class SurveyUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SurveyUserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
			List<SurveyHeader> objSurveyHeaderList = _db.SurveyHeaders.ToList();
			return View(objSurveyHeaderList);
		}
        public async Task<IActionResult> Fill(Guid? id)
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
        public IActionResult Fill(SurveyHeaderViewModel model)
        {
            foreach (var section in model.SurveySections)
            {
                foreach (var question in section.SurveyQuestions)
                {
                    if (!question.Option.IsNullOrEmpty())
                    {
                        // Save the selected option value and the text (from the Options list) into the database
                        var option = new Option
                        {
                            //OptionId = Guid.NewGuid(),
                            //OptionText = question.Options.FirstOrDefault(o => o.OptionValue == question.SelectedOption)?.OptionText,
                            //OptionValue = question.SelectedOption,
                            //QuestionId = question.QuestionId
                        };

                        // Save the option to the database (add your DB save logic here)
                    }
                }
            }

            // Save other form data as needed
            return RedirectToAction("Index");
        }

    }
}
