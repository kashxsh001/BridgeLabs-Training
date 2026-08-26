using System;

class Program
{
    static int LengthOfLIS(int[] nums)
    {
        if (nums == null || nums.Length == 0)
            return 0;

        int[] longest = new int[nums.Length];
        int size = 0;

        foreach (int num in nums)
        {
            int left = 0;
            int right = size;
            while (left < right)
            {
                int mid = left + (right - left) / 2;

                if (longest[mid] < num)
                    left = mid + 1;
                else
                    right = mid;
            }

            longest[left] = num;

            if (left == size)
                size++;
        }

        return size;
    }

    static void Main()
    {
        int[] nums = { 10, 9, 2, 5, 3, 7, 101, 18 };

        Console.WriteLine(LengthOfLIS(nums));
    }
}
