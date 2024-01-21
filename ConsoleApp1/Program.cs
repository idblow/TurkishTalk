using TurkishTalk.Persistance;
using TurkishTalk.Persistance.Models;

var context = new ApplicationDBContext();
context.Set<User>();