using System.ComponentModel.DataAnnotations;

namespace server.DTOs;

public class CreateSchoolRequest
{
    [Required] public string School { get; set; }

    [Required] public string Department { get; set; }

    [Required] public string State { get; set; }

    public int? Grade { get; set; }
}
