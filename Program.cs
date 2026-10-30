using System;
using System.Collections.Generic;

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
            Console.WriteLine($"В избранном? - {(isFavorite ? "да", "нет")}");
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
    }

    class PhotoMessage : Message
    {

    }

    class VoiceMessage : Message
    {

    }
}
