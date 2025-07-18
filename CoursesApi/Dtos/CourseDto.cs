namespace CoursesApi.Dtos
{
    public record CourseDto(Guid Id, string Name, IEnumerable<StudentDto> Students);
}
