using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

// создаём класс для клиентов
class Client
{
    // приветные поля класса
    private string name;
	private string pol;
	private int year;
	private string sity;
	private string diagnosis;

    // публичные методы
    // сеттеры (установка значения)
    public void setName(string text) {
        name = text;
    }
	public void setPol(string text) {
        pol = text;
    }
	public void setYear(int text) {
        year = text;
    }
	public void setSity(string text) {
        sity = text;
    }
	public void setDiagnosis(string text) {
        diagnosis = text;
    }

    // геттеры (получение значений полей)
    public string getName() {
        return name;
    }
	public string getPol() {
        return pol;
    }
	public int getYear() {
        return year;
    }
	public string getSity() {
        return sity;
    }
	public string getDiagnosis() {
        return diagnosis;
    }

    //вывод всего массива клиентов
    public static void PrintClient(List<Client> people) {
		for (int i = 0; i < people.Count; i++)
        {
            Console.WriteLine("Ф.И.О: " + people[i].getName() + "\nПол: "
                              + people[i].getPol() + "\nГод: " + people[i].getYear() 
                              + "\nМесто прооживания: " + people[i].getSity() + "\nДиагноз: " 
                              + people[i].getDiagnosis());
			Console.WriteLine("--------");
        }
	}

    // вывод если клиент инногородний
    public static void IfNotBarnaulPrint(List<Client> people) {
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].getSity() != ("город")) {
                Console.WriteLine("Ф.И.О: " + people[i].getName() + "\tМесто прооживания: " + people[i].getSity());
            }
        }
    }

    // записать в файл если возраст клиента больше или равен заданному
    public static void InputHighAgeInFile(StreamWriter fout, List<Client> people, int ageNew){
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].getYear() >= ageNew) {
                fout.WriteLine(people[i].getName() + "\n" + people[i].getYear() + "\n" + people[i].getDiagnosis() + "\n");
            }
        }
    }

    //вывод из файла
    public static void PrintRes(StreamReader fin){
		string N;
        int count = 0;

        while ((N = fin.ReadLine()) != null) {
            if (count == 0)
            {
                Console.WriteLine("Ф.И.О.: " + N);
            }
            else if (count == 1)
            {
                Console.WriteLine("Возраст: " + N);
            }
            else if (count == 2)
            {
                Console.WriteLine("Диагноз: " + N);
            }
            else
            {
                count = -1;
            }
            count++;
        }
    }

    //удалить из файла инногороднего клиента
    public static void DeletePeopleIfNotBarnaulWithFile(List<Client> people, StreamWriter inputFinalFin)
    {
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].getSity() == ("город")) {
                inputFinalFin.WriteLine(people[i].getName() + "\n" + people[i].getPol() 
                                        + "\n" + people[i].getYear() + "\n" + people[i].getSity() 
                                        + "\n" + people[i].getDiagnosis() + "\n");
            }
            else
            {
                people.RemoveAt(i);
                i--;
            }
        }
    }

    public static List<Client> AddPeople(List<Client> people)
    {
        Console.WriteLine("Заполните данные о пациенте");


        Client first = new Client();

        Console.Write("Введите Ф.И.О. - ");
        string name = Console.ReadLine();
        first.setName(name);

        Console.Write("Введите пол - ");
        string pol = Console.ReadLine();
        first.setPol(pol);

        Console.Write("Введите возраст - ");
        int year;
        while (!int.TryParse(Console.ReadLine(), out year)) Console.WriteLine("введите число!");
        first.setYear(year);

        Console.Write("Введите место проживания (город, село) - ");
        string sity = Console.ReadLine();
        first.setSity(sity);

        Console.Write("Введите диагноз - ");
        string diagnosis = Console.ReadLine();
        first.setDiagnosis(diagnosis);

        people.Add(first);


        try
        {
            using (StreamWriter fin = new StreamWriter(@"C:\Users\mvideo\Desktop\C#\test.txt"))
            {
                Client.WriteInFile(people, fin);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"The file could not be opened: '{e}'");
        }

        return people;
    }

    // изменить диагноз
    public static void RemakeDiagnos(List<Client> people){
        // перезаписываем диагноз
        HelloWorld.remake(people);
    }

    public static List<Client> ScanFromFile(StreamReader printFout)
    {
        List<string> check = new List<string>();
        List<Client> people = new List<Client>();

        string N;
        while ((N = printFout.ReadLine()) != null)
        {
            check.Add(N);
        }

        for (int i = 0; i < check.Count; i += 6)
        {
            Client first = new Client();
            first.setName(check[i]);
            first.setPol(check[i + 1]);
            first.setYear(Convert.ToInt32(check[i + 2]));
            first.setSity(check[i + 3]);
            first.setDiagnosis(check[i + 4]);
            people.Add(first);
        }
        printFout.Close();

        return people;
    }

    public static bool updateFlag(bool flagMenu)
    {
        return flagMenu = true;
    }

    public static List<Client> ConsoleWrite(List<Client> people)
    {
        Console.WriteLine( "Заполнить данные пациентов");
        // заполняем стартовый массив
        
        try
        {
            using (StreamWriter fin = new StreamWriter(@"C:\Users\mvideo\Desktop\C#\test.txt"))
            {
                people = Client.ConsoleWritePeople();
                Client.WriteInFile(people, fin);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine($"Не удалось открыть файл!!");
        }

        return people;
    }

    // консольно заполнить массив клиентов
    public static List<Client> ConsoleWritePeople(){

        Console.WriteLine("Сколько пациентов");
        int res;
        while (!int.TryParse(Console.ReadLine(), out res)) Console.WriteLine("введите число!");

        List<Client> people = new List<Client>();
        // заполняем массив клиентов
        for (int i = 0; i < res; i ++)
        {
            Client first = new Client();

            Console.Write("Введите Ф.И.О. - ");
            string name = Console.ReadLine();
			first.setName(name);

            Console.Write("Введите пол - ");
            string pol = Console.ReadLine();
			first.setPol(pol);

            Console.Write("Введите возраст - ");
            int year;
            while (!int.TryParse(Console.ReadLine(), out year)) Console.WriteLine("введите число!");
            first.setYear(year);

            Console.Write("Введите место проживания (город, село) - ");
            string sity = Console.ReadLine();
			first.setSity(sity);

            Console.Write("Введите диагноз - ");
            string diagnosis = Console.ReadLine();
			first.setDiagnosis(diagnosis);

            people.Add(first);
        }

        return people;
    }

    // записать в файл
    public static void WriteInFile(List<Client> people, StreamWriter fout){
        for (int i = 0; i < people.Count; i++)
        {
            fout.WriteLine(people[i].getName() + "\n" + people[i].getPol() 
                           + "\n" + people[i].getYear() + "\n" + people[i].getSity() 
                           + "\n" + people[i].getDiagnosis() + "\n");
        }
        fout.Close();
    }
};
class HelloWorld {
    // функция изменения диагноза
    public static void remake(List<Client> people){
        string fio, diagnosisNew;
        Console.WriteLine("Введите ФИО - ");
        fio = Console.ReadLine();
        Console.WriteLine("Введите диагноз - ");
        diagnosisNew = Console.ReadLine();

		for (int i = 0; i < people.Count; i++)
		{
			if (people[i].getName() == fio) {
				people[i].setDiagnosis(diagnosisNew);
			}
		}
    }
  static void Main() {
      // подключаем русский язык
      Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
      var enc1251 = Encoding.GetEncoding(1251);
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;
      System.Console.InputEncoding = enc1251;

      // people массив под клиентов
      List<Client> people = new List<Client>();
      bool flagMenu = false;

      int age = 0;

    
	
    // бесконечный цикл для реализации меню
	while(true){
        // текст меню
		Console.WriteLine("1. Считать содержимое из файла\n" +
                          "2. Заполнить пациентов через консоль\n" +
                          "3. Выдать на экран содержимое файла\n" +
                          "4. Выдать на экран список всех иногородних пациентов\n" +
                          "5. Создать файл пациентов больше заданого возраста\n" +
                          "6. Распечатать файл пациентов больше заданого возраста\n" +
                          "7. Добавить данные нового пациента\n" +
                          "8. Удалить все элементы записи инногородних пациентов\n" +
                          "9. Изменить диагноз у определённого пациетна\n" +
                          "10. Выход"
        );
		string numberFunctionInput = Console.ReadLine();
		switch(numberFunctionInput){
            case ("1"):
                try{
                    people = Client.ScanFromFile(new StreamReader(@"C:\Users\mvideo\Desktop\C#\test.txt"));
                    flagMenu = Client.updateFlag(flagMenu);
                }catch (IOException e)
                {
                    Console.WriteLine("Начального файла нет");
                    FileInfo fileInfo = new FileInfo(@"C:\Users\mvideo\Desktop\C#\test.txt");
                    FileStream fs = fileInfo.Create();
                    fs.Close();
                    people = Client.ConsoleWrite(people);
                    flagMenu = Client.updateFlag(flagMenu);
                    Console.WriteLine($"Файл создан");
                }
                break;
            case ("2"):
                try{
                    people = Client.ConsoleWrite(people);
                    flagMenu = Client.updateFlag(flagMenu);
                }catch (IOException e){
                    FileInfo fileInfo = new FileInfo(@"C:\Users\mvideo\Desktop\C#\test.txt");
                    FileStream fs = fileInfo.Create();
                    fs.Close();
                    people = Client.ConsoleWrite(people);
                    flagMenu = Client.updateFlag(flagMenu);
                    Console.WriteLine($"Файл создан");
                }
                break;
            case ("3"):
                if (flagMenu)
                {
                    // выводим, что заполнили
                    Client.PrintClient(people);
                }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
				
				break;
			case("4"):
                if (flagMenu)
                {
                    // Проверка на инногородних и выводим их
                    Client.IfNotBarnaulPrint(people);
                    }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
                break;
			case("5"):
                if (flagMenu)
                {
                    // запрашиваем возраст
                    Console.Write("Введите возраст (выведутся пациенты больше или такому же значению) - ");
                    while (!int.TryParse(Console.ReadLine(), out age)) Console.WriteLine("введите число!");
                    try
                    {
                        using (StreamWriter fin = new StreamWriter(@"C:\Users\mvideo\Desktop\C#\res.txt"))
                        {
                            // записываем в другой файл >= age	
                            Client.InputHighAgeInFile(fin, people, age);
                            fin.Close();
                        }
                    }
                    catch (IOException e)
                    {
                        File.Create(@"C:\Users\mvideo\Desktop\C#\res.txt");
                        StreamWriter fin = new StreamWriter(@"C:\Users\mvideo\Desktop\C#\res.txt");
                        Client.InputHighAgeInFile(fin, people, age);
                        fin.Close();
                        Console.WriteLine($"Не удалось открыть файл!!");
                    }
                }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
                break;
			case("6"):
                if (flagMenu)
                {
                    // распечатать файл пациентов больше заданого возраста
                    try
                    {
                        using (StreamReader fin = new StreamReader(@"C:\Users\mvideo\Desktop\C#\res.txt"))
                        {
                            Client.PrintRes(fin);
                            fin.Close();
                        }

                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Не удалось открыть файл!!");
                    }
                }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
                break;
			case("7"):
                if (flagMenu)
                {
                    try
                    {
                        people = Client.AddPeople(people);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Не удалось открыть файл!!");
                    }
                }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
                break;
			case("8"):
                if (flagMenu)
                {
                    try
                    {
                        // отчищаем файл
                        File.WriteAllText(@"C:\Users\mvideo\Desktop\C#\test.txt", string.Empty);
                        using (StreamWriter fin = new StreamWriter(@"C:\Users\mvideo\Desktop\C#\test.txt", true))
                        {
                            // удаляем записи иногородних
                            Client.DeletePeopleIfNotBarnaulWithFile(people, fin);
                            fin.Close();
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Не удалось открыть файл!!");
                    }
                }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
                break;
			case("9"):
                if (flagMenu)
                {
                    try
                    {
                        // отчищаем файл
                        File.WriteAllText(@"C:\Users\mvideo\Desktop\C#\test.txt", string.Empty);
                        using (StreamWriter fin = new StreamWriter(@"C:\Users\mvideo\Desktop\C#\test.txt", true))
                        {
                            // надо ли перезаписывать диагноз
                            Client.RemakeDiagnos(people);
                            Client.WriteInFile(people, fin);
                            fin.Close();
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Не удалось открыть файл!!");
                    }
                }
                else
                {
                    Console.WriteLine("Заполнете список пациентов консольно или через файл test.txt");
                }
                break;
			case("10"):
                // выход из программы
				Environment.Exit(0);
				break;
			default:
                // ложное значение
				Console.WriteLine("Недопустимое значение!"); 
				break;
		}
	}
	}
}