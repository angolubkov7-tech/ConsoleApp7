using System;
using System.Collections.Generic;

namespace Medvedev
{
    class Building
    {
        private int number; //уникальный номер
        private double height; //высота 
        private int floors; //этажность
        private int apartments; //количество кваритир
        private int entrances; //количество подьездов

        //Статическое поле одно на весь класс хранит последний использованный номер здания

        private static int lastNumber = 0;

        private static int GetNextNumber()
        {
            lastNumber++;
            return lastNumber;
        }

        public Building()
        {
            number = GetNextNumber();
        }

        //Методы заполнения полей
        public void SetHeight(double height)
        {
            if (height > 0) { this.height = height; }
        }

        public void SetFloors(int floors)
        {
            if(floors > 0) { this.floors = floors; }
        }

        public void SetApartments(int apartments)
        {
            if(apartments > 0) { this.apartments = apartments; }
        }

        public void SetEntrances(int entrances)
        {
            if (entrances > 0) { this.entrances = entrances; }
        }

        //Методы присвоения
        public int GetNumber() { return number; }
        public double GetHeight() { return height; }
        public int GetFloors() { return floors; }
        public int GetApartments() { return apartments; }
        public int GetEntrances() { return entrances; }

        //Методы вычислений
        public double FloorHeight()
        {
            if(floors == 0) return 0;
            return height / floors;
        }

        //Вычисления квартир в одном подьезде
        public double ApartmentsPerEntrance()
        {
            if (entrances == 0) return 0;
            return apartments / entrances;
        }

        //Количество квартир на одном этаже
        public double ApartmentsPerFloor()
        {
            if(floors == 0) return 0;
            return (double)apartments / floors;
        }

        //Печать
        public void Print()
        {
            Console.WriteLine($"Номер здания: {number}");
            Console.WriteLine($"Высота: {height}");
            Console.WriteLine($"Этажность: {floors}");
            Console.WriteLine($"Квартир: {apartments}");
            Console.WriteLine($"Подьездов: {entrances}");
            Console.WriteLine($"Высота этажа: {FloorHeight():F3}");
            Console.WriteLine($"Количество квартир на одном этаже: {ApartmentsPerFloor():F1}");
            Console.WriteLine($"Количество кварир в подьезде: {ApartmentsPerEntrance():F1}");
        }
    }
    
    class Program 
    {
        public static void Main()
        {
            Building b1 = new Building();
            b1.SetHeight(30);
            b1.SetFloors(10);
            b1.SetApartments(120);
            b1.SetEntrances(4);

            Building b2 = new Building();
            b2.SetHeight(15.0);
            b2.SetFloors(5);
            b2.SetApartments(60);
            b2.SetEntrances(2);

            Building b3 = new Building();
            b3.SetHeight(45.5);
            b3.SetFloors(15);
            b3.SetApartments(180);
            b3.SetEntrances(3);

            b1.Print();
            Console.WriteLine();
            b2.Print();
            Console.WriteLine();
            b3.Print();
            Console.WriteLine();
        }
    }
}