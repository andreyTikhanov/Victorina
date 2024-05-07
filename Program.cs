using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace help {
    internal class Program {
        const string PathUser = "users.json";
        const string PathQusetions = "questions.json";
        static void Hi(User user) {
            Console.SetCursorPosition(2, 2);
            DateTime currentTime = DateTime.Now;
            if (currentTime.Hour >= 5 && currentTime.Hour < 10) {
                Console.Write($"Доброе утро, "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(user.Name); Console.ForegroundColor = ConsoleColor.DarkYellow;

            }
            else if (currentTime.Hour >= 10 && currentTime.Hour < 18) {
                Console.Write($"Добрый день, "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(user.Name); Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else if (currentTime.Hour >= 18 && currentTime.Hour < 23) {
                Console.Write($"Добрый вечер, "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(user.Name); Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            else {
                Console.Write($"Доброй ночи, "); Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(user.Name); Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
        }
        static void FirstMenu(string[] items, int index) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Установка цвета текста
            Console.SetCursorPosition(57, 3);
            Console.WriteLine("ВИКТОРИНА");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < items.Length; i++) {
                Console.SetCursorPosition(50, 6 + i * 3);
                if (i == index) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("-> ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else {
                    Console.Write("   ");
                }
                Console.WriteLine(items[i]);
            }
        }
        static void Menu(string[] items, int index) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Установка цвета текста
            Console.SetCursorPosition(57, 3);
            Console.WriteLine("ВИКТОРИНА");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < items.Length; i++) {
                Console.SetCursorPosition(50, 6 + i * 3);
                if (i == index) {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("-> ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else {
                    Console.Write("   ");
                }
                Console.WriteLine(items[i]);
            }
        }
       static void Main(string[] args) {
            Victorina victorina = new Victorina();
            List<User> users = new List<User>();
            victorina.LoadUsers(PathUser);
            ShowMenu1();
            User user = new User();
            int selectedIndex = 0;
            string[] firstMenu = { "Войти", "Зарегистрироваться" };
            string[] mainMenu = { "Пройти викторину", "Создать викторину", "Посмотреть результаты", "Информация о пользователе", "Настройки", "Выйти" };
            string[] userMenu = { "Изменить пароль", "Изменить дату рождения","Изменить номер телефона", "Назад" };
            while (true) {
                Console.Clear();
                //Выводим приветствие
                Console.SetCursorPosition(53, 2);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Добро пожаловать \n\n");
                Console.ForegroundColor = ConsoleColor.White;
                // Выводим меню
                FirstMenu(firstMenu, selectedIndex);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.UpArrow) {
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow) {
                    selectedIndex = Math.Min(firstMenu.Length - 1, selectedIndex + 1);
                }
                else if (keyInfo.Key == ConsoleKey.Enter) {
                    break;
                }
            }
            if (selectedIndex == 1) {
                while (true) {
                    Console.Clear();
                    Console.SetCursorPosition(42, 6);
                    Console.Write("Введите Ваше имя: ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string name = Console.ReadLine();
                    user.Name = name;
                    if (victorina.CheckLogin(name)) {
                        while (true) {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(42, 6);
                            Console.Write("Введите пароль:");
                            string password = "";
                            ConsoleKeyInfo key;
                            do {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                key = Console.ReadKey(true);
                                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) {
                                    password += key.KeyChar;
                                    Console.Write("*");
                                }
                                else if (key.Key == ConsoleKey.Backspace && password.Length > 0) {
                                    password = password.Substring(0, (password.Length - 1));
                                    Console.Write("\b \b");
                                }

                            } while (key.Key != ConsoleKey.Enter);
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.SetCursorPosition(42, 8);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Повторите пароль:");
                            string keyPassword = "";
                            do {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                key = Console.ReadKey(true);
                                if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) {
                                    keyPassword += key.KeyChar;
                                    Console.Write("*");
                                }
                                else if (key.Key == ConsoleKey.Backspace && keyPassword.Length > 0) {
                                    keyPassword = keyPassword.Substring(0, (keyPassword.Length - 1));
                                    Console.Write("\b \b");
                                }
                            } while (key.Key != ConsoleKey.Enter);
                            if (password == keyPassword) {
                                user.SetPassword(password);
                                break;
                            }
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.SetCursorPosition(32, 5);
                            Console.WriteLine("Вы неверно повторили пароли, попробуйте ещё раз"); Console.ReadKey();
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        //используем паттерн для записи номера телефона
                        string phonePattern = @"^\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
                        while (true) {
                            Console.Clear();
                            Console.SetCursorPosition(25, 6);
                            Console.Write("Введите номер телефона в формате: x(xxx)xxx-xx-xx: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string phone = Console.ReadLine();
                            Regex regex = new Regex(phonePattern);
                            bool isMatch = regex.IsMatch(phone);
                            if (isMatch) {
                                user.Phone = phone;
                                break;
                            }
                            else {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.SetCursorPosition(27, 6);
                                Console.WriteLine("Вы ввели неверный формат ввода, попробуйте ещё раз! "); Console.ReadKey();
                                Console.ForegroundColor = ConsoleColor.White;
                            }

                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        while (true) {
                            Console.Clear();
                            Console.SetCursorPosition(25, 6);
                            Console.Write("Введите дату рождения в формате дд.мм.гггг: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string input = Console.ReadLine();
                            DateTime date;
                            if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                                user.DateBirthday = date;
                                break;
                            }
                            else {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.SetCursorPosition(27, 6);
                                Console.WriteLine("Вы ввели неверный формат ввода, попробуйте ещё раз! ");
                                Console.ReadKey();
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }
                        victorina.AddUsers(user);
                        victorina.SaveUsers(PathUser);
                        Console.Clear();
                        Console.SetCursorPosition(15, 3);
                        Console.WriteLine("Данные успешно сохранены, для корректной работы необходимо перезапустить приложение");
                        Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.White;
                        return;

                    }
                    else {
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(32, 5);
                        Console.WriteLine("Пользователь с таким именем уже существует, попробуйте ещё раз"); Console.ReadKey();
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

            }
            else if (selectedIndex == 0) {
                //блок входа пользователя
                while (true) {
                    Console.Clear();
                    Console.SetCursorPosition(42, 6);
                    Console.Write("Введите Ваше имя: ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    string name = Console.ReadLine();
                    user.Name = name;
                    ConsoleKeyInfo key;
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(44, 6);
                    Console.Write("Введите пароль:");
                    string password = "";
                    do {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        key = Console.ReadKey(true);
                        if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) {
                            password += key.KeyChar;
                            Console.Write("*");
                        }
                        else if (key.Key == ConsoleKey.Backspace && password.Length > 0) {
                            password = password.Substring(0, (password.Length - 1));
                            Console.Write("\b \b");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                    } while (key.Key != ConsoleKey.Enter);
                    user.SetPassword(password);
                    if (victorina.CheckPerson(user)) {
                        break;
                    }
                }
                while (true) {
                    while (true) {
                        Console.Clear();
                        Menu(mainMenu, selectedIndex);
                        Hi(user);
                        Console.SetCursorPosition(0, 20);
                        ConsoleKeyInfo keyInfo = Console.ReadKey();
                        if (keyInfo.Key == ConsoleKey.UpArrow) {
                            selectedIndex = Math.Max(0, selectedIndex - 1);
                        }
                        else if (keyInfo.Key == ConsoleKey.DownArrow) {
                            selectedIndex = Math.Min(mainMenu.Length - 1, selectedIndex + 1);
                        }
                        else if (keyInfo.Key == ConsoleKey.Enter) {
                            break;
                        }
                    }
                    if (selectedIndex == 0) {
                        int countRigh = 0;
                        victorina.LoadAllQuestions(PathQusetions);
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.SetCursorPosition(57, 3);
                        Console.WriteLine("ВИКТОРИНА");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(48, 6);
                        Console.Write("Введите номер темы викторины:");
                        Console.WriteLine("\n\n");
                        for (int i = 0; i < victorina.Generals.Count; i++) {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine($"\t\t\t\t\t\t\t{i + 1}. {victorina.Generals[i].Title}");
                        }
                        int menuIndex=Convert.ToInt32(Console.ReadLine())-1;
                        General selectedGeneral = victorina.Generals[menuIndex];
                        Console.Clear();
                        Console.SetCursorPosition(48, 6);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($"Вы выбрали тему: ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(selectedGeneral.Title);
                        foreach (var question in selectedGeneral.Questions) {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(48, 6);
                            Console.Write($"Вопрос: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine(question.Title);
                            Console.SetCursorPosition(48, 8);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Варианты ответов:");
                            Console.WriteLine();
                            for (int i = 0; i < question.Answers.Count; i++) {
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine($"\t\t\t\t\t\t{i + 1}. {question.Answers[i]}");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(48, 20);
                            Console.Write("Введите номер правильного ответа: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            int userAnswer = int.Parse(Console.ReadLine())-1;
                            if (userAnswer == question.RightIndex) {
                                countRigh++;
                            }
                        }
                        user.CorrectAnswersCount = countRigh;
                        Console.WriteLine(user.CorrectAnswersCount);
                        Console.ReadKey();
                        User existingUser = victorina.users.FirstOrDefault(u => u.Name == user.Name);
                        if (existingUser != null) {
                            existingUser.CorrectAnswersCount = user.CorrectAnswersCount;
                        }
                        else {
                            victorina.users.Add(user);
                        }
                        victorina.SaveUsers(PathUser);

                    }
                    else if (selectedIndex == 1) {
                        Console.Clear();
                        General general = new General();
                        Console.ForegroundColor = ConsoleColor.DarkYellow; 
                        Console.SetCursorPosition(57, 3);
                        Console.WriteLine("ВИКТОРИНА");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(42, 6);
                        Console.Write("Введите тему викторины: ");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        string topic = Console.ReadLine();
                        general.Title = topic;
                        while (true) {
                            Console.Clear();
                            Console.SetCursorPosition(42, 6);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Введите название вопроса: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string quest = Console.ReadLine();
                            if (quest == "") break;
                            Question question = new Question();
                            question.Title = quest;
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(42, 6);
                            Console.Write("Введите 1 вариант: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string answer1 = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(42, 8);
                            Console.Write("Введите 2 вариант: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string answer2 = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(42, 10);
                            Console.Write("Введите 3 вариант: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string answer3 = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(42, 12);
                            Console.Write("Введите 4 вариант: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string answer4 = Console.ReadLine();
                            question.Answers = new List<string> { answer1, answer2, answer3, answer4 };
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(42, 14);
                            Console.Write("Введите номер правильного ответа: ");
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            int index = Convert.ToInt32(Console.ReadLine()) - 1;
                            question.RightIndex = index;
                            general.Questions.Add(question);
                        }
                        victorina.AddGeneral(general);
                        victorina.SaveAllQuestions(PathQusetions);
                    }

                    else if (selectedIndex == 2) {
                        Console.Clear();
                        victorina.ShowAllUserScores();
                        Console.ReadKey();
                    }
                    else if (selectedIndex == 3) {
                        Console.Clear();
                        victorina.ShowUser(user.Name);
                        Console.ReadKey();
                    }
                    else if (selectedIndex == 4) {
                        while (true) {
                            Console.Clear();
                            Menu(userMenu, selectedIndex);
                            ConsoleKeyInfo keyInfo = Console.ReadKey();
                            if (keyInfo.Key == ConsoleKey.UpArrow) {
                                selectedIndex = Math.Max(0, selectedIndex - 1);
                            }
                            else if (keyInfo.Key == ConsoleKey.DownArrow) {
                                selectedIndex = Math.Min(userMenu.Length - 1, selectedIndex + 1);
                            }
                            else if (keyInfo.Key == ConsoleKey.Enter) {
                                break;
                            }
                        }
                        if (selectedIndex == 0) {
                            while (true) {
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(42, 6);
                                Console.Write("Введите пароль:");
                                string password = "";
                                ConsoleKeyInfo key;
                                do {
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    key = Console.ReadKey(true);
                                    if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace) {
                                        password += key.KeyChar;
                                        Console.Write("*");
                                    }
                                    else if (key.Key == ConsoleKey.Backspace && password.Length > 0) {
                                        password = password.Substring(0, (password.Length - 1));
                                        Console.Write("\b \b");
                                    }

                                } while (key.Key != ConsoleKey.Enter);
                                if (password.Length > 0) {
                                    victorina.UpdateUserPassword(user.Name, password);
                                    victorina.SaveUsers(PathUser);
                                    Console.Clear();
                                    Console.SetCursorPosition(47, 3);
                                    Console.WriteLine("Пароль успешно изменён!");
                                    Console.ReadKey();
                                    break;
                                }
                                else {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(32, 5);
                                    Console.WriteLine("Вы неверно повторили пароли, попробуйте ещё раз"); Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                        }
                        else if (selectedIndex == 1) {
                            while (true) {
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.SetCursorPosition(42, 8);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Clear();
                                Console.SetCursorPosition(25, 6);
                                Console.Write("Введите дату рождения в формате дд.мм.гггг: ");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                string input = Console.ReadLine();
                                if (input == "") { break; }
                                DateTime date;
                                if (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                                    Console.Clear();
                                    Console.SetCursorPosition(47, 3);
                                    Console.WriteLine("Дата рождения изменена!");
                                    victorina.UpdateUserDateOfBirth(user.Name, date);
                                    victorina.SaveUsers(PathUser);
                                    break;
                                }
                                else {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(27, 6);
                                    Console.WriteLine("Вы ввели неверный формат ввода, попробуйте ещё раз! ");
                                    Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                        }
                        else if (selectedIndex == 2) {
                            Console.ForegroundColor = ConsoleColor.White;
                            //используем паттерн для записи номера телефона
                            string phonePattern = @"^\d{1}\(\d{3}\)\d{3}-\d{2}-\d{2}$";
                            while (true) {
                                Console.Clear();
                                Console.SetCursorPosition(25, 6);
                                Console.Write("Введите номер телефона в формате: x(xxx)xxx-xx-xx: ");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                string phone = Console.ReadLine();
                                if (phone == "") { break; }
                                Regex regex = new Regex(phonePattern);
                                bool isMatch = regex.IsMatch(phone);
                                if (isMatch) {
                                    Console.Clear();
                                    Console.SetCursorPosition(47, 3);
                                    Console.WriteLine("Номер телефона изменен!");
                                    victorina.UpdateUserPhone(user.Name, phone);
                                    victorina.SaveUsers(PathUser);
                                    Console.ReadKey();
                                    break;
                                }
                                else {
                                    Console.Clear();
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.SetCursorPosition(27, 6);
                                    Console.WriteLine("Вы ввели неверный формат ввода, попробуйте ещё раз! "); Console.ReadKey();
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                        }
                        else if (selectedIndex == 3) { }
                    }



                    else if (selectedIndex == 5) {
                        return;
                    }

                }
            }

        }
        static void ShowMenu1() {
            Console.ForegroundColor = ConsoleColor.DarkYellow; // Установка цвета текста
            Console.SetCursorPosition(57, 12);
            Console.WriteLine("ВИКТОРИНА");
            Console.WriteLine();
            Console.SetCursorPosition(52, 18);
            Console.WriteLine("Проверь свои знания!");
            Console.ForegroundColor = ConsoleColor.White;
            // Ждем 2 секунды
            Thread.Sleep(2000);
            Console.Clear(); // Очищаем консоль
        }


    }
}
