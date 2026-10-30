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
    }
}