public class StoredFile
{
    public Guid Id { get; set; }
    public string OriginalName { get; set; }
    public string StoredName { get; set; }
    public string ContentType { get; set; }
    public long Size { get; set; }
    public string Path { get; set; }
    public DateTime UploadedAt { get; set; }
}