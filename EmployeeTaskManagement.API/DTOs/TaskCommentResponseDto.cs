namespace EmployeeTaskManagement.API.DTOs
{
    public class TaskCommentResponseDto
    {
        public int CommentId { get; set; }

        public int TaskId { get; set; }

        public string? TaskTitle { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public string CommentText { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}