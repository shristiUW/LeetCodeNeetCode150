using System;
namespace PracticeLeetcode
{
    public class BinarySearch
    {
        public BinarySearch()
        {

        }
        static public void main()
        {
            int[] nums = new int[] { -1, 0, 3, 5, 9, 12 };
            int target = 9;
            SearchSecond(nums, target);
            int[] nums1 = new int[] { 1, 3 };
            int[] nums2 = new int[] { 2 };
            FindMedianSortedArrays2(nums2, nums1);
            int[] piles = new int[] { 3, 6, 7, 11 };
            int h = 8;
            MinEatingSpeed(piles, h);
            int[] piles1 = new int[] { 805306368, 805306368, 805306368 };
            int h1 = 1000000000;
            MinEatingSpeedBinarySearch(piles1, h1);
        }

        static public int Search(int[] nums, int target)
        {

            int l = 0;
            int r = nums.Length - 1;
            int res = -1;

            while (r >= l)
            {
                int m = (l + r) / 2;
                if (target == nums[m])
                {
                    return m;

                }
                else if (target >= nums[l] && target < nums[m])
                {
                    r = m - 1;
                }
                else if (target > nums[m] && target <= nums[r])
                {

                    l = m + 1;
                }
                else
                {
                    break;
                }

            }
            return -1;
        }

        static public int SearchSecond(int[] nums, int target)
        {

            int l = 0;
            int r = nums.Length - 1;
            while (l <= r)
            {
                int m = l + (r - l) / 2;

                if (target > nums[m])
                {
                    l = m + 1;
                }
                else if (target < nums[m])
                {
                    r = m - 1;
                }
                else
                {
                    return m;
                }
            }

            return -1;


        }

        static public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int n1 = nums1.Length;
            int n2 = nums2.Length;
            if (n1 > n2)
            {
                FindMedianSortedArrays(nums2, nums1);
            }
            else
            {
                int low = 0;
                int high = n1;
                while (low <= high)
                {
                    int cut1 = (low + high) / 2;
                    int cut2 = (n1 + n2 + 1) / 2 - cut1;
                    int l1 = (cut1 == 0) ? int.MinValue : nums1[cut1 - 1];
                    int l2 = (cut2 == 0) ? int.MinValue : nums2[cut2 - 1];
                    int r1 = (cut1 == n1) ? int.MaxValue : nums1[cut1];
                    int r2 = (cut2 == n2) ? int.MaxValue : nums2[cut2];

                    if (l1 > r2)
                    {
                        high = cut1 - 1;
                    }
                    else if (l2 > r1)
                    {
                        low = cut1 + 1;
                    }
                    else
                    {
                        return (n1 + n2) % 2 == 0 ? (Math.Max(l1, l2) + Math.Min(r1, r2)) / 2 : Math.Max(l1, l2);
                    }

                }

            }
            return 0;



        }

        static public double FindMedianSortedArrays2(int[] nums1, int[] nums2)
        {

            int num1Length = nums1.Length;
            int num2Length = nums2.Length;

            if (num2Length < num1Length)
            {
                FindMedianSortedArrays(nums2, nums1);
            }
            else
            {
                int low = 0;
                int high = num1Length;
                while (low <= high)
                {
                    int cut1 = (low + high) / 2;
                    int cut2 = (num1Length + num2Length + 1) / 2 - cut1;
                    int l1 = (cut1 == 0) ? int.MinValue : nums1[cut1 - 1];
                    int l2 = (cut2 == 0) ? int.MinValue : nums2[cut2 - 1];
                    int r1 = (cut1 == num1Length) ? int.MaxValue : nums1[cut1];
                    int r2 = (cut2 == num2Length) ? int.MaxValue : nums2[cut2];
                    if (l1 > r2)
                    {
                        high = cut1 - 1;
                    }
                    else if (l2 > r1)
                    {
                        low = cut1 + 1;
                    }
                    else
                    {
                        return ((num1Length + num2Length) % 2 == 0) ? (Math.Max(l1, l2) + Math.Min(r1, r2)) / 2.00 : Math.Max(l1, l2);
                    }
                }
            }
            return 0;

        }

        //Koko Eating Banana
        static public int MinEatingSpeed(int[] piles, int h)
        {
            for (int i = 1; i <= piles.Max(); i++)
            {
                int time = 0;
                time = totaltime(piles, i);
                if (time < h)
                {
                    return i;
                }


            }
            return 0;
        }

        static int totaltime(int[] piles, int hourly)
        {
            int totalHour = 0;
            for (int i = 0; i < piles.Length; i++)
            {
                totalHour += (int)Math.Ceiling(Convert.ToDouble(piles[i]) / Convert.ToDouble(hourly));
            }
            return totalHour;
        }

        static public int MinEatingSpeedBinarySearch(int[] piles, int h)
        {
            int l = 1;
            int r = piles.Max();
            int min = int.MaxValue;
            while (l <= r)
            {
                int m = (l + r) / 2;

                int time = totaltime(piles, m);
                if(time<0)
                {
                    break;
                }
                if (time <= h)
                {
                    r = m - 1;
                    min = Math.Min(min, m);

                }
                else
                {
                    l = m + 1;
                }

            }
            return min;
        }


    }



}
