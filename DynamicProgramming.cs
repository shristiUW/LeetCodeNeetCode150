using System;
using System.Reflection;

namespace PracticeLeetcode
{
	public static class DynamicProgramming
	{
		//https://www.youtube.com/watch?v=hLQYQ4zj0qg
		public static bool wordBreak(string s, List<string> list)
		{ //less lookup time that s why we are putting everything from list to hashset
			HashSet<string> set = new HashSet<string>();
			Dictionary<string, bool> dict = new Dictionary<string, bool>();
			foreach (string str in list)
			{
				set.Add(str);

			}
			return dfsWordBreak(s,set);
			//return dfsWordBreakMem(s, set, dict);
		}

		//word break problem
		public static bool dfsWordBreak(string s, HashSet<string> set)
		{
			if (s.Equals(""))
			{
				return true;
			}
			for (int i = 1; i <= s.Length; i++)
			{
				if (set.Contains(s.Substring(0, i)) && dfsWordBreak(s.Substring(i), set))
				{
					return true;
				}


			}
			return false;
		}

		//with memozition
		public static bool dfsWordBreakMem(string s, HashSet<string> set, Dictionary<string, bool> dict)
		{
			if (s.Equals(""))
			{
				return true;
			}
			if (dict.ContainsKey(s))
			{
				return dict[s];
			}
			for (int i = 1; i <= s.Length; i++)
			{
				if (set.Contains(s.Substring(0, i)) && dfsWordBreakMem(s.Substring(i), set, dict))
				{
					dict.Add(s.Substring(i), true);
					return true;
				}


			}
			dict.Add(s, false);
			return false;
		}
		//tebular bottom up approach

		public static bool wordBreakDP(string s, List<string> list)
		{
			bool[] dp = new bool[s.Length + 1];
			dp[0] = true;
			for (int length = 1; length <= s.Length; length++)
			{
				for (int i = 0; i < length; i++)
				{
					if (dp[i] && list.Contains(s.Substring(i, length - i)))
					{
						dp[length] = true; break;
					}
				}

			}
			return dp[s.Length];
		}

		public static int findTargetSumWays(int[] nums, int target)
		{
			Dictionary<(int, int), int> dp = new Dictionary<(int, int), int>();


			int backtrack(int i, int total)
			{
				if (i == nums.Length)
				{
					if (target == total)
						return 1;
					else
						return 0;
				}
				if (dp.ContainsKey((i, total)))
				{
					return dp[(i, total)];

				}
				int x = backtrack(i + 1, nums[i] + total);
				int y = backtrack(i + 1, total - nums[i]);
				dp[(i, total)] = x + y;
				return dp[(i, total)];


			}

			return backtrack(0, 0);
		}



		public static IList<IList<int>> CombinationSum2(int[] candidates, int target)
		{
			Array.Sort(candidates);
			List<IList<int>> res = new List<IList<int>>();

			List<int> curr = new List<int>();
			dfs(0, target, curr);
			return res;

			void dfs(int i, int target, List<int> curr)
			{
				if (target == 0)
				{
					List<int> clonedcurr = new List<int>(curr);
					res.Add(curr);
				}
				if (target <= 0 || i >= candidates.Length)
					return;
				curr.Add(candidates[i]);
				dfs(i + 1, target - candidates[i], curr);
				curr.RemoveAt(curr.Count - 1);
				while (i + 1 < candidates.Length && candidates[i] == candidates[i + 1])
				{
					i++;

				}
				dfs(i + 1, target, curr);


			}
		}
		//https://leetcode.com/problems/longest-increasing-subsequence/
		//Video is saved in youtube
		public static int longestlenSubsequence(int[] nums)
		{
			//recursive call
			int len = RecursiveLIS(nums, 0, -1);

			//memoization call
			/*int[,] dp = new int[nums.Length+1, nums.Length+1];
			for(int i=0; i< nums.Length+1; i++)
			{
				for( int j=0; j< nums.Length+1;j++)
				{
					dp[i, j] = -1;
				}
			}
			int len = RecursiveLISMem(nums, 0, -1, dp);*/

			//bottom up approach

			//int len = LongestSubsequencebottumUp(nums);
			return len;

		}

