namespace EmployeeTaskManagement.API.DTOs
{
    public class TaskCommentUpdateDto
    {
        public int CommentId { get; set; }

        public string CommentText { get; set; } = string.Empty;
    }
}