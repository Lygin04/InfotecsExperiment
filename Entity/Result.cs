namespace InfotecsExperiment.Entity
{
    public class Result
    {
        public int Id { get; set; }

        public DateTime FirstExperimentDate { get; set; }
        public DateTime LastExperimentDate { get; set; }

        public int MaxTimeExperiment { get; set; }
        public int MinTimeExperiment { get; set; }
        public int AvgTimeExperiment { get; set; }

        public double Median { get; set; }
        public double MaxScore { get; set; }
        public double MinScore { get; set; }
        public int CountExperiment { get; set; }

        public int FileId { get; set; }
        public File File { get; set; }
    }
}
