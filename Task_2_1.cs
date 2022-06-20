using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace Laba_2_semestr.Laba_2
{
    internal class Task_2_1
    {
        //Створити клас с атрибутами та конструктором. +
        //У методі main() ініціалізувати створення екземплярів класу та продемонструвати роботу його методів згідно умов завдання. +

        //Створити клас для зберігання комплексних чисел.Реалізувати операції над комплексними числами:
        //додавання, віднімання, множення, ділення, пару, зведення в ступінь, добування кореня.
        //Передбачити можливість зміни форми запису комплексного числа: алгебраїчна форма, тригонометрическая форма, експоненціальна форма.
        //Зробити властивості класу приватними, а для їх читання створити методи-геттери.
        //Створити у попередньому завданні два методи з використанням серіалізації та десеріалізації JSON.
        //Метод 1. Зберігає створений об’єкт класу з Завдання 1 у JSON файл
        //Метод 2. Відкриває JSON файл з даними та створює об’єкт класу з цими даними для виконання Завдання 1.
        public static void Start()
        {
            Complex_Number a = new Complex_Number(3,2);
            Complex_Number b = new Complex_Number(4,3);
            Complex_Number c = new Complex_Number(0,0);
            List<Complex_Number> lis = new List<Complex_Number>();
            List<Complex_Number> res_lis = new List<Complex_Number>();
            lis.Add(a);
            lis.Add(b);
            Console.WriteLine("Plus ");
            c = Complex_Number.Plus(a, b);
            lis.Add(c);
            Console.WriteLine(c.a + "|" + c.b);
            Console.WriteLine("Minus ");
            c = Complex_Number.Minus(a, b);
            lis.Add(c);
            Console.WriteLine(c.a + "|" + c.b);
            Console.WriteLine("Multiplication ");
            c = Complex_Number.Multiplication(a, b);
            lis.Add(c);
            Console.WriteLine(c.a + "|" + c.b);
            Console.WriteLine("Division ");
            c = Complex_Number.Division(a, b);
            lis.Add(c);
            Console.WriteLine(c.a + "|" + c.b);
            Console.WriteLine("Degree ");
            double degr = Complex_Number.Degree(a, 2);
            Console.WriteLine(Math.Round(degr,3));
            Console.WriteLine("Root ");
            double root = Complex_Number.Root(b,2);
            Console.WriteLine(Math.Round(root, 3));
            Console.WriteLine("Res List");

            c.Json_Write(lis);
            Console.WriteLine("Data from file");
            res_lis= c.Json_Read();
            foreach (var item in res_lis)
            {
                Console.WriteLine(item.a + "|" + item.b);
            }



        }


    }
    public class Complex_Number
    {
        public  double a { get; set; }
        public double b { get; set; }
        public Complex_Number(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public static Complex_Number Plus(Complex_Number C1, Complex_Number C2) 
        {
            return new Complex_Number(C1.a + C2.a, C1.b + C2.b);
        }
        public static Complex_Number Minus(Complex_Number C1, Complex_Number C2)
        {
            return new Complex_Number(C1.a - C2.a, C1.b - C2.b);
        }
        public static Complex_Number Multiplication(Complex_Number C1, Complex_Number C2) 
        {
            Complex_Number C = new Complex_Number((C1.a * C2.a - C1.b * C2.b), (C2.a * C1.b + C1.a * C2.b));
           // C.a = (C1.a * C2.a - C1.b * C2.b);
           // C.b= (C2.a * C1.b + C1.a * C2.b);
            return C;
        }
        public static Complex_Number Division(Complex_Number C1, Complex_Number C2)
        {
            Complex_Number C = new Complex_Number((C1.a * C2.a + C1.b * C2.b) / (C2.a * C2.a + C2.b * C2.b), (C2.a * C1.b - C2.b * C1.a) / (C2.a * C2.a + C2.b * C2.b));
            // C.a = (C1.a * C2.a + C1.b * C2.b) / (C2.a * C2.a + C2.b * C2.b);
            // C.b = (C2.a * C1.b - C2.b * C1.a) / (C2.a * C2.a + C2.b * C2.b);
            C.a = Math.Round(C.a,3);
            C.b = Math.Round(C.b,3);
            return C;
        }
        public static double Degree (Complex_Number C1 , int degre)
        {
            double degre_num = Math.Sqrt(Math.Pow(C1.a, degre) + Math.Pow(C1.b, degre));
            return degre_num;
        }
        public static double Root(Complex_Number C1 , double degre)
        {
            if(degre == 1)
            {
                degre = -1;
            }
            else
            {
                degre = degre - (degre * 2);
            }
            double degre_num = Math.Sqrt(Math.Pow(C1.a, degre) + Math.Pow(C1.b, degre));
            return degre_num;
        }


        public static double Get_A_Parametr(Complex_Number C)
        {
            return C.a;
        }
        public static double Get_B_Parametr(Complex_Number C)
        {
            return C.b;
        }
        public async void Json_Write(List<Complex_Number> lis)
        {
            string path = "D:\\Laba_2_semestr\\file.json";
            FileStream fs = new FileStream(path,FileMode.OpenOrCreate);
            await System.Text.Json.JsonSerializer.SerializeAsync<List<Complex_Number>>(fs,lis);
            Console.WriteLine("Data has been saved to file");
            fs.Close();
            
        }
        public List<Complex_Number> Json_Read()
        {
            string path = "D:\\Laba_2_semestr\\file.json";
            var Js = File.ReadAllText(path);
            List<Complex_Number> res = new List<Complex_Number>();
            res = System.Text.Json.JsonSerializer.Deserialize<List<Complex_Number>>(Js);
            return res;

        }
    }
}
