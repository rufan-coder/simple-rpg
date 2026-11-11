namespace Team_Project.View
{
    // ============================================================
    // GameForm (게임 화면 공통 부모 클래스)
    // ------------------------------------------------------------
    //  - 직업 선택, 전투, 게임 오버, 클리어 화면이 상속
    //  - 사용자가 X 버튼으로 닫으면 → 프로그램 종료
    //  - 화면을 넘길 때(NavigateTo)는 → 현재 화면만 닫고 프로그램은 유지
    //  - 예전에는 화면을 Hide()로 숨기기만 해서, 다시 시작할 때마다
    //    숨겨진 창이 메모리에 계속 쌓였음 → 이제는 실제로 닫음
    // ============================================================
    public class GameForm : Form
    {
        private bool closingForNavigation = false;

        public GameForm()
        {
            FormClosing += GameForm_FormClosing;
        }

        // 다음 화면을 띄우고 현재 화면을 닫음 (프로그램은 종료하지 않음)
        protected void NavigateTo(Form nextForm)
        {
            nextForm.Show();
            CloseWithoutExit();
        }

        // 프로그램을 종료하지 않고 현재 화면만 닫음
        protected void CloseWithoutExit()
        {
            closingForNavigation = true;
            Close();
        }

        private void GameForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // 사용자가 직접 닫은 경우에만 종료
            // (Application.Exit()로 닫히는 중일 때는 다시 호출하지 않음)
            if (!closingForNavigation && e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
