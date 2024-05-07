using System;
using System.Security.Cryptography;
using System.Text;

namespace help {
    [Serializable]
    public class User : IUser {
        private string _password;
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime DateBirthday { get; set; }
        public string Password {
            get {
                return _password;
            }
            set {
                _password = value;
            }
        }
        public void SetPassword(string password) {
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            string cryptPassword = Encoding.UTF8.GetString(hash);
            this._password = cryptPassword;
        }
        public int CorrectAnswersCount { get; set; } 
        public User() { }
        public User(string name, string phone, DateTime date, string pass, int count) {
            this.Name = name;
            this.Phone = phone;
            this.DateBirthday = date;
            this.Password = pass;
            this.CorrectAnswersCount = count;

        }
        







    }
}
