using Microsoft.AspNetCore.Mvc.RazorPages;
using LightForum.Services;
using LightForum.Models;

namespace LightForum.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserService _userService;
        private readonly TopicService _topicService;

        public List<User> Users { get; set; } = new();
        public List<Topic> Topics { get; set; } = new();

        public IndexModel(UserService userService, TopicService topicService)
        {
            _userService = userService;
            _topicService = topicService;
        }

        public void OnGet()
        {
            Users = _userService.GetAllUsers();
            Topics = _topicService.GetAllTopics();
        }
    }
}
