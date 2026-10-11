using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(
            ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        // GET ALL TASKS
        public async Task<List<TaskResponseDto>> GetAllAsync()
        {
            var tasks =
                await _taskRepository.GetAllAsync();

            return tasks.Select(task =>
                new TaskResponseDto
                {
                    TaskId = task.TaskId,

                    ProjectId = task.ProjectId,

                    ProjectName = task.ProjectName,

                    AssignedTo = task.AssignedTo,

                    AssignedEmployee = task.AssignedEmployee,

                    TaskTitle = task.TaskTitle,

                    Description = task.Description,

                    Priority = task.Priority,

                    Status = task.Status,

                    DueDate = task.DueDate,

                    CreatedBy = task.CreatedBy,

                    CreatedByName = task.CreatedByName,

                    CreatedAt = task.CreatedAt,

                    UpdatedAt = task.UpdatedAt

                }).ToList();
        }

        // GET TASK BY ID
        public async Task<TaskResponseDto?> GetByIdAsync(
            int taskId)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException(
                    "Valid TaskId is required.");
            }

            var task =
                await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
            {
                return null;
            }

            return new TaskResponseDto
            {
                TaskId = task.TaskId,

                ProjectId = task.ProjectId,

                ProjectName = task.ProjectName,

                AssignedTo = task.AssignedTo,

                AssignedEmployee = task.AssignedEmployee,

                TaskTitle = task.TaskTitle,

                Description = task.Description,

                Priority = task.Priority,

                Status = task.Status,

                DueDate = task.DueDate,

                CreatedBy = task.CreatedBy,

                CreatedByName = task.CreatedByName,

                CreatedAt = task.CreatedAt,

                UpdatedAt = task.UpdatedAt
            };
        }

        // CREATE TASK
        public async Task<int> CreateAsync(
            TaskCreateDto dto)
        {
            if (dto.ProjectId <= 0)
            {
                throw new ArgumentException(
                    "Valid ProjectId is required.");
            }

            if (dto.AssignedTo <= 0)
            {
                throw new ArgumentException(
                    "Valid AssignedTo UserId is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.TaskTitle))
            {
                throw new ArgumentException(
                    "TaskTitle is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Priority))
            {
                throw new ArgumentException(
                    "Priority is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                throw new ArgumentException(
                    "Status is required.");
            }

            if (dto.CreatedBy <= 0)
            {
                throw new ArgumentException(
                    "Valid CreatedBy UserId is required.");
            }

            return await _taskRepository.InsertAsync(
                dto.ProjectId,
                dto.AssignedTo,
                dto.TaskTitle,
                dto.Description,
                dto.Priority,
                dto.Status,
                dto.DueDate,
                dto.CreatedBy);
        }

        // UPDATE TASK
        public async Task<bool> UpdateAsync(
            int taskId,
            TaskUpdateDto dto)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException(
                    "Valid TaskId is required.");
            }

            if (dto.ProjectId <= 0)
            {
                throw new ArgumentException(
                    "Valid ProjectId is required.");
            }

            if (dto.AssignedTo <= 0)
            {
                throw new ArgumentException(
                    "Valid AssignedTo UserId is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.TaskTitle))
            {
                throw new ArgumentException(
                    "TaskTitle is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Priority))
            {
                throw new ArgumentException(
                    "Priority is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                throw new ArgumentException(
                    "Status is required.");
            }

            return await _taskRepository.UpdateAsync(
                taskId,
                dto.ProjectId,
                dto.AssignedTo,
                dto.TaskTitle,
                dto.Description,
                dto.Priority,
                dto.Status,
                dto.DueDate,
                dto.CreatedBy);
        }

        // DELETE TASK
        public async Task<bool> DeleteAsync(
            int taskId)
        {
            if (taskId <= 0)
            {
                throw new ArgumentException(
                    "Valid TaskId is required.");
            }

            return await _taskRepository.DeleteAsync(taskId);
        }
    }
}