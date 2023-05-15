#include <iostream>
#include <string>
#include <fstream>
#include <vector>
#include <windows.h>

using namespace std;

// создаём класс для клиентов
class Client
{
private:
	string name;
	string pol;
	int year;
	string sity;
	string diagnosis;

public:
	void setName(string text) {
		name = text;
	}

	void setPol(string text) {
		pol = text;
	}

	void setYear(int text) {
		year = text;
	}

	void setSity(string text) {
		sity = text;
	}

	void setDiagnosis(string text) {
		diagnosis = text;
	}

	string getName() {
		return name;
	}

	string getPol() {
		return pol;
	}

	int getYear() {
		return year;
	}

	string getSity() {
		return sity;
	}

	string getDiagnosis() {
		return diagnosis;
	}

	static void PrintClient(vector<Client> people) {
		for (int i = 0; i < people.size(); i++)
		{
			cout << "Ф.И.О: " << people[i].getName() << "\nПол: " << people[i].getPol() << "\nГод: " << people[i].getYear() << "\nГород: " << people[i].getSity() << "\nДиагноз: " << people[i].getDiagnosis() << endl;
			cout << "--------" << endl;
		}
	}

	static void IfNotBarnaulPrint(vector<Client> people) {
		for (int i = 0; i < people.size(); i++)
		{
			if (people[i].getSity() != ("Барнаул")) {
				cout << "Ф.И.О: " << people[i].getName() << "\tГород: " << people[i].getSity() << endl;
			}
		}
	}

	static void InputHighAgeInFile(ofstream fout, vector<Client> people, int ageNew) {
		for (int i = 0; i < people.size(); i++)
		{
			if (people[i].getYear() >= ageNew) {
				fout << people[i].getName() << endl << people[i].getYear() << endl << people[i].getDiagnosis() << endl;
			}
		}
		fout.close();
	}

	static void HigtAgeInFile(ofstream inputFin, ifstream printFout) {
		string N;
		// добавляем >= age в исходный файл
		while(getline(printFout, N)) {
			inputFin << N << endl;
		}
		printFout.close();
		inputFin.close();
	}

	static void PrintRes(ifstream fin) {
		string N;
		vector<string> check;
		while (getline(fin, N)) {
			cout << N << endl;
		}
		fin.close();
	}

	static void DeletePeopleIfNotBarnaulWithFile(vector<Client> people, ofstream inputFinalFin) {
		for (int i = 0; i < people.size(); i++)
		{
			if (people[i].getSity() == ("Барнаул")) {
				inputFinalFin << people[i].getName() << endl << people[i].getPol() << endl << people[i].getYear() << endl << people[i].getSity() << endl << people[i].getDiagnosis() << endl;
			}
			else {
				people.erase(people.begin() + i);
			}
		}
		inputFinalFin.close();
	}

	static void RemakeDiagnos(vector<Client> people) {
		cout << "Изменить диагноз? д/н" << endl;
		char test;
		cin >> test;
		if (test == 'д') {
			// перезаписываем диагноз
			/*remake(people);*/
			string fio, diagnosisNew;
			cin.get();
			getline(cin, fio);
			cin >> diagnosisNew;

			for (int i = 0; i < people.size(); i++)
			{
				if (people[i].getName() == fio) {
					people[i].setDiagnosis(diagnosisNew);
				}
			}
		}
	}

	static vector<Client> ConsoleWritePeople() {
		
		
		cout << "Сколько пациентов" << endl;
		int res;
		cin >> res;
		while (cin.fail())
		{
			cin.clear();
			cin.sync();
			cin.ignore(99999, '\n');
			cout << "Введите значение повторно:";
			cin >> res;
		}

		vector<Client> people;
		// заполняем массив клиентов
		for (int i = 0; i < res; i++)
		{
			while ((getchar()) != '\n');
			string name, pol, sity, diagnosis;
			int year;
			Client first;

			cout << "Введите Ф.И.О. - ";
			getline(cin, name);
			first.setName(name);

			cout << "Введите пол - ";
			getline(cin, pol);
			first.setPol(pol);


			cout << "Введите возраст - ";
			cin >> year;	
			while (cin.fail())
			{
				cin.clear();
				cin.sync();
				cin.ignore(99999, '\n');
				cout << "Введите значение повторно:";
				cin >> year;
			}
			first.setYear(year);
			
			while ((getchar()) != '\n');

			cout << "Введите город - ";
			getline(cin, sity);
			first.setSity(sity);

			cout << "Введите диагноз - ";
			getline(cin, diagnosis);
			first.setDiagnosis(diagnosis);

			people.push_back(first);
		}

		return people;
	}

	static void WriteInFile(vector<Client> people, ofstream fout) {
		for (int i = 0; i < people.size(); i++)
		{
			fout << people[i].getName() << endl << people[i].getPol() << endl << people[i].getYear() << endl << people[i].getSity() << endl << people[i].getDiagnosis() << endl << endl;
		}
		fout.close();
	}
};

int main()
{

	cout << "Заполнить данные пациентов" << endl;
	// check под все строки из исходного файла
	vector <string> check;
	// people массив под клиентов
	vector <Client> people;

	int age = 0;

	Client clas;
	try{
		people = clas.ConsoleWritePeople();
		clas.WriteInFile(people, ofstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));
	}
	catch (...) {
		cout << "type error" << endl;
	}

	while (true) {
		cout << "1. Выдать на экран содержимое файла\n" <<
				"2. Выдать на экран список всех иногородних пациентов\n" <<
				"3. Создать файл пациентов больше заданого возраста\n" <<
				"4. Распечатать файл пациентов больше заданого возраста\n" <<
				"5. Добавить пациентов больше заданого возраста в исходный файл\n" <<
				"6. Удалить все элементы записи инногородних пациентов\n" <<
				"7. Изменить диагноз у определённого пациетна\n" <<
				"8. Выход" << endl;
		char task;
		cin >> task;
		switch (task) {
		case '1':
			// выводим, что заполнили
			clas.PrintClient(people);
			break;
		case '2':
			// Проверка на инногородних и выводим их
			clas.IfNotBarnaulPrint(people);
			break;
		case '3':
			// запрашиваем возраст
			cout << "Введите возраст (выведутся пациенты больше или такому же значению) - ";
			cin >> age;
			try {
				// записываем в другой файл >= age	
				clas.InputHighAgeInFile(ofstream ("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/res.txt"), people, age);
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '4':
			try {
				clas.PrintRes(ifstream ("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/res.txt"));
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '5':
			try {
				// добавляем >= age в исходный файл
				clas.HigtAgeInFile(ofstream ("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"), ifstream ("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/res.txt"));
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '6':
			try {
				// отчищаем файл
				// удаляем записи инногородних
				clas.DeletePeopleIfNotBarnaulWithFile(people, ofstream ("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '7':
			try {
				// отчищаем файл
				// надо ли перезаписывать диагноз
				clas.RemakeDiagnos(people);
				clas.WriteInFile(people, ofstream ("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));;
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '8':
			exit(3);
			break;
		default:
			cout << "Недопустимое значение!" << endl;
			break;
		}
	}
	return 0;
}