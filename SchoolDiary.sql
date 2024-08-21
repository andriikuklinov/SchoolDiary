USE master;
GO
--kill connection
DECLARE @kill varchar(8000) = '';
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(1000), session_id) + ';'
FROM sys.dm_exec_sessions
WHERE database_id = db_id('SchoolDiary');

EXEC(@kill);
GO
--drop database
DROP DATABASE IF EXISTS SchoolDiary;
GO
-- create database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE [name]='SchoolDiary')
BEGIN
	CREATE DATABASE SchoolDiary;
END;
GO

USE SchoolDiary;
GO

CREATE TABLE [Group](
	Id int NOT NULL IDENTITY(1,1),
	[Name] nvarchar(5) NOT NULL,
	CONSTRAINT PK_Group_Id PRIMARY KEY(Id)
);
GO

CREATE TABLE Student(
	Id int NOT NULL IDENTITY(1,1),
	FirstName nvarchar(20) NOT NULL,
	LastName nvarchar(20) NOT NULL,
	MiddleName nvarchar(20) NOT NULL,
	Email nvarchar(30),
	PhoneNumber nvarchar(30) NOT NULL,
	[Address] nvarchar(30) NOT NULL,
	Birthdate date NOT NULL,
	EnrolnmentDate date,
	CONSTRAINT PK_Student_Id PRIMARY KEY(Id),
	CONSTRAINT UQ_Student_Email UNIQUE(Email),
	CONSTRAINT UQ_Student_PhoneNumber UNIQUE(PhoneNumber)
);
GO

ALTER TABLE Student ADD CONSTRAINT DF_Student_EnrolnmentDate DEFAULT GETDATE() FOR EnrolnmentDate;
ALTER TABLE Student ADD GroupId int NOT NULL;
ALTER TABLE Student ADD CONSTRAINT FK_Student_GroupId FOREIGN KEY(GroupId) REFERENCES [Group](Id);
GO

CREATE TABLE Class(
	Id int NOT NULL IDENTITY(1,1),
	[Name] nvarchar(5),
	[GroupId] int NOT NULL,
	CONSTRAINT PK_Class_Id PRIMARY KEY(Id),
	CONSTRAINT FK_Class_GroupId FOREIGN KEY (GroupId) REFERENCES [Group](Id)
);
GO

CREATE TABLE ClassScore(
	Id int NOT NULL IDENTITY(1,1),
	Score int,
	ScoreDate date,
	ClassId int NOT NULL,
	StudentId int NOT NULL,
	CONSTRAINT PK_ClassScore_Id PRIMARY KEY(Id),
	CONSTRAINT FK_ClassScore_ClassId FOREIGN KEY(ClassId) REFERENCES Class(Id),
	CONSTRAINT FK_ClassScore_StudentId FOREIGN KEY(StudentId) REFERENCES Student(Id)
);
GO

--CREATE TABLE StudentClass(
--	Id int NOT NULL IDENTITY(1,1),
--	StudentId int NOT NULL,
--	ClassId int NOT NULL,
--	Score tinyint,
--	ScoreDate date NOT NULL,
--	CONSTRAINT PK_StudentClass_Id PRIMARY KEY(Id),
--	CONSTRAINT FK_StudentClass_StudentId FOREIGN KEY(StudentId) REFERENCES Student(Id),
--	CONSTRAINT FK_StudentClass_ClassId FOREIGN KEY(ClassId) REFERENCES Class(Id)
--);
--GO

--ALTER TABLE StudentClass ADD CONSTRAINT DF_StudentClass_ScoreDate DEFAULT GETDATE() FOR ScoreDate;
--ALTER TABLE StudentClass ADD CONSTRAINT DF_StudentClass_Score DEFAULT 0 FOR Score;
--GO

CREATE TABLE Teacher(
	Id int NOT NULL IDENTITY(1,1),
	FirstName nvarchar(30) NOT NULL,
	MiddleName nvarchar(30) NOT NULL,
	LastName nvarchar(30) NOT NULL,
	Birthdate date,
	PhoneNumber nvarchar(30) NOT NULL,
	Email nvarchar(30) NOT NULL,
	CONSTRAINT PK_Teacher_Id PRIMARY KEY(Id),
	CONSTRAINT UQ_Teacher_PhoneNumber UNIQUE(PhoneNumber),
	CONSTRAINT UQ_Teacher_Email UNIQUE(Email)
);
GO

ALTER TABLE Class ADD TeacherId int NOT NULL;
ALTER TABLE Class ADD CONSTRAINT FK_Class_TeacherId FOREIGN KEY(TeacherId) REFERENCES Teacher(Id);
GO

