using System;
using System.Text;
using System.Collections.Generic;


namespace StructConsole
{
	struct Airplane
	{
		public string StartCity;
		public string FinishCity;
		public Date StartDate;
		public Date FinishDate;
		
		public Airplane(string StartCity, string FinishCity, int[] startData, int[] finishData)
		{
			this.StartCity = StartCity;
			this.FinishCity = FinishCity;
			StartDate = new Date(startData);
			FinishDate = new Date(finishData);
		}
		
		public int GetTotalTime()
		{
			int StartMinutes = StartDate.Minutes + StartDate.Hours * 60 + StartDate.Day * 60 * 24 + StartDate.Month * 60 * 24 * 31 + StartDate.Year * 60 * 24 * 31 * 365;      
			int FinishMinutes = FinishDate.Minutes + FinishDate.Hours * 60 + FinishDate.Day * 60 * 24 + FinishDate.Month * 60 * 24 * 31 + FinishDate.Year * 60 * 24 * 31 * 365;
			return FinishMinutes - StartMinutes;
		}
		public bool IsArrivingToday()
		{
			if(StartDate.Year == FinishDate.Year && StartDate.Month == FinishDate.Month && StartDate.Day == FinishDate.Day) return true;
			return false;
		}
	}
	struct Date
	{
		public int Year;
		public int Month;
		public int Day;
		public int Hours;
		public int Minutes;
		
		public Date (int[] data)
		{
			this.Year = data[0];
			this.Month = data[1];
			this.Day = data[2];
			this.Hours = data[3];
			this.Minutes = data[4];
		}
	}
	class Program
	{
		static Airplane[] ReadAirplaneArray()
		{
			Console.WriteLine("Input number of structures: ");
			int n = Convert.ToInt32(Console.ReadLine());
			Airplane[] array = new Airplane[n];
			for(int i = 0; i < n; i++)
			{
				Console.WriteLine("Input Start City: ");
				string StartCity = Console.ReadLine();
				Console.WriteLine("Input Finish City");
				string FinishCity = Console.ReadLine();
				Console.WriteLine("Input Start Date");
				int[] StartDate = InputDate();
				Console.WriteLine("Input Finish Date");
				int[] FinishDate = InputDate();
				array[i] = new Airplane(StartCity, FinishCity, StartDate, FinishDate);
			}
			return array;
		}
		
		static void PrintAirplane(Airplane plane)
		{
			Console.WriteLine("Start City: {0}, FinishCity: {1}, StartDate {2}/{3}/{4} {5}:{6}, FinishDate {7}/{8}/{9} {10}:{11}", plane.StartCity, plane.FinishCity, plane.StartDate.Year, plane.StartDate.Month,
							  plane.StartDate.Day, plane.StartDate.Hours, plane.StartDate.Minutes, plane.FinishDate.Year, plane.FinishDate.Month, plane.FinishDate.Day, plane.FinishDate.Hours, plane.FinishDate.Minutes);
		}
		
		static void PrintAirplanes(Airplane[] planes)
		{
			for(int i = 0; i < planes.Length; i++)
			{
				Console.WriteLine("Airplane #{0} :",i);
				PrintAirplane(planes[i]);
			}
		}
		
		static void GetAirplaneInfo(Airplane[] planes, out int min, out int max)
		{
			for(int i = 0; i < planes.Length; i++)
			{
				int time = planes[i].GetTotalTime();
				if(min > time)
					min = time;
				if(max < time)
					max = time;
			}
		}
        
        static void SortAirplanesByDate(ref Airplane[] planes)
        {
            Array.Sort(planes, new DateComparer());
        }
        
        static void SortAirplanesByTotalTime(ref Airplane[] planes)
        {
            Array.Sort(planes, new TimeComparer());
        }
		
		static int[] InputDate()
		{
			int[] a = new int[5];
			Console.WriteLine("Input Year");
			a[0] = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Input Month");
			a[1] = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Input Day");
			a[2] = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Input Hours");
			a[3] = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Input Minutes");
			a[4] = Convert.ToInt32(Console.ReadLine());
			return a;
		}
		
		static void Main(string[] args)
		{
			Console.Title = "Лабораторна робота №4";
			Console.SetWindowSize(100, 25);
			Console.BackgroundColor = ConsoleColor.White;
			Console.ForegroundColor = ConsoleColor.DarkBlue;
			Airplane[] airplanes = ReadAirplaneArray();
			PrintAirplanes(airplanes);
            int min, max;
            GetAirplaneInfo(airplanes, out min, out max);
            Console.WriteLine("min = {0}, max = {1}", min, max);
            SortAirplanesByTotalTime(ref airplanes);
		}
	}
	class DateComparer : IComparer<Airplane>
    {
        public int Compare(Airplane p1, Airplane p2)
        {
            int p1Date = Convert.ToInt32(("{0}{1}{2}{3}{4}",p1.StartDate.Year, p1.StartDate.Month, p1.StartDate.Day, p1.StartDate.Hours, p1.StartDate.Minutes));
            int p2Date = Convert.ToInt32(("{0}{1}{2}{3}{4}",p2.StartDate.Year, p2.StartDate.Month, p2.StartDate.Day, p2.StartDate.Hours, p2.StartDate.Minutes));
            if(p1Date > p2Date)
                return 1;
            else if(p1Date < p2Date)
                return -1;
            else
                return 0;
        }
    }
    class TimeComparer : IComparer<Airplane>
    {
        public int Compare(Airplane p1, Airplane p2)
        {
            if(p1.GetTotalTime() > p2.GetTotalTime())
                return 1;
            else if(p2.GetTotalTime() < p2.GetTotalTime())
                return -1;
            else
                return 0;
        }
    }
}
