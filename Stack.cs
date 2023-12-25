using System;
namespace PracticeLeetcode
{
	public class Stack
	{
		public Stack()
		{
		}
		static public void main()
		{

			GenerateParenthesis(3);
			int[] temperatures = new int[] { 73, 74, 75, 71, 69, 72, 76, 73 };
			DailyTemperatures(temperatures);

		}
		//https://leetcode.com/problems/generate-parentheses/
		static public IList<string> GenerateParenthesis(int n)
		{
			Stack<char> stack = new Stack<char>();
			List<string> list = new List<string>();
			dfs(0, 0);
			return list;
			void dfs(int opencase, int closecase)
			{

				if (opencase == closecase && opencase == n)
				{
					list.Add(string.Join("", stack.Reverse()));
					return;

				}
				if (opencase < n)
				{
					stack.Push('(');

					dfs(opencase + 1, closecase);
					stack.Pop();

				}
				if (closecase < opencase)
				{
					stack.Push(')');

					dfs(opencase, closecase + 1);
					stack.Pop();

				}
			}
		}

		static public int[] DailyTemperatures(int[] temperatures)
		{
			int[] list = new int[temperatures.Length];
			Stack<(int, int)> stack = new Stack<(int, int)>();
			stack.Push((temperatures[0], 0));
			for (int i = 1; i < temperatures.Length; i++)
			{
                var (temperature, idx) = stack.Peek();
                while (temperatures[i] > temperature)
				{                 
                    stack.Pop();
					list[idx]=i - idx;
					if (stack.Count > 0)
					{
						(temperature, idx) = stack.Peek();
					}
					else
					{
						break;
					}

                }
				stack.Push((temperatures[i], i));

			}
			return list.ToArray();
		}
	}
}

