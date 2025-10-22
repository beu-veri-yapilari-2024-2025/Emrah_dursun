using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GradeNode
{
    public int StudentNumber { get; set; }
    public string CourseCode { get; set; }
    public string LetterGrade { get; set; }

    public GradeNode NextStudentInCourse { get; set; }
    public GradeNode NextCourseOfStudent { get; set; }
}

public class StudentHead
{
    public int StudentNumber { get; set; }
    public GradeNode FirstCourse { get; set; }
}

public class CourseHead
{
    public string CourseCode { get; set; }
    public GradeNode FirstStudent { get; set; }
}
public class GradeManager
{
    public List<StudentHead> Students = new List<StudentHead>();
    public List<CourseHead> Courses = new List<CourseHead>();

    public void AddCourseToStudent(int studentNumber, string courseCode, string letterGrade)
    {
        var student = Students.FirstOrDefault(s => s.StudentNumber == studentNumber);
        if (student == null)
        {
            student = new StudentHead { StudentNumber = studentNumber };
            Students.Add(student);
        }

        var course = Courses.FirstOrDefault(c => c.CourseCode == courseCode);
        if (course == null)
        {
            course = new CourseHead { CourseCode = courseCode };
            Courses.Add(course);
        }

        var newNode = new GradeNode
        {
            StudentNumber = studentNumber,
            CourseCode = courseCode,
            LetterGrade = letterGrade
        };

        // Öğrenciye ders ekle
        if (student.FirstCourse == null)
            student.FirstCourse = newNode;
        else
        {
            var current = student.FirstCourse;
            while (current.NextCourseOfStudent != null)
                current = current.NextCourseOfStudent;
            current.NextCourseOfStudent = newNode;
        }

        // Derse öğrenci ekle
        if (course.FirstStudent == null)
            course.FirstStudent = newNode;
        else
        {
            var current = course.FirstStudent;
            while (current.NextStudentInCourse != null)
                current = current.NextStudentInCourse;
            current.NextStudentInCourse = newNode;
        }
    }

    public void RemoveCourseFromStudent(int studentNumber, string courseCode)
    {
        var student = Students.FirstOrDefault(s => s.StudentNumber == studentNumber);
        if (student == null) return;

        GradeNode prev = null;
        var current = student.FirstCourse;
        while (current != null)
        {
            if (current.CourseCode == courseCode)
            {
                if (prev == null)
                    student.FirstCourse = current.NextCourseOfStudent;
                else
                    prev.NextCourseOfStudent = current.NextCourseOfStudent;
                break;
            }
            prev = current;
            current = current.NextCourseOfStudent;
        }

        var course = Courses.FirstOrDefault(c => c.CourseCode == courseCode);
        if (course == null) return;

        prev = null;
        current = course.FirstStudent;
        while (current != null)
        {
            if (current.StudentNumber == studentNumber)
            {
                if (prev == null)
                    course.FirstStudent = current.NextStudentInCourse;
                else
                    prev.NextStudentInCourse = current.NextStudentInCourse;
                break;
            }
            prev = current;
            current = current.NextStudentInCourse;
        }
    }

    public void ListStudentsInCourse(string courseCode)
    {
        var course = Courses.FirstOrDefault(c => c.CourseCode == courseCode);
        if (course == null)
        {
            Console.WriteLine("Ders bulunamadı.");
            return;
        }

        var list = new List<GradeNode>();
        var current = course.FirstStudent;
        while (current != null)
        {
            list.Add(current);
            current = current.NextStudentInCourse;
        }

        foreach (var node in list.OrderBy(n => n.StudentNumber))
            Console.WriteLine($"Öğrenci: {node.StudentNumber}, Harf Notu: {node.LetterGrade}");
    }

    public void ListCoursesOfStudent(int studentNumber)
    {
        var student = Students.FirstOrDefault(s => s.StudentNumber == studentNumber);
        if (student == null)
        {
            Console.WriteLine("Öğrenci bulunamadı.");
            return;
        }

        var list = new List<GradeNode>();
        var current = student.FirstCourse;
        while (current != null)
        {
            list.Add(current);
            current = current.NextCourseOfStudent;
        }

        foreach (var node in list.OrderBy(n => n.CourseCode))
            Console.WriteLine($"Ders: {node.CourseCode}, Harf Notu: {node.LetterGrade}");
    }
}

class Program
{
    static GradeManager manager = new GradeManager();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("\n--- Öğrenci-Ders Sistemi ---");
            Console.WriteLine("1 - Öğrenciye yeni ders ekle");
            Console.WriteLine("2 - Derse yeni öğrenci ekle");
            Console.WriteLine("3 - Öğrencinin bir dersini sil");
            Console.WriteLine("4 - Dersteki bir öğrenciyi sil");
            Console.WriteLine("5 - Dersteki öğrencileri listele");
            Console.WriteLine("6 - Öğrencinin derslerini listele");
            Console.WriteLine("0 - Çıkış");
            Console.Write("Seçim: ");
            var secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                case "2":
                    Console.Write("Öğrenci numarası: ");
                    int sn = int.Parse(Console.ReadLine());
                    Console.Write("Ders kodu: ");
                    string dc = Console.ReadLine();
                    Console.Write("Harf notu: ");
                    string hn = Console.ReadLine();
                    manager.AddCourseToStudent(sn, dc, hn);
                    Console.WriteLine("Eklendi.");
                    break;
                case "3":
                case "4":
                    Console.Write("Öğrenci numarası: ");
                    int snSil = int.Parse(Console.ReadLine());
                    Console.Write("Ders kodu: ");
                    string dcSil = Console.ReadLine();
                    manager.RemoveCourseFromStudent(snSil, dcSil);
                    Console.WriteLine("Silindi.");
                    break;
                case "5":
                    Console.Write("Ders kodu: ");
                    string dersKod = Console.ReadLine();
                    manager.ListStudentsInCourse(dersKod);
                    break;
                case "6":
                    Console.Write("Öğrenci numarası: ");
                    int ogrNo = int.Parse(Console.ReadLine());
                    manager.ListCoursesOfStudent(ogrNo);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim.");
                    break;
            }
        }
    }
}