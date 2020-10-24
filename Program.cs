using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Coloring2
{
	class Program
	{
		static void Main(string[] args)
		{
			//string filePath = @"..\..\..\input\";//visual studio
			string filePath = @".\input\";//visual studio code
			string fileName = "test1.txt";

			int n, m, maxColor = 0;
			int[][] data;
			List<int> order;
			int[] level, color;

			#region readfile and process data
			StreamReader file = new StreamReader(Path.Combine(filePath, fileName));
			int[] tempArray = Array.ConvertAll<string, int>(file.ReadLine().Split(' '), s=>int.Parse(s));
			n = tempArray[0];
			data = new int[n][];
			order = Enumerable.Range(0, n).ToList();
			level = new int[n];
			color = new int[n];
			m = tempArray[1];
			for (int i = 0; i < n; i++)
			{
				string row = file.ReadLine();
				data[i] = Array.ConvertAll<string, int>(
					row.Split(' '),
					s => {
						var result = int.Parse(s);
						if(result > m)
							return 0;
						if(result > 0)
							return 1;
						return result;
					}
				);
				int count = 0;
				for (int j = 0; j < n; j++)
				{
					count += data[i][j];
				}
				level[i] = count;
			}
			file.Close();

			order = order.OrderBy(num => level[num]).ToList();
			#endregion

			for (int i = order.Count - 1; i > -1; i--)
			{
				int ii = order[i];
				maxColor++;
				color[ii] = maxColor;
				order.RemoveAt(i);
				for (int j = order.Count - 1; j > -1; j--)
				{
					var jj = order[j];

					bool nearColorII = false;
					for (int z = 0; z < n; z++)
					{
						if (data[jj][z] == 1 && color[z] == color[ii])
						{
							nearColorII = true;
							break;
						}
					}

					if (!nearColorII)
					{
						color[jj] = maxColor;
						order.RemoveAt(j);
						i--;
					}
				}
			}
			Console.WriteLine($"Max Color: {maxColor}");
			Console.WriteLine($"Color: {JsonSerializer.Serialize(color)}");
		}
	}
}
