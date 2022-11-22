using System;
using System.Collections.Generic;

namespace TIP_01
{
    public class Program
    {
        static MainCore mainCore = new MainCore();

        static void Main(string[] args)
        {
            // Всего в программе записано 2 набора правил. Раскомментируйте одну из этих строк:

            mainCore.example_languagePresent();         // (1) - Язык есть
            //mainCore.example_notLanguagePresent();    // (2) - Языка нет

            mainCore.checkIsHaveTermOnRules();

            mainCore.printCanBelanguage(); // Выводит, пуст ли язык
        }
    }

    public class MainCore
    {
        #region cout

        // Переопределяю метод вывода на консоль, для удобства
        // Теперь вывод работает почти так-же, как и в С++
        public void cout<Type>(Type Input)
        {
            Console.WriteLine(Input);
        }

        public void coutnn<Type>(Type Input) // Тот-же вывод значений, только без переноса каретки 
        {
            Console.Write(Input);
        }

        #endregion

        // Обявляю NEPS
        public List<string> NoTerminals = new List<string>();
        public List<string> Terminals = new List<string>();
        List<List<string>> Rules = new List<List<string>>();
        public string Axioma;

        public List<string> GoodSimvols = new List<string>();

        /*
            Например правило A -> BC, Bc, c;
            Будет выглядеть: rules[n] = [["A", "Bc", "Bc", "c"]], где n - это номер этого правила

            Лямбда - будет & 
        */

        #region Rules

        // В основной программе, при загрузке значений используйте любые из этих 3х методов:

        public void example_languagePresent()
        {
            example_01();
            AllPrint();            
        }

        public void example_notLanguagePresent()
        {
            example_02();
            AllPrint();            
        }

        /*
        public void LoadNormalExample()
        {
            SimpleExample();
            notRecurceExample();

            AllPrint(); // Не убирать это отсюда, процедура нужна для вычисления максимальной длинны правил
        }

        public void LoadExampleRecurce()
        {
            SimpleExample();
            RecurceExample();

            AllPrint(); 
        }

        public void LoadExample_notLanguage()
        {
            notLanguageExample();

            AllPrint(); 
        }

        // Но не используйте эти 4. Я специально сделал их без public

        void SimpleExample()
        {
            NoTerminals.Add("A");
            NoTerminals.Add("B");
            NoTerminals.Add("C");
            NoTerminals.Add("D");

            Terminals.Add("a");
            Terminals.Add("b");
            Terminals.Add("c");
            Terminals.Add("d");

            Axioma = "A";

            // Добавляем правила: 

            List<string> list1 = new List<string>();

            list1.Add("A");
            list1.Add("BC");
            list1.Add("Bc");
            list1.Add("&"); // Лямбда-правило

            Rules.Add(list1);


            List<string> list2 = new List<string>();

            list2.Add("BC");
            list2.Add("bc");

            Rules.Add(list2);

            List<string> list3 = new List<string>();

            list3.Add("B");
            list3.Add("D");
            list3.Add("d");

            Rules.Add(list3);


            List<string> list4 = new List<string>();

            list4.Add("D");
            list4.Add("C");
            list4.Add("&"); // Лямбда-правило

            Rules.Add(list4);

            List<string> list5 = new List<string>(); // Бесполезное правило (никогда не выполнится)

            list5.Add("DCA");
            list5.Add("dca");

            Rules.Add(list5);
        }
        */

        void example_01()
        {
            NoTerminals.Add("A");
            NoTerminals.Add("B");
            NoTerminals.Add("C");
            NoTerminals.Add("D");

            Terminals.Add("a");
            Terminals.Add("b");
            Terminals.Add("c");
            Terminals.Add("d");

            Axioma = "A";

            List<string> list1 = new List<string>();

            list1.Add("A");
            list1.Add("B");
            list1.Add("C");

            Rules.Add(list1);

            List<string> list2 = new List<string>();

            list2.Add("C");
            list2.Add("D");

            Rules.Add(list2);

            List<string> list3 = new List<string>();

            list3.Add("D");
            list3.Add("d");

            Rules.Add(list3);
        }

        void example_02()
        {
            NoTerminals.Add("A");
            NoTerminals.Add("B");
            NoTerminals.Add("C");
            NoTerminals.Add("D");

            Terminals.Add("a");
            Terminals.Add("b");
            Terminals.Add("c");
            Terminals.Add("d");

            Axioma = "A";

            List<string> list1 = new List<string>();

            list1.Add("A");
            list1.Add("B");
            list1.Add("C");

            Rules.Add(list1);

            List<string> list2 = new List<string>();

            list2.Add("C");
            list2.Add("DD");

            Rules.Add(list2);

            List<string> list3 = new List<string>();

            list3.Add("DD");
            list3.Add("D");

            Rules.Add(list3);
        }

