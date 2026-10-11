using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class TaskCommentService : ITaskCommentService
    {
        private readonly ITaskCommentRepository _taskCommentRepository;

        public TaskCommentService(
            ITaskCommentRepository taskCommentRepository)
        {
            _taskCommentRepository = taskCommentRepository;
        }

        // GET ALL COMMENTS
        public async Task<List<TaskCommentResponseDto>> GetAllAsync()
        {
            var comments = await _taskCommentRepository.GetAllAsync();

            return comments.Select(comment =>
                new TaskCommentResponseDto
                {
                    CommentId = comment.CommentId,
                    TaskId = comment.TaskId,
                    TaskTitle = comment.TaskTitle,
                    UserId = comment.UserId,
                    UserName = comment.UserName,
                    CommentText = comment.CommentText,
                    CreatedAt = comment.CreatedAt,
                    UpdatedAt = comment.UpdatedAt
                }).ToList();
        }

        // GET COMMENT BY ID
        public async Task<TaskCommentResponseDto?> GetByIdAsync(
            int commentId)
        {
            if (commentId <= 0)
                throw new ArgumentException(
                    "Valid CommentId is required.");

            var comment =
                await _taskCommentRepository.GetByIdAsync(commentId);

            if (comment == null)
                return null;

            return new TaskCommentResponseDto
            {
                CommentId = comment.CommentId,
                TaskId = comment.TaskId,
                TaskTitle = comment.TaskTitle,
                UserId = comment.UserId,
                UserName = comment.UserName,
                CommentText = comment.CommentText,
                CreatedAt = comment.CreatedAt,
                UpdatedAt = comment.UpdatedAt
            };
        }

        // CREATE COMMENT
        public async Task<int> CreateAsync(
            TaskCommentCreateDto dto)
        {
            if (dto.TaskId <= 0)
                throw new ArgumentException(
                    "Valid TaskId is required.");

            if (dto.UserId <= 0)
                throw new ArgumentException(
                    "Valid UserId is required.");

            if (string.IsNullOrWhiteSpace(dto.CommentText))
                throw new ArgumentException(
                    "CommentText is required.");

            return await _taskCommentRepository.InsertAsync(
                dto.TaskId,
                dto.UserId,
                dto.CommentText);
        }

        // UPDATE COMMENT
        public async Task<bool> UpdateAsync(
            int commentId,
            TaskCommentUpdateDto dto)
        {
            if (commentId <= 0)
                throw new ArgumentException(
                    "Valid CommentId is required.");

            if (string.IsNullOrWhiteSpace(dto.CommentText))
                throw new ArgumentException(
                    "CommentText is required.");

            return await _taskCommentRepository.UpdateAsync(
                commentId,
                dto.CommentText);
        }

        // DELETE COMMENT
        public async Task<bool> DeleteAsync(
            int commentId)
        {
            if (commentId <= 0)
                throw new ArgumentException(
                    "Valid CommentId is required.");

            return await _taskCommentRepository.DeleteAsync(
                commentId);
        }
    }
}