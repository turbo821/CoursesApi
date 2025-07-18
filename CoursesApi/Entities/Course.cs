namespace CoursesApi.Entities
{
    public class Course
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public List<Student> Students { get; set; } = new List<Student>();
    }
}
