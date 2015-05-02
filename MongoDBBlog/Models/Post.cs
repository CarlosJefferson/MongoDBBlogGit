using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDBBlog.Models;
using System;
using System.Collections.Generic;

namespace MongoDBBlog
{
    public class Post
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Autor { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
        public DateTime DataPublicacao { get; set; }
        public List<Comentario> Comentarios { get; set; }
        public string[] Tags { get; set; }
    }
}
