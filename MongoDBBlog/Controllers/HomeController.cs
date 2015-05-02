using MongoDB.Driver;
using MongoDBBlog.Models;
using MongoDBBlog.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MongoDBBlog.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var blogContext = new BlogContext();
            var postsRecentes = await blogContext.Posts.Find(x => true)
                .SortByDescending(x => x.DataPublicacao)
                .Limit(10)
                .ToListAsync();

            
            var model = new IndexModel
            {
                PostsRecentes = postsRecentes,
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult NovoPost()
        {
            return View(new NovoPostModel());
        }

        [HttpPost]
        public async Task<ActionResult> NovoPost(NovoPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var blogContext = new BlogContext();
            var post = new Post
            {
                Autor = model.Autor,
                Titulo = model.Titulo,
                Texto = model.Texto,
                Tags = model.Tags.Split(' ', ',', ';'),
                DataPublicacao = DateTime.UtcNow,
                Comentarios = new List<Comentario>()
            };

            await blogContext.Posts.InsertOneAsync(post);

            return RedirectToAction("Post", new { id = post.Id });
        }

        [HttpGet]
        public async Task<ActionResult> Post(string id)
        {
            var blogContext = new BlogContext();

            var post = await blogContext.Posts.Find(x => x.Id == id).SingleOrDefaultAsync();

            if (post == null)
            {
                return RedirectToAction("Index");
            }

            var model = new PostModel
            {
                Post = post,
                NovoComentario = new NovoComentarioModel
                {
                    PostId = id
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Posts(string tag = null)
        {
            var blogContext = new BlogContext();

            Expression<Func<Post, bool>> filter = x => true;

            if (tag != null)
            {
                filter = x => x.Tags.Contains(tag);
            }

            var posts = await blogContext.Posts.Find(filter)
                .SortByDescending(x => x.DataPublicacao)
                .ToListAsync();

            return View(posts);
        }

        [HttpPost]
        public async Task<ActionResult> NovoComentario(NovoComentarioModel novoComentarioModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Post", new { id = novoComentarioModel.PostId });
            }

            var comentario = new Comentario
            {
                Autor = novoComentarioModel.Autor,
                Texto = novoComentarioModel.Texto,
                DataPublicacao = DateTime.UtcNow
            };

            var blogContext = new BlogContext();

            await blogContext.Posts.UpdateOneAsync(
                x => x.Id == novoComentarioModel.PostId,
                Builders<Post>.Update.Push(x => x.Comentarios, comentario));


            return RedirectToAction("Post", new { id = novoComentarioModel.PostId });
        }
    }
}