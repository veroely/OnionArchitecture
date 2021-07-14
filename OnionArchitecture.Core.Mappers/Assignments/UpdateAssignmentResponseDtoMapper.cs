using OnionArchitecture.Core.Domain.Entities;

namespace OnionArchitecture.Core.Mappers.Assignments
{
    public class UpdateAssignmentResponseDtoMapper:IMapper<UpdateAssignmentResponseDto, Assignment>
    {
        public UpdateAssignmentResponseDto Map(Assignment source)
        {
            var updateAssignmentResponseDto = new UpdateAssignmentResponseDto
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Completed = source.Completed
            };

            return updateAssignmentResponseDto;
        }
    }
}
