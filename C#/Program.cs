using System;
using System.IO;
using System.Text;

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
                              + "\nГород: " + people[i].getSity() + "\nДиагноз: " 
                              + people[i].getDiagnosis());
			Console.WriteLine("--------");
        }
	}

    // вывод если клиент инногородний
    public static void IfNotBarnaulPrint(List<Client> people) {
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].getSity() != ("Барнаул")) {
                Console.WriteLine("Ф.И.О: " + people[i].getName() + "\tГород: " + people[i].getSity());
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

    // добавляем >= age в исходный файл
    public static void HigtAgeInFile(StreamWriter inputFin, StreamReader printFout){
		string N;
        // добавляем >= age в исходный файл
        while ((N = printFout.ReadLine()) != null) {
            inputFin.WriteLine(N);
        }
		printFout.Close();
    }

    //вывод из файла
    public static void PrintRes(StreamReader fin){
		string N;
        List<string> check = new List<string>();
        while ((N = fin.ReadLine()) != null) {
            Console.WriteLine(N);
        }
    }

    //удалить из файла инногороднего клиента
    public static void DeletePeopleIfNotBarnaulWithFile(List<Client> people, StreamWriter inputFinalFin){
        for (int i = 0; i < people.Count; i++)
        {
            if (people[i].getSity() == ("Барнаул")) {
                inputFinalFin.WriteLine(people[i].getName() + "\n" + people[i].getPol() 
                                        + "\n" + people[i].getYear() + "\n" + people[i].getSity() 
                                        + "\n" + people[i].getDiagnosis() + "\n");
            }else{
				people.RemoveAt(i);
			}
        }
    }

    // изменить диагноз
    public static void RemakeDiagnos(List<Client> people){
        Console.WriteLine("Изменить диагноз? д/н");
        char test = Convert.ToChar(Console.ReadLine());
        if (test == 'д') {
            // перезаписываем диагноз
            HelloWorld.remake(people);
        }
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

            Console.Write("Введите город - ");
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
                           + "\n" + people[i].getDiagnosis() + "\n\n");
        }
    }
};
class HelloWorld {
    // функция изменения диагноза
    public static void remake(List<Client> people){
        string fio, diagnosisNew;
        fio = Console.ReadLine();
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


      Console.WriteLine("Заполнить данные пациентов");
      // people массив под клиентов
      List<Client> people = new List<Client>();

	int age = 0;

    // заполняем стартовый массив
    try
    {
	    using (StreamWriter fin = new StreamWriter(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\test.txt")){
		    people = Client.ConsoleWritePeople();
		    Client.WriteInFile(people, fin);
		    fin.Close();
	    }
	}catch(IOException e){
		Console.WriteLine($"The file could not be opened: '{e}'");
	}
	
    // бесконечный цикл для реализации меню
	while(true){
        // текст меню
		Console.WriteLine( "1. Выдать на экран содержимое файла\n" +
							"2. Выдать на экран список всех иногородних пациентов\n" +
							"3. Создать файл пациентов больше заданого возраста\n" +
							"4. Распечатать файл пациентов больше заданого возраста\n" +
							"5. Добавить пациентов больше заданого возраста в исходный файл\n" +
							"6. Удалить все элементы записи инногородних пациентов\n" +
							"7. Изменить диагноз у определённого пациетна\n" +
							"8. Выход"
		);
		String numberFunctionInput = Console.ReadLine();
		switch(numberFunctionInput){
			case("1"):
				// выводим, что заполнили
				Client.PrintClient(people);
				break;
			case("2"):
				// Проверка на инногородних и выводим их
				Client.IfNotBarnaulPrint(people);
				break;
			case("3"):
				// запрашиваем возраст
				Console.Write("Введите возраст (выведутся пациенты больше или такому же значению) - ");
                while (!int.TryParse(Console.ReadLine(), out age)) Console.WriteLine("введите число!");
				try{
					using (StreamWriter fin = new StreamWriter(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\res.txt")){
						// записываем в другой файл >= age	
						Client.InputHighAgeInFile(fin, people, age);
						fin.Close();
					}
				}catch(IOException e){
					Console.WriteLine($"The file could not be opened: '{e}'");
				} 
				break;
			case("4"):
                // распечатать файл пациентов больше заданого возраста
				try{
					using (StreamReader fin = new StreamReader(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\res.txt")){
						Client.PrintRes(fin);
						fin.Close();
					}
					
				}catch(IOException e){
					Console.WriteLine($"The file could not be opened: '{e}'");
				}
				break;
			case("5"):
				try{
					using (StreamWriter fin = new StreamWriter(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\test.txt", true)){
						// добавляем >= age в исходный файл
						Client.HigtAgeInFile(fin, new StreamReader(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\res.txt"));
						fin.Close();
					}
				}catch(IOException e){
					Console.WriteLine($"The file could not be opened: '{e}'");
				}
				break;
			case("6"):
				try{
					// отчищаем файл
					File.WriteAllText(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\test.txt", string.Empty);
					using (StreamWriter fin = new StreamWriter(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\test.txt", true)){
						
						// удаляем записи иногородних
						Client.DeletePeopleIfNotBarnaulWithFile(people, fin);
                        Client.HigtAgeInFile(fin, new StreamReader(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\res.txt"));
                        fin.Close();
					}
				}catch(IOException e){
					Console.WriteLine($"The file could not be opened: '{e}'");
				}
				break;
			case("7"):
				try{
					// отчищаем файл
					File.WriteAllText(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\test.txt", string.Empty);
					using (StreamWriter fin = new StreamWriter(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\test.txt", true)){
                        // надо ли перезаписывать диагноз
						Client.RemakeDiagnos(people);
						Client.WriteInFile(people, fin);
                        Client.HigtAgeInFile(fin, new StreamReader(@"C:\Users\setInterval\Desktop\C++\semestr_2\Новая папка\C#\res.txt"));
                        fin.Close();
					}
				}catch(IOException e){
					Console.WriteLine($"The file could not be opened: '{e}'");
				}
				break;
			case("8"):
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