CREATE TABLE [Subject](
	Id int NOT NULL IDENTITY(1,1),
	SubjectName nvarchar(30) NOT NULL,
	CONSTRAINT PK_Subject_Id PRIMARY KEY(Id)
);
GO

ALTER TABLE Class ADD SubjectId INT NOT NULL;
ALTER TABLE Class ADD CONSTRAINT FK_Class_SubjectId FOREIGN KEY(SubjectId) REFERENCES Subject(Id);
GO

CREATE TABLE ClassRoom(
	Id int NOT NULL IDENTITY(1,1),
	ClassName nvarchar(30),
	CONSTRAINT PK_ClassRoom_Id PRIMARY KEY(Id)
);
GO

ALTER TABLE Class ADD ClassRoomId int NOT NULL;
ALTER TABLE Class ADD CONSTRAINT FK_Class_ClassRoomId FOREIGN KEY(ClassRoomId) REFERENCES ClassRoom(Id);
GO

CREATE TABLE ClassPeriod(
	Id int NOT NULL IDENTITY(1,1),
	[DayOfWeek] tinyint, --Select DATENAME(WEEKDAY, 0)
	StartTime time NOT NULL,
	EndTime time NOT NULL,
	CONSTRAINT PK_ClassPeriod_Id PRIMARY KEY(Id)
);
GO

ALTER TABLE Class ADD ClassPeriodId int NOT NULL;
ALTER TABLE Class ADD CONSTRAINT FK_Class_ClassPeriodId FOREIGN KEY(ClassPeriodId) REFERENCES ClassPeriod(Id);
GO

CREATE TABLE Term(
	Id int NOT NULL IDENTITY(1,1),
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	CONSTRAINT PK_Term_Id PRIMARY KEY(Id)
);
GO

ALTER TABLE Class ADD TermId int NOT NULL;
ALTER TABLE Class ADD CONSTRAINT FK_Class_TermId FOREIGN KEY(TermId) REFERENCES Term(Id);
GO

CREATE TABLE SchoolYear(
	Id int NOT NULL IDENTITY(1,1),
	YearName nvarchar(30),
	StartDate date,
	EndDate date,
	CONSTRAINT PK_SchoolYear_Id PRIMARY KEY(Id)
);
GO

ALTER TABLE Term ADD YearId int NOT NULL;
ALTER TABLE Term ADD CONSTRAINT FK_Term_YearId FOREIGN KEY(YearId) REFERENCES SchoolYear(Id);
GO

INSERT INTO [Group]([Name])VALUES
('1A'),
('1B'),
('2A'),
('2B')
GO

INSERT INTO Student(FirstName, LastName, MiddleName, Email, PhoneNumber, [Address], Birthdate, EnrolnmentDate, GroupId) VALUES
('Andrii', 'Kuklinov', 'Petrovich', 'akuklinov@email.com', '+380675423411', 'Lviv', '20180222', '20240901', 1),
('Igor', 'Bartko', 'Ivanovich', 'ibartko@email.com', '+380675423412','Lviv', '20180201', '20240901', 1),
('Ivan', 'Kovaluk', 'Petrovich', 'ikovaluk@email.com', '+380675423413','Lviv', '20180812', '20240901', 1),
('Rostislav', 'Kroshnui', 'Olegovuch', 'rkroshnui@email.com', '+380675423414','Lviv', '20181222', '20240901', 1),
('Oleg', 'Dubanuch', 'Igorovuch', 'odubanuch@email.com', '+380675423415','Lviv', '20180701', '20240901', 1),
('Olga', 'Kulikova', 'Petrivna', 'okuklinova@email.com', '+380675423416','Lviv', '20190504', '20250901', 2),
('Ivanna', 'Vaschuk', 'Ivanivna', 'ivaschuk@email.com', '+380675423417','Lviv', '20190123', '20250901', 2),
('Ksenia', 'Ivanec', 'Petrivna', 'kivanec@email.com', '+380675423418','Lviv','20190112', '20250901', 2),
('Sasha', 'Kulikova', 'Olexandrivna', 'skuklinova@email.com', '+380675423419','Lviv', '20191119', '20250901', 2),
('Olexandr', 'Boreckii', 'Andriivna', 'oboreckii@email.com', '+380675423410','Lviv', '20191103', '20250901', 2)
GO

INSERT INTO [Subject](SubjectName) VALUES
('Math'),
('Nature'),
('English'),
('Ukrainian'),
('Art'),
('Sport'),
('Craft'),
('Geography'),
('OBG');
GO

