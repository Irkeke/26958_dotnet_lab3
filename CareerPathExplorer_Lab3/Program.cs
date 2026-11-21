using CareerPathExplorer.Views;
using CareerPathExplorer.Data;
namespace CareerPathExplorer;
internal static class Program {
 [STAThread] static void Main() {
  ApplicationConfiguration.Initialize();
  try { Database.EnsureCreated(); Application.Run(new LoginForm()); }
  catch(Exception ex) { MessageBox.Show("Database setup/connection failed. Install SQL Server LocalDB and run Database/Setup.sql.\n\n"+ex.Message); }
 }
}