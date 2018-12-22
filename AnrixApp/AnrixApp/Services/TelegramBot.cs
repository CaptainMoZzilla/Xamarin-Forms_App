using AnrixApp.Models;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace AnrixApp.Services
{
    public class TelegramBot
    {
        private static ITelegramBotClient Bot = new TelegramBotClient("685145928:AAGVX0D5HWJDIldsYVJq7iUZ38RVG0DaJPE");
        private const string ERROR_MESSAGE = "Проверьте правильность вашей анкеты:\nПример /showexample";
        public delegate void OnMessage(StudentRequest student);
        public static event OnMessage OnMessagePut;

        static TelegramBot()
        {
            Bot.OnMessage += Bot_OnMessage;
        }

        private async static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e.Message;
            if ("Anrix_Official".Equals(message.Chat.Username))
            {
                if ("/showexample".Equals(message.Text))
                { 
                    await Bot.SendTextMessageAsync(chatId: message.Chat.Id, 
                        text: $"*{e.Message.Chat.FirstName}*, вот пример анкеты,\nкоторую ты можешь мне отправить:\n" +
                        $"\n`Бережнов Даниил Олегович`\n" +
                        $"`753503 10.0`\n" +
                        $"[Ссылка на фотку студента](http://www.god-answers-prayers.com/god_answers_prayers_gallery/img/Jesus_Computer.jpg)",
                        parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown);
                } else
                {
                    var student = ParseStudent(message.Text);
                    if (student == null)
                    {
                        await Bot.SendTextMessageAsync(chatId: message.Chat.Id, text: ERROR_MESSAGE,
                            replyToMessageId: message.MessageId);
                        return;
                    }
                    OnMessagePut(new StudentRequest(student) { ChatId = message.Chat.Id, MessageId = message.MessageId } );
                }
            }
        }

        public static void TelegramBot_OnRecievingUpdate(bool state)
        {
            if (state)
                Bot.StartReceiving();
            else if (Bot.IsReceiving) {
                Bot.StopReceiving();
            }
        }

        private static Student ParseStudent(string line)
        {
            var Array = line.Split(' ', '\n');
            if (Array.Length != 6)
                return null;
            
            try
            {
                var mark = double.Parse(Array[4]);
                return new Student(Array[0], Array[1], Array[2], mark, Array[3], Array[5]);

            } catch (Exception)
            {
                return null;
            }
        }

        public static async Task SendSuccessMessage(StudentRequest student) => await Bot.SendTextMessageAsync(chatId: student.ChatId,
            replyToMessageId: student.MessageId, text: "Одобрена администратором!");

        public static async Task SendErrorMessage(StudentRequest student) => await Bot.SendTextMessageAsync(chatId: student.ChatId,
            replyToMessageId: student.MessageId, text: "Отклонена администратором!");
    }
}