		public static int RecursiveLIS(int[] nums, int currIndex, int prevIndex)
		{
			if (currIndex == nums.Length)
			{
				return 0;
			}
			int includeLen = 0;
			//Include logic
			//There are two options for every single array item that we include it or not include it in our length calculation
			//if currIndex value is greater than prevIndex value means it is increasing subsequence.
			if (prevIndex == -1 || nums[currIndex] > nums[prevIndex])
			{
				includeLen = 1 + RecursiveLIS(nums, currIndex + 1, currIndex);
			}

			// exclude
			int notIncludeLen = 0 + RecursiveLIS(nums, currIndex + 1, prevIndex);
			return Math.Max(includeLen, notIncludeLen);

		}
		//With Memoization
		public static int RecursiveLISMem(int[] nums, int currIndex, int prevIndex, int[,] dp)
		{
			if (currIndex == nums.Length)
			{
				return 0;
			}
			//We will check if value exist in dp array
			if (dp[currIndex, prevIndex + 1] != -1)
			{
				return dp[currIndex, prevIndex + 1];
			}
			int includeLen = 0;
			//include logic
			if (prevIndex == -1 || nums[currIndex] > nums[prevIndex])
			{
				includeLen = 1 + RecursiveLISMem(nums, currIndex + 1, currIndex, dp);
			}
			//not include
			int notIncludeLen = RecursiveLISMem(nums, currIndex + 1, prevIndex, dp);

			dp[currIndex, prevIndex + 1] = Math.Max(includeLen, notIncludeLen);

			return dp[currIndex, prevIndex + 1];

		}
		//bottom up approach
		public static int LongestSubsequencebottumUp(int[] nums)
		{
			int[,] dp = new int[nums.Length + 1, nums.Length + 1];
			for (int curr = nums.Length - 1; curr >= 0; curr--)
			{
				for (int prev = curr - 1; prev >= -1; prev--)
				{
					//include
					int include = 0;
					if (prev == -1 || nums[curr] > nums[prev])
					{
						include = 1 + dp[curr + 1, curr + 1];
					}
					//notinclude
					int notInclude = 0 + dp[curr + 1, prev + 1];
					dp[curr, prev + 1] = Math.Max(include, notInclude);
				}
			}
			return dp[0, 0];
		}

		//Unique Path
		public static int UniquePathsRecursive(int m, int n)
		{
			//Base Condition
			if (m == 1 || n == 1)
			{
				return 1;
			}
			//recursion
			return UniquePathsRecursive(m - 1, n) + UniquePathsRecursive(m, n - 1);
		}

