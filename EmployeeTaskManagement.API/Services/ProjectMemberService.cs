using EmployeeTaskManagement.API.DTOs;
using EmployeeTaskManagement.API.Repositories;

namespace EmployeeTaskManagement.API.Services
{
    public class ProjectMemberService : IProjectMemberService
    {
        private readonly IProjectMemberRepository
            _projectMemberRepository;

        public ProjectMemberService(
            IProjectMemberRepository projectMemberRepository)
        {
            _projectMemberRepository =
                projectMemberRepository;
        }

        // GET ALL PROJECT MEMBERS
        public async Task<List<ProjectMemberResponseDto>>
            GetAllAsync()
        {
            var projectMembers =
                await _projectMemberRepository.GetAllAsync();

            return projectMembers.Select(pm =>
                new ProjectMemberResponseDto
                {
                    ProjectMemberId =
                        pm.ProjectMemberId,

                    ProjectId =
                        pm.ProjectId,

                    ProjectName =
                        pm.ProjectName,

                    UserId =
                        pm.UserId,

                    EmployeeName =
                        pm.EmployeeName,

                    Email =
                        pm.Email,

                    JoinedAt =
                        pm.JoinedAt,

                    IsActive =
                        pm.IsActive

                }).ToList();
        }

        // GET PROJECT MEMBER BY ID
        public async Task<ProjectMemberResponseDto?>
            GetByIdAsync(int projectMemberId)
        {
            var projectMember =
                await _projectMemberRepository
                    .GetByIdAsync(projectMemberId);

            if (projectMember == null)
                return null;

            return new ProjectMemberResponseDto
            {
                ProjectMemberId =
                    projectMember.ProjectMemberId,

                ProjectId =
                    projectMember.ProjectId,

                ProjectName =
                    projectMember.ProjectName,

                UserId =
                    projectMember.UserId,

                EmployeeName =
                    projectMember.EmployeeName,

                Email =
                    projectMember.Email,

                JoinedAt =
                    projectMember.JoinedAt,

                IsActive =
                    projectMember.IsActive
            };
        }

        // CREATE PROJECT MEMBER
        public async Task<int> CreateAsync(
            ProjectMemberCreateDto dto)
        {
            if (dto.ProjectId <= 0)
            {
                throw new ArgumentException(
                    "Valid ProjectId is required.");
            }

            if (dto.UserId <= 0)
            {
                throw new ArgumentException(
                    "Valid UserId is required.");
            }

            return await _projectMemberRepository.InsertAsync(
                dto.ProjectId,
                dto.UserId);
        }

        // UPDATE PROJECT MEMBER
        public async Task<bool> UpdateAsync(
            int projectMemberId,
            ProjectMemberUpdateDto dto)
        {
            if (projectMemberId <= 0)
            {
                throw new ArgumentException(
                    "Valid ProjectMemberId is required.");
            }

            return await _projectMemberRepository.UpdateAsync(
                projectMemberId,
                dto.IsActive);
        }

        // DELETE PROJECT MEMBER
        public async Task<bool> DeleteAsync(
            int projectMemberId)
        {
            if (projectMemberId <= 0)
            {
                throw new ArgumentException(
                    "Valid ProjectMemberId is required.");
            }

            return await _projectMemberRepository.DeleteAsync(
                projectMemberId);
        }
    }
}