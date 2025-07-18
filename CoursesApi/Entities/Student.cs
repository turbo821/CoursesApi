namespace CoursesApi.Entities
{
    public class Student
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; } = null!;

        public Guid? CourseId {  get; set; }
        public Course? Course { get; set; }
    }
}
