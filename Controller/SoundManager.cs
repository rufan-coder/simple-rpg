using WMPLib;

namespace Team_Project.Controller
{
    // ============================================================
    // SoundManager (싱글톤)
    // ------------------------------------------------------------
    //  - 배경음악(BGM)과 효과음(SFX) 재생을 한곳에서 관리
    //  - BGM: 플레이어 1개를 계속 재사용 (한 번에 한 곡만 재생)
    //  - SFX: 플레이어 여러 개를 미리 만들어 돌려 쓰는 풀(pool) 방식
    //         → 효과음끼리, 그리고 BGM과 동시에 재생 가능
    //         → 재생할 때마다 새 객체를 만들지 않아 메모리가 쌓이지 않음
    // ============================================================
    public sealed class SoundManager
    {
        // static readonly 필드는 CLR이 처음 사용될 때 딱 한 번만 초기화하므로
        // 여러 스레드에서 접근해도 인스턴스가 하나만 만들어짐 (스레드 안전)
        private static readonly SoundManager instance = new SoundManager();
        public static SoundManager Instance => instance;

        private const int SfxPoolSize = 4;    // 동시에 겹쳐 재생할 수 있는 효과음 수

        // 사운드 이름 → 실행 파일 기준 절대 경로
        private readonly Dictionary<string, string> soundPaths = new Dictionary<string, string>();

        private readonly WindowsMediaPlayer bgmPlayer;
        private readonly WindowsMediaPlayer[] sfxPlayers;
        private int nextSfxIndex = 0;
        private string? currentBgm;           // 지금 재생 중인 BGM 이름

        private SoundManager()
        {
            bgmPlayer = CreatePlayer();
            sfxPlayers = new WindowsMediaPlayer[SfxPoolSize];
            for (int i = 0; i < SfxPoolSize; i++)
            {
                sfxPlayers[i] = CreatePlayer();
            }
        }

        private static WindowsMediaPlayer CreatePlayer()
        {
            var player = new WindowsMediaPlayer();
            player.settings.autoStart = false;   // URL만 바꿨을 때 자동 재생되지 않게
            return player;
        }

        // 사운드 파일 경로를 등록 (상대 경로는 실행 파일 위치 기준으로 변환)
        // → 바로가기 등으로 다른 폴더에서 실행해도 파일을 찾을 수 있음
        public void LoadSound(string name, string relativePath)
        {
            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);
            soundPaths[name] = fullPath;
        }

        // 배경음악을 반복 재생 (이미 같은 곡이 재생 중이면 그대로 둠)
        public void PlayBGM(string name, int volume)
        {
            if (currentBgm == name) return;
            if (!soundPaths.TryGetValue(name, out string? path)) return;

            currentBgm = name;
            bgmPlayer.URL = path;
            bgmPlayer.settings.volume = volume;
            bgmPlayer.settings.setMode("loop", true);
            bgmPlayer.controls.play();
        }

        // 배경음악 정지
        public void StopBGM()
        {
            bgmPlayer.controls.stop();
            currentBgm = null;
        }

        // 효과음 재생: 풀에서 다음 플레이어를 꺼내 재생 (가장 오래된 효과음 자리를 재사용)
        public void PlaySFX(string name, int volume)
        {
            if (!soundPaths.TryGetValue(name, out string? path)) return;

            WindowsMediaPlayer sfxPlayer = sfxPlayers[nextSfxIndex];
            nextSfxIndex = (nextSfxIndex + 1) % SfxPoolSize;

            sfxPlayer.URL = path;
            sfxPlayer.settings.volume = volume;
            sfxPlayer.controls.play();
        }
    }
}
