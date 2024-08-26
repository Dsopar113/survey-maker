using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using WebApp1.Data;
using WebApp1.Models;
using WebApp1.ViewModels;

namespace WebApp1.Controllers
{
	public class SurveyParticipantController : Controller
	{
		private readonly ApplicationDbContext _db;
		public SurveyParticipantController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			List<Participant> participants = _db.Participants.ToList();
			return View(participants);
		}

		[HttpGet]
		public IActionResult AddParticipant()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddParticipant(SurveyParticipantViewModel model)
		{
			if (ModelState.IsValid)
			{
				var participant = new Participant
				{
					ParticipantMail = model.ParticipantEmail,
					ParticipantName = model.ParticipantFirstName,
					ParticipantLastName = model.ParticipantLastName,
					ParticipantId = Guid.NewGuid()
                };

				// Save to database
				_db.Participants.Add(participant);
				await _db.SaveChangesAsync();
				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult Edit(Guid? id)
		{
			if(id==null)
			{
				return NotFound();
			}
			Participant participant = _db.Participants.Find(id);
			if (participant == null)
			{
				return NotFound();
			}
			
			return View(participant);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Participant model)
		{
			if (ModelState.IsValid)
			{
				_db.Participants.Update(model);
				try
				{
					await _db.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException ex) {
                    TempData["ErrorMessage"] = "An error occurred while updating the participant. Please try again.";
                }
				return RedirectToAction("Index");
			}
			return View(model);
		}

        [HttpGet]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Participant participant = _db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }
            return View(participant);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeletePOST(Guid? id)
        {
			Participant? obj = _db.Participants.Find(id);
			if (obj == null) { 
				return NotFound();
			}
			_db.Participants.Remove(obj);
            await _db.SaveChangesAsync();
			return RedirectToAction("Index");
        }

        [HttpGet]
		public async Task<IActionResult> AssignParticipant(Guid id)
		{
			var surveys = _db.SurveyHeaders.ToList();
			var assignedSurveyIds = _db.SurveyHeaders_Participants
				.Where(sp => sp.ParticipantId == id)
				.Select(sp => sp.SurveyId)
				.ToList();

			// Create a dictionary to quickly check assignment status
			var assignedSurveys = surveys.ToDictionary(
				s => s.SurveyId,
				s => assignedSurveyIds.Contains(s.SurveyId)
			);

			ViewBag.ParticipantId = id;
			ViewBag.AssignedSurveys = assignedSurveys; // Pass the assignment status

			return View(surveys);
		}

		[HttpPost]
		public IActionResult AssignToParticipant(Guid surveyId, Guid participantId)
		{
			var survey = _db.SurveyHeaders.FirstOrDefault(s => s.SurveyId == surveyId);
			var participant = _db.Participants.FirstOrDefault(p => p.ParticipantId == participantId);

			if (survey == null || participant == null)
			{
				return NotFound();
			}

			var surveyParticipant = new SurveyHeader_Participant
			{
				SurveyHeader_ParticipantId = Guid.NewGuid(),
				SurveyId = surveyId,
				ParticipantId = participantId,
				IsFinished = false,
				ValidTo = DateTime.Now.AddMonths(1)  // Example
			};

			_db.SurveyHeaders_Participants.Add(surveyParticipant);
			_db.SaveChanges();

			return RedirectToAction("Index", new { participantId = participantId });
		}

		[HttpPost]
		public async Task<IActionResult> RemoveFromParticipant(Guid surveyId, Guid participantId)
		{
			List<SurveyHeader_Participant> connection = _db.SurveyHeaders_Participants.ToList();
			foreach(SurveyHeader_Participant obj in connection)
			{
				if(obj.SurveyId == surveyId || obj.ParticipantId == participantId)
				{
					_db.SurveyHeaders_Participants.Remove(obj);
					await _db.SaveChangesAsync();
					return RedirectToAction("Index");
				}
			}
			return View("AssignParticipant");
		}
	}
}