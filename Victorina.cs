using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace help {
    [Serializable]
    public class Victorina {
        public List<User> users = new List<User>();
        public List<General> Generals { get; set; }

        public Victorina() {
            Generals = new List<General>();
        }
        public void SaveUsers(string path) {
            try {
                string json = JsonSerializer.Serialize(users);
                File.WriteAllText(path, json);
            }
            catch (Exception ex) { Console.WriteLine("Not saves"); }
        }
        public void LoadUsers(string path) {
            if (!File.Exists(path)) { return; }
            try {
                var json = File.ReadAllText(path);
                users = JsonSerializer.Deserialize<List<User>>(json);
            }
            catch (Exception ex) { Console.WriteLine("Not loaded"); }
        }
        public void AddUsers(User user) {
            users.Add(user);
        }
        public bool CheckPerson(User user) {
            foreach (var person in users) {
                if (user.Name == person.Name &&
                    user.Password == person.Password) {
                    return true;
                }
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(32, 5);
            Console.WriteLine("Вы неверно ввели пароль или имя, попробуйте ещё раз"); Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
            return false;
        }
        public bool CheckLogin(string name) {
            foreach (var log in users) {
                if (log.Name == name) {
                    return false;
                }
            }
            return true;
        }
        public void DeleteUsers(User user) {
            users.Remove(user);
        }
        public void UpdateUserDateOfBirth(string userName, DateTime dateTime) {
            User userToUpdate = users.Find(user => user.Name == userName);
            if (userToUpdate != null) {
                userToUpdate.DateBirthday = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
            }
            else {
                Console.WriteLine($"User {userName} not found");
            }
        }
        public void UpdateUserPassword(string userName, string password) {
            User userToUpdate = users.Find(user => user.Name == userName);
            if (userToUpdate != null) {
                userToUpdate.SetPassword(password);
            }
            else {
                Console.WriteLine($"User {userName} not found");
            }
        }
        public void UpdateUserPhone(string userName, string phone) {
            User userToUpdate = users.Find(user => user.Name == userName);
            if (userToUpdate != null) {
                userToUpdate.Phone = phone;
            }
            else {
                Console.WriteLine($"User {userName} not found");
            }
        }
        public void ShowUser(string name) {
            User userShow = users.Find(user => user.Name == name);
            if (userShow != null) {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Установка цвета текста
                Console.SetCursorPosition(57, 3);
                Console.WriteLine("ВИКТОРИНА\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\n\n\tИмя пользователя: "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(userShow.Name); Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\tДата рождения: "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(userShow.DateBirthday.ToString("dd.MM.yyyy")); // Проверьте формат отображения даты рождения
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("\tНомер телефона: "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(userShow.Phone); // Убедитесь, что номер телефона правильно отображается
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }
        public void SaveAllQuestions(string path) {
            try {
                var json = JsonSerializer.Serialize(Generals);
                File.WriteAllText(path, json);
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка сохранения вопросов: " + ex.Message);
            }
        }
        public void LoadAllQuestions(string path) {
            if (!File.Exists(path)) {
                Console.WriteLine("Файл с вопросами не найден.");
                return;
            }

            try {
                string json = File.ReadAllText(path);
                Generals = JsonSerializer.Deserialize<List<General>>(json);
            }
            catch (Exception ex) {
                Console.WriteLine("Ошибка загрузки вопросов: " + ex.Message);
            }
        }
        public void AddGeneral(General general) {
            Generals.Add(general);
        }
        public void ShowGenerals() {
            for (int i = 0; i < Generals.Count; i++) {
                Console.WriteLine($"{i + 1}. {Generals[i].Title}");
            }
        }
        public void ShowAllUserScores() {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(57, 3);
            Console.WriteLine("ВИКТОРИНА");
            Console.WriteLine("\n\n");
            foreach (var user in users) {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\t\t\t\t\t\tИмя участника: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(user.Name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"\t\t\t\t\t\tКоличество баллов: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(user.CorrectAnswersCount);

                Console.WriteLine(); 
            }
        }

    }
}

