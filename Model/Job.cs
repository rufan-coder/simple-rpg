namespace Team_Project.Model
{
    // ============================================================
    // Warrior (전사)
    // ------------------------------------------------------------
    //  - 체력과 방어력이 높아 오래 버티는 직업
    // ============================================================
    public class Warrior : Player
    {
        public const string Description = "튼튼한 체력과 방어력으로 버티면서 적을 물리치는 직업입니다.";

        public Warrior()
        {
            Name = "전사";
            MaxHP = 140;
            HP = MaxHP;
            MaxMP = 40;
            MP = MaxMP;
            AttackPower = 20;
            DefensePower = 10;
            CurrentJobType = JobType.Warrior;
        }

        protected override void LevelUp()
        {
            base.LevelUp();
            DefensePower += 2; // 전사는 방어력이 추가로 더 오름
            MaxHP += 5;        // 체력도 추가 증가
        }
    }

    // ============================================================
    // Mage (마법사)
    // ------------------------------------------------------------
    //  - 체력은 낮으나 공격력과 마나가 높아 스킬 피해량이 큼
    // ============================================================
    public class Mage : Player
    {
        public const string Description = "체력과 방어력은 약하나 높은 공격력과 마나로 공격적인 플레이가 가능한 직업입니다.";

        public Mage()
        {
            Name = "마법사";
            MaxHP = 80;
            HP = MaxHP;
            MaxMP = 60;
            MP = MaxMP;
            AttackPower = 30;   // 세 직업 중 가장 높은 공격력
            DefensePower = 4;
            CurrentJobType = JobType.Mage;
        }

        protected override void LevelUp()
        {
            base.LevelUp();     // 기본 성장 수행
            AttackPower += 3;
            MaxMP += 5;
        }
    }

    // ============================================================
    // Rogue (도적)
    // ------------------------------------------------------------
    //  - 기본 능력치는 평균적이나 확률적으로 공격을 회피
    // ============================================================
    public class Rogue : Player
    {
        public const string Description = "평균적인 능력치를 가지고 있으나 확률적으로 적의 공격을 회피할 수 있는 직업입니다.";

        public int DodgeChance { get; protected set; } = 20;   // 회피 확률(%)

        public Rogue()
        {
            Name = "도적";
            MaxHP = 90;
            HP = MaxHP;
            MaxMP = 50;
            MP = MaxMP;
            AttackPower = 25;
            DefensePower = 6;
            CurrentJobType = JobType.Rogue;
        }

        // 회피에 성공하면 피해 0
        public override int TakeDamage(int damage)
        {
            if (Rand.Next(1, 101) <= DodgeChance)
            {
                return 0;
            }
            return base.TakeDamage(damage);
        }

        protected override void LevelUp()
        {
            base.LevelUp();     // 기본 성장 수행
            AttackPower += 2;   // 도적은 공격력 중심 성장
        }
    }
}
