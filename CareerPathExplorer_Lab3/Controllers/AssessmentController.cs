using CareerPathExplorer.Data;
using CareerPathExplorer.Models;
namespace CareerPathExplorer.Controllers;
public class AssessmentController {
 public List<Question> Questions() {
  var list=new List<Question>(); using var c=Database.Open(); using var cmd=c.CreateCommand();
  cmd.CommandText="SELECT q.QuestionId,q.QuestionText,o.OptionId,o.OptionText,o.InterestArea FROM Questions q JOIN AnswerOptions o ON q.QuestionId=o.QuestionId ORDER BY q.QuestionId,o.OptionId";
  using var r=cmd.ExecuteReader(); while(r.Read()) { int id=r.GetInt32(0); var q=list.FirstOrDefault(x=>x.Id==id); if(q==null){q=new Question{Id=id,Text=r.GetString(1)};list.Add(q);} q.Options.Add(new Option{Id=r.GetInt32(2),Text=r.GetString(3),Area=r.GetString(4)}); } return list;
 }
 public int Submit(int userId,Dictionary<int,Option> answers) {
  var qs=Questions(); if(qs.Count==0||answers.Count!=qs.Count||qs.Any(q=>!answers.ContainsKey(q.Id)))throw new Exception("Please answer every question.");
  using var c=Database.Open(); using var tx=c.BeginTransaction(); try {
   using var a=c.CreateCommand();a.Transaction=tx;a.CommandText="INSERT INTO Assessments(UserId) OUTPUT INSERTED.AssessmentId VALUES(@u)";a.Parameters.AddWithValue("@u",userId);int aid=(int)a.ExecuteScalar()!;
   foreach(var pair in answers){using var b=c.CreateCommand();b.Transaction=tx;b.CommandText="INSERT INTO AssessmentAnswers(AssessmentId,QuestionId,OptionId) VALUES(@a,@q,@o)";b.Parameters.AddWithValue("@a",aid);b.Parameters.AddWithValue("@q",pair.Key);b.Parameters.AddWithValue("@o",pair.Value.Id);b.ExecuteNonQuery();}
   tx.Commit();return aid;
  }catch{tx.Rollback();throw;}
 }
 public List<Career> Recommendations(int assessmentId) {
  using var c=Database.Open();using var cmd=c.CreateCommand();
  cmd.CommandText="SELECT TOP 3 c.CareerId,c.CareerName,c.Description,c.InterestArea FROM Careers c JOIN (SELECT o.InterestArea,COUNT(*) Score FROM AssessmentAnswers a JOIN AnswerOptions o ON a.OptionId=o.OptionId WHERE a.AssessmentId=@id GROUP BY o.InterestArea) s ON s.InterestArea=c.InterestArea ORDER BY s.Score DESC,c.CareerName";
  cmd.Parameters.AddWithValue("@id",assessmentId);var result=new List<Career>();using var r=cmd.ExecuteReader();
  while(r.Read())result.Add(new Career{Id=r.GetInt32(0),Name=r.GetString(1),Description=r.GetString(2),Area=r.GetString(3)});return result;
 }
 public List<Career> AllCareers(){using var c=Database.Open();using var cmd=c.CreateCommand();cmd.CommandText="SELECT CareerId,CareerName,Description,InterestArea FROM Careers ORDER BY CareerName";var x=new List<Career>();using var r=cmd.ExecuteReader();while(r.Read())x.Add(new Career{Id=r.GetInt32(0),Name=r.GetString(1),Description=r.GetString(2),Area=r.GetString(3)});return x;}
}