		//Unique Path
		public static int UniquePathsotherRecursive(int m, int n)
		{
			int i = 0;
			int j = 0;
			int dfs(int i, int j)
			{
				//base case
				if (i == m - 1 && j == n - 1)
				{
					return 1;

				}
				if (i >= m || j >= n)
				{
					return 0;
				}
				return dfs(i + 1, j) + dfs(i, j + 1);
			}
			return dfs(i, j);
		}
		//With memoization
		public static int UniquePathsotherMem(int m, int n)
		{
			int[,] dp = new int[m + 1, n + 1];
			for (int i = 0; i < m + 1; i++)
			{
				for (int j = 0; j < n + 1; j++)
				{
					dp[i, j] = -1;
				}
			}

			int dfs(int i, int j)
			{
				if (i >= m || j >= n)
				{
					return 0;
				}
				if (i == m - 1 && j == n - 1)
				{
					return 1;

				}
				if (dp[i, j] != -1)
				{
					return dp[i, j];
				}
				return dp[i, j] = dfs(i + 1, j) + dfs(i, j + 1);

			}
			dp[0, 0] = dfs(0, 0);
			return dp[0, 0];
		}
		public static int uniquePathwithTebulization(int m, int n)
		{
			int[,] dp = new int[m + 1, n + 1];
			//base case
			for (int i = 0; i < m + 1; i++)
			{
				dp[i, n] = 0;

			}
			for (int j = 0; j < n + 1; j++)
			{
				dp[m, j] = 0;
			}

			for (int i = m - 1; i >= 0; i--)
			{

				for (int j = n - 1; j >= 0; j--)
				{
					if (i == m - 1 && j == n - 1)
					{
						dp[i, j] = 1;
					}
					else
					{
						dp[i, j] = dp[i + 1, j] + dp[i, j + 1];
					}
				}
			}
			return dp[0, 0];

		}
		public static void main()
		{

			ClimbStairs(3);
			uniquePathwithTebulization(2, 3);
			int[] coins = new int[] { 1, 2, 5 };
			int amount = 11;
			int n = CoinChange(coins, amount);
			//CoinChangeSecondMethod(coins, amount);

			int[] candidate = new int[] { 2, 3, 7 };
			int target = 7;
			IList<IList<int>> res = CombinationSum(candidate, target);
			int[] robHouse = new int[] { 1, 2, 3 };
			int[] robHouse1 = new int[] { 1 };
			//RobWithMem(robHouse);
			RobWithtebulization(robHouse);
			NumDecodings("06");
			NumDecodingsteb("226");
			int[] jump = new int[] { 2, 5, 0, 0 };
			CanJump(jump);
			CoinChangedp(coins, amount);
			diceCombination(3);
			diceCombinationDP(3);
			Rob(robHouse1);
			LongestPalindrome("cbbd");
			NumDecodingssecondApproach("226");
			CoinChangePractice(coins, amount);
			CoinChangeAnotherapproch(coins, amount);
			CoinChangeAnotherapprochDP(coins, amount);
            string strWordBreak = "code";
            List<string> listWordBreak = new List<string>() { "c", "od", "e", "x" };
			WordBreakPractice(strWordBreak, listWordBreak);
			WordBreakPracticeDP(strWordBreak, listWordBreak);

            int l = 2;
			int m = 9;
			int[] numscandidate = new int[] { 4, 5, 7, 1, 9, 10 };
			CandidatesPotential(numscandidate, l, m);
			int[] numsPartition = new int[] { 1, 5, 11, 5 };
			CanPartition(numsPartition);
			CanPartitionDP(numsPartition);
			UniquePaths(2, 3);
			UniquePathsDP(2, 3);
			LongestCommonSubsequence("abcde", "ace");


        }
		public static int CoinChange(int[] coins, int amount)
		{
			int min = 0;


			int ansIncludeMin = int.MaxValue;

			min = dfs(0, 0, 0);

			return ansIncludeMin;

			int dfs(int total, int i, int answerInclude)
			{


				//basecase

				if (amount < 0)
				{
					return -1;
				}
				if (i >= coins.Length)
				{
					return 0;
				}

				if (amount == 0)
				{
					return 0;
				}

				if (amount == total)
				{

					ansIncludeMin = Math.Min(ansIncludeMin, answerInclude);


				}
				if (total > amount)
				{
					return 0;
				}


				answerInclude++;
				dfs(total + coins[i], i, answerInclude);
				answerInclude--;
				dfs(total, i + 1, answerInclude);

				return ansIncludeMin;




			}
		}



