using Team_Project.Controller;

namespace Team_Project.View
{
    // ============================================================
    // StartForm (시작 화면, 프로그램의 메인 폼)
    // ------------------------------------------------------------
    //  - 타이틀과 "Press Any Key to Start" 문구 표시
    //  - ESC 키: 종료 확인 후 프로그램 종료
    //  - 아무 키 / 마우스 클릭: 직업 선택 화면으로 이동
    //  - 메인 폼이므로 닫지 않고 숨겨 두었다가, 다시 시작할 때 재사용
    //    (메인 폼이 닫히면 프로그램이 종료됨)
    // ============================================================
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent(); // 디자이너 파일에서 정의된 UI 요소 초기화
            KeyPreview = true;     // 폼이 키보드 입력을 먼저 받도록 설정
        }

        // ============================================================
        // ReturnToStart()
        // ------------------------------------------------------------
        //  - 게임 오버/클리어 화면의 '다시 하기'에서 호출
        //  - 숨겨 둔 시작 화면을 다시 보여주고 시작 BGM 재생
        // ============================================================
        public static void ReturnToStart()
        {
            StartForm start = Application.OpenForms.OfType<StartForm>().FirstOrDefault() ?? new StartForm();
            SoundManager.Instance.PlayBGM("Start", 30);
            start.Show();
        }

        // 폼이 처음 로드될 때 문구 설정 및 BGM 재생
        private void StartForm_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "SIMPLE RPG";
            lblStart.Text = "Press Any Key or Touch to Start";
            SoundManager.Instance.PlayBGM("Start", 30);
        }

        // ============================================================
        // KeyDown 이벤트
        // ------------------------------------------------------------
        // - ESC 키: 종료 확인 메시지 표시
        // - 그 외의 키: 직업 선택 화면으로 이동
        // ============================================================
        private void StartForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult result = MessageBox.Show("정말로 종료하시겠습니까?", "종료 확인", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            else
            {
                GoToJobSelect();
            }
        }

        // 마우스 클릭으로도 시작 (터치 화면처럼 직관적인 시작)
        private void StartForm_MouseClick(object sender, MouseEventArgs e)
        {
            GoToJobSelect();
        }

        // 직업 선택 화면을 띄우고 시작 화면은 숨김 (메인 폼이므로 닫지 않음)
        private void GoToJobSelect()
        {
            new JobSelectForm().Show();
            Hide();
        }
    }
}
