namespace RetrofitLesson.WEB.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    public class RegisterReturnModel 
    {
        public string token { get; set; }
    }
}
