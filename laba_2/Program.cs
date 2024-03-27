using Newtonsoft.Json;
using System;
using System.IO;

public class Time
{
    private int hour;
    private int minute;
    private int second;

    public int Hour
    {
        get { return hour; }
        set
        {
            if (value >= 0 && value < 24)
                hour = value;
            else
                throw new ArgumentException("Година повинна бути у діапазоні від 0 до 23");
        }
    }

    public int Minute
    {
        get { return minute; }
        set
        {
            if (value >= 0 && value < 60)
                minute = value;
            else
                throw new ArgumentException("Хвилина повинна бути у діапазоні від 0 до 59");
        }
    }

    public int Second
    {
        get { return second; }
        set
        {
            if (value >= 0 && value < 60)
                second = value;
            else
                throw new ArgumentException("Секунда повинна бути у діапазоні від 0 до 59");
        }
    }

    public Time(int hour, int minute, int second)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
    }

    public void AddHours(int hours)
    {
        int totalHours = hour + hours;
        hour = totalHours % 24;
    }

    public void AddMinutes(int minutes)
    {
        int totalMinutes = minute + minutes;
        int addHours = totalMinutes / 60;
        minute = totalMinutes % 60;
        AddHours(addHours);
    }

    public void AddSeconds(int seconds)
    {
        int totalSeconds = second + seconds;
        int addMinutes = totalSeconds / 60;
        second = totalSeconds % 60;
        AddMinutes(addMinutes);
    }

    public void SaveToJson(string filePath)
    {
        string json = JsonConvert.SerializeObject(this);
        File.WriteAllText(filePath, json);
    }

    public static Time LoadFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<Time>(json);
    }
    public override string ToString()
    {
        return $"{hour:D2}:{minute:D2}:{second:D2}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
            Time time = new Time(10, 30, 45);
            Console.WriteLine("Початковий час: " + time);

            string filePath = "B:\\kpi\\ООП\\semeseter_2\\laba_2\\time.json";
            time.SaveToJson(filePath);
            Console.WriteLine("Об'єкт збережено у файлі: " + filePath);

            time.AddHours(2);
            Console.WriteLine("Час після додавання 2 годин: " + time);

            time.AddMinutes(75);
            Console.WriteLine("Час після додавання 75 хвилин: " + time);

            time.AddSeconds(200);
            Console.WriteLine("Час після додавання 200 секунд: " + time);

            Time loadedTime = Time.LoadFromJson(filePath);
            Console.WriteLine("Об'єкт завантажено з файлу: " + loadedTime);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Помилка: Файл не знайдено - " + ex.Message);
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Помилка при обробці JSON: " + ex.Message);
        }
    }
}
