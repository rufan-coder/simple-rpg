namespace Team_Project.Model
{
    public enum JobType { Warrior, Mage, Rogue }

    // ============================================================
    // Skill (스킬)
    // ------------------------------------------------------------
    //  - MP를 소모해 공격력 × 배율만큼 피해를 주는 기술
    //  - 직업 제한과 습득 레벨을 가짐
    // ============================================================
    public class Skill
    {
        public string Name { get; }
        public int MpCost { get; }
        public double AtkMultiplier { get; }
        public string Description { get; }
        public JobType JobLimit { get; }
        public int RequiredLevel { get; }

        public Skill(string name, int mpCost, double atkMultiplier,
                     string description, JobType jobLimit, int requiredLevel)
        {
            Name = name;
            MpCost = mpCost;
            AtkMultiplier = atkMultiplier;
            Description = description;
            JobLimit = jobLimit;
            RequiredLevel = requiredLevel;
        }
    }
}
