namespace Team_Project.Model
{
    // ============================================================
    // Player (플레이어 추상 클래스)
    // ------------------------------------------------------------
    //  - 경험치·레벨업, 스킬 습득, 인벤토리, 버프를 관리
    //  - 직업별 차이는 Warrior / Mage / Rogue 가 상속해서 구현
    //  - Model 계층이므로 사운드·화면 같은 Controller/View 기능은 호출하지 않음
    // ============================================================
    public abstract class Player : Character
    {
        public const int BuffDuration = 5;                  // 버프 지속 턴 수

        public int Round { get; set; } = 1;                 // 현재 라운드 (몇 번째 몬스터인지)
        public int Experience { get; private set; } = 0;    // 누적 경험치
        public int NextLevelExp { get; private set; } = 25; // 다음 레벨까지 필요 경험치
        public JobType CurrentJobType { get; protected set; }
        public int BuffTurn { get; private set; } = 0;
        public double BuffAttack { get; private set; } = 1.0;
        public bool Buff { get; private set; } = false;
        public List<Skill> AvailableSkills { get; } = new List<Skill>();
        public List<Item> Inventory { get; } = new List<Item>();

        // ============================================================
        // GainExperience()
        // ------------------------------------------------------------
        //  - 몬스터 처치 시 BattleManager가 호출
        //  - 경험치를 더하고, 필요 경험치를 넘을 때마다 레벨업
        //    (한 번에 여러 레벨이 오를 수 있음)
        //  - 오른 레벨 수를 반환 (0이면 레벨업 없음)
        // ============================================================
        public int GainExperience(int amount)
        {
            Experience += amount;
            int levelsGained = 0;

            while (Experience >= NextLevelExp)
            {
                Experience -= NextLevelExp;     // 남은 경험치는 다음 레벨로 이월
                LevelUp();
                levelsGained++;
            }
            return levelsGained;
        }

        // ============================================================
        // LevelUp()
        // ------------------------------------------------------------
        //  - 기본적인 능력치 상승 및 HP/MP 회복
        //  - 각 직업 클래스에서 Override
        // ============================================================
        protected virtual void LevelUp()
        {
            Level++;
            MaxHP += 20;
            MaxMP += 10;
            AttackPower += 4;
            DefensePower += 1;

            // 레벨업으로 오른 만큼 현재 HP/MP도 증가
            HP += 20;
            MP += 10;

            NextLevelExp = (int)(NextLevelExp * 1.1);  // 다음 레벨업에 필요한 경험치를 점점 늘림
        }

        // ============================================================
        // LearnSkills()
        // ------------------------------------------------------------
        //  - 현재 배울 수 있는 스킬 목록(candidates)을 받아
        //    아직 배우지 않은 스킬을 모두 습득
        //  - 새로 배운 스킬 목록을 반환 (로그 출력은 Controller 담당)
        // ============================================================
        public List<Skill> LearnSkills(IEnumerable<Skill> candidates)
        {
            var learnedNames = AvailableSkills.Select(s => s.Name).ToHashSet();
            var newSkills = candidates.Where(s => !learnedNames.Contains(s.Name)).ToList();

            AvailableSkills.AddRange(newSkills);
            return newSkills;
        }

        // ============================================================
        // TrySpendMp()
        // ------------------------------------------------------------
        //  - MP가 충분하면 차감하고 true, 부족하면 false
        // ============================================================
        public bool TrySpendMp(int cost)
        {
            if (MP < cost) return false;
            MP -= cost;
            return true;
        }

        // ============================================================
        // AddItem()
        // ------------------------------------------------------------
        //  - 같은 이름의 아이템이 있으면 개수만 늘리고, 없으면 새로 추가
        // ============================================================
        public void AddItem(Item newItem, int count = 1)
        {
            Item? existingItem = Inventory.Find(i => i.Name == newItem.Name);

            if (existingItem != null)
            {
                existingItem.Count += count;
            }
            else
            {
                newItem.Count = count;
                Inventory.Add(newItem);
            }
        }

        public int GetCurrentAttackPower()
        {
            return (int)(AttackPower * BuffAttack);
        }

        // ============================================================
        // Use()
        // ------------------------------------------------------------
        //  - 아이템 효과를 적용하고 개수를 1 줄임 (0개가 되면 인벤토리에서 제거)
        //  - 버프가 이미 지속 중이면 버프 포션은 사용하지 않음 → false 반환
        // ============================================================
        public bool Use(Item item)
        {
            switch (item.Type)
            {
                case ItemType.HealthPotion:
                    HP = Math.Min(MaxHP, HP + item.Amount);
                    break;
                case ItemType.ManaPotion:
                    MP = Math.Min(MaxMP, MP + item.Amount);
                    break;
                case ItemType.BuffPotion:
                    if (Buff) return false;
                    ApplyBuff(item.Amount, BuffDuration);
                    break;
            }

            item.Count--;
            if (item.Count <= 0)
            {
                Inventory.Remove(item);
            }
            return true;
        }

        public void ApplyBuff(int percentage, int duration)
        {
            Buff = true;
            BuffTurn = duration;
            BuffAttack = 1.0 + (percentage / 100.0);
        }

        // ============================================================
        // DecreaseBuffTurn()
        // ------------------------------------------------------------
        //  - 턴 종료 시 호출하여 버프 남은 턴을 줄이거나 해제
        //  - 출력할 메시지를 반환 (버프가 없으면 null)
        // ============================================================
        public string? DecreaseBuffTurn()
        {
            if (!Buff) return null;

            BuffTurn--;
            if (BuffTurn <= 0)
            {
                Buff = false;
                BuffAttack = 1.0; // 공격력 배율 원상 복구
                return "공격력 버프 효과가 끝났습니다. 공격력이 원래대로 돌아옵니다.";
            }
            return $"공격력 버프 효과가 {BuffTurn}턴 남았습니다.";
        }
    }
}
