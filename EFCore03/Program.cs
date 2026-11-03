using EFCore03.Data;
using EFCore03.Models;

using var db = new AppDbContext();

SeedFromCode(db);

Header("CREATE");
AddInstructor(db);
AddCourse(db, instructorId: 1, categoryId: 3);
AddCourse(db, instructorId: 99, categoryId: 3);   // instructor مش موجود
AddStudents(db);
EnrollStudent(db, studentId: 4, courseId: 3);     // تمام
EnrollStudent(db, studentId: 4, courseId: 4);     // كورس مش منشور
EnrollStudent(db, studentId: 4, courseId: 3);     // مسجل قبل كدا
EnrollStudent(db, studentId: 99, courseId: 3);    // طالب مش موجود

        // الأجزاء الجاية هتتضاف هنا (READ / UPDATE / DELETE / BULK)
    

    static void Header(string title) => Console.WriteLine($"\n===== {title} =====");

// ---------- Seeding من الكود (بس لو الجدول فاضي) ----------
static void SeedFromCode(AppDbContext db)
{
    if (!db.Students.Any())
    {
        db.Students.AddRange(
            new Student { FullName = "Esraa Mohamed", Email = "esraa@mail.com", DateOfBirth = new DateTime(2002, 5, 1) },
            new Student { FullName = "Mona Said", Email = "mona@mail.com", DateOfBirth = new DateTime(2001, 8, 20) },
            new Student { FullName = "Youssef Adel", Email = "youssef@mail.com", DateOfBirth = new DateTime(2000, 2, 11) },
            new Student { FullName = "Nour Tarek", Email = "nour@mail.com", DateOfBirth = new DateTime(2003, 12, 5) },
            new Student { FullName = "Hana Samir", Email = "hana@mail.com", DateOfBirth = new DateTime(1999, 9, 9) });
        db.SaveChanges();
    }

    if (!db.Enrollments.Any())
    {
        var ids = db.Students.OrderBy(s => s.Id).Select(s => s.Id).Take(5).ToList();
        var d = new DateTime(2024, 2, 1);
        db.Enrollments.AddRange(
            new Enrollment { StudentId = ids[0], CourseId = 1, EnrolledAt = d, ProgressPercentage = 40, Status = EnrollmentStatus.Active },
            new Enrollment { StudentId = ids[0], CourseId = 2, EnrolledAt = d, ProgressPercentage = 100, Status = EnrollmentStatus.Completed, FinalGrade = 92.5m },
            new Enrollment { StudentId = ids[1], CourseId = 1, EnrolledAt = d, ProgressPercentage = 100, Status = EnrollmentStatus.Completed, FinalGrade = 85m },
            new Enrollment { StudentId = ids[1], CourseId = 3, EnrolledAt = d, ProgressPercentage = 20, Status = EnrollmentStatus.Active },
            new Enrollment { StudentId = ids[2], CourseId = 1, EnrolledAt = d, ProgressPercentage = 10, Status = EnrollmentStatus.Dropped },
            new Enrollment { StudentId = ids[2], CourseId = 5, EnrolledAt = d, ProgressPercentage = 60, Status = EnrollmentStatus.Active },
            new Enrollment { StudentId = ids[3], CourseId = 5, EnrolledAt = d, ProgressPercentage = 5, Status = EnrollmentStatus.Dropped },
            new Enrollment { StudentId = ids[4], CourseId = 2, EnrolledAt = d, ProgressPercentage = 75, Status = EnrollmentStatus.Active });
        db.SaveChanges();
    }
}

// ---------- CREATE ----------
static void AddInstructor(AppDbContext db)
{
    var i = new Instructor { FullName = "Laila Nabil", Email = "laila@learn.com", Bio = "Data engineer", JoinedAt = DateTime.Today };
    db.Instructors.Add(i);
    db.SaveChanges();
    Console.WriteLine($"Instructor added. Id = {i.Id}");
}

static void AddCourse(AppDbContext db, int instructorId, int categoryId)
{
    if (!db.Instructors.Any(i => i.Id == instructorId)) { Console.WriteLine($"Instructor {instructorId} does not exist."); return; }
    if (!db.Categories.Any(c => c.Id == categoryId)) { Console.WriteLine($"Category {categoryId} does not exist."); return; }

    var c = new Course { Title = "EF Core Deep Dive", Description = "Advanced EF Core", Price = 180m, Level = CourseLevel.Advanced, InstructorId = instructorId, CategoryId = categoryId };
    db.Courses.Add(c);
    db.SaveChanges();
    Console.WriteLine($"Course added. Id = {c.Id}");
}

static void AddStudents(AppDbContext db)
{
    var list = new List<Student>
        {
            new() { FullName = "Ali Gamal", Email = "ali@mail.com", DateOfBirth = new DateTime(2002, 1, 1) },
            new() { FullName = "Dina Fathy", Email = "dina@mail.com", DateOfBirth = new DateTime(2001, 3, 3) },
            new() { FullName = "Karim Wael", Email = "karim@mail.com", DateOfBirth = new DateTime(2000, 7, 7) }
        };
    db.Students.AddRange(list);
    db.SaveChanges();
    Console.WriteLine("Students added. Ids: " + string.Join(", ", list.Select(s => s.Id)));
}

static void EnrollStudent(AppDbContext db, int studentId, int courseId)
{
    var student = db.Students.Find(studentId);
    if (student == null) { Console.WriteLine($"Enrollment refused: student {studentId} not found."); return; }

    var course = db.Courses.Find(courseId);
    if (course == null) { Console.WriteLine($"Enrollment refused: course {courseId} not found."); return; }

    if (!course.IsPublished) { Console.WriteLine($"Enrollment refused: course '{course.Title}' is not published."); return; }

    if (db.Enrollments.Any(e => e.StudentId == studentId && e.CourseId == courseId))
    { Console.WriteLine($"Enrollment refused: {student.FullName} is already enrolled in '{course.Title}'."); return; }

    db.Enrollments.Add(new Enrollment { StudentId = studentId, CourseId = courseId, EnrolledAt = DateTime.Now, ProgressPercentage = 0, Status = EnrollmentStatus.Active });
    db.SaveChanges();
    Console.WriteLine($"{student.FullName} enrolled in '{course.Title}'.");
}
