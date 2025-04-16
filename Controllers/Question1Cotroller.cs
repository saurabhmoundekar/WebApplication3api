using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication3API.Models;

namespace WebApplication3API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Question1Controller : ControllerBase
    {
        private readonly string _connectionString;

        public Question1Controller(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CRUDCS");
        }

        // ✅ GET: api/question1 (Fetch all questions)
        [HttpGet]
        public ActionResult<IEnumerable<Question>> GetQuestions()
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Questions";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    questions.Add(new Question
                    {
                        Id = (int)reader["Id"],
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        CorrectAnswer = reader["CorrectAnswer"].ToString()
                    });
                }
            }
            return questions;
        }

        // ✅ GET: api/question1/{id} (Fetch question by ID)
        [HttpGet("{id}")]
        public ActionResult<Question> GetQuestion(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Questions WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Question
                    {
                        Id = (int)reader["Id"],
                        QuestionText = reader["QuestionText"].ToString(),
                        OptionA = reader["OptionA"].ToString(),
                        OptionB = reader["OptionB"].ToString(),
                        OptionC = reader["OptionC"].ToString(),
                        OptionD = reader["OptionD"].ToString(),
                        CorrectAnswer = reader["CorrectAnswer"].ToString()
                    };
                }
            }
            return NotFound();
        }

        // ✅ POST: api/question1 (Add a new question)
        [HttpPost]
        public IActionResult AddQuestion([FromBody] Question question)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Questions (QuestionText, OptionA, OptionB, OptionC, OptionD, CorrectAnswer) " +
                               "VALUES (@QuestionText, @OptionA, @OptionB, @OptionC, @OptionD, @CorrectAnswer)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@QuestionText", question.QuestionText);
                cmd.Parameters.AddWithValue("@OptionA", question.OptionA);
                cmd.Parameters.AddWithValue("@OptionB", question.OptionB);
                cmd.Parameters.AddWithValue("@OptionC", question.OptionC);
                cmd.Parameters.AddWithValue("@OptionD", question.OptionD);
                cmd.Parameters.AddWithValue("@CorrectAnswer", question.CorrectAnswer);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return CreatedAtAction(nameof(GetQuestion), new { id = question.Id }, question);
        }

        // ✅ DELETE: api/question1/{id} (Delete a question)
        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Questions WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 0)
                    return NotFound();
            }
            return NoContent();
        }
    }
}
