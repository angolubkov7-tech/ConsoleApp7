using System;
using System.Collections.Generic;
using System.Globalization;

namespace Medvedev
{
    enum DeliveryStatus
    {
        Sent,
        Delivered,
        Read,
        Deleted
    }
    abstract class Message
    {
        private string author;
        private DateTime sentime;
        private DeliveryStatus status;
        private bool isFavorite;

        protected Message(string author)
        {
            this.author = author;
            this.sentime = DateTime.Now;
            this.status = DeliveryStatus.Sent;
            this.isFavorite = false;
        }

        //Свойства
        private string Author
        {
            get { return author; }
            set {if(!string.IsNullOrEmpty(value)) author = value; }
        }

        public DateTime Sentime
        {
            get { return sentime; }
        }

        private DeliveryStatus Status
        {
            get { return status; }
            set { status  = value; }
        }

        private bool IsFavorite
        {
            get { return isFavorite; }
        }

        //Абстрактные методы
        public abstract string GetMessageType();
        public abstract double GetLength(); //текст - символы фото - КБ голос - сек
        public abstract string GetLengthUnit(); //еденица измерения
        protected abstract Message CreateCopy(string newAuthor); //создаем копию

        //Общие методы

        public void Read()
        {
            if (status == DeliveryStatus.Deleted)
            {
                Console.WriteLine($"[{GetMessageType()}] - удаленно нельзя прочитать");
                return;
            }

            status = DeliveryStatus.Read;
            Console.WriteLine($"[{GetMessageType()}] от {author} прочитано");
        }


        public void Delete()
        {
            status = DeliveryStatus.Deleted;
            Console.WriteLine($"[{GetMessageType()}] удалено");
        }

        public Message Forvard(string newAurhor)
        {
            Message copy = CreateCopy(newAurhor);
            Console.WriteLine($"[{GetMessageType()}] пересланно пользователю{newAurhor}");
            return copy;
        }

        public void SaveToFavorites()
        {
            isFavorite = true;
            Console.WriteLine($"[{GetMessageType()}] сохранено в избранное");
        }

        public void PrintInfo()
        {
            Console.WriteLine($"[{GetMessageType()}]");
            Console.WriteLine($"Автор - {author}");
            Console.WriteLine($"Время - {sentime::HH::mm::ss}");
            Console.WriteLine($"Статус - {status}");
            Console.WriteLine($"В избранном? - {(isFavorite ? "да": "нет")}");
            Console.WriteLine($"Длинна - {GetLength():F1} {GetLengthUnit()}");
        }

    }

    class TextMessage : Message
    {
        private string text;

        public string Text
        {
            get { return text; } set { text = value ?? ""; }
        }

        //конструктор 1 
        public TextMessage(string author, string text) : base(author)
        {
            this.text = text ?? "";
        }

        //конструктор 2 аноним
        public TextMessage(string text) : base("Аноним")
        {
            this.text = text ?? "";
        }

        public override string GetMessageType() { return "Текствое"; }
        public override double GetLength() { return text.Length; }
        public override string GetLengthUnit() { return "символов"; }

        protected override Message CreateCopy(string newAuthor)
        {
            return new TextMessage(newAuthor, text);
        }

        //доп метод для подсчета слов
        //public int CountWords()
        //{
        //    if (string.IsNullOrWhiteSpace(text)) return 0;
        //    string[] parts = text.Split(
        //        new char[] { ' ', ',', '.', '!', '?' },
        //        StringSplitOptions.RemoveEmptyEntries);
        //    return parts.Length;
        //}
        //public bool Contains(string word)
        //{
        //    if (text == null) return false;
        //    return text.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0;
        //}
    }

    //наслдник 2 фото
    class PhotoMessage : Message
    {
        private string fileName;
        private int fileSizeKb;
        private int width;
        private int height;

        public string FileName
        {
            get { return fileName; } 
            set { fileName = value ?? ""; }
        }

        public int FileSizeKb
        {
            get { return fileSizeKb; }
            set { if(value > 0)  fileSizeKb = value; }
        }

        public int Width
        {
            get { return width; }
            set { if (value > 0) width = value; }
        }

        public int Height
        {
            get { return height; }
            set { if (value > 0) height = value; }
        }

        //конструктор 1
        public PhotoMessage(string author, string fillName, int fileSizeKb, int width, int height) : base(author)
        {
            this .fileName = fillName;
            this.fileSizeKb = fileSizeKb;
            this.width = width;
            this.height = height;
        }

        //конструктор 2 по умолчанию 1000X1000

        public PhotoMessage(string author, string fillName, int fileSizeKb) : base(author)
        {
            this.fileName = fillName;
            this.fileSizeKb = fileSizeKb;
            this.width = 1000;
            this.height = 1000;
        }

        public override string GetMessageType() { return "Фото"; }
        public override double GetLength() { return fileSizeKb; }
        public override string GetLengthUnit() { return "КБ"; }

        protected override Message CreateCopy(string newAuthor)
        {
            return new PhotoMessage(newAuthor, fileName, fileSizeKb, width, height);
        }
    }

    //Наследник 3 голосовое
    class VoiceMessage : Message
    {
        private int durationSec; //длительность в секундах
        private string transcription;

        public int DurationSec
        {
            get { return durationSec; }
            set { if (value >= 0) durationSec = value; }
        }

        public string Transcription
        {
            get { return transcription; }
            set { transcription = value ?? ""; }
        }
        
        //Конструктор первый полный
        public VoiceMessage(string author, int durationSec, string transcription) : base(author) 
        {
            this.durationSec = durationSec;
            this.transcription = transcription ?? "";
        }
        //Конструтор второй без расшифровки 
        public VoiceMessage(string author, int durationSec) : base(author)
        {
            this.durationSec = durationSec;
            this.transcription = "Без расшифровки";
        }

        public override string GetMessageType() { return "Голосовое сообщение"; }
        public override double GetLength() { return durationSec; }
        public override string GetLengthUnit() { return "Сек"; }

        protected override Message CreateCopy(string newAuthor)
        {
            return new VoiceMessage(newAuthor, durationSec, transcription);
        }
    }

    //Главный класс Main
    class Program
    {
        private static void Main()
        {
            List<Message> chat = new List<Message>();

            //Текст ввод с клавиатуры
            Console.Write("Ввидите сообщение: ");
            string userText = Console.ReadLine();
            chat.Add(new TextMessage("Иван", userText));

            //Фото и голосовое сразу в коде
            chat.Add(new PhotoMessage("Мария", "photo.jpg", 850, 1920, 1080));
            chat.Add(new VoiceMessage("Иван", 80, "Привет как дела?"));
            chat.Add(new TextMessage("Ольга", "Короткое сообщение"));
            chat.Add(new VoiceMessage("Анна", 90));
            Console.WriteLine();

            //Все сообщения в чате через цикл for
            Console.WriteLine(" Все сообщения в чате ");
            for (int i = 0; i < chat.Count; i++)
            {
                Console.WriteLine($"#{i + 1}: ");
                chat[i].PrintInfo();
                Console.WriteLine();
            }


        }
    }
}