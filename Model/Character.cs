namespace Team_Project.Model
{
    // ============================================================
    // Character (최상위 추상 클래스)
    // ------------------------------------------------------------
    //  - 모든 전투 캐릭터(플레이어, 몬스터)의 공통 속성과 행동 정의
    //  - 데미지 공식 관련 로직 포함
    //  - 직접 생성 불가(abstract), 반드시 상속을 통해 사용
    //  - 능력치는 외부에서 마음대로 바꾸지 못하도록 protected set
    // ============================================================
    public abstract class Character
    {
        // ======== 공통 속성 ========
        public string Name { get; protected set; } = "";  // 캐릭터 이름
        public int Level { get; protected set; } = 1;      // 현재 레벨
        public int HP { get; protected set; }              // 현재 체력
        public int MP { get; protected set; }              // 현재 마나
        public int MaxHP { get; protected set; }           // 최대 체력
        public int MaxMP { get; protected set; }           // 최대 마나
        public int AttackPower { get; protected set; }     // 공격력
        public int DefensePower { get; protected set; }    // 방어력

        // 난수 발생기 (.NET 6 이상에서 제공하는 공용 인스턴스 사용)
        protected static Random Rand => Random.Shared;

        public int Attack()
        {
            return AttackPower;
        }

        // ============================================================
        // TakeDamage()
        // ------------------------------------------------------------
        //  - 방어력에 따라 피해를 줄이고(50 / (50 + 방어력)),
        //    ±10% 무작위 편차를 더해 실제 피해를 계산
        //  - 실제로 받은 피해량을 반환
        // ============================================================
        public virtual int TakeDamage(int damage)
        {
            const double DEFENSE_CONSTANT = 50.0;
            double reductionRatio = DEFENSE_CONSTANT / (DEFENSE_CONSTANT + DefensePower);
            double baseDamage = damage * reductionRatio;
            double randomMultiplier = (Rand.NextDouble() * 0.2) + 0.9;
            int finalDamage = (int)(baseDamage * randomMultiplier);

            HP -= finalDamage;    // HP에서 실제 피해만큼 감소
            if (HP < 0) HP = 0;
            return finalDamage;
        }
    }
}
