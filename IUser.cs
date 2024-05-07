using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace help {
   public interface IUser {
        string Name { get; set; }
        string Password {  get; set; }
        string Phone {  get; set; }
        DateTime DateBirthday { get; set; }

    }
}
