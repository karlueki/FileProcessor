namespace FileProcessor.Dto
{
    public class UserUploads
    {
        public  List<User> Users { get; set; }
        public UserUploads()
        {
            Users = new List<User>();
        }
    }
}
