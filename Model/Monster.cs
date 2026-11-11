namespace Team_Project.Model
{
    // ============================================================
    // Monster (몬스터)
    // ------------------------------------------------------------
    //  - 라운드별 능력치와 보상 경험치, 이미지 경로를 가짐
    //  - 이미지가 없는 상태(예: 드래곤 사망 이미지)는 null로 표현
    // ============================================================
    public class Monster : Character
    {
        public int RewardExp { get; }
        public string Image { get; }
        public string? HitImage { get; }
        public string? DeadImage { get; }

        public Monster(string name, int round, int hp, int atk, int def, int exp,
                       string image, string? hitImage, string? deadImage)
        {
            Level = round;
            Name = name;
            MaxHP = hp;
            HP = hp;

            MaxMP = 0; // 몬스터는 마나를 사용하지 않음
            MP = 0;
            AttackPower = atk;
            DefensePower = def;
            RewardExp = exp;
            Image = image;
            HitImage = hitImage;
            DeadImage = deadImage;
        }
    }
}
