using Team_Project.Model;

namespace Team_Project.Controller
{
    // ============================================================
    // BattleManager (전투 Controller)
    // ------------------------------------------------------------
    //  - 공격·스킬·아이템 사용, 몬스터 반격, 승리·패배 판정 등 전투 규칙 담당
    //  - 화면 처리는 IBattleView에게 요청만 하고 직접 하지 않음
    // ============================================================
    public class BattleManager
    {
        private const int MonsterTurnDelayMs = 400;   // 몬스터 반격 전 대기 시간

        private readonly IBattleView view;
        private readonly Player player;
        private Monster monster;

        // ============================================================
        // 생성자
        // ------------------------------------------------------------
        //  - 화면(IBattleView)과 플레이어를 전달받아 첫 전투를 준비
        // ============================================================
        public BattleManager(IBattleView view, Player player)
        {
            this.view = view;
            this.player = player;

            monster = MonsterManager.GetMonster(player.Round);
            StartBattle();
        }

        // View에서 필요한 데이터 제공
        public Player Player => player;

        // ============================================================
        //  StartBattle()
        // ------------------------------------------------------------
        //  - 현재 몬스터로 전투 시작: 스킬 확인, 상태·이미지 표시
        // ============================================================
        private void StartBattle()
        {
            GrantNewSkills();

            view.ShowRound(player.Round);   // 예전에는 라벨이 처음 값(Round 1)에서 바뀌지 않았음
            view.AddBattleLog($"{monster.Name}이(가) 나타났다!");
            view.AddBattleLog("");
            view.UpdateStatus(player, monster);
            view.LoadMonsterImage(monster.Image);
            view.SetButtonsState(BattleState.PlayerTurn);
        }

        // 다음 라운드 몬스터 불러오기 (승리 후 '다음 전투' 버튼)
        public void LoadNextBattle()
        {
            monster = MonsterManager.GetMonster(player.Round);
            StartBattle();
        }

        // ============================================================
        //  PlayerAttack()
        // ------------------------------------------------------------
        //  - 플레이어의 기본 공격
        // ============================================================
        public async Task PlayerAttack()
        {
            view.SetButtonsState(BattleState.MonsterTurn); // 입력 잠금 (연속 클릭 방지)

            int damage = monster.TakeDamage(player.GetCurrentAttackPower());

            view.AddBattleLog($"{player.Name}의 공격!");
            view.AddBattleLog($"{monster.Name}에게 {damage}의 피해를 입혔다!");
            SoundManager.Instance.PlaySFX("Attack", 90);
            await view.ShowMonsterHitEffect(monster.HitImage);

            await NextTurn();
        }

        // ============================================================
        //  PlayerUseSkill()
        // ------------------------------------------------------------
        //  - MP를 소모해 (공격력 × 스킬 배율) 피해를 줌
        // ============================================================
        public async Task PlayerUseSkill(Skill skill)
        {
            // 최종 방어: 스킬 창에서 막지만, 혹시 MP가 부족하면 턴을 소모하지 않음
            if (!player.TrySpendMp(skill.MpCost))
            {
                view.AddBattleLog($"MP가 부족합니다! (필요: {skill.MpCost})");
                return;
            }

            view.SetButtonsState(BattleState.MonsterTurn); // 입력 잠금

            int calculatedDamage = (int)(player.GetCurrentAttackPower() * skill.AtkMultiplier);
            int damage = monster.TakeDamage(calculatedDamage);

            view.AddBattleLog($"{player.Name}의 {skill.Name}!");
            view.AddBattleLog($"{monster.Name}에게 {damage}의 피해를 입혔다!");
            view.UpdateStatus(player, monster);
            SoundManager.Instance.PlaySFX("Attack", 90);
            await view.ShowMonsterHitEffect(monster.HitImage);

            await NextTurn();
        }