		public static IList<IList<int>> CombinationSum(int[] candidates, int target)
		{
			List<IList<int>> lists = new List<IList<int>>();
			List<int> list = new List<int>();
			dfs(0, target);
			return lists;
			void dfs(int i, int target)
			{
				//base case
				if (target == 0)
				{
					List<int> clonedList = new List<int>(list);
					lists.Add(clonedList);
					return;

				}
				if (target < 0)
				{
					return;
				}
				if (i >= candidates.Length)
				{
					return;
				}
				list.Add(candidates[i]);
				dfs(i, target - candidates[i]);
				list.RemoveAt(list.Count - 1);
				dfs(i + 1, target);
			}
		}
		//House Rob2
		public static int Rob(int[] nums)
		{
			int[] first1 = new int[nums.Length - 1];
			int[] last1 = new int[nums.Length - 1];


			for (int i = 0; i < nums.Length; i++)
			{
				if (i != nums.Length - 1)
				{
					first1[i] = nums[i];

				}
				if (i != 0)
				{
					last1[i - 1] = nums[i];
				}

			}

			int[] first = nums[0..(nums.Length - 1)];
			int[] last = nums[1..(nums.Length)];
			return Math.Max(dfs(0, first), dfs(0, last));

			int dfs(int i, int[] array)
			{

				//basecase

				if (i >= array.Length)
				{
					return 0;
				}
				if (array.Length == 1)
				{
					return array[0];
				}

				int incl = dfs(i + 2, array) + array[i];
				int excl = dfs(i + 1, array) + 0;

				return Math.Max(incl, excl);
			}


		}
		public static int RobWithtebulization(int[] nums)
		{
			int[] first = new int[nums.Length - 1];
			int[] last = new int[nums.Length - 1];

			int[] dp1 = new int[first.Length + 1];
			int[] dp2 = new int[last.Length + 1];

			for (int i = 0; i < nums.Length; i++)
			{
				if (i != nums.Length - 1)
				{
					first[i] = nums[i];

				}
				if (i != 0)
				{
					last[i - 1] = nums[i];
				}

			}
			int n = first.Length;
			dp1[n] = 0;
			dp1[n - 1] = first[n - 1];
			dp2[n] = 0;
			dp2[n - 1] = last[n - 1];
			for (int i = first.Length - 2; i >= 0; i--)
			{
				int inclFirst = dp1[i + 2] + dp1[i];
				int exclFirst = dp1[i + 1];
				dp1[i] = Math.Max(inclFirst, exclFirst);
				int inclLast = dp2[i + 2] + dp2[i];
				int exclLast = dp2[i + 1];
				dp2[i] = Math.Max(inclLast, exclLast);
			}

			return Math.Max(dp2[0], dp1[0]);


		}

		public static int NumDecodings(string s)
		{
			/*int[] dp = new int[s.Length];
			for(int i=0; i<dp.Length; i++)
			{
				dp[i] = -1;
			}*/
			string str = "0123456";
			return dfs(0);
			int dfs(int i)

			{
				//base case
				if (i >= s.Length)
				{
					return 1;
				}
				if (s[i] == '0')
				{
					return 0;
				}
				/*if (dp[i] != -1)
                {
                    return dp[i];
                }*/
				int res = dfs(i + 1);
				if ((i + 1) < s.Length && ((Convert.ToChar(s[i]) == '1') || (Convert.ToChar(s[i]) == '2') && str.Contains(s[i + 1])))
				{
					res += dfs(i + 2);
				}
				//return dp[i] = res;
				return res;

			}
		}

		public static IList<IList<int>> CombinationSum1(int[] candidates, int target)
		{
			List<IList<int>> res = new List<IList<int>>();
			List<int> list = new List<int>();
			dfs(0, target);
			return res;
			void dfs(int i, int total)
			{
				if (i >= candidates.Length)
				{
					return;
				}
				if (total < 0)
				{
					return;
				}
				if (total == 0)
				{
					List<int> clonedList = new List<int>(list);
					res.Add(clonedList);
					return;

				}
				list.Add(candidates[i]);
				dfs(i, total - candidates[i]);
				list.RemoveAt(list.Count - 1);
				dfs(i + 1, total);
			}
		}

		public static IList<IList<int>> CombinationSumTwo(int[] candidates, int target)
		{
			List<IList<int>> res = new List<IList<int>>();
			List<int> list = new List<int>();
			Array.Sort(candidates);
			dfs(0, target);
			return res;
			void dfs(int i, int total)
			{
				if (total == 0)
				{
					List<int> clonedList = new List<int>(list);
					res.Add(clonedList);
					return;
				}
				if (i >= candidates.Length)
				{
					return;
				}
				if (total < 0)
				{
					return;

				}

				list.Add(candidates[i]);
				dfs(i + 1, total - candidates[i]);
				list.RemoveAt(list.Count - 1);
				while (i + 1 < candidates.Length && candidates[i] == candidates[i + 1])
				{
					i++;
				}
				dfs(i + 1, total);

			}

		}

