namespace AjaxDemo.Models
{
    public class UserDTO
    {
        public string? userName { get; set; }
        public string? userEmail { get; set; }
        public int? userAge { get; set; }
        public IFormFile? userPhoto { get; set; }
    }
}
