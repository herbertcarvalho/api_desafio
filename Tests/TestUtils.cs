using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tests
{
    public static class TestUtils
    {
        public async static Task ExecuteAssert(Task func, Tuple<List<int>, List<string>> tuple, string? NameOfTest = null)
        {
            try
            {
                await func;
                Console.WriteLine($"Success on test {NameOfTest ?? func.MethodName()}");
                tuple.Item1[1]++;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("ERRO - REVISE O NOME DESSE TESTE");
                tuple.Item1[0]++;
            }
            catch (Exception ex)
            {
                AppendText(NameOfTest ?? func.MethodName());
                Console.WriteLine($"Error   on test {NameOfTest ?? func.MethodName()}");
                tuple.Item1[0]++;
            }
        }

        public async static Task ExecuteAssert(Action func, Tuple<List<int>, List<string>> tuple)
        {
            try
            {
                func.Invoke();
                Console.WriteLine($"Success on test {func.MethodName()}");
                tuple.Item1[1]++;
            }
            catch (Exception)
            {
                //AppendText(func.MethodName());
                Console.WriteLine($"Error   on test {func.MethodName()}");
                tuple.Item1[0]++;
            }
        }

        public static void ResetText() => File.Delete("TestsFailed.txt");

        public static void WriteTextCmd(string name)
        {
            Console.WriteLine("");
            Console.WriteLine("############################");
            Console.WriteLine(name);
            Console.WriteLine("############################");
            Console.WriteLine("");
        }

        public static void WriteTextSimpleCmd(string name)
        {
            Console.WriteLine("");
            Console.WriteLine($"Starting {name}");
            Console.WriteLine("");
        }

        public static string GenerateFixedLengthString(int length)
        {

            var random = new Random();
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                char randomChar = chars[random.Next(chars.Length)];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }

        public static List<string?> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            bool isValid = Validator.TryValidateObject(model, validationContext, validationResults);
            return validationResults.Select(x => x.ErrorMessage).ToList();
        }

        private static void AppendText(string message) => File.AppendAllText("TestsFailed.txt", message);

        private static string MethodName(this Task task)
        {
            const string start = "+<";
            const string end = ">d__";

            var fullName = task.GetType().FullName;
            if (string.IsNullOrEmpty(fullName))
                return string.Empty;

            var methodName = fullName[(fullName.IndexOf(start) + start.Length)..];
            return methodName.Remove(methodName.IndexOf(end));
        }

        private static string MethodName(this Action action) => action.Method.Name;

        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    }
}