		//tebulization
		public static int NumDecodingsteb(string s)
		{
			int[] dp = new int[s.Length + 1];
			int n = s.Length;
			//dp[n + 1] = 1;
			dp[n] = 1;
			for (int i = s.Length - 1; i >= 0; i--)
			{
				if (s[i] == '0')
				{
					dp[i] = 0;
				}
				else
				{
					dp[i] = dp[i + 1];
				}
				if (i + 1 < s.Length)
				{
					if (s[i] - '0' == 1 || (s[i] - '0' == 2) && s[i + 1] - '0' >= 0 && s[i + 1] - '0' <= 6)
					{
						dp[i] += dp[i + 2];
					}
				}

			}
			return dp[0];
		}

		public static bool CanJump(int[] nums)
		{
			return dfs(0);


			bool dfs(int i)
			{
				if (i >= nums.Length - 1 || (nums[i] == 0 && i >= nums.Length - 1))
				{
					return true;

				}
				if (nums[i] == 0)
				{
					return false;
				}
				bool res = false;
				for (int j = 1; j <= nums[i]; j++)
				{
					res = dfs(i + j);
					if (res == true)
					{
						break;
					}
				}
				return res;
			}
		}
		public static int CoinChangedp(int[] coins, int amount)
		{

			int[] dp = new int[amount + 1];
			for (int i = 1; i <= amount; i++)
			{
				dp[i] = amount + 1;

			}
			dp[0] = 0;
			foreach (int coin in coins)
			{
				for (int i = 1; i <= amount; i++)
				{
					if (coin <= i)
						dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
				}
			}
			return dp[amount] > amount ? -1 : dp[amount];
		}

		static int ClimbStairs(int n)
		{
			if (n == 0)
			{
				return 1;
			}
			if (n < 0)
			{
				return 0;

			}
			if (n == 1)
			{
				return 1;
			}

			int res1 = ClimbStairs(n - 1);
			int res2 = ClimbStairs(n - 2);
			return res1 + res2;
		}
		//https://cses.fi/problemset/task/1633
		static int diceCombination(int n)
		{
			int res = 0;

			if (n == 0)
			{
				return 1;
			}


			for (int i = 1; i <= n; i++)
			{
				res += diceCombination(n - i);
			}
			return res;

		}
		//tebulization
		static int diceCombinationDP(int n)
		{
			int res = 0;
			int[] dp = new int[n + 1];
			dp[0] = 1;
			for (int i = 1; i <= n; i++)
			{
				dp[i] += dp[i - 1];
			}
			return dp[n];
		}

		static public int Rob1(int[] nums)
		{
			int n = nums.Length;
			int[] dp = new int[n];
			dp[0] = 0;
			for (int i = 1; i < n; i++)
			{
				int take = (i > 2) ? nums[i] + dp[n - 2] : nums[i];
				int nontake = 0 + dp[n - 1];
				dp[i] = Math.Max(take, nontake);
			}
			return dp[n - 1];

		}


		static public string LongestPalindrome(string s)
		{

			int len = 0;
			string maxstr = string.Empty;
			for (int i = 0; i < s.Length; i++)
			{
				//odd
				int l = i;
				int r = i;
				while (l >= 0 && r < s.Length && s[l] == s[r])
				{
					if (r - l + 1 > len)
					{
						len = r - l + 1;
						maxstr = s.Substring(l, r - l + 1);
					}
					l--;
					r++;
				}
				//even
				l = i;
				r = i + 1;
				while (l >= 0 && r < s.Length && s[l] == s[r])
				{
					if (r - l + 1 > len)
					{
						len = r - l + 1;
						maxstr = s.Substring(l, r - l + 1);
					}
					l--;
					r++;

				}
			}
			return maxstr;


		}

