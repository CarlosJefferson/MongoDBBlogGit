using System;

namespace MongoDBBlog.Models
{
    public class Comentario
    {
        public string Autor { get; set; }
        public string Texto { get; set; }
        public string Email { get; set; }
        public DateTime DataPublicacao { get; set; }
    }
}