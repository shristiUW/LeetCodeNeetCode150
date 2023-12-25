using System;
namespace PracticeLeetcode
{
	public class LeetCodeChallenges
	{
		public LeetCodeChallenges()
		{
		}

		static void main()
		{

		}
		//https://leetcode.com/contest/weekly-contest-372/problems/make-three-strings-equal/
		//Sol-https://www.youtube.com/watch?v=3ECUu5wwjAE&t=387s
		public int FindMinimumOperations(string s1, string s2, string s3)
		{
			int len1 = s1.Length;
			int len2 = s2.Length;
			int len3 = s3.Length;

			if (s1[0] != s2[0] || s2[0] != s3[0] || s1[0] != s3[0])
			{
				return -1;
			}
			int i = 0, j = 0, k = 0;
			while (i < len1 && j < len2 && k<len3)
			{
				if (s1[i] != s2[i] || s1[i] != s3[i] || s2[i] != s3[i])
				{
					break;
				}
				i++;
				j++;
				k++;

			}
			return len1 - i + len2 - j + len3 - k;
		}
	}
}

