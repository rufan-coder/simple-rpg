using Team_Project.Controller;
using Team_Project.View;

namespace Team_Project
{
    // ============================================================
    // Program
    // ------------------------------------------------------------
    //  - 프로그램의 시작점(Entry Point)
    //  - 사운드 파일을 등록한 뒤 시작 화면(StartForm)을 메인 폼으로 실행
    // ============================================================
    static class Program
    {
        [STAThread] // Single Thread Apartment: Windows Forms(및 COM 기반 Windows Media Player) 사용 시 필수
        static void Main()
        {
            InitializeSounds();

            // 버튼 등 컨트롤을 운영체제 기본 스타일로 그림
            Application.EnableVisualStyles();
            // 텍스트 렌더링 방식 설정 (GDI 사용, 특별한 이유가 없으면 false)
            Application.SetCompatibleTextRenderingDefault(false);

            // 메인 폼 실행: 이 폼이 닫히면 프로그램이 종료됨
            Application.Run(new StartForm());
        }

        // 게임에서 사용할 사운드 파일 등록
        private static void InitializeSounds()
        {
            SoundManager.Instance.LoadSound("Start", @"Sound\MainForm_bgm.mp3");
            SoundManager.Instance.LoadSound("Battle", @"Sound\Battle_bgm.mp3");
            SoundManager.Instance.LoadSound("Attack", @"Sound\AttackSound.mp3");
            SoundManager.Instance.LoadSound("Level", @"Sound\LevelUpSound.mp3");
            SoundManager.Instance.LoadSound("Select", @"Sound\SelectSound.mp3");   // 등록만 되어 있고 아직 사용처 없음
            SoundManager.Instance.LoadSound("GameOver", @"Sound\GameOverSound.mp3");
        }
    }
}
