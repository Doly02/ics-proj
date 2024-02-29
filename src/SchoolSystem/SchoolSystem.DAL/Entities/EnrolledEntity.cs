namespace SchoolSystem.DAL.Entities {

    // Entity between StudentEntity and SubjectEntity (Many-to-many)
    public record EnrolledEntity()
    {
        public required Guid Id { get; set; }
        
        public required StudentEntity Student { get; set; }
        public required SubjectEntity Subject { get; set; }
        
        public required Guid StudentId { get; set; }
        public required Guid SubjectId { get; set; }
    }
}

