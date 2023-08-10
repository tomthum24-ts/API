namespace API.APPLICATION.ViewModels.Permission
{
    public class AllCredentialResponseViewModel
    {
        public int Id { get; set; }
        public string NameModule { get; set; }
        public int? IdParent { get; set; }
        public int? IdPermission { get; set; }
        public string Note { get; set; }
    }
}