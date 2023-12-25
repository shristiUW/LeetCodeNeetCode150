using System;
using System.Collections.Generic;

namespace PracticeLeetcode
{
	public class Recursion
	{
		public static void main()
		{
			List<int> order = new List<int>()
				{ 2,10,30};
			int widgetNo = 40;
			int count = filledOrder(order, widgetNo);
			Console.WriteLine("No of Maximum order filled", count);
			int[] nums = new int[] { 1, 2, 3 };
			Subsets(nums);
			int[] nums2 = new int[] { 1, 2, 2, 3 };
			Subsets2(nums2);
			Permute(new int[] { 1, 2, 3 });
			string s = "aab";
			PallindromPartitioning(s);
			int[] cost = new int[] { 10, 15, 20 };
			MinCostClimbingStairs(cost);
			GenerateParenthesis(2);
			SubsetsForPractice(nums);
			PermuteAnotherApproach(nums);
			Partition(s);
			SolveNQueens(4);



        }
		public static int filledOrder(List<int> order, int k)
		{
			int count = 0;
			Array.Sort(order.ToArray());
			int target = k;
			for (int i = 0; i < order.Count; i++)
			{
				List<int> list = new List<int>();
				count = Math.Max(count, dfs(i, target, list));
			}

			int dfs(int i, int target, List<int> list)
			{

				//base case
				if (target == 0)
				{
					return list.Count;
				}
				if (i < order.Count && target >= order[i])
				{
					list.Add(order[i]);
					dfs(i + 1, target - order[i], list);
				}
				else
				{
					while (i < order.Count && target < order[i])
					{
						i++;
					}
					if (i < order.Count)
					{
						list.Add(order[i]);
						dfs(i + 1, target = order[i], list);
					}
					else
					{
						return list.Count;
					}
				}
				return list.Count;
			}
			return count;
		}
		//https://leetcode.com/problems/subsets/
		public static IList<IList<int>> Subsets(int[] nums)
		{
			List<IList<int>> list = new List<IList<int>>();
			List<int> sublist = new List<int>();
			dfsSubset(nums, list, sublist, 0);
			return list;

		}
		static void dfsSubset(int[] nums, List<IList<int>> list, List<int> sublist, int i)
		{
			// base case
			if (i >= nums.Length)
			{
				List<int> clonedList = new List<int>(sublist);
				list.Add(clonedList);
				return;
			}
			//include nums[i]
			sublist.Add(nums[i]);
			dfsSubset(nums, list, sublist, i + 1);
			//exclude logic
			sublist.RemoveAt(sublist.Count - 1);
			dfsSubset(nums, list, sublist, i + 1);
		}
		public static IList<IList<int>> Subsets2(int[] nums)
		{
			Array.Sort(nums);
			List<IList<int>> list = new List<IList<int>>();
			List<int> sublist = new List<int>();
			dfsSubset2(nums, list, sublist, 0);
			return list;

		}
		static void dfsSubset2(int[] nums, List<IList<int>> list, List<int> sublist, int i)
		{
			// base case
			if (i >= nums.Length)
			{
				List<int> clonedList = new List<int>(sublist);
				list.Add(clonedList);
				return;
			}
			//include nums[i]
			sublist.Add(nums[i]);
			dfsSubset2(nums, list, sublist, i + 1);
			//exclude logic
			sublist.RemoveAt(sublist.Count - 1);
			while (i + 1 < nums.Length && nums[i] == nums[i + 1])
			{

				i++;
			}
			dfsSubset2(nums, list, sublist, i + 1);
		}


		public static IList<IList<int>> Permute(int[] nums)
		{
			List<IList<int>> res = new List<IList<int>>();
			List<int> sublist = new List<int>();
			bool[] freq = new bool[nums.Length];
			dfsPermutation(nums, res, sublist, freq);
			return res;


		}


		static void dfsPermutation(int[] nums, List<IList<int>> res, List<int> sublist, bool[] freq)
		{
			//base case
			if (nums.Length == sublist.Count)
			{
				List<int> cloned = new List<int>(sublist);
				res.Add(cloned);
				return;


			}
			// iterating for each element
			for (int i = 0; i < nums.Length; i++)
			{
				if (freq[i] != true)
				{
					sublist.Add(nums[i]);
					freq[i] = true;

					dfsPermutation(nums, res, sublist, freq);
					sublist.RemoveAt(sublist.Count - 1);
					freq[i] = false;
				}

			}


		}

		static IList<IList<string>> PallindromPartitioning(string s)
		{
			List<IList<string>> list = new List<IList<string>>();
			List<string> sublist = new List<string>();
			dfsParallelPartioning(s, 0, list, sublist);
			return list;


		}
		static void dfsParallelPartioning(string s, int i, List<IList<string>> list, List<string> sublist)
		{
			if (s.Length==0)
			{
				List<string> cloned = new List<string>(sublist);
				list.Add(cloned);
				return;

			}
			for (int len = 1; len <= s.Length; len++)
			{
				string s1 = s.Substring(i, len);
				if (Ispallendrom(s1))
				{
					sublist.Add(s1);
					string s2 = s.Substring(i + len);

					dfsParallelPartioning(s2, i, list, sublist);
					sublist.RemoveAt(sublist.Count - 1);

				}
			}
		}

