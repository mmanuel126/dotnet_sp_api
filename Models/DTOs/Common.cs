namespace dotnet_sp_api.Models.DTOs
{
    /// <summary>
    /// stores a list of ads depending on type - used in Common API controller
    /// </summary>
    public class Ads
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HeaderText { get; set; } = string.Empty;
        public DateTime PostingDate { get; set; }
        public string TextField { get; set; } = string.Empty;
        public string NavigateUrl { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }

    /// <summary>
    /// Stores list of schools by state info - Used by the Member controller.
    /// </summary>
    public class SchoolByState
    {
        public string SchoolId { get; set; } = string.Empty;
        public string SchoolName { get; set; } = String.Empty;
    }

    /// <summary>
    /// Holds sports list data - used in
    /// </summary>
    public class SportsList
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }


}