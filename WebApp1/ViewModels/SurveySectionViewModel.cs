using System.Collections.Generic;

namespace WebApp1.ViewModels
{
	public class SurveySectionViewModel
	{
		public string SectionName { get; set; }
		public string SectionDescription { get; set; }
		public List<SurveyQuestionViewModel> SurveyQuestions { get; set; }
	}

}