using System;
namespace PracticeLeetcode
{
	public class SlidingWindow
	{
		public SlidingWindow()
		{
		}
		static public void main()
		{
			CheckInclusion("ab", "eidbaooo");
        }
        //https://leetcode.com/problems/permutation-in-string/

       static public bool CheckInclusion(string s1, string s2)
		{
			Dictionary<char, int> dicts1 = new  Dictionary<char, int>();
			Dictionary<char, int> dicts2 = new Dictionary<char, int>();
			foreach(char c in s1)
			{
				if(dicts1.ContainsKey(c))
				{
					dicts1[c]++;
				}
				else
				{
					dicts1.Add(c, 1);
				}
			}
			int l = 0;
			int r = 0;
			while(r<s2.Length)
			{
				if (dicts2.ContainsKey(s2[r]))
				{
					dicts2[s2[r]]++;
				}
				else
				{
					dicts2.Add(s2[r], 1);
				}
				if(r-l+1>s1.Length)
				{
					if (dicts2[s2[l]]==1)
					{
						dicts2.Remove(s2[l]);
					}
					else
					{
						dicts2[s2[l]]--;
					}
					l++;
				}
				if(r-l+1==s1.Length)
				{
					bool equal = true;
					foreach(char c in dicts1.Keys)
					{
						if(!dicts2.ContainsKey(c))
						{
							equal = false;
							break;
						}
						else
						{
							if (dicts2[c] != dicts1[c])
							{
								equal = false;
								break;
							}
						}


					}
					if(equal==true)
					{
						return true;
						break;
					}

				}

				r++;
			}
			return false;
		}

        }
    }

