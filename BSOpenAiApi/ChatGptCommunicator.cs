using BSOpenAiApi.OpenAiDataBuilders;
using BSOpenAiApi.OpenAIModels;
using System.Security.Cryptography;
using System.Threading.Tasks.Dataflow;

namespace BSOpenAiApi.BaseModel
{
    public class ChatGptCommunicator
    {
        string ApiKey = "";
        string apiUrl = "";
        List<OpenAiMessage> Messages;
        IChatGptAPIModel apiversion;
        /// <summary>
        /// Létrehozásaok meg kell adni az API kulcsot, a model típusát. Valamint ha változás töténne és nem követné a verzióm az url módosulását, akkor apiurl-t is írhatunk hozzá.
        /// Amennyiben be szeretnénk állítani (max token, hőmérséklet) akkor Gptmodel enum helyett közvetlen az osztályt kell definiálni. ( new ChatGpt3_5() )
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="apimodel"></param>
        /// <param name="apiUrl"></param>
        public ChatGptCommunicator(string apiKey, GptModel apimodel, string apiUrl = "https://api.openai.com/v1/chat/completions")
        {
            ApiKey = apiKey;
            Messages = new List<OpenAiMessage>();
            apiversion = apimodel switch
            {
                GptModel.Gpt3_5 => new ChatGpt3_5(),
                GptModel.Gpt4_0 => new ChatGpt4_0(),
                _ => new ChatGpt3_5(),
            };
            this.apiUrl = apiUrl;
        }
        /// <summary>
         /// Létrehozásaok meg kell adni az API kulcsot, a model típusát. Valamint ha változás töténne és nem követné a verzióm az url módosulását, akkor apiurl-t is írhatunk hozzá.
         /// Az alapértelmezett értékek használatához, elég az enumot használni.
         /// </summary>
         /// <param name="apiKey"></param>
         /// <param name="apimodel"></param>
         /// <param name="apiUrl"></param>
        public ChatGptCommunicator(string apiKey, IChatGptAPIModel apimodel, string apiUrl = "https://api.openai.com/v1/chat/completions")
        {
            ApiKey = apiKey;
            Messages = new List<OpenAiMessage>();
            apiversion = apimodel;
            this.apiUrl = apiUrl;
        }
        string[] Tordeles(string longText, int v)
        {
            int chunkSize = v;
            int textLength = longText.Length;
            int numChunks = (int)Math.Ceiling((double)textLength / chunkSize);

            string[] textChunks = new string[numChunks];

            for (int i = 0; i < numChunks; i++)
            {
                int startIndex = i * chunkSize;
                int remainingLength = textLength - startIndex;

                if (remainingLength < chunkSize)
                {
                    textChunks[i] = longText.Substring(startIndex);
                }
                else
                {
                    textChunks[i] = longText.Substring(startIndex, chunkSize);
                }
            }
            return textChunks;
        }
        /// <summary>
        /// Hozzáad egy üzenetet a user nevében.
        /// </summary>
        /// <param name="mes"></param>
        public void AddUserMessage(string mes) => Messages.Add(new OpenAiMessage { role = "user", content = mes });
        /// <summary>
        /// Hozzáad egy üzenetet az assistant nevében.
        /// </summary>
        /// <param name="mes"></param>
        public void AddAssistantMessage(string mes) => Messages.Add(new OpenAiMessage { role = "assistant", content = mes });
        /// <summary>
        /// Hozzáad egy üzenetet a system nevében.
        /// </summary>
        /// <param name="mes"></param>
        public void AddSystemMessage(string mes) => Messages.Add(new OpenAiMessage { role = "system", content = mes });
        /// <summary>
        /// Hozzáad egy üzenetet a megadott role nevében.
        /// </summary>
        /// <param name="role"></param>
        /// <param name="mes"></param>
        public void AddMessage(string role, string mes) => Messages.Add(new OpenAiMessage { role = role, content = mes });
        /// <summary>
        /// Visszaadja az eddigi üzenetek listáját.
        /// </summary>
        /// <returns></returns>
        public List<OpenAiMessage> GetMessages() => Messages;
        /// <summary>
        /// Elküldi az üzeneteket és megvárja a válasz érkezését. Timeout annyit jelent, hogy kérésenként ennyit hajlandó várni a program.
        /// Fontos megjegyezni, hogyha a válasz várhatóan olyan hosszú, hogy több üzenetben érkezik vissza, akkor ez a szám megemelkedik annyival.
        /// </summary>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public async Task<string> GetResponse(int timeout = 200)
        {
            var data = apiversion.GetData(this.Messages);
            var oac = new OpenAiApiConnection(ApiKey, apiUrl, timeout);
            var res = await oac.GetResponse(data);
            var res_string = await res.Content.ReadAsStringAsync();
            var response = apiversion.DeSerialize(res_string);

            string valasz = response.choices[0].message.content;
            while (response.choices[0].finish_reason != "stop")
            {
                Messages.Add(response.choices[0].message);
                data = apiversion.GetData(Messages);

                res = await oac.GetResponse(data);
                res_string = await res.Content.ReadAsStringAsync();
                response = apiversion.DeSerialize(res_string);
                valasz += response.choices[0].message.content;
            }
            Messages.Add(response.choices[0].message);
            return valasz;
        }
    }
}