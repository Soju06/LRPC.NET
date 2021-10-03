namespace LRPC.NET.Http {
    /// <summary>
    /// 파일 콘텐츠
    /// </summary>
    public class FileContent : HttpContent {
        public FileContent(string fileName) {
            if (!File.Exists(fileName)) throw new 
                    FileNotFoundException("file not found.", fileName);
            FileName = fileName;
        }

        /// <summary>
        /// 파일 이름
        /// </summary>
        public string FileName { get; set; }

        public override async Task CopyToAsync(Stream stream) {
            using var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);
            await fs.CopyToAsync(stream);
        }
    }
}
