using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3API.Models;

namespace WebApplication3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {

        private readonly QuestionContext questionContext;
        public QuestionController(QuestionContext questionContext)
        {
            this.questionContext = questionContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestion()
        {
            if(questionContext.Questions==null)
            {
                return NotFound();
            }
            return await questionContext.Questions.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            if (questionContext.Questions == null)
            {
                return NotFound();
            }
            var question = questionContext.Questions.Find(id);
            if(question==null)
            {
                return NotFound();
            }
            return question;       
        }
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            questionContext.Questions.Add(question);
            await questionContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> PutQuestion(int id,Question question)
        {
            if(id!=question.Id)
            {
                BadRequest();
            }
            questionContext.Entry(question).State = EntityState.Modified;
            try
            {
                await questionContext.SaveChangesAsync();

            }
            catch(DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion(int id)
        {
            if(questionContext.Questions==null)
            {
                return NotFound();
            }
            var question = await questionContext.Questions.FindAsync(id);
            if(question==null)
            {
                return NotFound();
            }
            questionContext.Questions.Remove(question);
            await questionContext.SaveChangesAsync();

            return Ok();
        }
    }
}
