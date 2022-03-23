namespace ASP_Net_MVC.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public IFormFile File { get; set; }
    }
}
