DELETE FROM Students;
INSERT INTO Faculties (FacultyName) VALUES (N'Công nghệ thông tin');
INSERT INTO Faculties (FacultyName) VALUES (N'Kinh tế');
SELECT * FROM Faculties;
INSERT INTO Students (StudentCode, FullName, Age, FacultyID) 
VALUES ('SV001', N'Nguyễn Văn Anh', 20, 1); -- Thuộc khoa CNTT

INSERT INTO Students (StudentCode, FullName, Age, FacultyID) 
VALUES ('SV002', N'Trần Thị Bình', 21, 2); -- Thuộc khoa Kinh tế