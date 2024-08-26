namespace SchoolDiary.WEB.Models
{
    public class AddUserToRoleModel
    {
        public string UserId { get; set; }
        public List<string> Roles { get; set; }
    }
}
