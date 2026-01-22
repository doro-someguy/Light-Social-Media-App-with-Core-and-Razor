using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using App.Models;
using App.Models.Sets;
using App.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ILogger<TopicsController> _logger;
        private readonly ForumDB _db;

        public TopicsController(ILogger<TopicsController> logger, ForumDB db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> CreateTopic()
        {
            if (Request.Method == "POST")
            {
                var form = await Request.ReadFormAsync();
                var lastTopic = _db.topics.OrderByDescending(t => t.id).FirstOrDefault();
                string title = form["title"];
                string content = form["content"];

                if(title.Length == 0)
                {
                    ViewData["errmessage"] = "Topic title cannot be empty!";
                    return View();
                }
                if (content.Length == 0)
                {
                    ViewData["errmessage"] = "Topic content cannot be empty!";
                    return View();
                }

                var topic = new Topic
                {
                    title = title,
                    content = content,
                    author = HttpContext.Session.GetString("AccountUser")
                };
                ViewData["message"] = "Topic has been created!";
                _db.topics.Add(topic);
                _db.SaveChanges();
                return View();
            } 
            else
            {  
                return View(); 
            }
        }


        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> ViewTopic(int id)
        {
            PostAndComments postAndComments;
            if (Request.Method == "POST") 
            {
                var form = await Request.ReadFormAsync();
                var author = HttpContext.Session.GetString("AccountUser");
                postAndComments = ServiceViewTopic.GetTopicAndCommentsAndPost(_db, form, id, author);
            } 
            else
            {
                postAndComments = ServiceViewTopic.GetTopicAndComments(_db, id);
            }
            return View(postAndComments);
        }

        [HttpGet]
        public IActionResult ViewTopics()
        {
            var topics = ServiceViewTopics.GetTopics(_db);
            return View(topics);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [HttpGet]
        [HttpPost]
        public IActionResult DeleteTopic(int id)
        {
            ViewData["deletedTopicId"] = ServiceDeleteTopic.DeleteTopic(_db, id);
            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult DeleteComment(int id)
        {
            List<int> res = ServiceDeleteComment.DeleteComment(_db, id);
            ViewData["deletedCommentId"] = res[0].ToString();
            ViewData["parentTopic"] = res[1].ToString();
            return View();
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> EditComment(int id)
        {
            if(Request.Method == "POST")
            {
                var form = await Request.ReadFormAsync();
                int parentTopic = ServiceEditComment.FinishEditComment(_db, form, id);
                return RedirectToAction("ViewTopic", new { id = parentTopic });
            }
            else
            {
                var comment = ServiceEditComment.ReturnComment(_db, id);
                return View(comment);
            }
        }
    }
}
