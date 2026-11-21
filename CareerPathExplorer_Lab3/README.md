# Career Path Explorer — Lab 3
**Student:** Ineza Rwibasira Kenia  
**ID:** 26958

C# Windows Forms desktop application using SQL Server LocalDB and MVC-style separation.

## Implemented
- Registration with required field, email, password-length and duplicate-email validation
- Login and logout
- Profile updates
- Career questionnaire with required-answer validation
- Database storage of assessment and answers
- Rule-based career recommendations
- Career catalogue
- Assessment history

## Requirements
- Windows 10/11
- Visual Studio 2022 with **.NET desktop development**
- .NET 8 SDK
- SQL Server Express LocalDB

## Setup
1. Extract the ZIP.
2. Open `CareerPathExplorer.csproj` in Visual Studio 2022.
3. Connect to `(localdb)\MSSQLLocalDB` using SQL Server Management Studio or Visual Studio SQL Server Object Explorer.
4. Optional: the app creates the database, tables and sample data automatically on first launch. You can also run `Database/Setup.sql` manually, but **running it again drops the tables and deletes stored project data.**
5. Restore NuGet packages, then press F5.

Connection string is in `Data/Database.cs`. Change it if your SQL Server instance differs.

## MVC folders
- Models: data classes
- Views: Windows Forms
- Controllers: validation and application/database logic
- Data: database connection
- Database: SQL setup and sample data

## Limitations
The recommendation logic is a simple classroom demonstration based on the most frequent interest category, not a professional aptitude assessment. Favorites and admin management screens are not implemented in this version. The password hash uses a shared classroom salt; production software should use a unique random salt per account and stronger security controls. Test the project in your own environment before submitting.

## GitHub upload
Create an empty GitHub repository, then run these commands in the extracted project folder:
```bash
git init
git add .
git commit -m "Implement Career Path Explorer WinForms MVC"
git branch -M main
git remote add origin https://github.com/YOUR-USERNAME/CareerPathExplorer.git
git push -u origin main
```
Replace `YOUR-USERNAME` with your GitHub username.