INSERT INTO Teacher(FirstName, LastName, MiddleName, Email, PhoneNumber, Birthdate) VALUES
('Andrii', 'Kuklinov', 'Petrovich', 'andriikuklinov@email.com', '+380673665654', '19940901'),
('Olga', 'Oschuk', 'Ivanivna', 'ooschuk@email.com', '+38067111111', '19950101'),
('Ivan', 'Ivanchuk', 'Petrovich', 'iivanchuk@email.com', '+380633665658', '19910703'),
('Andrii', 'Pichishin', 'Andriiovich', 'apitchishin@email.com', '+380653115624', '19910111')
GO

INSERT INTO ClassRoom(ClassName) VALUES
('10'),
('11'),
('12'),
('13'),
('14'),
('15'),
('21'),
('22'),
('23'),
('24'),
('25'),
('31'),
('32'),
('33'),
('34'),
('35')
GO

INSERT INTO ClassPeriod([DayOfWeek], StartTime, EndTime) VALUES
(0, '08:30', '09:15'),
(0, '09:25', '11:10'),
(0, '11:20', '12:05'),
(1, '08:30', '09:15'),
(1, '09:25', '11:10'),
(1, '11:20', '12:05'),
(2, '08:30', '09:15'),
(2, '09:25', '11:10'),
(2, '11:20', '12:05'),
(3, '08:30', '09:15'),
(3, '09:25', '11:10'),
(3, '11:20', '12:05'),
(4, '08:30', '09:15'),
(4, '09:25', '11:10');
GO

INSERT INTO SchoolYear(YearName, StartDate, EndDate) VALUES
('2024-2025', '20240901', '20250530');
GO

INSERT INTO Term(StartDate, EndDate, YearId) VALUES
('20240901', '20241230',1),
('20250112', '20250530',1);

INSERT INTO Class(TeacherId, SubjectId, ClassRoomId, ClassPeriodId, TermId, GroupId) VALUES
(1, 1, 1, 1, 1, 1),
(1, 2, 2, 2, 1, 1),
(2, 3, 1, 3, 1, 1),
(2, 4, 3, 4, 1, 1),
(2, 5, 4, 5, 1, 1),
(3, 6, 5, 6, 1, 1),
(4, 7, 5, 7, 1, 1),
(4, 8, 5, 8, 1, 1),
(3, 9, 5, 9, 1, 1),
(4, 1, 1, 10, 1, 1),
(4, 2, 2, 11, 1, 1),
(3, 3, 3, 12, 1, 1),
(3, 1, 3, 13, 1, 1),
(3, 8, 4, 14, 1, 1);
GO

INSERT INTO ClassScore(Score, ScoreDate, ClassId, StudentId) VALUES
(4, '20240902', 1, 1),
(5, '20240902', 2, 1),
(3, '20240902', 3, 1),
(1, '20240902', 1, 2),
(1, '20240902', 3, 2),
(1, '20240907', 2, 3),
(1, '20240907', 2, 3),
(1, '20240909', 1, 5);
GO

-- GET schedule for specified group
CREATE PROCEDURE GetGroupSchedule
	@groupName nvarchar(5)
AS
	BEGIN
		SELECT g.[Name], sub.SubjectName, CONCAT(t.FirstName, ' ', t.LastName) AS [Name], DATENAME(WEEKDAY, cp.DayOfWeek) AS [WeekDay], cr.ClassName, cp.StartTime, cp.EndTime FROM [Group] g  
			INNER JOIN Class c ON c.GroupId = g.Id
			INNER JOIN [Subject] sub ON sub.Id = c.SubjectId
			INNER JOIN ClassPeriod cp ON cp.Id = c.ClassPeriodId
			INNER JOIN ClassRoom cr ON cr.Id = c.ClassRoomId
			INNER JOIN Teacher t ON t.Id = c.TeacherId
		WHERE g.[Name]=@groupName
		ORDER BY cp.DayOfWeek
	END;
GO

DECLARE @groupName nvarchar(5) = '1A';
EXEC GetGroupSchedule @groupName;
GO

--GET scores for specified student and subject
CREATE PROCEDURE GetStudentScoresForSubject
	@studentId int,
	@subjectName nvarchar(30)
	AS
	BEGIN
		SELECT CONCAT(s.FirstName, ' ', s.LastName) AS [Name], sub.SubjectName, cs.Score, cs.ScoreDate FROM Student s
			INNER JOIN [Group] g ON g.Id = s.GroupId
			INNER JOIN Class c ON c.GroupId = g.Id
			INNER JOIN ClassScore cs ON cs.ClassId = c.Id AND cs.StudentId = s.Id
			INNER JOIN [Subject] sub ON sub.Id = c.SubjectId
		WHERE s.Id =1 AND sub.SubjectName = 'Math';
	END
GO

DECLARE @studentId  int = 1, @subjectName nvarchar(30) = 'Math'
EXEC GetStudentScoresForSubject @studentId, @subjectName;
GO
