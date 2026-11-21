using CareerPathExplorer.Controllers;
namespace CareerPathExplorer.Views;
public class LoginForm:Form {
 TextBox email=new(){Width=260},pass=new(){Width=260,UseSystemPasswordChar=true}; UserController users=new();
 public LoginForm(){Text="Career Path Explorer - Login";Width=520;Height=330;StartPosition=FormStartPosition.CenterScreen;
 var p=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,Padding=new Padding(35)};
 p.Controls.Add(new Label{Text="Career Path Explorer",Font=new Font("Segoe UI",20,FontStyle.Bold),AutoSize=true});
 p.Controls.Add(new Label{Text="Email"});p.Controls.Add(email);p.Controls.Add(new Label{Text="Password"});p.Controls.Add(pass);
 var login=new Button{Text="Log in",AutoSize=true};login.Click+=(_,_)=>{try{var u=users.Login(email.Text,pass.Text);if(u==null){MessageBox.Show("Incorrect email or password.");return;}Hide();using var f=new DashboardForm(u);f.ShowDialog();Show();}catch(Exception ex){MessageBox.Show(ex.Message);}};
 var reg=new Button{Text="Create account",AutoSize=true};reg.Click+=(_,_)=>{using var f=new RegisterForm();f.ShowDialog();};
 p.Controls.Add(login);p.Controls.Add(reg);Controls.Add(p);}
}