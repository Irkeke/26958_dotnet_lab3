using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.SqlClient;
using CareerPathExplorer.Data;
using CareerPathExplorer.Models;
namespace CareerPathExplorer.Controllers;
public class UserController {
 static string Hash(string p)=>Convert.ToHexString(Rfc2898DeriveBytes.Pbkdf2(p,Encoding.UTF8.GetBytes("Classroom-Career-Explorer-Salt"),100000,HashAlgorithmName.SHA256,32));
 static bool ValidEmail(string e){e=e.Trim();try{var a=new System.Net.Mail.MailAddress(e);return a.Address==e&&e.Contains('.',StringComparison.Ordinal)&&e.IndexOf('.',e.IndexOf('@'))>0;}catch{return false;}}
 public void Register(string name,string email,string password) {
  if(string.IsNullOrWhiteSpace(name)||string.IsNullOrWhiteSpace(email)||string.IsNullOrWhiteSpace(password)) throw new Exception("All fields are required.");
  if(!ValidEmail(email)) throw new Exception("Enter a valid email.");
  if(password.Length<6) throw new Exception("Password must be at least 6 characters.");
  using var c=Database.Open(); using var cmd=c.CreateCommand();
  cmd.CommandText="INSERT INTO Users(FullName,Email,PasswordHash) VALUES(@n,@e,@p)";
  cmd.Parameters.AddWithValue("@n",name.Trim()); cmd.Parameters.AddWithValue("@e",email.Trim().ToLower()); cmd.Parameters.AddWithValue("@p",Hash(password));
  try{cmd.ExecuteNonQuery();}catch(SqlException ex) when(ex.Number==2601||ex.Number==2627){throw new Exception("Email is already registered.");}
 }
 public User? Login(string email,string password) {
  using var c=Database.Open(); using var cmd=c.CreateCommand(); cmd.CommandText="SELECT UserId,FullName,Email,Role,PasswordHash FROM Users WHERE Email=@e"; cmd.Parameters.AddWithValue("@e",email.Trim().ToLower());
  using var r=cmd.ExecuteReader(); if(!r.Read()||r.GetString(4)!=Hash(password))return null;
  return new User{Id=r.GetInt32(0),Name=r.GetString(1),Email=r.GetString(2),Role=r.GetString(3)};
 }
 public void Update(int id,string name,string email) {
  if(string.IsNullOrWhiteSpace(name)||!ValidEmail(email))throw new Exception("Enter a name and valid email.");
  using var c=Database.Open(); using var cmd=c.CreateCommand(); cmd.CommandText="UPDATE Users SET FullName=@n,Email=@e WHERE UserId=@id";
  cmd.Parameters.AddWithValue("@n",name.Trim());cmd.Parameters.AddWithValue("@e",email.Trim().ToLower());cmd.Parameters.AddWithValue("@id",id);
  try{cmd.ExecuteNonQuery();}catch(SqlException ex) when(ex.Number==2601||ex.Number==2627){throw new Exception("That email is already registered to another account.");}
 }
}