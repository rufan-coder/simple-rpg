using System.Diagnostics;

namespace Team_Project.Controller
{
    // ============================================================
    // ImageManager
    // ------------------------------------------------------------
    //  - 이미지 파일을 실행 파일 위치 기준으로 읽어 Image 객체로 반환
    //  - 실패하면 null을 반환하되, 이유를 디버그 출력에 남김
    //    (예전에는 예외를 그냥 삼켜서 파일 이름 오타를 찾지 못했음)
    // ============================================================
    public static class ImageManager
    {
        public static Image? LoadImage(string? relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                return null;   // 이미지가 없는 것이 정상인 경우 (예: 드래곤 사망 이미지)
            }

            string fullPath = Path.Combine(AppContext.BaseDirectory, relativePath);

            if (!File.Exists(fullPath))
            {
                Debug.WriteLine($"[ImageManager] 파일 없음: {fullPath}");
                return null;
            }

            try
            {
                // 파일 내용을 메모리로 복사한 뒤 파일은 바로 닫음 → 파일 잠금이 남지 않음
                // MemoryStream은 닫지 않음: Image.FromStream으로 만든 이미지는
                // 살아 있는 동안 원본 스트림이 열려 있어야 하기 때문 (GIF 애니메이션 등)
                var ms = new MemoryStream();
                using (var fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    fs.CopyTo(ms);
                }
                ms.Position = 0;
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                // 파일이 손상되었거나 권한이 없는 경우
                Debug.WriteLine($"[ImageManager] 로드 실패: {fullPath} ({ex.Message})");
                return null;
            }
        }
    }
}
