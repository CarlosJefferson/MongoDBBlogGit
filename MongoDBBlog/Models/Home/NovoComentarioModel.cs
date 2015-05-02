using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MongoDBBlog.Models.Home
{
    public class NovoComentarioModel
    {
        [HiddenInput(DisplayValue = false)]
        public string PostId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Autor { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Texto { get; set; }

        
    }
}