        /*
        void RecurceExample()
        {
            List<string> list6 = new List<string>();

            list6.Add("C");
            list6.Add("C");
            list6.Add("c");

            Rules.Add(list6);
        }

        void notRecurceExample()
        {
            List<string> list6 = new List<string>();

            list6.Add("C");
            list6.Add("c");
            list6.Add("&");

            Rules.Add(list6);
        }
        */

        #endregion

        public void printingList(List<string> MyList)
        {
            for (int i = 0; i < MyList.Count; i++)
            {
                if (i > 0) coutnn(", ");
                coutnn(MyList[i]);
            }
        }

        public void sep() // Выводит разделитель
        {
            cout(" ");
        }

        public void AllPrint()
        {
            cout("Все нетерминалы: "); 
            printingList(NoTerminals); sep();

            sep(); cout("Все терминалы: ");
            printingList(Terminals); sep();

            sep(); cout("Все правила: "); sep();
            cout(" & - это лямбда"); sep();

            for (int i = 0; i < Rules.Count; i++) // Чуть сложного кода для карсивого вывода)
            {
                coutnn("    " + Rules[i][0] + " -> ");
                for (int j = 1; j < Rules[i].Count; j++) // Проверить на ошибки при выводе 
                {
                    if (j > 1) coutnn(", ");
                    coutnn(Rules[i][j]);
                }
                cout(" ");                
            }

            sep(); cout("Аксиома: "); 
            cout(Axioma); sep();
        }

        // Находит все терминалы в правилах (только односимвольные), и отправляет их в Finder
        public void checkIsHaveTermOnRules() 
        {
            for (int i = 0; i < Rules.Count; i++)
            {
                for (int j = 0; j < Rules[i].Count; j++) // Смотрим по всем правилам
                {
                    for (int k = 0; k < Terminals.Count; k++) // По всем терминалам
                    {
                        if (Rules[i][j] == Terminals[k]) // Сравниваем каждый элемент
                        {
                            // Нашли, что в каком-то правиле есть терминал

                            string ruleCondition = Rules[i][0];

                            GoodSimvols.Add(ruleCondition);

                            cout("При начальном обходе нашли терминал в правилах, это: " + Rules[i][j]);
                            Finder(ruleCondition); // Отправляем его в Finder
                        }
                    }
                }
            }
        }

        // Получает на вход лексему, и ищет существует ли правило, из которого эту лексему можно получить
        // Если такое правило есть, то рекурсивно отправляет в самого себя это правило, в качестве входного значения
        // Если это найденное правило является аксиомой - то язык не пуст
        void Finder(string findCell) 
        {
            if (isProgrammEnd == false)
            {
                cout("Ищем элемент " + findCell + " во всех правилах");
                for (int i = 0; i < Rules.Count; i++)
                {
                    for (int j = 1; j < Rules[i].Count; j++)
                    {
                        if (Rules[i][j] == findCell)
                        {
                            coutnn("Нашли. ");
                            cout("Начало правила: " + Rules[i][0] + ". Теперь ищем правило, из которого этот элемент получался бы");
                            GoodSimvols.Add(Rules[i][0]);
                            Finder(Rules[i][0]);
                        }
                    }
                }
            }
        }

        // Ищет во множестве хороших смиволов Аксимоу
        void isLangEmpty()
        {
            for (int i = 0; i < GoodSimvols.Count; i++)
            {
                if (GoodSimvols[i] == Axioma)
                {
                    cout("В множестве хороших смиволов нашли аксиому. Значит язык не пуст");
                    termWord = true;
                    break;
                }
            }
        }

        bool termWord = false;          // Если false - то язык пуст
        bool isProgrammEnd = false;     // Если true - то все рекурсивные вызовы завершаются
        bool isValPrint = false;        // Если true - то ответ больше не печатается

        // Печатает, пуст ли язык
        public void printCanBelanguage()
        {
            if (isValPrint == false)
            {
                cout(" ");
                isLangEmpty();
                cout(" ");
                isValPrint = true;
                if (termWord == true) cout("Язык не пуст");
                else cout("Язык пуст");
            }
        }
    }
}
