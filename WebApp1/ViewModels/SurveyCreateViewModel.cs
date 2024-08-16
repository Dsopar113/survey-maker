using System.Collections.Generic;

namespace WebApp1.ViewModels
{
	public class SurveyCreateViewModel
	{
		public SurveyHeaderViewModel SurveyHeader { get; set; }
		public List<SurveySectionViewModel> Sections { get; set; }
	}
}