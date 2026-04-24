public class FileSummary
{
    public Guid Id { get; set; }
    public string OriginalName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public DateTime UploadedAt { get; set; }
}