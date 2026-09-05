using lhhLesson04.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace lhhLesson04.Controllers
{
    public class lhhAccountController : Controller
    {
        public IActionResult lhhIndex()
        {
            List<Account> accounts = new List<Account>
            {
                new Account()
                {
                    AcountID = "1",
                    AcountName = "John Doe",
                    AccountEmail = "hoangvb1802@gmail.com",
                    AcountPhone = "123-456-7890",
                    AcountAvatar = Url.Content("~/images/1.webp"),
                    AcountAddress = "123 Main St, Anytown, USA",
                    AcountBio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Gender = 1,
                    AcountBirthday = new DateTime(1990, 1, 1)
                },
                new Account()
                {
                    AcountID = "2",
                    AcountName = "Jane Smith",
                    AccountEmail = "jane.smith@example.com",
                    AcountPhone = "098-765-4321",
                    AcountAvatar = Url.Content("~/images/2.jpg"),
                    AcountAddress = "456 Oak Ave, Somewhere, USA",
                    AcountBio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Gender = 1,
                    AcountBirthday = new DateTime(1990, 1, 1)
                },
                new Account()
                {
                    AcountID = "3",
                    AcountName = "Alice Johnson",
                    AccountEmail = "alice.johnson@example.com",
                    AcountPhone = "555-1234",
                    AcountAvatar = Url.Content("~/images/3.webp"),
                    AcountAddress = "789 Pine St, Elsewhere, USA",
                    AcountBio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    Gender = 0,
                    AcountBirthday = new DateTime(1990, 1, 1)
                }
            };
            ViewBag.Accounts = accounts;
            return View(accounts);
        }
        [Route("ho-socua-toi",Name ="profile")]
        public IActionResult lhhProfile()
        {
            Account account = new Account()
            {
                AcountID = "1",
                AcountName = "John Doe",
                AccountEmail = "hoangvb1802@gmail.com",
                AcountPhone = "123-456-7890",
                AcountAvatar = Url.Content("~/images/1.webp"),
                AcountAddress = "123 Main St, Anytown, USA",
                AcountBio = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Gender = 0,
                AcountBirthday = new DateTime(1990, 1, 1)
            };
            ViewBag.Account = account;
            return View();
        }
    }
}
