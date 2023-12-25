using System;
using System.Text;

namespace PracticeLeetcode
{
	public class TwoPointers
	{
		public TwoPointers()
		{
		}

		public static void main()
		{
			//string s = "A man, a plan, a canal: Panama";
            string s = ".,";
            IsPalindromeSecondMethod(s);

		}
		static public bool IsPalindrome(string s)
		{
			s = s.ToLower();
			int l = 0;
			int r = s.Length - 1;
			if (s.Length == 0)
			{
				return true;
			}
			while (l <= r)
			{
				if (!char.IsLetterOrDigit(s[l]) && l < s.Length)
				{
					l++;

				}
				else if (!char.IsLetterOrDigit(s[r]) && r >= 0)
				{
					r--;

				}
				else if (s[l] == s[r])
				{
					l++;
					r--;
				}
				else
				{
					return false;
				}
			}
			return true;
		}
		static public bool IsPalindromeSecondMethod(string s)
		{
			s = s.ToLower();
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < s.Length; i++)
			{
				if ((s[i] >= 'a' && s[i] <= 'z') || (s[i] >= '0' && s[i] <= '9'))
				{
					sb.Append(s[i]);
				}
			}
			int n = sb.Length;
			for (int i = 0; i < n / 2; i++)
			{
				if (sb[i] != sb[n - i - 1])
				{
					return false;
				}
			}
			return true;
		}
	}
}

