using System;
using System.Collections.Generic;
using System.Text;

namespace OnionArchitecture.Core.Mappers.Assignments
{
    public class UpdateAssignmentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}
