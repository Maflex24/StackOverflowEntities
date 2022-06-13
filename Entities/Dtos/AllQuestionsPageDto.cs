using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOverflowEntities.Entities.Dtos
{
    public class AllQuestionsPageDto
    {
        public int TotalQuestions { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int ResultsFrom { get; set; }
        public int ResultsTo { get; set; }
        public List<QuestionInfoDto> QuestionsDto { get; set; }

        public AllQuestionsPageDto(int totalQuestions, int currentPage, int resultsPerPage, List<QuestionInfoDto> questionDto)
        {
            TotalQuestions = totalQuestions;
            CurrentPage = currentPage;
            QuestionsDto = questionDto;
            TotalPages = totalQuestions / resultsPerPage;
            ResultsFrom = resultsPerPage * (currentPage - 1) + 1;
            ResultsTo = ResultsFrom + resultsPerPage - 1;
        }
    }
}
