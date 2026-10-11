namespace EmployeeTaskManagement.API.DTOs
{
    public class TaskCommentCreateDto
    {
        public int TaskId { get; set; }

        public int UserId { get; set; }

        public string CommentText { get; set; } = string.Empty;
    }
}