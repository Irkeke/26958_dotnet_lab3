using CareerPathExplorer.Controllers;
using CareerPathExplorer.Models;
using CareerPathExplorer.Data;
namespace CareerPathExplorer.Views;
public class DashboardForm:Form {
 User user; AssessmentController ac=new();
 public DashboardForm(User u){user=u;Text="Career Path Explorer - Dashboard";Width=700;Height=480;StartPosition=FormStartPosition.CenterScreen;
 var p=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,Padding=new Padding(35)};
 p.Controls.Add(new Label{Text="Welcome, "+u.Name,AutoSize=true,Font=new Font("Segoe UI",20,FontStyle.Bold)});
 var test=new Button{Text="Take career assessment",AutoSize=true};test.Click+=(_,_)=>{using var f=new AssessmentForm(user);f.ShowDialog();};p.Controls.Add(test);
 var careers=new Button{Text="Browse career catalogue",AutoSize=true};careers.Click+=(_,_)=>{try{var cs=ac.AllCareers();MessageBox.Show(string.Join("\n\n",cs.Select(x=>x.Name+" ("+x.Area+")\n"+x.Description)),"Career catalogue");}catch(Exception ex){MessageBox.Show(ex.Message);}};p.Controls.Add(careers);
 var profile=new Button{Text="Update profile",AutoSize=true};profile.Click+=(_,_)=>{using var f=new Form{Text="Update profile",Width=420,Height=220};var n=new TextBox{Text=user.Name,Width=250};var e=new TextBox{Text=user.Email,Width=250};var b=new Button{Text="Save"};var l=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown};l.Controls.Add(new Label{Text="Name"});l.Controls.Add(n);l.Controls.Add(new Label{Text="Email"});l.Controls.Add(e);b.Click+=(_,_)=>{try{new UserController().Update(user.Id,n.Text,e.Text);user.Name=n.Text;user.Email=e.Text;MessageBox.Show("Profile updated.");f.Close();}catch(Exception ex){MessageBox.Show(ex.Message);}};l.Controls.Add(b);f.Controls.Add(l);f.ShowDialog();};p.Controls.Add(profile);
 var history=new Button{Text="Assessment history",AutoSize=true};history.Click+=(_,_)=>{try{using var c=Database.Open();using var cmd=c.CreateCommand();cmd.CommandText="SELECT AssessmentId,SubmittedAt FROM Assessments WHERE UserId=@u ORDER BY SubmittedAt DESC";cmd.Parameters.AddWithValue("@u",user.Id);using var r=cmd.ExecuteReader();var rows=new List<string>();while(r.Read())rows.Add($"Assessment #{r.GetInt32(0)} - {r.GetDateTime(1):g}");MessageBox.Show(rows.Count==0?"No assessments yet.":string.Join("\n",rows));}catch(Exception ex){MessageBox.Show(ex.Message);}};p.Controls.Add(history);
 var logout=new Button{Text="Log out"};logout.Click+=(_,_)=>Close();p.Controls.Add(logout);Controls.Add(p);}
}