		static public int CountSubstrings(string s)
		{
			int count = 0;
			for (int i = 0; i < s.Length; i++)
			{
				//odd
				int l = i;
				int r = i;
				while (l >= 0 && r < s.Length && s[l] == s[r])
				{
					count++;
					l--;
					r++;
				}
				//even
				l = i;
				r = i + 1;

				while (l >= 0 && r < s.Length && s[l] == s[r])
				{
					count++;
					l--;
					r++;
				}
			}
			return count;





		}


		static public int NumDecodingssecondApproach(string s)
		{
			int count = 0;
			return dfs(0);
			int dfs(int i)
			{
				if (i >= s.Length)
				{
					return 1;

				}
				if (s[i] == 0)
				{
					return 0;
				}


				count = dfs(i + 1);

				if (i + 1 < s.Length && (s[i] - '0' == 1) || (s[i] - '0' == 2 && s[i + 1] - '0' >= 0 && s[i + 1] - '0' <= 6))
				{
					count += dfs(i + 2);
				}
				return count;
			}



		}

		static public int CoinChangePractice(int[] coins, int amount)
		{
			int min = int.MaxValue;

			return dfs(0, 0, 0);
			int dfs(int i, int total, int noofcoins)
			{

				if (total == amount)
				{

					min = Math.Min(min, noofcoins);
					return min;
				}
				if (i >= coins.Length)
				{
					return 0;
				}
				if (total > amount)
				{
					return 0;

				}
				//take
				if (total < amount)
				{
					dfs(i, total + coins[i], noofcoins + 1);
				}
				//not take
				dfs(i + 1, total, noofcoins);

				return noofcoins;

			}


		}

		//love babbar
		public static int CoinChangeAnotherapproch(int[] coins, int amount)
		{
			int[] dp = new int[amount+1];
			for(int i=0; i<=amount; i++)
			{
				dp[i] = -1;
			}
			int ans= dfs(coins, amount);

			if(ans==int.MaxValue)
			{
				return -1;
			}
			else
			{
				return ans;
			}
			int dfs(int[] coins, int amount)
			{
				if (amount < 0)
				{
					return int.MaxValue;

				}
				if (amount == 0)
				{
					return 0;
				}
				if(dp[amount]!=-1)
				{
					return dp[amount];

				}
				int min = int.MaxValue;
				for (int i = 0; i < coins.Length; i++)
				{
					int ans = dfs(coins, amount - coins[i]);
					if (ans != int.MaxValue)
					{
						min = Math.Min(min, ans + 1);
					}


				}
				return dp[amount]=min;
			}







		}

        //love babbar
        public static int CoinChangeAnotherapprochDP(int[] coins, int amount)
        {
            int[] dp = new int[amount + 1];
			for(int i=0; i<=amount; i++)
			{
				dp[i] = int.MaxValue;
			}
			dp[0] = 0;
                
                int min = int.MaxValue;
			for (int i = 0; i <= amount; i++)

			{
				for (int j = 0; j <coins.Length; j++)
				{
					if(i - coins[j]>=0)
					dp[i] = Math.Min(dp[i], 1 + dp[i - coins[j]]);



				}
			}
				return dp[amount];
            







        }


		static public bool WordBreakPractice(string s, IList<string> wordDict)
		{
			HashSet<string> hash = new HashSet<string>(wordDict);
			return dfs(0, s);
			bool dfs(int i, string s)
			{
				//base case
				if(s.Length==0)
				{
					return true;
				}
				if(i>=s.Length)
				{
					return true;
				}

				for(int len =1; len<=s.Length; len++)
				{
					string sub = s.Substring(i, len);
					if(hash.Contains(sub)&& dfs(i,s.Substring(len)))
						{
						return true;
						
					}

				}
				return false;
			}
		}

		static public bool WordBreakPracticeDP(string s, IList<string> wordDict)
		{
			HashSet<string> hash = new HashSet<string>(wordDict);
		
			int n = s.Length;
			bool[] dp = new bool[n+1];
			dp[0] = true;
			for(int len=1;len<=n; len++ )
			{
				for(int i=0; i<len; i++)
				{
					if (dp[i] && hash.Contains(s.Substring(i,len-i)))
					{
						dp[len] = true;
						break;

					}
				}
			}
			return dp[n];
		}

