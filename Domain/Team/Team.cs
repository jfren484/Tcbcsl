namespace Domain.Team
{
    public class Team
    {
        public int TeamId { get; private set; }
        public int Year { get; private set; }
        public string Name { get; private set; }
        public string Division { get; private set; }
        public int ChurchId { get; private set; }
        public string ChurchName { get; private set; }
        public int HeadCoachId { get; private set; }
        public string HeadCoachName { get; private set; }
        public string FieldInformation { get; private set; }
        public string Comments { get; private set; }
    }
}
