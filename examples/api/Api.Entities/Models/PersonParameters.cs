namespace Api.Entities
{
    public class PersonParameters : PageParameters
    {
        public PersonParameters()
        {
            OrderBy = "FullName";
        }

        public string? SearchName { get; set; }
    }
}