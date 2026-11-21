IF DB_ID('CareerPathExplorerDB') IS NULL CREATE DATABASE CareerPathExplorerDB;
GO
USE CareerPathExplorerDB;
GO
IF OBJECT_ID('AssessmentAnswers','U') IS NOT NULL DROP TABLE AssessmentAnswers;
IF OBJECT_ID('Assessments','U') IS NOT NULL DROP TABLE Assessments;
IF OBJECT_ID('AnswerOptions','U') IS NOT NULL DROP TABLE AnswerOptions;
IF OBJECT_ID('Questions','U') IS NOT NULL DROP TABLE Questions;
IF OBJECT_ID('Careers','U') IS NOT NULL DROP TABLE Careers;
IF OBJECT_ID('Users','U') IS NOT NULL DROP TABLE Users;
GO
CREATE TABLE Users(UserId INT IDENTITY PRIMARY KEY,FullName NVARCHAR(100) NOT NULL,Email NVARCHAR(150) NOT NULL UNIQUE,PasswordHash CHAR(64) NOT NULL,Role NVARCHAR(20) NOT NULL DEFAULT 'User',CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME());
CREATE TABLE Questions(QuestionId INT IDENTITY PRIMARY KEY,QuestionText NVARCHAR(500) NOT NULL);
CREATE TABLE AnswerOptions(OptionId INT IDENTITY PRIMARY KEY,QuestionId INT NOT NULL REFERENCES Questions(QuestionId),OptionText NVARCHAR(250) NOT NULL,InterestArea NVARCHAR(80) NOT NULL);
CREATE TABLE Careers(CareerId INT IDENTITY PRIMARY KEY,CareerName NVARCHAR(120) NOT NULL,Description NVARCHAR(1000) NOT NULL,InterestArea NVARCHAR(80) NOT NULL);
CREATE TABLE Assessments(AssessmentId INT IDENTITY PRIMARY KEY,UserId INT NOT NULL REFERENCES Users(UserId),SubmittedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME());
CREATE TABLE AssessmentAnswers(AssessmentAnswerId INT IDENTITY PRIMARY KEY,AssessmentId INT NOT NULL REFERENCES Assessments(AssessmentId) ON DELETE CASCADE,QuestionId INT NOT NULL REFERENCES Questions(QuestionId),OptionId INT NOT NULL REFERENCES AnswerOptions(OptionId),CONSTRAINT UQ_Assessment_Question UNIQUE(AssessmentId,QuestionId));
INSERT INTO Questions(QuestionText) VALUES
('Which activity interests you most?'),('Which task would you enjoy doing?'),('Which school subject do you prefer?'),('What kind of problem would you like to solve?'),('Which work environment sounds best?');
INSERT INTO AnswerOptions(QuestionId,OptionText,InterestArea) VALUES
(1,'Building apps and working with technology','Technology'),(1,'Helping people','People'),(1,'Designing visual content','Creativity'),(1,'Business planning','Business'),
(2,'Writing code','Technology'),(2,'Teaching or advising','People'),(2,'Creating media','Creativity'),(2,'Organizing projects','Business'),
(3,'Computer science or mathematics','Technology'),(3,'Psychology or social studies','People'),(3,'Art or communication','Creativity'),(3,'Economics','Business'),
(4,'Improving digital systems','Technology'),(4,'Solving community challenges','People'),(4,'Creating designs','Creativity'),(4,'Improving organizations','Business'),
(5,'Technology company','Technology'),(5,'School or hospital','People'),(5,'Creative studio','Creativity'),(5,'Business office','Business');
INSERT INTO Careers(CareerName,Description,InterestArea) VALUES
('Software Developer','Builds and maintains software applications.','Technology'),('Data Analyst','Uses data to find patterns and support decisions.','Technology'),('Cybersecurity Analyst','Protects computer systems and information.','Technology'),
('Teacher','Helps learners develop knowledge and skills.','People'),('Human Resources Officer','Supports recruitment and staff development.','People'),('Counsellor','Guides people through personal or educational challenges.','People'),
('Graphic Designer','Creates visual communication materials.','Creativity'),('UX/UI Designer','Designs useful and easy-to-use digital experiences.','Creativity'),('Content Creator','Produces written, visual or video content.','Creativity'),
('Project Manager','Plans work and coordinates teams.','Business'),('Business Analyst','Studies business needs and recommends improvements.','Business'),('Entrepreneur','Develops opportunities into a business.','Business');
GO