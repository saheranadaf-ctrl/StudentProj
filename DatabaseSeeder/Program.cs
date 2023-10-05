using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<StudentDataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("CS")));

        var app = builder.Build();

        // Seed the database
        Seeder.Initialize(app.Services);

        app.Run();


class Seeder
    {
        public static void SeedDatabase()
        {
            // Your seeding logic goes here
            Console.WriteLine("Database seeded successfully!");
        }

                public static void Initialize(IServiceProvider serviceProvider)
        {
    using (var context = new TestDemoContext(
    serviceProvider.GetRequiredService<DbContextOptions<TestDemoContext>>()))
        {
                // Check if the database is already seeded
                if (context.StudentData.Any())
                {
                    Console.WriteLine("Database already seeded.");
                    return; // Database is already seeded
                }

                // Add your seeding logic here
                var students = new StudentDatum[]
                {
                    new StudentDatum
                    {
                        StudentId = "STDN00001",
                        Gender = "F",
                        Nationlity = "USA",
                        PlaceOfBirth = "New York",
                        StageId = "lowerlevel",
                        GradeId = "G01",
                        SectionId = "A",
                        Topic = "Math",
                        Semester = "F",
                        Relation = "Mother",
                        RaisedHands = 10,
                        VisitedResources = 20,
                        AnnouncementsView = 5,
                        Discussion = 15,
                        ParentAnsweringSurvey = "Yes",
                        ParentschoolSatisfaction = "Good",
                        StudentAbsenceDays = "Under-7",
                        StudentMarks = 85,
                        Classes = "M"
                    },
                    // Add more students as needed
                };

                context.StudentData.AddRange(students);
                context.SaveChanges();
                Console.WriteLine("Database seeded successfully.");
        }
    }
}

    


