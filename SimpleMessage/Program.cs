// See https://aka.ms/new-console-template for more information
using BSOpenAiApi;
using BSOpenAiApi.BaseModel;
using BSOpenAiApi.OpenAiDataBuilders;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;

Console.WriteLine("Dadogós dumás");
Console.WriteLine("1) Chat");
Console.WriteLine("2) DallE");
Console.WriteLine("3) Whisper");

string apikey = "";

var s = Console.ReadLine();

switch (s)
{
    case "1":
        await Chat();
        break;
    case "2":
        await DallE();
        break;
    case "3":
        await Whisper();
        break;
    default:
        break;
}

async Task Whisper()
{
    var dc = new WhisperCommunicator(apikey, "https://api.openai.com/v1/audio/transcriptions");
    var r = await dc.GetResponse("E:\\teszt.wav","");
    Console.WriteLine(r);
}

async Task DallE()
{
    var dc = new DallECommunicator(apikey, "https://api.openai.com/v1/images/generations", "256x256");
    var r = await dc.GetResponse("fluffy cat by Tim Burton");
    Console.WriteLine(r);
    Console.ReadLine();
}

async Task Chat()
{
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
}


