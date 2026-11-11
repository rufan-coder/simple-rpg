namespace Team_Project.View
{
    partial class BattleForm
    {
        // WinForms 내부 리소스 해제용 컨테이너
        private System.ComponentModel.IContainer components = null;


        // ===== 전투 화면에서 사용하는 UI 컨트롤 필드 =====
        private Label lblPlayer;        // 하단 패널 내부: 플레이어 HP/MP/레벨 상태 텍스트
        private Label lblMonster;       // 중앙 상단: 몬스터 이름/HP 텍스트
        private TextBox txtBattleLog;     // 중앙: 전투 로그(행동 결과) 표시
        private Button btnFight;        // 하단 패널: "공격"
        private Button btnSkill;        // 하단 패널: "스킬"
        private Button btnItem;       // 하단 패널: "방어"
        private Button btnStatus;       // 하단 패널: "상태"
        private Button btnNextBattle;   // 하단 패널: "다음 전투" (전투 종료 후 표시/활성)
        private Panel bottomPanel;      // 하단 명령창 영역(버튼/플레이어 상태 포함)
        

        // Dispose(): WinForms 컨트롤/컴포넌트가 사용하는 핸들/리소스 정리
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();   // 디자이너 컨테이너가 관리하는 모든 구성 요소 해제
            base.Dispose(disposing);
        }


        // InitializeComponent()
        // - 디자이너가 생성하는 UI 초기화 메서드 (컨트롤 생성/속성 설정/배치/계층 구성)
        // - 실행 시 Form() 생성자에서 가장 먼저 호출됨
        private void InitializeComponent()
        {
            lblPlayer = new Label();
            lblMonster = new Label();
            txtBattleLog = new TextBox();
            btnFight = new Button();
            btnSkill = new Button();
            btnItem = new Button();
            btnStatus = new Button();
            btnNextBattle = new Button();
            bottomPanel = new Panel();
            lblRound = new Label();
            pbMonsterImage = new PictureBox();
            bottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbMonsterImage).BeginInit();
            SuspendLayout();
            // 
            // lblPlayer
            // 
            lblPlayer.Font = new Font("Consolas", 16F, FontStyle.Bold);
            lblPlayer.ForeColor = Color.White;
            lblPlayer.Location = new Point(40, 25);
            lblPlayer.Name = "lblPlayer";
            lblPlayer.Size = new Size(1200, 40);
            lblPlayer.TabIndex = 0;
            lblPlayer.Text = "플레이어 HP/MP:";
            lblPlayer.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMonster
            // 
            lblMonster.Font = new Font("Consolas", 26F, FontStyle.Bold);
            lblMonster.ForeColor = Color.White;
            lblMonster.Location = new Point(90, 30);
            lblMonster.Name = "lblMonster";
            lblMonster.Size = new Size(320, 131);
            lblMonster.TabIndex = 0;
            lblMonster.Text = "몬스터";
            lblMonster.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtBattleLog
            // 
            txtBattleLog.BackColor = Color.Black;
            txtBattleLog.BorderStyle = BorderStyle.FixedSingle;
            txtBattleLog.Font = new Font("Consolas", 18F);
            txtBattleLog.ForeColor = Color.White;
            txtBattleLog.Location = new Point(90, 255);
            txtBattleLog.Multiline = true;
            txtBattleLog.Name = "txtBattleLog";
            txtBattleLog.ReadOnly = true;
            txtBattleLog.ScrollBars = ScrollBars.Horizontal;
            txtBattleLog.Size = new Size(1100, 244);
            txtBattleLog.TabIndex = 1;
            txtBattleLog.TabStop = false;
            // 
            // btnFight
            // 
            btnFight.Font = new Font("Consolas", 16F);
            btnFight.ForeColor = Color.White;
            btnFight.Location = new Point(260, 100);
            btnFight.Name = "btnFight";
            btnFight.Size = new Size(150, 60);
            btnFight.TabIndex = 1;
            btnFight.Text = "공격";
            btnFight.Click += btnFight_Click;
            // 
            // btnSkill
            // 
            btnSkill.Font = new Font("Consolas", 16F);
            btnSkill.ForeColor = Color.White;
            btnSkill.Location = new Point(440, 100);
            btnSkill.Name = "btnSkill";
            btnSkill.Size = new Size(150, 60);
            btnSkill.TabIndex = 2;
            btnSkill.Text = "스킬";
            btnSkill.Click += btnSkill_Click;
            // 
            // btnItem
            // 
            btnItem.Font = new Font("Consolas", 16F);
            btnItem.ForeColor = Color.White;
            btnItem.Location = new Point(620, 100);
            btnItem.Name = "btnItem";
            btnItem.Size = new Size(150, 60);
            btnItem.TabIndex = 3;
            btnItem.Text = "아이템";
            btnItem.Click += btnItem_Click;
            // 
            // btnStatus
            // 
            btnStatus.Font = new Font("Consolas", 16F);
            btnStatus.ForeColor = Color.White;
            btnStatus.Location = new Point(800, 100);
            btnStatus.Name = "btnStatus";
            btnStatus.Size = new Size(150, 60);
            btnStatus.TabIndex = 4;
            btnStatus.Text = "상태";
            btnStatus.Click += btnStatus_Click;
            // 
            // btnNextBattle
            // 
            btnNextBattle.Font = new Font("Consolas", 16F);
            btnNextBattle.ForeColor = Color.White;
            btnNextBattle.Location = new Point(980, 100);
            btnNextBattle.Name = "btnNextBattle";
            btnNextBattle.Size = new Size(150, 60);
            btnNextBattle.TabIndex = 5;
            btnNextBattle.Text = "다음 전투";
            btnNextBattle.Click += btnNextBattle_Click;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.FromArgb(25, 25, 25);
            bottomPanel.Controls.Add(lblPlayer);
            bottomPanel.Controls.Add(btnFight);
            bottomPanel.Controls.Add(btnSkill);
            bottomPanel.Controls.Add(btnItem);
            bottomPanel.Controls.Add(btnStatus);
            bottomPanel.Controls.Add(btnNextBattle);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 520);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(1280, 200);
            bottomPanel.TabIndex = 2;
            // 
            // lblRound
            // 
            lblRound.AutoSize = true;
            lblRound.Font = new Font("Consolas", 16F, FontStyle.Bold);
            lblRound.ForeColor = Color.White;
            lblRound.Location = new Point(970, 30);
            lblRound.Name = "lblRound";
            lblRound.Size = new Size(134, 32);
            lblRound.TabIndex = 3;
            lblRound.Text = "Round  1";
            // 
            // pbMonsterImage
            // 
            pbMonsterImage.Location = new Point(433, 25);
            pbMonsterImage.Name = "pbMonsterImage";
            pbMonsterImage.Size = new Size(444, 221);
            pbMonsterImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbMonsterImage.TabIndex = 4;
            pbMonsterImage.TabStop = false;
            // 
            // BattleForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.Black;
            ClientSize = new Size(1280, 720);
            Controls.Add(pbMonsterImage);
            Controls.Add(lblRound);
            Controls.Add(lblMonster);
            Controls.Add(txtBattleLog);
            Controls.Add(bottomPanel);
            Font = new Font("Consolas", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "BattleForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Battle (Undertale Style)";
            bottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbMonsterImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblRound;
        private PictureBox pbMonsterImage;
    }
}