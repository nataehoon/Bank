namespace BankPresentation.Models
{
    public class MenuItem
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Disabled { get; set; }
        public bool Separator { get; set; }
        public IEnumerable<MenuItem> Items { get; set; }
    }
}
