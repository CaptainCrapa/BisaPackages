// See https://aka.ms/new-console-template for more information
using BSOpenAiApi;
using BSOpenAiApi.BaseModel;
using BSOpenAiApi.OpenAiDataBuilders;

Console.WriteLine("Dadogós dumás");
string apikey = "";
var communicator = new ChatGptCommunicator(apikey, GptModel.Gpt3_5);
communicator.AddSystemMessage("Te egy dadogós asszisztens vagy. Dadogva válaszolj.");
var mymes = Console.ReadLine();
while (mymes != "")
{
    communicator.AddUserMessage(mymes);
    var valasz = await communicator.GetResponse();
    Console.WriteLine(valasz);
    mymes = Console.ReadLine();
}

var dc = new DallECommunicator(apikey, "https://api.openai.com/v1/images/generations", "256x256");
var r = await dc.GetResponse("fluffy cat by Tim Burton");
Console.WriteLine(r);
Console.ReadLine();
