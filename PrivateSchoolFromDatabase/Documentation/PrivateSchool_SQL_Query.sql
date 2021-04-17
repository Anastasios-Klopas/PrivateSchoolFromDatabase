use PrivateSchool;
-- ta insert eginan me indetify specification me ka8e insert na auksanetai me bhma 1
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Kwstas','Kwstakhs','1985/01/01',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Stauros', 'Stauraras', '1980/01/16', 2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Giannhs', 'Giannakhs', '1990/02/17', 2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Antrianna','Antriannoula','2000/02/28',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Basilhs','Basilakhs','2002/10/30',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Maria','Maraki','2001/11/12',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Elenh','Elenitsa','1999/09/10',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Giwta','Giwtoula','1998/08/14',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Giwrgos','Giwrgakhs','1997/07/25',2500);
insert into Students (StudentID,FirstName,LastName,DateOfBirth,TuitionFee)
values ('Xristina','Xristinaki','1996/06/02',2500);

insert into Trainers (FirstName,LastName,Subject)
values ('Dhmhtrhs', 'Dhmhtrakhs', 'C#');
insert into Trainers (FirstName,LastName,Subject)
values ('Stella', 'Stellitsa', 'C#');
insert into Trainers (FirstName,LastName,Subject)
values ('Mpamphs', 'Mpampakhs', 'C#');
insert into Trainers (FirstName,LastName,Subject)
values ('Thodwrhs', 'Thodwrakhs', 'C#');
insert into Trainers (FirstName,LastName,Subject)
values ('Manwlhs', 'Manwlakhs', 'Java');
insert into Trainers (FirstName,LastName,Subject)
values ('Eua', 'Euaki', 'Java');

insert into Courses (CourseTitle,Stream,Type,StartDate,EndDate)
values ('CB1', 'C#', 'Part Time','2020/03/20','2020/09/20');
insert into Courses (CourseTitle,Stream,Type,StartDate,EndDate)
values ('CB1', 'C#', 'Full Time','2020/04/15','2020/07/15');
insert into Courses (CourseTitle,Stream,Type,StartDate,EndDate)
values ('CB2', 'Java', 'Part Time','2021/02/10','2021/08/10');
insert into Courses (CourseTitle,Stream,Type,StartDate,EndDate)
values ('CB2', 'Java', 'Full Time','2020/09/15','2020/11/15');

insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Individual project', 'Calculator','2020/10/10', 40,60);
insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Assignment Part 1', 'Private School','2020/05/15', 30,70);
insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Assignment Part 2', 'Video Club','2020/05/16', 80,50);
insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Assignment Part 3', 'Trapeza','2020/02/28', 100,100);
insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Individual team project', 'E-Shop','2020/09/01', 80,40);
insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Assignment Part 4', 'University','2020/02/28', 100,66);
insert into Assignments (AssignmentTitle,Description,SubDateTime,OralMark,TotalMark)
values ('Assignment Part 5', 'Twitter','2020/01/30', 100,80)

--------------------------------------------

-- list of students in private school
select * from Students;

-- list of trainers in private school
select * from Trainers;

-- list of courses in private school
select * from Courses;

-- list of assignments in private school
select * from Assignments;

-- course per assignments
select  CourseTitle,Stream,Type,AssignmentTitle,Description from Courses
inner join Assign on Assign.CourseID=Courses.CourseID
inner join Assignments on Assignments.AssignmentID=Assign.AssignmentID
order by CourseTitle,Stream,Type,AssignmentTitle,Description;

-- course per students
select CourseTitle,Stream,Type,FirstName,LastName from Courses
inner join Watch on Watch.CourseID=Courses.CourseID
inner join Students on Students.StudentID=Watch.StudentID
order by CourseTitle,Stream,Type,FirstName,LastName;

-- course per trainers
select CourseTitle,Stream,Type,FirstName,LastName from Courses
inner join Teach on Teach.CourseID=Courses.CourseID
inner join Trainers on Trainers.TrainerID=Teach.TrainerID
order by CourseTitle,Stream,Type,FirstName,LastName;

-- assignments per course per student 
select FirstName,LastName,CourseTitle,Stream,Type,AssignmentTitle,Description from Courses
inner join Watch on Watch.CourseID=Courses.CourseID
inner join Students on Students.StudentID=Watch.StudentID
inner join Assign on Assign.CourseID=Courses.CourseID
inner join Assignments on Assignments.AssignmentID=Assign.AssignmentID
order by FirstName,LastName,CourseTitle,Stream,Type,AssignmentTitle,Description;

-- list of students that belongs to more than one courses
select distinct FirstName,LastName,CourseTitle,Stream,Type from Students
inner join Watch w on w.StudentID=Students.StudentID
inner join Watch s on w.StudentID=s.StudentID
inner join Courses on Courses.CourseID=w.CourseID and w.CourseID<>s.CourseID
order by FirstName,LastName,CourseTitle,Stream,Type;

select Count(Watch.CourseID),FirstName,LastName from Watch
inner join Students on Students.StudentID=Watch.StudentID
group by FirstName,LastName 
having Count(Watch.CourseID)>=2;

--SELECT S1.StudentID AS Student, S2.StudentID AS Student, S1.CourseID
--FROM watch S1, watch S2
--WHERE S1.CourseID <> S2.CourseID
--AND S1.StudentID = S2.StudentID 
--ORDER BY S1.CourseID 




