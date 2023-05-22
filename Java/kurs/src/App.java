import java.nio.file.Files;
import java.nio.file.Paths;
import java.nio.file.StandardOpenOption;
import java.util.*;
import java.io.*;

// создаём класс для клиентов
class Client
{
    // приветные поля класса
    private String name;
    private String pol;
    private int year;
    private String sity;
    private String diagnosis;

    // публичные методы
    // сеттеры (установка значения)
    public void setName(String text) {
        name = text;
    }
    public void setPol(String text) {
        pol = text;
    }
    public void setYear(int text) {
        year = text;
    }
    public void setSity(String text) {
        sity = text;
    }
    public void setDiagnosis(String text) {
        diagnosis = text;
    }

    // геттеры (получение значений полей)
    public String getName() {
        return name;
    }
    public String getPol() {
        return pol;
    }
    public int getYear() {
        return year;
    }
    public String getSity() {
        return sity;
    }
    public String getDiagnosis() {
        return diagnosis;
    }

    //вывод всего массива клиентов
    static void PrintClient(ArrayList<Client> people) {
        for(int i = 0; i < people.size(); i++)
            {
                System.out.printf("Ф.И.О: " + people.get(i).getName() +
                        "\nПол: " + people.get(i).getPol() + "\nВозраст: "
                        + people.get(i).getYear() + "\nМесто прооживания: " + people.get(i).getSity()
                        + "\nДиагноз: " + people.get(i).getDiagnosis() + "\n");
                System.out.println("--------");
            }
    }

    // вывод если клиент инногородний
    static void IfNotBarnaulPrint(ArrayList<Client> people) {
        for(int i = 0; i < people.size(); i++)
        {
            if (!people.get(i).getSity().equals("город")) {
                System.out.printf("Ф.И.О: " + people.get(i).getName() + "\tМесто прооживания: " + people.get(i).getSity() + "\n");
            }
        }
    }

    // записать в файл если возраст клиента больше или равен заданному
    static void InputHighAgeInFile(FileWriter fout, ArrayList<Client> people, int ageNew) throws IOException{
        for (int i = 0; i < people.size(); i++)
        {
            if (people.get(i).getYear() >= ageNew) {
                fout.write(people.get(i).getName() + "\n" + people.get(i).getYear() + "\n" + people.get(i).getDiagnosis() + "\n");
            }
        }
        fout.flush();
        fout.close();
    }

    // добавляем >= age в исходный файл
    static void HigtAgeInFile(String inputFin, FileReader printFout) throws IOException{
        Scanner Reader1 = new Scanner(printFout);

        // добавляем >= age в исходный файл
        while (Reader1.hasNextLine()) {
            String data = Reader1.nextLine();
            System.out.println(data);
            Files.write(Paths.get(inputFin), (data + '\n').getBytes(), StandardOpenOption.APPEND);
        }
    }

    //вывод из файла
    static void PrintRes(FileReader fin){
        Scanner Reader1 = new Scanner(fin);
        int count = 0;
        while (Reader1.hasNextLine()) {
            String data = Reader1.nextLine();

            if(count == 0){
                System.out.printf("Ф.И.О.: " + data + "\n");
            } else if(count == 1){
                System.out.printf("Возраст: " + data + "\n");
            } else if(count == 2){
                System.out.printf("Диагноз: " + data + "\n");
            } else {
                count = 0;
            }

            count++;
        }
    }

    // удалить из файла инногороднего клиента
    static void DeletePeopleIfNotBarnaulWithFile(ArrayList<Client> people, FileWriter inputFinalFin) throws IOException{
        for (int i = 0; i < people.size(); i++)
        {
            if (people.get(i).getSity().equals("город")) {
                inputFinalFin.write(people.get(i).getName() + "\n"
                        + people.get(i).getPol() + "\n" + people.get(i).getYear()
                        + "\n" + people.get(i).getSity() + "\n" + people.get(i).getDiagnosis() + "\n\n");
            } else{
                people.remove(i);
                i--;
            }
        }
        inputFinalFin.flush();
        inputFinalFin.close();
    }

    // отчистить файл
    static void ClearFile(FileWriter inputFinalFin){
        PrintWriter clearFinalFin = new PrintWriter(inputFinalFin);
        clearFinalFin.print("");
        clearFinalFin.flush();
    }

