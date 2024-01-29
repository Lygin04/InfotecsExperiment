namespace InfotecsExperiment.Entity
{
    public class File
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public Result? Result { get; set; }
        public List<Value>? Value { get; set; }
    }
}
