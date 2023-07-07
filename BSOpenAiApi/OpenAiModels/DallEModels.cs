using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSOpenAiApi.OpenAiModels
{
    internal class DallEModels
    {
    }
    public class DalleData
    {
        public string url { get; set; }
    }

    public class DalleResponse
    {
        public int created { get; set; }
        public List<DalleData> data { get; set; }
    }
}
