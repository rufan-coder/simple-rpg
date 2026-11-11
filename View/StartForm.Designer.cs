namespace Team_Project.View
{
    // 디자이너 영역(partial): 시각 요소 배치/속성 정의를 전담
    partial class StartForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle; // 게임 타이틀 라벨
        private Label lblStart; // "Press Any Key..." 안내 문구 라벨

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        // ============================================================
        // InitializeComponent()
        // ------------------------------------------------------------
        // StartForm의 UI 구성 요소들을 초기화하고 배치함
        // ============================================================
        private void InitializeComponent()
        {
            this.lblTitle = new Label();
            this.lblStart = new Label();

            this.SuspendLayout();


            // ======== 폼 기본 설정 ========
            this.AutoScaleMode = AutoScaleMode.None; // DPI 자동 조정 비활성화 (디자이너/런타임 일치)
            this.ClientSize = new Size(1280, 720);
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // 창 크기 고정 (컨트롤이 절대 좌표로 배치되어 있음)
            this.MaximizeBox = false;
            this.BackColor = Color.Black;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Simple RPG - Start";
            this.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);


            // ======== lblTitle (타이틀 텍스트) ========
            this.lblTitle.Font = new Font("Consolas", 48F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Size = new Size(1280, 200);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTitle.Location = new Point(0, 200);
            this.lblTitle.Text = "SIMPLE RPG";


            // ======== lblStart ("Press Any Key" 안내문) ========
            this.lblStart.Font = new Font("Consolas", 20F, FontStyle.Regular);
            this.lblStart.ForeColor = Color.White;
            this.lblStart.Size = new Size(1280, 100);
            this.lblStart.TextAlign = ContentAlignment.MiddleCenter;
            this.lblStart.Location = new Point(0, 500);
            this.lblStart.Text = "Press Any Key or Touch to Start";


            // ======== 이벤트 연결 ========
            this.Load += StartForm_Load;           // 폼 로드 시 이벤트 연결
            this.KeyDown += StartForm_KeyDown;     // 키 입력 감지 이벤트
            this.MouseClick += StartForm_MouseClick; // 마우스 클릭 이벤트


            // ======== 폼 컨트롤 추가 ========
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblStart);

            this.ResumeLayout(false);
        }
    }
}
