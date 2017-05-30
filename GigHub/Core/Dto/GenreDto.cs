using System.ComponentModel.DataAnnotations;

namespace GigHub.Core.Dto
{
    public class GenreDto
    {
        public byte Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }
    }
}