using System.ComponentModel.DataAnnotations;

namespace BookCatalogApi.Models;

public class Book
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required]
    public string Author { get; set; } = string.Empty;
    [Required]
    public string Genre { get; set; } = string.Empty;
    [Range(1500, 2100, ErrorMessage = "Published year must be between 1500 and 2100")]
    public int PublishedYear { get; set; }
}
