using System.ComponentModel.DataAnnotations;

namespace MongoDBBlog.Models.Home
{
    public class NovoPostModel
    {
        [Required]
        [DataType(DataType.Text)]
        public string Autor { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Titulo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Texto { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Tags { get; set; }
    }
}