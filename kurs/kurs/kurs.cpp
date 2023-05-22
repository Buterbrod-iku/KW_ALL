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
			cout << "Ф.И.О: " << people[i].getName() << "\nПол: " << people[i].getPol() << "\nВозраст: " << people[i].getYear() << "\nМесто прооживания: " << people[i].getSity() << "\nДиагноз: " << people[i].getDiagnosis() << endl;
			cout << "--------" << endl;
		}
	}

	static void IfNotBarnaulPrint(vector<Client> people) {
		for (int i = 0; i < people.size(); i++)
		{
			if (people[i].getSity() != ("город")) {
				cout << "Ф.И.О: " << people[i].getName() << "\tМесто прооживания: " << people[i].getSity() << endl;
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
		int count = 0;
		while (getline(fin, N)) {
			if (count == 0) {
				cout << "Ф.И.О.: " << N  << endl;
			}
			else if (count == 1) {
				cout << "Возраст: " << N << endl;
			}
			else if (count == 2) {
				cout << "Диагноз: " << N << endl;
			}
			else {
				count = 0;
			}

			count++;
		}
		fin.close();
	}

	static void DeletePeopleIfNotBarnaulWithFile(vector<Client> people, ofstream inputFinalFin) {
		for (int i = 0; i < people.size(); i++)
		{
			if (people[i].getSity() == ("город")) {
				inputFinalFin << people[i].getName() << endl << people[i].getPol() << endl << people[i].getYear() << endl << people[i].getSity() << endl << people[i].getDiagnosis() << endl;
			}
			else {
				people.erase(people.begin() + i);
				i--;
			}
		}
		inputFinalFin.close();
	}

	static vector<Client> AddPeople(vector<Client> people){
		cout << "Заполнить данные пациентa" << endl;

		// заполняем массив клиентов
		Client first;
		string name, pol, sity, diag;

		cout << "Введите Ф.И.О. - ";
		while ((getchar()) != '\n');
		getline(cin, name);
		first.setName(name);

		cout << "Введите пол - ";
		getline(cin, pol);
		first.setPol(pol);

		cout << "Введите возраст - ";
		int year;
		while (cin.fail())
		{
			cin.clear();
			cin.sync();
			cin.ignore(99999, '\n');
			cout << "Введите значение повторно:";
			cin >> year;
		}
		cin >> year;
		first.setYear(year);

		while ((getchar()) != '\n');
		cout << "Введите место проживания (город, село) - ";
		getline(cin, sity);
		first.setSity(sity);

		cout << "Введите диагноз - ";
		getline(cin, diag);
		first.setDiagnosis(diag);

		people.push_back(first);


		try {
			// отчищаем файл
			// надо ли перезаписывать диагноз
			WriteInFile(people, ofstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));
		}
		catch (char* name) {
			cout << "The file could not be opened: " << name << endl;
		}

		return people;
	}

	static void RemakeDiagnos(vector<Client> people) {
		// перезаписываем диагноз
		/*remake(people);*/
		string fio, diagnosisNew;
		cin.get();
		cout << "Введите ФИО - " << endl;
		getline(cin, fio);
		cout << "Введите диагноз - " << endl;
		cin >> diagnosisNew;

		for (int i = 0; i < people.size(); i++)
		{
			if (people[i].getName() == fio) {
				people[i].setDiagnosis(diagnosisNew);
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

			cout << "Введите место проживания (город, село) - ";
			getline(cin, sity);
			first.setSity(sity);

			cout << "Введите диагноз - ";
			getline(cin, diagnosis);
			first.setDiagnosis(diagnosis);

			people.push_back(first);
		}

		return people;
	}

	static vector<Client> ConsoleWrite(vector<Client> people){
		cout << "Заполнить данные пациентов" << endl;

		try {
			// отчищаем файл
			// надо ли перезаписывать диагноз
			people = ConsoleWritePeople();
			WriteInFile(people, ofstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));
		}
		catch (char* name) {
			cout << "The file could not be opened: " << name << endl;
		}

		return people;
	}

	static vector<Client> ScanFromFile(ifstream printFout){
		vector<string> check;
		vector<Client> people;

		string N;
		while (getline(printFout, N)) {
			check.push_back(N);
		}

		for (int i = 0; i < check.size(); i += 6)
		{
			Client first;
			first.setName(check[i]);
			first.setPol(check[i + 1]);
			first.setYear(stoi(check[i + 2]));
			first.setSity(check[i + 3]);
			first.setDiagnosis(check[i + 4]);
			people.push_back(first);
		}

		return people;
	}

	static bool updateFlag(bool flagMenu) {
		return flagMenu = true;
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
	// people массив под клиентов
	vector <Client> people;
	int age = 0;
	Client clas;
	bool flagMenu = false;

	while (true) {
		cout << "1. Считать содержимое из файла\n" <<
				"2. Заполнить пациентов через консоль\n" <<
				"3. Выдать на экран содержимое файла\n" <<
				"4. Выдать на экран список всех иногородних пациентов\n" <<
				"5. Создать файл пациентов больше заданого возраста\n" <<
				"6. Распечатать файл пациентов больше заданого возраста\n" <<
				"7. Добавить пациентов больше заданого возраста в исходный файл\n" <<
				"8. Удалить все элементы записи инногородних пациентов\n" <<
				"9. Изменить диагноз у определённого пациетна\n" <<
				"10. Выход" << endl;
		char task;
		cin >> task;
		switch (task) {
		case '1':
			try {
				people = clas.ScanFromFile(ifstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/res.txt"));
				flagMenu = clas.updateFlag(flagMenu);
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '2':
			try {
				people = clas.ConsoleWrite(people);
				flagMenu = clas.updateFlag(flagMenu);
			}
			catch (char* name) {
				cout << "The file could not be opened: " << name << endl;
			}
			break;
		case '3':
			if (flagMenu) {
				// выводим, что заполнили
				clas.PrintClient(people);
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '4':
			if (flagMenu) {
				// Проверка на инногородних и выводим их
				clas.IfNotBarnaulPrint(people);
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '5':
			if (flagMenu) {
				// запрашиваем возраст
				cout << "Введите возраст (выведутся пациенты больше или такому же значению) - ";
				cin >> age;
				try {
					// записываем в другой файл >= age	
					clas.InputHighAgeInFile(ofstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/res.txt"), people, age);
				}
				catch (char* name) {
					cout << "The file could not be opened: " << name << endl;
				}
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '6':
			if (flagMenu) {
				try {
					clas.PrintRes(ifstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/res.txt"));
				}
				catch (char* name) {
					cout << "The file could not be opened: " << name << endl;
				}
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '7':
			if (flagMenu) {
				try {
					// добавляем >= age в исходный файл
					people = clas.AddPeople(people);
				}
				catch (char* name) {
					cout << "The file could not be opened: " << name << endl;
				}
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '8':
			if (flagMenu) {
				try {
					// отчищаем файл
					// удаляем записи инногородних
					clas.DeletePeopleIfNotBarnaulWithFile(people, ofstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));
				}
				catch (char* name) {
					cout << "The file could not be opened: " << name << endl;
				}
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '9':
			if (flagMenu) {
				try {
					// отчищаем файл
					// надо ли перезаписывать диагноз
					clas.RemakeDiagnos(people);
					clas.WriteInFile(people, ofstream("C:/Users/Buterbrod/Desktop/C++/semestr_2/Новая папка/kurs/test.txt"));
				}
				catch (char* name) {
					cout << "The file could not be opened: " << name << endl;
				}
			}
			else {
				cout << "Заполнете список пациентов консольно или через файл test.txt" << endl;
			}
			break;
		case '10':
			exit(3);
			break;
		default:
			cout << "Недопустимое значение!" << endl;
			break;
		}
	}
	return 0;
}