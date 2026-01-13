public static class DbSeeder
{
    public static void Seed(SchoolDbContext context)
    {
        if (context.Schools.Any()) return;

        var schools = new List<School>();
        for (int i = 1; i <= 10; i++)
        {
            schools.Add(new School
            {
                Name = $"School {i}",
                Principal = $"Principal {i}",
                Address = $"Address {i}"
            });
        }

        context.Schools.AddRange(schools);
        context.SaveChanges();

        var students = new List<Student>();
        for (int i = 1; i <= 20; i++)
        {
            students.Add(new Student
            {
                FullName = $"Student {i}",
                StudentId = $"STD{i:000}",
                Email = $"student{i}@example.com",
                Phone = "0123456789",
                SchoolId = schools[i % 10].Id   
            });
        }

        context.Students.AddRange(students);
        context.SaveChanges();
    }
}
