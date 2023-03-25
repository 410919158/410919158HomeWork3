using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _410919158HomeWork3.Controllers
{
    public class HomeController : Controller
    {
        private static string answer = GenerateAnswer();
        private static List<string> GuessHistory = new List<string>();
        private static string GenerateAnswer()
        {
            Random random = new Random();
            // 產生一個 1234~9876的隨機數字
            int number = 0;
            // 總共幾組 用來判斷有沒有重複的數字
            int count = 0;
            while (count != 4)
            {
                number = random.Next(1234, 9877);
                string temp = Convert.ToString(number);
                // LINQ 的 GroupBy 方法 去分組
                var result = temp.GroupBy(x => x);
                // 分成幾組
                count = result.Count();
            }
            return Convert.ToString(number);
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.A = 0;
            ViewBag.B = 0;
            ViewBag.GuessCount = GuessHistory.Count;
            ViewBag.GuessHistory = GuessHistory;
            @ViewBag.Answer = answer;
            return View();
        }
        [HttpPost]
        public ActionResult Check(string UserNumber)
        {
            int a = 0, b = 0;
            for (int i = 0; i < 4; i++)
            {
                if (answer[i] == UserNumber[i]) a++;
                else if (answer.Contains(UserNumber[i])) b++;
            }
            GuessHistory.Add($"{UserNumber}: {a}A{b}B");

            if (a == 4)
            {
                ViewBag.A = a;
                ViewBag.GuessCount = GuessHistory.Count;
                ViewBag.GuessHistory = GuessHistory;
                ViewBag.Answer = answer;
                answer = GenerateAnswer();
            }
            else
            {
                ViewBag.A = a;
                ViewBag.B = b;
                ViewBag.GuessCount = GuessHistory.Count;
                ViewBag.GuessHistory = GuessHistory;
            }
            return View("Index");
        }
    }
}