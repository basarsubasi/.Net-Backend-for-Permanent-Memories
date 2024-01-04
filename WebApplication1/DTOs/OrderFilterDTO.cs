public class OrderFilterDto
{
    public string? UserName { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public string SortOrder { get; set; } = "descending"; // Default to ascending
}
