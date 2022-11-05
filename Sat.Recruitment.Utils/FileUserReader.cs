using Sat.Recruitment.Domain.Models;

namespace Sat.Recruitment.Utils;

public class FileUserReader
{
    public static List<User> ReadAllUsers()
    {
        var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";
        if (!File.Exists(path))
            return null;

        var result = new List<User>();
        var lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var array = line.Split(',');
            result.Add(new User
            {
                Name = array[0],
                Email = array[1],
                Phone = array[2],
                Address = array[3],
                UserType = array[4],
                Money = decimal.Parse(array[5])
            });
        }

        return result;
    }
}
