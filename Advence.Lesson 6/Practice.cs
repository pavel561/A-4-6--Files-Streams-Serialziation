using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Advence.Lesson_6
{
    public partial class Practice
    {
        /// <summary>
        /// AL6-P1/7-DirInfo. Вывести на консоль следующую информацию о каталоге “c://Program Files”:
        /// Имя
        /// Дата создания
        /// </summary>
        public static void AL6_P1_7_DirInfo()
        {
			var dirInfo = new DirectoryInfo("C://Program Files");
			Console.WriteLine(dirInfo.Name);
			Console.WriteLine(dirInfo.CreationTime);
        }


        /// <summary>
        /// AL6-P2/7-FileInfo. Получить список файлов каталога и для каждого вывести значение:
        /// Имя
        /// Дата создания
        /// Размер 
        /// </summary>
        public static void AL6_P2_7_FileInfo()
        {
			var dirInfo = new DirectoryInfo("D://");
			var files = dirInfo.GetFiles("*.txt");
			foreach (FileInfo file in files)
			{
				Console.WriteLine(file.Name);
				Console.WriteLine(file.CreationTime);
				Console.WriteLine(file.Length);
			}
		}

        /// <summary>
        /// AL6-P3/7-CreateDir. Создать пустую директорию “c://Program Files Copy”.
        /// </summary>
        public static void AL6_P3_7_CreateDir()
        {
			var dir = Directory.CreateDirectory("D:\\Lection_6");
			Console.WriteLine(dir.Name);
			Console.WriteLine(dir.CreationTime);
			//Console.WriteLine(dir.);
		}
        /// <summary>
        /// AL6-P4/7-CopyFile. Скопировать первый файл из Program Files в новую папку.
        /// </summary>
        public static void AL6_P4_7_CopyFile()
        {
			var dir = new DirectoryInfo("D:\\");
			var files = dir.GetFiles("*.txt");
			files[0].CopyTo("D:\\Lection_6\\text" + ".txt");
			//Console.WriteLine(dir.Name);
			//Console.WriteLine(dir.CreationTime);
		}

        /// <summary>
        /// AL6-P5/7-FileChat. Написать программу имитирующую чат. 
        /// Пускай в ней будут по очереди запрашивается реплики для User 1 и User 2 (используйте цикл из 5-10 итераций).  Сохраняйте данные реплики с ником пользователя и датой в файл на диске.
        /// </summary>
        public static void AL6_P5_7_FileChat()
        {
			for (int i = 0; i < 10; i++)
			{
				if (i % 2 == 0) Console.Write("User 1: ");
				else Console.Write("User 2: ");

				string text = Console.ReadLine();
				//var stream = File.OpenWrite("D://History.txt");
				var adapter = new StreamWriter("D://History.txt", true);
				adapter.Write(DateTime.Now.ToShortDateString());
				adapter.Write(" ");
				if (i % 2 == 0) adapter.Write("User 1: ");
				else adapter.Write("User 2: ");
				adapter.Write(text);
				adapter.WriteLine();
				adapter.Close();
				//stream.Close();
			}
        }

        /// <summary>
        /// AL6-P6/7-ConsoleSrlz. (Демонстрация). 
        /// Сериализовать обьект класса Song в XML.Вывести на консоль.
        /// Десериализовать XML из строковой переменной в объект.
        /// </summary>
        public static void AL6_P6_7_ConsoleSrlzn()
        {
			//Создание экземпляра объекта
            Song song = new Song()
            {
                Title = "Title 1",
                Duration = 247,
                Lyrics = "Lyrics 1"
            };
			//Выводим десериализованный объект в консоль
			XmlSerializer serializer = new XmlSerializer(typeof(Song));
			serializer.Serialize(Console.Out, song);
			//Создаем поток в памяти
			MemoryStream memStream = new MemoryStream();
			//Десериализуем объект в поток
			serializer.Serialize(memStream, song);
			//Создаем строку
			string xmlString;

			using (StreamReader strReader = new StreamReader(memStream))
			{
				//Указываем текущую позицию в потоке
				strReader.BaseStream.Position = 0;
				//Записываем в строку поток из памяти
				xmlString = strReader.ReadToEnd();
			}
			using (TextReader reader = new StringReader(xmlString))
			{
				var strToSong = serializer.Deserialize(reader);
			}
		}

        /// <summary>
        /// AL6-P7/7-FileSrlz.
        /// Отредактировать предыдущий пример для поддержки сериализации и десериализации в файл.
        /// </summary>
        public static void AL6_P7_7_FileSrlz()
        {
			//Создание экземпляра объекта
			Song song = new Song()
			{
				Title = "Title 1",
				Duration = 247,
				Lyrics = "Lyrics 1"
			};
			XmlSerializer serializer = new XmlSerializer(typeof(Song));
			using (FileStream fs = new FileStream("D://song.xml", FileMode.OpenOrCreate))
			{
				serializer.Serialize(fs, song);
				Console.WriteLine("Объект сериализован");
			}

			var deserialisedSong = serializer.Deserialize(new FileStream("D://song.xml", FileMode.Open));
			Console.WriteLine("Объект десериализован");
		}

    }
}