    static ArrayList<Client> AddPeople(ArrayList<Client> people) throws IOException{
        System.out.println( "Заполнить данные пациентa");

        // заполняем массив клиентов
        Scanner inputInt = new Scanner(System.in);
        Scanner inputText = new Scanner(System.in);
        Client first = new Client();

        System.out.print("Введите Ф.И.О. - ");
        String name = inputText.nextLine();
        first.setName(name);

        System.out.print("Введите пол - ");
        String pol = inputText.nextLine();
        first.setPol(pol);

        System.out.print("Введите возраст - ");
        int year;
        while (!inputInt.hasNextInt()) {
            System.out.println("Введите целое число");
            inputInt.next(); // this is important!
        }
        year = inputInt.nextInt();
        first.setYear(year);

        System.out.print("Введите место проживания (город, село) - ");
        String sity = inputText.nextLine();
        first.setSity(sity);

        System.out.print("Введите диагноз - ");
        String diag = inputText.nextLine();
        first.setDiagnosis(diag);

        people.add(first);


        if(new File("./src/test.txt").exists()){
            try(FileReader fin = new FileReader("./src/test.txt")){
                Client.WriteInFile(people, new FileWriter("./src/test.txt"));
            }catch(IOException ex){
                System.out.println(ex.getMessage());
            }
        } else{
            FileReader fin = new FileReader("./src/test.txt");
            try(fin){
                Client.WriteInFile(people, new FileWriter("./src/test.txt"));
            }catch(IOException ex){
                System.out.println(ex.getMessage());
            }
        }

        return people;
    }

    // изменить диагноз
    static void RemakeDiagnos(ArrayList<Client> people){
        // перезаписываем диагноз
        App.Remake(people);
    }

    // консольно заполнить массив клиентов
    static ArrayList<Client> ConsoleWritePeople(){
        Scanner inputInt = new Scanner(System.in);

        System.out.println("Сколько пациентов");
        int res;
        while (!inputInt.hasNextInt()) {
            System.out.println("Введите целое число");
            inputInt.next(); // this is important!
        }
        res = inputInt.nextInt();

        ArrayList<Client> people = new ArrayList<Client>();
        // заполняем массив клиентов
        for (int i = 0; i < res; i ++)
        {
            Scanner inputText = new Scanner(System.in);
            Client first = new Client();

            System.out.print("Введите Ф.И.О. - ");
            String name = inputText.nextLine();
            first.setName(name);

            System.out.print("Введите пол - ");
            String pol = inputText.nextLine();
            first.setPol(pol);

            System.out.print("Введите возраст - ");
            int year;
            while (!inputInt.hasNextInt()) {
                System.out.println("Введите целое число");
                inputInt.next(); // this is important!
            }
            year = inputInt.nextInt();
            first.setYear(year);

            System.out.print("Введите место проживания (город, село) - ");
            String sity = inputText.nextLine();
            first.setSity(sity);

            System.out.print("Введите диагноз - ");
            String diag = inputText.nextLine();
            first.setDiagnosis(diag);

            people.add(first);
        }

        return people;
    }

    static ArrayList<Client> ConsoleWrite(ArrayList<Client> people) throws IOException{
        System.out.println( "Заполнить данные пациентов");

        if (new File("./src/test.txt").exists()){
            try(FileReader fin = new FileReader("./src/test.txt")){
                people = Client.ConsoleWritePeople();
                Client.WriteInFile(people, new FileWriter("./src/test.txt"));
            }catch(IOException ex){
                System.out.println(ex.getMessage());
            }
        } else{
            FileReader fin = new FileReader("./src/test.txt");
            try(fin){
                people = Client.ConsoleWritePeople();
                Client.WriteInFile(people, new FileWriter("./src/test.txt"));
            }catch(IOException ex){
                System.out.println(ex.getMessage());
            }
        }

        return people;
    }

    static ArrayList<Client> ScanFromFile(FileReader printFout) throws IOException{
        ArrayList<String> check = new ArrayList<String>();
        ArrayList<Client> people = new ArrayList<Client>();
        Scanner Reader1 = new Scanner(printFout);

        while (Reader1.hasNextLine()) {
            String data = Reader1.nextLine();
            check.add(data);
        }

        for (int i = 0; i < check.size(); i += 6)
        {
            Client first = new Client();
            first.setName(check.get(i));
            first.setPol(check.get(i+1));
            first.setYear(Integer.parseInt(check.get(i+2)));
            first.setSity(check.get(i+3));
            first.setDiagnosis(check.get(i+4));
            people.add(first);
        }

        return people;
    }

    static Boolean updateFlag(Boolean flagMenu){
        return flagMenu = true;
    }

    // записать в файл
    static void WriteInFile(ArrayList<Client> people, FileWriter fout) throws IOException{
        for (int i = 0; i < people.size(); i++)
        {
            fout.write(people.get(i).getName() + "\n" + people.get(i).getPol()
                    + "\n" + people.get(i).getYear() + "\n" + people.get(i).getSity()
                    + "\n" + people.get(i).getDiagnosis() + "\n\n");
        }
        fout.flush();
        fout.close();
    }
};