        // ============================================================
        //  PlayerUseItem()
        // ------------------------------------------------------------
        //  - 아이템 사용도 한 턴을 소모함 (사용 후 몬스터 반격)
        // ============================================================
        public async Task PlayerUseItem(Item item)
        {
            if (item.Type == ItemType.BuffPotion && player.Buff)
            {
                view.AddBattleLog($"이미 버프 지속 중입니다. (남은 턴: {player.BuffTurn})");
                return;
            }

            view.SetButtonsState(BattleState.MonsterTurn); // 입력 잠금 (예전에는 빠져 있어 몬스터 턴에도 공격 가능했음)
            player.Use(item);

            view.AddBattleLog($"{player.Name}이(가) [{item.Name}]을(를) 사용했습니다.");
            view.AddBattleLog(item.Type switch
            {
                ItemType.HealthPotion => $"체력이 {item.Amount} 회복되었습니다.",
                ItemType.ManaPotion => $"마나가 {item.Amount} 회복되었습니다.",
                _ => $"공격력이 {item.Amount}% 증가되었습니다. ({Player.BuffDuration}턴)"
            });

            view.UpdateStatus(player, monster);
            await NextTurn();
        }

        // ============================================================
        //  NextTurn()
        // ------------------------------------------------------------
        //  - 플레이어 행동 이후의 처리 순서를 한곳에서 관리
        //    1) 몬스터가 살아 있으면 반격
        //    2) 플레이어 사망 확인 → 게임 오버
        //    3) 몬스터 사망 확인 → 승리 처리 / 아니면 버프 턴 감소 후 플레이어 턴
        // ============================================================
        private async Task NextTurn()
        {
            // 1. 몬스터 생존 시 반격
            if (monster.HP > 0)
            {
                view.AddBattleLog("");
                view.AddBattleLog($"[{monster.Name}의 턴]");
                await Task.Delay(MonsterTurnDelayMs);

                int damage = player.TakeDamage(monster.Attack());

                if (damage == 0) view.AddBattleLog($"{monster.Name}이(가) 반격했으나 {player.Name}이(가) 회피했다!");
                else view.AddBattleLog($"{monster.Name}의 공격! {damage}의 피해를 입었다!");
            }

            view.UpdateStatus(player, monster);

            // 2. 플레이어 사망 처리
            if (player.HP <= 0)
            {
                SoundManager.Instance.StopBGM();
                SoundManager.Instance.PlaySFX("GameOver", 40);
                view.ShowGameOver();
                return;
            }

            // 3. 몬스터 사망 처리
            if (monster.HP <= 0)
            {
                ProcessVictory();
                return;
            }

            string? buffLog = player.DecreaseBuffTurn();
            if (buffLog != null) view.AddBattleLog(buffLog);
            view.AddBattleLog("");

            view.SetButtonsState(BattleState.PlayerTurn); // 입력 잠금 해제
        }

        // ============================================================
        //  ProcessVictory()
        // ------------------------------------------------------------
        //  - 경험치·레벨업·스킬 습득·아이템 드롭 처리
        //  - 마지막 라운드였다면 게임 클리어
        // ============================================================
        private void ProcessVictory()
        {
            view.ShowMonsterDeadEffect(monster.DeadImage);

            // 최종 보스 처치 시 게임 클리어
            if (player.Round >= MonsterManager.FinalRound)
            {
                view.ShowGameClear();
                return;
            }
            player.Round++;

            view.AddBattleLog("");
            view.AddBattleLog($"{monster.Name}을(를) 쓰러뜨렸다! {monster.RewardExp} 경험치를 얻었다!");

            int levelsGained = player.GainExperience(monster.RewardExp);
            if (levelsGained > 0)
            {
                SoundManager.Instance.PlaySFX("Level", 90);
                view.AddBattleLog(levelsGained == 1
                    ? $"레벨이 올랐습니다! (Lv.{player.Level})"
                    : $"레벨이 {levelsGained} 올랐습니다! (Lv.{player.Level})");
            }
            GrantNewSkills();

            // 아이템 드롭
            if (Random.Shared.Next(0, 100) < ItemManager.DropChancePercent)
            {
                Item dropped = ItemManager.GetRandomDrop();
                int count = Random.Shared.Next(1, 4);   // 1~3개
                player.AddItem(dropped, count);
                view.AddBattleLog($"[{dropped.Name}] {count}개 획득!");
            }

            view.UpdateStatus(player, monster);
            view.SetButtonsState(BattleState.Victory);
        }

        // 현재 레벨에서 배울 수 있는 스킬을 모두 습득하고 로그 출력
        private void GrantNewSkills()
        {
            var candidates = SkillManager.GetAvailableSkills(player.CurrentJobType, player.Level);
            foreach (Skill skill in player.LearnSkills(candidates))
            {
                view.AddBattleLog($"[{skill.Name}]을(를) 배웠습니다!");
            }
        }
    }
}
