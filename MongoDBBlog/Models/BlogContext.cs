using MongoDB.Driver;
using System.Configuration;

namespace MongoDBBlog.Models
{
    public class BlogContext
    {
        private const string CONNECTION_STRING_NAME = "Blog";
        private const string DATABASE_NAME = "blog";
        private const string POSTS_COLLECTION_NAME = "posts";
        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _database;

        static BlogContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(DATABASE_NAME);
        }
        public IMongoClient Client
        {
            get { return _client; }
        }
        public IMongoCollection<Post> Posts
        {
            get { return _database.GetCollection<Post>(POSTS_COLLECTION_NAME); }
        }
    }
}