public class App {
    // изменить диагноз
    public static void Remake(ArrayList<Client> people){
        Scanner resetDiagnosis = new Scanner(System.in);
        System.out.println("Введите ФИО - ");
        String fio = resetDiagnosis.nextLine();
        System.out.println("Введите диагноз - ");
        String diagnosisNew = resetDiagnosis.nextLine();

        for (int i = 0; i < people.size(); i++)
        {
            if (fio.equals(people.get(i).getName())) {
                people.get(i).setDiagnosis(diagnosisNew);
            }
        }
    }

    public static void main(String[] args) throws Exception {
        // people массив под клиентов
        ArrayList<Client> people = new ArrayList<Client>();
        // заполняем стартовый массив
        int age = 0;
        Boolean flagMenu = false;

        // бесконечный цикл для реализации меню
        while(true){
            // текст меню
            System.out.println( "1. Считать содержимое из файла\n" +
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
            Scanner numberFunction = new Scanner(System.in);
            String numberFunctionInput = numberFunction.nextLine();
            switch(numberFunctionInput){
                case("1"):
                    if (new File("./src/test.txt").exists()){
                        try(FileReader fin = new FileReader("./src/test.txt")){
                            people = Client.ScanFromFile(new FileReader("./src/test.txt"));
                            flagMenu = Client.updateFlag(flagMenu);
                        }catch(IOException ex){
                            System.out.println(ex.getMessage());
                        }
                    } else{
                        System.out.println("Файл не найден");
                    }
                    break;
                case("2"):
                    if (new File("./src/test.txt").exists()){
                        try(FileReader fin = new FileReader("./src/test.txt")){
                            Client.ClearFile(new FileWriter("./src/test.txt"));
                            people = Client.ConsoleWrite(people);
                            flagMenu = Client.updateFlag(flagMenu);
                        }catch(IOException ex){
                            System.out.println(ex.getMessage());
                        }
                    } else{
                        System.out.println("Файл не найден");
                    }
                    break;
                case("3"):
                    if (flagMenu){
                        // выводим, что заполнили
                        Client.PrintClient(people);
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("4"):
                    if (flagMenu){
                        // Проверка на инногородних и выводим их
                        Client.IfNotBarnaulPrint(people);
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("5"):
                    if (flagMenu){
                        // запрашиваем возраст
                        System.out.print("Введите возраст (выведутся пациенты больше или такому же значению) - ");
                        Scanner ageNum = new Scanner(System.in);
                        while (!ageNum.hasNextInt()) {
                            System.out.println("Введите целое число");
                            ageNum.next(); // this is important!
                        }
                        age = ageNum.nextInt();
                        try{
                            // записываем в другой файл >= age
                            Client.InputHighAgeInFile(new FileWriter("./src/res.txt"), people, age);
                        }catch(IOException ex){
                            System.out.println(ex.getMessage());
                        }
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("6"):
                    if (flagMenu){
                        // распечатать файл пациентов больше заданого возраста
                        if (new File("./src/res.txt").exists()){
                            try{
                                Client.PrintRes(new FileReader("./src/res.txt"));
                            }catch(IOException ex){
                                System.out.println(ex.getMessage());
                            }
                        } else{
                            System.out.println("Файл не найден");
                        }
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("7"):
                    if (flagMenu){
                        if (new File("./src/test.txt").exists()){
                            try(FileReader fin = new FileReader("./src/test.txt")){
                                people = Client.AddPeople(people);
                            }catch(IOException ex){
                                System.out.println(ex.getMessage());
                            }
                        } else{
                            System.out.println("Файл не найден");
                        }
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("8"):
                    if (flagMenu){
                        if (new File("./src/test.txt").exists()){
                            try(FileReader fin = new FileReader("./src/test.txt")){
                                // отчищаем файл
                                Client.ClearFile(new FileWriter("./src/test.txt"));
                                // удаляем записи инногородних
                                Client.DeletePeopleIfNotBarnaulWithFile(people, new FileWriter("./src/test.txt"));
                            }catch(IOException ex){
                                System.out.println(ex.getMessage());
                            }
                        } else{
                            System.out.println("Файл не найден");
                        }
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("9"):
                    if (flagMenu){
                        if (new File("./src/test.txt").exists()){
                            try(FileReader fin = new FileReader("./src/test.txt")){
                                // отчищаем файл
                                Client.ClearFile(new FileWriter("./src/test.txt"));
                                // надо ли перезаписывать диагноз
                                Client.RemakeDiagnos(people);
                                Client.WriteInFile(people, new FileWriter("./src/test.txt"));
                            }catch(IOException ex){
                                System.out.println(ex.getMessage());
                            }
                        } else{
                            System.out.println("Файл не найден");
                        }
                    } else {
                        System.out.println("Заполнете список пациентов консольно или через файл test.txt");
                    }
                    break;
                case("10"):
                    // выход из программы
                    System.exit(0);
                    break;
                default:
                    // ложное значение
                    System.out.println("Недопустимое значение!"); 
            }
        }
    }
}
