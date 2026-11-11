using Team_Project.Controller;
using Team_Project.Model;

namespace Team_Project.View
{
    // ============================================================
    //  BattleForm (전투 화면)
    // ------------------------------------------------------------
    //  - 버튼 입력을 BattleManager(Controller)에 전달
    //  - BattleManager의 요청(IBattleView)에 따라 로그·상태·이미지·버튼을 갱신
    //  - 전투 규칙은 갖고 있지 않음 (화면 표시만 담당)
    // ============================================================
    public partial class BattleForm : GameForm, IBattleView
    {
        private const int HitEffectDelayMs = 300;   // 피격 이미지 표시 시간

        private readonly BattleManager controller;

        public BattleForm(Player selectedPlayer)
        {
            InitializeComponent();
            KeyPreview = true;

            // 초반 아이템 지급 (로그 문구는 실제 지급한 아이템으로 생성)
            string starterItems = ItemManager.GiveStarterItems(selectedPlayer);
            AddBattleLog($"{starterItems} 지급 완료");
            AddBattleLog("");

            // 컨트롤러 생성 (화면 자신과 플레이어를 넘김)
            controller = new BattleManager(this, selectedPlayer);

            // 포커스 강조 효과 연결
            FocusManager.FocusEvent(btnFight);
            FocusManager.FocusEvent(btnSkill);
            FocusManager.FocusEvent(btnItem);
            FocusManager.FocusEvent(btnStatus);
            FocusManager.FocusEvent(btnNextBattle);
        }

        // ============================================================
        //  이벤트 핸들러 (사용자 입력 → Controller로 전달)
        //  - 이벤트 핸들러는 async void가 허용되는 유일한 곳이라
        //    비동기 처리는 여기서 await 함
        // ============================================================

        private async void btnFight_Click(object sender, EventArgs e)
        {
            await controller.PlayerAttack();
        }

        // 스킬 창을 대화상자로 띄우고, 선택된 스킬이 있으면 사용
        private async void btnSkill_Click(object sender, EventArgs e)
        {
            Player player = controller.Player;
            Skill? selectedSkill;

            using (var skillForm = new SkillForm(player.AvailableSkills, player.MP))
            {
                if (skillForm.ShowDialog(this) != DialogResult.OK) return;
                selectedSkill = skillForm.SelectedSkill;
            }

            if (selectedSkill != null)
            {
                await controller.PlayerUseSkill(selectedSkill);
            }
        }

        // 아이템 창을 대화상자로 띄우고, 선택된 아이템이 있으면 사용
        private async void btnItem_Click(object sender, EventArgs e)
        {
            Item? selectedItem;

            using (var itemForm = new ItemForm(controller.Player.Inventory))
            {
                if (itemForm.ShowDialog(this) != DialogResult.OK) return;
                selectedItem = itemForm.SelectedItem;
            }

            if (selectedItem != null)
            {
                await controller.PlayerUseItem(selectedItem);
            }
        }

        // 상태 창은 대화상자로 띄움 (여러 개가 동시에 열리지 않음)
        private void btnStatus_Click(object sender, EventArgs e)
        {
            using var statusForm = new StatusForm(controller.Player);
            statusForm.ShowDialog(this);
        }

        private void btnNextBattle_Click(object sender, EventArgs e)
        {
            controller.LoadNextBattle();
        }

        // ============================================================
        //  IBattleView 구현 (Controller가 호출함)
        // ============================================================

        public void AddBattleLog(string message)
        {
            txtBattleLog.AppendText(message + Environment.NewLine);
            txtBattleLog.SelectionStart = txtBattleLog.Text.Length;
            txtBattleLog.ScrollToCaret();
        }

        public void ShowRound(int round)
        {
            lblRound.Text = $"Round  {round}";
        }

        public void UpdateStatus(Player p, Monster m)
        {
            lblPlayer.Text = $" {p.Name}  Lv.{p.Level}    HP:{p.HP}/{p.MaxHP}   MP:{p.MP}/{p.MaxMP}    EXP:{p.Experience}/{p.NextLevelExp}";
            lblMonster.Text = $"Lv.{m.Level} {m.Name}\n\nHP:{m.HP}/{m.MaxHP}";
        }

        public void LoadMonsterImage(string imagePath)
        {
            Image? img = ImageManager.LoadImage(imagePath);
            ReplaceMonsterImage(img);

            if (img == null) WarnImageLoadFailed(imagePath);
        }

        // 피격 이미지를 잠깐 보여준 뒤 원래 이미지로 되돌림
        public async Task ShowMonsterHitEffect(string? hitImagePath)
        {
            if (string.IsNullOrEmpty(hitImagePath)) return;

            Image? hitImg = ImageManager.LoadImage(hitImagePath);
            if (hitImg == null)
            {
                WarnImageLoadFailed(hitImagePath);
                return;
            }

            Image? original = pbMonsterImage.Image;
            pbMonsterImage.Image = hitImg;
            await Task.Delay(HitEffectDelayMs);
            pbMonsterImage.Image = original;
            hitImg.Dispose();
        }

        // 사망 이미지 표시 (사망 이미지가 없는 몬스터는 이미지를 지움)
        public void ShowMonsterDeadEffect(string? deadImagePath)
        {
            if (string.IsNullOrEmpty(deadImagePath))
            {
                ReplaceMonsterImage(null);
                return;
            }

            Image? img = ImageManager.LoadImage(deadImagePath);
            ReplaceMonsterImage(img);

            if (img == null) WarnImageLoadFailed(deadImagePath);
        }

        // ============================================================
        //  SetButtonsState
        // ------------------------------------------------------------
        //  - 턴의 종류에 따라 (플레이어 턴/몬스터 턴/승리) 버튼 활성화·비활성화
        // ============================================================
        public void SetButtonsState(BattleState state)
        {
            bool isPlayerTurn = (state == BattleState.PlayerTurn);

            btnFight.Enabled = isPlayerTurn;
            btnSkill.Enabled = isPlayerTurn;
            btnItem.Enabled = isPlayerTurn;
            btnStatus.Enabled = isPlayerTurn;

            btnNextBattle.Visible = (state == BattleState.Victory);

            if (state == BattleState.Victory) btnNextBattle.Focus();
            else if (isPlayerTurn) btnFight.Focus();
        }

        public void ShowGameOver()
        {
            NavigateTo(new GameOverForm(controller.Player));
        }

        public void ShowGameClear()
        {
            NavigateTo(new ClearForm());
        }

        // ============================================================
        //  내부 도우미
        // ============================================================

        // 몬스터 이미지를 바꾸고, 더 이상 쓰지 않는 이전 이미지는 해제
        private void ReplaceMonsterImage(Image? newImage)
        {
            Image? oldImage = pbMonsterImage.Image;
            pbMonsterImage.Image = newImage;
            oldImage?.Dispose();
        }

        private void WarnImageLoadFailed(string imagePath)
        {
            AddBattleLog($"[경고] 이미지 로드 실패: {imagePath} 파일을 찾거나 로드할 수 없습니다.");
        }
    }
}
