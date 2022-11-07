--UpdatedTableScript created by Joshua Wagner on 7/11/2022 to revise the previous version of this document to be implemented into SQL Server (instead of SQLite).
--Stores user data.
CREATE TABLE UserModel (
        UserID INT IDENTITY(1,1) PRIMARY KEY,
        Email VARCHAR(100) NOT NULL,
        Password VARCHAR(30) NOT NULL,
        Name VARCHAR(30),
        User_Type VARCHAR(20),
        Gender VARCHAR(10),
        Date_Created DATETIME,
        );

--Stores Family Group data.
CREATE TABLE FamilyGroup (
        GroupID INT IDENTITY(1,1) PRIMARY KEY,
        FamilyName VARCHAR(100) NOT NULL,
        DateCreated DATE,
        MemberStatus VARCHAR(10),
        );

--Stores the link between users and the family groups.
CREATE TABLE Membership (
        UserID INT,
        GroupID INT,
		IsAdmin INT,
		CONSTRAINT PK_Membership PRIMARY KEY (UserID, GroupID),
		FOREIGN KEY (UserID) REFERENCES UserModel(UserID),
        );

--Stores schedule data.
CREATE TABLE Schedule (
        ScheduleID INT IDENTITY(1,1) PRIMARY KEY,
        ScheduleName VARCHAR(30),
        StartDateTime DATETIME,
        EndDateTime DATETIME,
        ScheduleType VARCHAR(20),
        Frequency VARCHAR(10),
        Portion_Dose VARCHAR(20),
        Description VARCHAR(300),
        GroupID INT,
        FOREIGN KEY (GroupID) REFERENCES FamilyGroup(GroupID)
        );

--Stores event data.
CREATE TABLE Event (
        ScheduleID INT,
        EventName VARCHAR(100) NOT NULL,
        StartDateTime DATETIME,
        EndDateTime DATETIME,
        EventStatus VARCHAR(20),
        CompletedBy VARCHAR(30),
        PRIMARY KEY (ScheduleID, EventName),
        FOREIGN KEY (ScheduleID) REFERENCES Schedule(ScheduleID)
        );

--Stores pet data.
CREATE TABLE Pet (
        PetID INT IDENTITY(1,1) PRIMARY KEY,
        Name VARCHAR(100) NOT NULL,
        Species VARCHAR(50),
        Breed VARCHAR(50),
        DOB DATE,
        Allergies VARCHAR(300),
        GroupID INT,
        FOREIGN KEY (GroupID) REFERENCES FamilyGroup(GroupID)
        );

--Stores current pet trend data.
CREATE TABLE Trends (
        PetID INT,
        Date DATE,
        Height INTEGER,
        Weight INTEGER,
        PRIMARY KEY (PetID, Date),
        FOREIGN KEY (PetID) REFERENCES Pet(PetID)
        );