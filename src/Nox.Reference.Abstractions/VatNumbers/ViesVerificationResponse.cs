namespace Nox.Reference.Abstractions
{
    public class ViesVerificationResponse
    {
        public bool isValid { get; set; }
        public DateTime requestDate { get; set; }
        public string? userError { get; set; }
        public string? name { get; set; }
        public string? address { get; set; }
        public string? requestIdentifier { get; set; }
        public string? vatNumber { get; set; }
        public ViesApproximate? viesApproximate { get; set; }
    }
}
