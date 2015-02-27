using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Functional.Util {
    class WebPageDownloader {
        public async Task<string> FetchLinesAsync(string url, int lines) {
            var req = WebRequest.Create(url);
            var builder = new StringBuilder();
            using (var resp = await req.GetResponseAsync()) {
                using (var stream = resp.GetResponseStream()) {
                    using (var reader = new StreamReader(stream)) {
                        for (int i = 0; i < lines; i++)
                            builder.AppendLine(reader.ReadLine());
                    }
                }
            }
            return builder.ToString();
        }
    }
}
