using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSOpenAiApi.OpenAIModels
{
    internal class ChatGptModels
    {

    }
    public class OpenAiChatResponse
    {
        public string id { get; set; }
        public string @object { get; set; }
        public long created { get; set; }
        public List<OpenAiChatChoice> choices { get; set; }
        public OpenAiUsage usage { get; set; }
    }

    public class OpenAiChatChoice
    {
        public int index { get; set; }
        public OpenAiMessage message { get; set; }
        public string finish_reason { get; set; }
    }

    public class OpenAiMessage
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    public class OpenAiUsage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
}
