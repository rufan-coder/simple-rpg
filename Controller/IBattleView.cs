using Team_Project.Model;

namespace Team_Project.Controller
{
    // 전투 진행 상태 (버튼 활성화 기준)
    public enum BattleState { PlayerTurn, MonsterTurn, Victory }

    // ============================================================
    // IBattleView
    // ------------------------------------------------------------
    //  - BattleManager(Controller)가 화면에 요청할 수 있는 일을 정의한 인터페이스
    //  - BattleManager는 BattleForm을 직접 알지 않고 이 인터페이스만 사용
    //    → 화면을 바꾸거나, 가짜 화면으로 전투 로직을 테스트하기 쉬움
    // ============================================================
    public interface IBattleView
    {
        void AddBattleLog(string message);
        void ShowRound(int round);
        void UpdateStatus(Player player, Monster monster);
        void LoadMonsterImage(string imagePath);
        Task ShowMonsterHitEffect(string? hitImagePath);
        void ShowMonsterDeadEffect(string? deadImagePath);
        void SetButtonsState(BattleState state);
        void ShowGameOver();
        void ShowGameClear();
    }
}
