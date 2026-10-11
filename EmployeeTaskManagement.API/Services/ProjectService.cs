using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(
            IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // GET ALL PROJECTS
        public async Task<List<ProjectResponseDto>> GetAllAsync()
        {
            var projects =
                await _projectRepository.GetAllAsync();

            return projects.Select(p => new ProjectResponseDto
            {
                ProjectId = p.ProjectId,
                ProjectName = p.ProjectName,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                Status = p.Status,
                CreatedBy = p.CreatedBy,
                CreatedByName = p.CreatedByName,
                CreatedAt = p.CreatedAt
            }).ToList();
        }

        // GET PROJECT BY ID
        public async Task<ProjectResponseDto?> GetByIdAsync(
            int projectId)
        {
            var project =
                await _projectRepository.GetByIdAsync(projectId);

            if (project == null)
                return null;

            return new ProjectResponseDto
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Status = project.Status,
                CreatedBy = project.CreatedBy,
                CreatedByName = project.CreatedByName,
                CreatedAt = project.CreatedAt
            };
        }

        // CREATE PROJECT
        public async Task<int> CreateAsync(
            ProjectCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProjectName))
            {
                throw new ArgumentException(
                    "Project name is required.");
            }

            if (dto.CreatedBy <= 0)
            {
                throw new ArgumentException(
                    "Valid CreatedBy is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                throw new ArgumentException(
                    "Project status is required.");
            }

            if (dto.EndDate.HasValue &&
                dto.EndDate.Value < dto.StartDate)
            {
                throw new ArgumentException(
                    "End date cannot be before start date.");
            }

            return await _projectRepository.InsertAsync(
                dto.ProjectName,
                dto.Description,
                dto.StartDate,
                dto.EndDate,
                dto.CreatedBy,
                dto.Status);
        }

        // UPDATE PROJECT
        public async Task<bool> UpdateAsync(
            int projectId,
            ProjectUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.ProjectName))
            {
                throw new ArgumentException(
                    "Project name is required.");
            }

            if (string.IsNullOrWhiteSpace(dto.Status))
            {
                throw new ArgumentException(
                    "Project status is required.");
            }

            if (dto.EndDate.HasValue &&
                dto.EndDate.Value < dto.StartDate)
            {
                throw new ArgumentException(
                    "End date cannot be before start date.");
            }

            return await _projectRepository.UpdateAsync(
                projectId,
                dto.ProjectName,
                dto.Description,
                dto.StartDate,
                dto.EndDate,
                dto.Status);
        }

        // DELETE PROJECT
        public async Task<bool> DeleteAsync(
            int projectId)
        {
            return await _projectRepository.DeleteAsync(
                projectId);
        }
    }
}