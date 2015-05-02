using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MongoDBBlog.Startup))]
namespace MongoDBBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
