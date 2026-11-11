using Team_Project.Model;

namespace Team_Project.View
{
    // ============================================================
    // GameOverForm (게임 오버 화면)
    // ------------------------------------------------------------
    //  - 플레이어 HP가 0이 되었을 때 표시
    //  - 사망 당시의 능력치와 도달한 라운드를 보여줌
    //  - '처음으로' 버튼: 시작 화면으로 복귀
    // ============================================================
    public partial class GameOverForm : GameForm
    {
        public GameOverForm(Player player)
        {
            InitializeComponent();

            lblGameOver.Text = "GAME OVER";
            lblStats.Text = $"{player.Name} (Lv.{player.Level})\n" +
                            $"HP: {player.HP}/{player.MaxHP}\n" +
                            $"MP: {player.MP}/{player.MaxMP}\n" +
                            $"ATK: {player.AttackPower}   DEF: {player.DefensePower}\n" +
                            $"최종 라운드: {player.Round}";

            FocusManager.FocusEvent(btnRestart);
        }

        // 시작 화면으로 돌아가고 이 화면은 닫음
        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartForm.ReturnToStart();
            CloseWithoutExit();
        }
    }
}
