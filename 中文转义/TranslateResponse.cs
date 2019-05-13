using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 中文转义
{
    class TranslateResponse
    {
        public string from;
        public string to;
        public List<TranslateResult> trans_result;

    }
    class TranslateResult
    {
        public string src;
        public string dst;
    }
}