		static bool Ispallendrom(string s)
		{
			int l = 0;
			int r = s.Length - 1;
			while (l <= r && l >= 0 && r < s.Length)
			{
				if (s[l] != s[r])
				{
					return false;

				}
				else
				{
					l++;
					r--;
				}
			}
			return true;
		}

		static public int MinCostClimbingStairs(int[] cost)
		{

			int mindfs0 = dfs(0);
			int mindfs1 = dfs(1);
			return Math.Min(mindfs0, mindfs1);
			int dfs(int i)
			{
				if (i >= cost.Length)
				{
					return 0;
				}
				int res = cost[i] + Math.Min(dfs(i + 1), dfs(i + 2));
				return res;
			}
		}

		//https://leetcode.com/problems/generate-parentheses/description/
		static public List<string> GenerateParenthesis(int n)
		{
			//Three cases
			//1- Add open paranthesis if open<N
			//2- Add closed paranthesis if close<open
			//3- Done open==close==n
			List<string> list = new List<string>();
			Stack<string> stack = new Stack<string>();
			backtrack(0, 0);
			void backtrack(int open, int close)
			{
				//base case
				if (n == open && n == close)
				{
					list.Add(string.Join("", stack.Reverse()));

				}
				if (open < n)
				{
					stack.Push("(");
					backtrack(open + 1, close);
					stack.Pop();
				}
				if (close < open)
				{
					stack.Push(")");
					backtrack(open, close + 1);
					stack.Pop();
				}
			}
			return list;


		}

		static public IList<IList<int>> SubsetsForPractice(int[] nums)
		{
			List<IList<int>> res = new List<IList<int>>();
			List<int> sublist = new List<int>();
			dfsSubset(0, nums, sublist, res);
			return res;

		}
		static void dfsSubset(int index, int[] nums, List<int> sublist, List<IList<int>> res)
		{
			if (index >= nums.Length)
			{
				List<int> clone = new List<int>(sublist);
				res.Add(clone);
				return;

			}
			//include
			sublist.Add(nums[index]);
			dfsSubset(index + 1, nums, sublist, res);
			//exclude

			sublist.RemoveAt(sublist.Count - 1);
			dfsSubset(index + 1, nums, sublist, res);



		}

		static public IList<IList<int>> PermuteAnotherApproach(int[] nums)
		{
			List<IList<int>> res = new List<IList<int>>();
			//List<int> sublist = new List<int>();
			dfs(0);
			return res;
			void dfs(int index)
			{
				if (index == nums.Length)
				{
					List<int> clone = new List<int>(nums);
					res.Add(clone);
					return;
				}
				for (int i = index; i < nums.Length; i++)
				{

					swap(ref nums[index], ref nums[i]);
					dfs(index + 1);
					swap(ref nums[index], ref nums[i]);


				}
			}

			void swap(ref int val1, ref int val2)
			{
				int temp = val1;
				val1 = val2;
				val2 = temp;
			}




		}


   static public IList<IList<string>> Partition(string s)
        {

            List<IList<string>> list = new List<IList<string>>();

            List<string> sublist = new List<string>();

            dfs(0, sublist, s);
            return list;
            void dfs(int i, List<string> sublist, string s)
            {
                if (s.Length == 0)
                {
                    List<string> cloned = new List<string>(sublist);
                    list.Add(cloned);
                    return;
                }
                for (int len = 1; len < s.Length; len++)
                {
                    string s1 = s.Substring(i, len);
                    if (isPalindrome(s1))
                    {
                        sublist.Add(s1);
                        string s2 = s.Substring(i + len);
                        dfs(i, sublist, s2);
                        sublist.RemoveAt(sublist.Count - 1);
                    }
                }
            }
         static bool isPalindrome(string s)
            {
                int l = 0;
                int r = s.Length - 1;
                while (l < r)
                {
                    if (s[l] != s[r])
                    {
                        return false;

                    }
                    else
                    {
                        l++;
                        r--;
                    }
                }
                return true;

            }


        }


	static	public IList<IList<string>> SolveNQueens(int n)
		{

			List<IList<string>> res = new List<IList<string>>();

			List<string> sublist = new List<string>();
			HashSet<int> col = new HashSet<int>();
			HashSet<int> posDiag = new HashSet<int>();
            HashSet<int> negDiag = new HashSet<int>();
			string[][] board = new string[n][];
			for(int i=0; i<n; i++)
			{
				board[i] = new string[n];
				for(int j=0; j<n; j++)
				{
					board[i][j] = ".";
				}
				

			}
			dfs(0);
			return res;

			void dfs(int r )
			{
				//basecase
				if(r==n)
				{
					List<string> sublist = new List<string>();
					for(int i=0; i<n; i++)
					{
						sublist.Add(String.Join("", board[i]));


					}
					res.Add(sublist);
					return;
				}
				for (int c = 0; c < n; c++)
				{
					if (!col.Contains(c) && !posDiag.Contains((r+c)) && !negDiag.Contains((r - c)))
					{
						col.Add(c);
						posDiag.Add((r +c));
						negDiag.Add(r-c);
						board[r][c] = "Q";
						dfs(r + 1);
						col.Remove(c);
                        posDiag.Remove((r + c));
                        negDiag.Remove(r - c);
						board[r][c] = ".";


                    }
		


				}
				}

			}
		}

        }



