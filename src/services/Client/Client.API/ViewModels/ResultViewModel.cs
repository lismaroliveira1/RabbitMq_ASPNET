using System.ComponentModel.DataAnnotations;

namespace Client.API.ViewModels;

public class ResultViewModel
{
    [Required]
    public required string Message { get; set; }
    [Required]
    public bool Success { get; set; }
    [Required]
    public required dynamic Data { get; set; }
}