            /*a. Candidates Potential
    A team based on the candidates’ potential needs to be formed. Minimum (l) and maximum (m) values indicating the range of required potential are provided alongside an array of length n containing the potential of candidates. We need to provide the number of possible ways a team can be formed.
    Input :  n = 6 l = 2 m = 8
    Potential Array: [4,5,7,1,9,10]		

    Output : 7
    Explanation: We have 3 candidates within the required potential range(4,5,7). We can either select only one of them(3 ways),
    two of them(3 ways), or all 3 of them(1 way). Total = 3 + 3 + 1 = 7*/
            static	int	CandidatesPotential(int[] nums, int l, int m)
		{
			return dfs(0);
			int dfs(int i)
			{
				//base case
				if(i>=nums.Length)
				{
					return 0;
				}
				int ways = 0;
				//take
				if (nums[i]>l && nums[i]<m)
				{
					ways = 1 + dfs(i + 1);

				}
				//not take
				int waysnot = dfs(i + 1);
				return ways + waysnot;

            }
		}


		static public bool CanPartition(int[] nums)
		{
			int total = 0;
			for( int i=0; i<nums.Length;i++)
			{
				total += nums[i];
			}
			if (total % 2==0)
			{
				return dfs(0, total / 2);
			}
			return false;
				

            bool dfs(int index, int target)
			{
            //base case
			if(target==0)
				{
					return true;
				}
			if(target<0)
				{
					return false;
				}
			if(index>=nums.Length)
			{
					return false;

			}
				//take
				
				bool intake = dfs(index+1, target - nums[index]);
				//notake
				bool notake = dfs(index + 1, target);
				return intake || notake;


            }


        }

        static public bool CanPartitionDP(int[] nums)
		{
            int total = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                total += nums[i];
            }
			if (total % 2 != 0)
			{
				return false;

			}
			int n = nums.Length;
			 total = total / 2 ;

            bool[,] dp = new bool[n + 1, total + 1];
			for(int i=0; i<=n; i++)
			{
				dp[i,0] = true;

			}
			for(int i =n-1;i>=0; i--)
			{
				for(int target=0; target<=total; target++)
				{
					//take
					bool take = false;
                   if ( target - nums[i]>=0)
                     take=  dp[i + 1, target - nums[i]];
                    //notake
                    bool notake = dp[i + 1, target];
                    dp[i,target]= take || notake;
                }
			}

			return dp[0, total];


		}


		static public int UniquePaths(int m, int n)
		{
			return dfs(0, 0);
	     int dfs(int i, int j)
			{
				if(i==m-1&& j==n-1)
				{
					return 1;
				}
				if(i>=m || j>=n)
				{
					return 0;	
				}
				return dfs(i + 1, j) + dfs(i, j + 1);
			}

		}

        static public int UniquePathsDP(int m, int n)
        {
			
			int[,] dp = new int[m+1,n+1];
			dp[m - 1, n - 1] = 1;
			
			
			
            for (int i = m-1; i >=0; i--)
			{
				for (int j = n-1; j >= 0; j--)
				{
					if (i == m - 1 && j == n - 1)
					{ dp[m - 1, n - 1] = 1; }
					else
					{
						dp[i, j] = dp[i + 1, j] + dp[i, j + 1];
					}
				}
			}
			return dp[0, 0];
            }



		static public int LongestCommonSubsequence(string text1, string text2)
		{
			return dfs(0, 0);
			int dfs(int i, int j)
			{
                //base case
                if (i>=text1.Length || j>=text2.Length)
				{
					return 0;
				}
				int len = 0;
				if (text1[i]== text2[j])
				{
					len = 1 + dfs(i + 1, j + 1);
				}
				else
				{
					len = Math.Max(dfs(i + 1, j), dfs(i, j + 1));
				}
				return len;
			}


		}


        }



    }


