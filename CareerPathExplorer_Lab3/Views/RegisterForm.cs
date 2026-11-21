using CareerPathExplorer.Controllers;
namespace CareerPathExplorer.Views;
public class RegisterForm:Form {
 TextBox name=new(){Width=260},email=new(){Width=260},pass=new(){Width=260,UseSystemPasswordChar=true};
 public RegisterForm(){Text="Register";Width=480;Height=350;StartPosition=FormStartPosition.CenterParent;var p=new FlowLayoutPanel{Dock=DockStyle.Fill,FlowDirection=FlowDirection.TopDown,Padding=new Padding(25)};
 foreach(var pair in new[]{("Full name",name),("Email",email),("Password (minimum 6 characters)",pass)}){p.Controls.Add(new Label{Text=pair.Item1,AutoSize=true});p.Controls.Add(pair.Item2);}
 var b=new Button{Text="Register"};b.Click+=(_,_)=>{try{new UserController().Register(name.Text,email.Text,pass.Text);MessageBox.Show("Account created. You can now log in.");Close();}catch(Exception ex){MessageBox.Show(ex.Message);}};p.Controls.Add(b);Controls.Add(p);}
}