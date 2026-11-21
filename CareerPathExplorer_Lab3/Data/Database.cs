using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
namespace CareerPathExplorer.Data;
public static class Database {
 public const string Cs = @"Server=(localdb)\MSSQLLocalDB;Database=CareerPathExplorerDB;Trusted_Connection=True;TrustServerCertificate=True;";
 public const string MasterCs = @"Server=(localdb)\MSSQLLocalDB;Database=master;Trusted_Connection=True;TrustServerCertificate=True;";
 public static SqlConnection Open() { var c=new SqlConnection(Cs); c.Open(); return c; }

 // Creates the database, and (only if the tables are missing) runs Database/Setup.sql so the app works on first launch.
 public static void EnsureCreated() {
  using var c=new SqlConnection(MasterCs);
  c.Open();
  using(var cmd=c.CreateCommand()) { cmd.CommandText="IF DB_ID('CareerPathExplorerDB') IS NULL CREATE DATABASE CareerPathExplorerDB;"; cmd.ExecuteNonQuery(); }
  c.ChangeDatabase("CareerPathExplorerDB");
  using(var cmd=c.CreateCommand()) {
   cmd.CommandText="SELECT CASE WHEN OBJECT_ID('Users','U') IS NULL OR OBJECT_ID('Questions','U') IS NULL OR OBJECT_ID('Careers','U') IS NULL THEN 0 ELSE 1 END";
   if((int)cmd.ExecuteScalar()!=0) return;
  }
  string path=Path.Combine(AppContext.BaseDirectory,"Database","Setup.sql");
  if(!File.Exists(path)) throw new FileNotFoundException("Setup.sql was not found next to the application: "+path);
  // SSMS "GO" is not T-SQL, so split the script into batches on GO lines.
  foreach(var batch in Regex.Split(File.ReadAllText(path),@"^\s*GO\s*$",RegexOptions.Multiline|RegexOptions.IgnoreCase)) {
   if(string.IsNullOrWhiteSpace(batch)) continue;
   using var cmd=c.CreateCommand(); cmd.CommandText=batch; cmd.ExecuteNonQuery();
  }
 }
}
